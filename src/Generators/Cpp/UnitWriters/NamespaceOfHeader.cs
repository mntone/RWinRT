namespace Mntone.RWinRT.Generators.Cpp.UnitWriters
{
	// template<typename T>
	// struct __namespace_of { };
	public sealed class NamespaceOfHeader : Invokable<NamespaceOfHeader>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
			=> ctx.Builder.Append($"{ctx.CurrentIndent()}template<typename T> struct __namespace_of {{ }};{ctx.LineBreak}");
	}
}
