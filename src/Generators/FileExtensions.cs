using System.IO;
using System.Text;

namespace Mntone.RWinRT.Generators
{
	public static class FileExtensions
	{
		public static bool ExistsOutputDirectory(this ICodeWriterContext ctx)
			=> Directory.Exists(ctx.OutputDirectory);

		public static int CheckHashString(this ICodeWriterContext ctx, string inputHashString)
		{
			var filepath = Path.Combine(ctx.OutputDirectory, ctx.IntermediateFileName);
			using (var stream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				using (var reader = new StreamReader(stream, Encoding.UTF8, false, 1024 /* Default Buffer Size*/, true))
				{
					var generatedHashString = reader.ReadLine();
					if (generatedHashString == inputHashString)
					{
						return 1;
					}
				}

				stream.Position = 0;

				using (var writer = new StreamWriter(stream, Encoding.UTF8))
				{
					writer.Write(inputHashString);
				}
			}
			return 0;
		}

		public static int Save(this ICodeWriterContext ctx, string content)
		{
			// Get readonly state from generated file.
			var filepath = Path.Combine(ctx.OutputDirectory, ctx.FileName);

			FileAttributes attr;
			try
			{
				attr = File.GetAttributes(filepath);
				if (attr.HasFlag(FileAttributes.ReadOnly))
				{
					File.SetAttributes(filepath, attr & ~FileAttributes.ReadOnly);
				}
			}
			catch (DirectoryNotFoundException)
			{
				return -1;
			}
			catch (FileNotFoundException) { }

			// Write generated content
			using (var stream = new FileStream(filepath, FileMode.Create, FileAccess.Write))
			using (var writer = new StreamWriter(stream, Encoding.UTF8))
			{
				writer.Write(content);
			}

			// Set readonly to generated file.
			if (ctx.SetReadOnly)
			{
				attr = File.GetAttributes(filepath);
				if (!attr.HasFlag(FileAttributes.ReadOnly))
				{
					File.SetAttributes(filepath, attr | FileAttributes.ReadOnly);
				}
			}

			return 0;
		}
	}
}
