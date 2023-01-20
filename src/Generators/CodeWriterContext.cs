using System;
using System.Linq;
using System.Text;

namespace Mntone.RWinRT.Generators
{
	public abstract class CodeWriterContext : ICodeWriterContext
	{
		public StringBuilder Builder { get; }

#if NET6_0_OR_GREATER
		public Language Language { get; init; }

		public string FileName { get; init; }

		public string RootNamespace { get; init; }

		public string ImplNamespace { get; init; }

		public string Indent { get; init; }

		public string LineBreak { get; init; }

		public Func<string, string> PreferredTypeConverter { get; init; }

		public Func<string, string> PreferredNameConverter { get; init; }
#else
		public Language Language { get; }

		public string FileName { get; }

		public string RootNamespace { get; }

		public string ImplNamespace { get; }

		public string Indent { get; }

		public string LineBreak { get; }

		public Func<string, string> PreferredTypeConverter { get; }

		public Func<string, string> PreferredNameConverter { get; }

#endif

		public byte NestLevel { get; set; }

		protected CodeWriterContext(
			Language language,
			string filename,
			string rootNamespace,
			string implNamespace = "__impl",
			string indent = "\t",
			LineBreak lineBreak = RWinRT.LineBreak.LF)
		{
			Builder = new StringBuilder();
			Language = language;
			FileName = filename;
			RootNamespace = rootNamespace;
			ImplNamespace = implNamespace;
			Indent = indent;
			LineBreak = lineBreak.Actual();
			PreferredTypeConverter = originalName =>
			{
				if (originalName.EndsWith("Resources"))
				{
					return originalName.Substring(0, originalName.Length - 8);
				}
				return null;
			};
			PreferredNameConverter = originalName => originalName.Replace(".", "__").Replace("/", "___");
		}

		public string CurrentIndent()
			=> string.Concat(Enumerable.Repeat(Indent, NestLevel));
	}
}
