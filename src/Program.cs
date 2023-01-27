using CommandLine;
using Mntone.RWinRT.Generators;
using Mntone.RWinRT.Generators.Cpp;
using Mntone.RWinRT.Generators.CSharp;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mntone.RWinRT
{
	internal class Program
	{
		static int Main(string[] args)
		{
			var options = Parser.Default.ParseArguments<Options>(args);
			if (options.Tag == ParserResultType.NotParsed)
			{
				return -1;
			}

			var resources = options.Value.InputFilenames
				.Where(filename => options.Value.ExcludeResources.All(r => Path.GetFileName(filename).Split(new[] { '.' }, 2).FirstOrDefault() != r))
				.Select(filename =>
				{
					var resourceType = Path.GetFileName(filename).Split(new[] { '.' }, 2)[0];
					using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
					{
						var resourceData = ReswReader.Load(stream);
						var resourcesData = new ResourcesData()
						{
							ResourceType = resourceType,
							Qualifiers = ResourceQualifiers.Parse(filename, ResourceMode.UAP, options.Value.DefaultLanguage),
							Resources = resourceData,
							IsDefault = resourceType == options.Value.DefaultResourceFilename,
						};
						return resourcesData;
					}
				})
				.Where(r => r.Qualifiers.Language == options.Value.DefaultLanguage).ToArray();
			if (resources.Length == 0)
			{
				return -1;
			}

			// Calc hash
			var defaultResource = resources.SingleOrDefault(r => r.ResourceType == options.Value.DefaultResourceFilename);
			var otherResources = resources.Except(new[] { defaultResource }).ToArray();
			var hashBaseData = string.Concat(defaultResource.Resources.Select(r => r.BaseData)
				.Concat(otherResources.SelectMany(rs => rs.Resources.SelectMany(r => new string[] { "\n[", rs.ResourceType, "]\n", r.BaseData }))));
			string hashString;
			using (var hasher = SHA256.Create())
			{
				hashString = string.Concat(hasher.ComputeHash(Encoding.Unicode.GetBytes(hashBaseData)).Select(x => $"{x:X2}"));
			}

			// Generate
			if (options.Value.IsTest)
			{
				var output = string.Concat(new string[]
				{
					"===[ CppAutogen1 ]=========================================\n",
					CppAutogen1.Build(new CppWriterContext(options.Value), defaultResource, otherResources),
					"===[ CppAutogen2 ]=========================================\n",
					CppAutogen2.Build(new CppWriterContext(options.Value), defaultResource, otherResources),
					"===[ CSharpAutogen1 ]=========================================\n",
					CSharpAutogen1.Build(new CSharpWriterContext(options.Value), defaultResource, otherResources),
					"===[ CSharpAutogen3 ]=========================================\n",
					CSharpAutogen3.Build(new CSharpWriterContext(options.Value), defaultResource, otherResources),
				});
				Console.WriteLine(output);
				return 0;
			}
			else if (options.Value.LanguageVersion.GetIsCpp())
			{
				var ctx = new CppWriterContext(options.Value);
				if (!ctx.ExistsOutputDirectory())
				{
					return -1;
				}

				if (ctx.CheckHashString(hashString) > 0)
				{
					return 0;
				}

				string output;
				switch (options.Value.Mode)
				{
					case 1:
						output = CppAutogen1.Build(ctx, defaultResource, otherResources);
						break;
					case 2:
					default:
						output = CppAutogen2.Build(ctx, defaultResource, otherResources);
						break;
				}
				return ctx.Save(output);
			}
			else
			{
				var ctx = new CSharpWriterContext(options.Value);
				if (!ctx.ExistsOutputDirectory())
				{
					return -1;
				}

				if (ctx.CheckHashString(hashString) > 0)
				{
					return 0;
				}

				string output;
				switch (options.Value.Mode)
				{
					case 1:
						output = CSharpAutogen1.Build(ctx, defaultResource, otherResources);
						break;
					case 3:
					default:
						output = CSharpAutogen3.Build(ctx, defaultResource, otherResources);
						break;
				}
				return ctx.Save(output);
			}
		}
	}
}