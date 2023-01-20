namespace Mntone.RWinRT.Generators.Cpp.UnitWriters
{
	// [C++11 and later] using {preferred} = {original};
	// [before] typedef {original} {preferred};
	public sealed class Alias : Invokable<Alias>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
		{
			var originalName = vals[0];
			var preferredName = vals[1];
			if (ctx.Language.ToCppVersion() >= CppVersion.Cpp11)
			{
				ctx.Builder.Append($"{ctx.CurrentIndent()}using {preferredName} = {originalName};{ctx.LineBreak}");
			}
			else
			{
				ctx.Builder.Append($"{ctx.CurrentIndent()}typedef {originalName} {preferredName};{ctx.LineBreak}");
			}
		}
	}
}
