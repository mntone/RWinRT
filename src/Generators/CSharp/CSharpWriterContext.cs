namespace Mntone.RWinRT.Generators.CSharp
{
	public sealed class CSharpWriterContext : CodeWriterContext
	{
#if NET6_0
		public string Accessor { get; init; }
#else
		public string Accessor { get; }
#endif

		public CSharpWriterContext(
			string rootNamespace,
			string implNamespace = "__Impl",
			string filename = "Resources.g.cs",
			string indent = "\t",
			LineBreak lineBreak = RWinRT.LineBreak.LF,
			bool isPublic = false)
			: this(CSharpVersion.CSharp10, rootNamespace, implNamespace, filename, indent, lineBreak, isPublic)
		{ }

		public CSharpWriterContext(
			CSharpVersion languageVersion,
			string rootNamespace,
			string implNamespace = "__Impl",
			string filename = "Resources.g.cs",
			string indent = "\t",
			LineBreak lineBreak = RWinRT.LineBreak.LF,
			bool isPublic = false)
			 : base(languageVersion.AsLanguage(), filename, rootNamespace, implNamespace, indent, lineBreak)
		{
			Accessor = isPublic ? "public" : "internal";
		}
	}

	public static class ISourceCodeWriterContextCppExtensions
	{
		public static CSharpWriterContext AsCSharp(this ICodeWriterContext ctx) => (CSharpWriterContext)ctx;
	}
}
