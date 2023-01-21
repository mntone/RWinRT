using CommandLine;
using Mntone.RWinRT.Generators;
using Mntone.RWinRT.Generators.Cpp;
using Mntone.RWinRT.Generators.CSharp;
using System.IO;
using System.Linq;

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

			// Generate
			var defaultResource = resources.SingleOrDefault(r => r.ResourceType == options.Value.DefaultResourceFilename);
			var otherResources = resources.Except(new[] { defaultResource }).ToArray();
			if (options.Value.LanguageVersion.GetIsCpp())
			{
				var ctx = new CppWriterContext(
					options.Value.LanguageVersion.ToCppVersion(),
					options.Value.RootNamespace,
					implNamespace: options.Value.ImplNamespace,
					filename: options.Value.FileName ?? "res.g.h",
					lineBreak: options.Value.LineBreak);

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
				return ctx.Save(output, options.Value.OutputDirectory, false);
			}
			else
			{
				var ctx = new CSharpWriterContext(
					options.Value.LanguageVersion.ToCSharpVersion(),
					options.Value.RootNamespace,
					implNamespace: options.Value.ImplNamespace,
					filename: options.Value.FileName ?? "Resources.g.cs",
					lineBreak: options.Value.LineBreak,
					isPublic: options.Value.IsPublic);

				string output;
				switch (options.Value.Mode)
				{
					case 1:
						output = CSharpAutogen1.Build(ctx, defaultResource, otherResources);
						break;
					case 3:
						output = CSharpAutogen3.Build(ctx, defaultResource, otherResources);
						break;
					case 2:
					default:
						output = CSharpAutogen2.Build(ctx, defaultResource, otherResources);
						break;
				}
				return ctx.Save(output, options.Value.OutputDirectory);
			}
		}
	}
}