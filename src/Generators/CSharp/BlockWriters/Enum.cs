namespace Mntone.RWinRT.Generators.CSharp.BlockWriters
{
	// {accessor} enum {val}
	// {
	//   :
	//   :
	// }
	public sealed class Enum : Blockable<Enum>, ICodeBlockWriter
	{
		public byte NestLevel => 1;

		public void StartCore(ICodeWriterContext ctx, params string[] vals)
		{
			var name = vals[0];
			var accessor = ctx.AsCSharp().Accessor;
			ctx.Builder.Append($"{ctx.CurrentIndent()}{accessor} enum {name}{ctx.LineBreak}");
			ctx.Builder.Append($"{ctx.CurrentIndent()}{{{ctx.LineBreak}");
		}

		public void EndCore(ICodeWriterContext ctx, string val)
		{
			var accessor = ctx.AsCSharp().Accessor;
			ctx.Builder.Append($"{ctx.CurrentIndent()}}} // ^^^ {accessor} {val} ^^^{ctx.LineBreak}");
			ctx.Builder.Append(ctx.LineBreak);
		}
	}
}
