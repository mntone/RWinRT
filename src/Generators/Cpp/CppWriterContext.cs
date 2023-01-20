namespace Mntone.RWinRT.Generators.Cpp
{
	public sealed class CppWriterContext : CodeWriterContext
	{
		public CppWriterContext(
			string rootNamespace,
			string implNamespace = "__impl",
			string filename = "res.g.h",
			string indent = "\t",
			LineBreak lineBreak = RWinRT.LineBreak.LF)
			: this(CppVersion.Cpp17, rootNamespace, implNamespace, filename, indent, lineBreak)
		{ }

		public CppWriterContext(
			CppVersion languageVersion,
			string rootNamespace,
			string implNamespace = "__impl",
			string filename = "res.g.h",
			string indent = "\t",
			LineBreak lineBreak = RWinRT.LineBreak.LF)
			 : base(languageVersion.AsLanguage(), filename, rootNamespace, implNamespace, indent, lineBreak)
		{ }

		public new string RootNamespace => "winrt::" + string.Join("::", base.RootNamespace.Split('.'));
	}

	public static class ISourceCodeWriterContextCppExtensions
	{
		public static CppWriterContext AsCpp(this ICodeWriterContext ctx) => (CppWriterContext)ctx;
	}
}
