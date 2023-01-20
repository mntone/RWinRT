namespace Mntone.RWinRT.Generators.CSharp.BlockWriters
{
	// {define}
	// {
	//   :
	//   :
	// }
	public sealed class Block : Blockable<Block>, ICodeBlockWriter
	{
		public byte NestLevel => 1;

		public void StartCore(ICodeWriterContext ctx, params string[] vals)
		{
			var define = vals[0];
			ctx.Builder.Append($"{ctx.CurrentIndent()}{define}{ctx.LineBreak}");
			ctx.Builder.Append($"{ctx.CurrentIndent()}{{{ctx.LineBreak}");
			ctx.Builder.Append(ctx.LineBreak);
		}

		public void EndCore(ICodeWriterContext ctx, string val)
		{
			ctx.Builder.Append(ctx.LineBreak);
			ctx.Builder.Append($"{ctx.CurrentIndent()}}} // ^^^ {val} ^^^{ctx.LineBreak}");
			ctx.Builder.Append(ctx.LineBreak);
		}
	}
}
