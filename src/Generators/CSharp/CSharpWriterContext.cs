namespace Mntone.RWinRT.Generators.CSharp
{
	public sealed class CSharpWriterContext : CodeWriterContext
	{
#if NET6_0
		public string Accessor { get; init; }
#else
		public string Accessor { get; }
#endif

		public override bool SetReadOnly => true;

		public CSharpWriterContext(Options options) : this(
			languageVersion: options.LanguageVersion.ToCSharpVersion(CSharpVersion.CSharp10),
			rootNamespace: options.RootNamespace,
			implNamespace: options.ImplNamespace,
			outputDirectory: options.OutputDirectory.Trim(new[] { '"' }),
			filename: options.FileName ?? "Resources.g.cs",
			lineBreak: options.LineBreak,
			isPublic: options.IsPublic)
		{ }

		public CSharpWriterContext(
			string rootNamespace,
			string implNamespace = "__Impl",
			string outputDirectory = "./",
			string filename = "Resources.g.cs",
			string indent = "\t",
			LineBreak lineBreak = RWinRT.LineBreak.LF,
			bool isPublic = false)
			: this(CSharpVersion.CSharp10, rootNamespace, implNamespace, outputDirectory, filename, indent, lineBreak, isPublic)
		{ }

		public CSharpWriterContext(
			CSharpVersion languageVersion,
			string rootNamespace,
			string implNamespace = "__Impl",
			string outputDirectory = "./",
			string filename = "Resources.g.cs",
			string indent = "\t",
			LineBreak lineBreak = RWinRT.LineBreak.LF,
			bool isPublic = false)
			 : base(languageVersion.AsLanguage(), outputDirectory, filename, rootNamespace, implNamespace, indent, lineBreak)
		{
			Accessor = isPublic ? "public" : "internal";
		}
	}

	public static class ISourceCodeWriterContextCppExtensions
	{
		public static CSharpWriterContext AsCSharp(this ICodeWriterContext ctx) => (CSharpWriterContext)ctx;
	}
}
