namespace Mntone.RWinRT.Generators.Cpp.UnitWriters
{
	// #include <{filename}>;
	public sealed class Include : Invokable<Include>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
			=> ctx.Builder.Append($"#include \"{vals[0]}\"{ctx.LineBreak}");
	}
}
