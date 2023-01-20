using System.IO;
using System.Text;

namespace Mntone.RWinRT.Generators
{
	public static class FileExtensions
	{
		public static int Save(this ICodeWriterContext ctx, string content, string directory, bool setReadonly = true)
		{
			var outputDirectory = directory.Trim(new[] { '"' });
			if (!Directory.Exists(outputDirectory))
			{
				return -1;
			}

			// Get readonly state from generated file.
			var filepath = Path.Combine(outputDirectory, ctx.FileName);

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
			if (setReadonly)
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
