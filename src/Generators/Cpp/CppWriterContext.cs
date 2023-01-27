namespace Mntone.RWinRT.Generators.Cpp
{
	public sealed class CppWriterContext : CodeWriterContext
	{
		public override bool SetReadOnly => false;

		public CppWriterContext(Options options) : this(
			languageVersion: options.LanguageVersion.ToCppVersion(CppVersion.Cpp17),
			rootNamespace: options.RootNamespace,
			implNamespace: options.ImplNamespace,
			outputDirectory: options.OutputDirectory.Trim(new[] { '"' }),
			filename: options.FileName ?? "res.g.h",
			lineBreak: options.LineBreak)
		{ }

		public CppWriterContext(
			string rootNamespace,
			string implNamespace = "__impl",
			string outputDirectory = "./",
			string filename = "res.g.h",
			string indent = "\t",
			LineBreak lineBreak = RWinRT.LineBreak.LF)
			: this(CppVersion.Cpp17, rootNamespace, implNamespace, outputDirectory, filename, indent, lineBreak)
		{ }

		public CppWriterContext(
			CppVersion languageVersion,
			string rootNamespace,
			string implNamespace = "__impl",
			string outputDirectory = "./",
			string filename = "res.g.h",
			string indent = "\t",
			LineBreak lineBreak = RWinRT.LineBreak.LF)
			 : base(languageVersion.AsLanguage(), outputDirectory, filename, rootNamespace, implNamespace, indent, lineBreak)
		{ }

		public new string RootNamespace => "winrt::" + string.Join("::", base.RootNamespace.Split('.'));
	}

	public static class ISourceCodeWriterContextCppExtensions
	{
		public static CppWriterContext AsCpp(this ICodeWriterContext ctx) => (CppWriterContext)ctx;
	}
}
