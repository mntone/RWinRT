namespace Mntone.RWinRT.Generators.CSharp.BlockWriters
{
	// switch ({arg})
	// {
	//   :
	//   :
	// }
	public sealed class Switch : Blockable<Switch>, ICodeBlockWriter
	{
		public byte NestLevel => 1;

		public void StartCore(ICodeWriterContext ctx, params string[] vals)
		{
			var arg = vals[0];
			ctx.Builder.Append($"{ctx.CurrentIndent()}switch ({arg}){ctx.LineBreak}");
			ctx.Builder.Append($"{ctx.CurrentIndent()}{{{ctx.LineBreak}");
		}

		public void EndCore(ICodeWriterContext ctx, string val)
		{
			ctx.Builder.Append($"{ctx.CurrentIndent()}}} // ^^^ switch ({val}) ^^^{ctx.LineBreak}");
			ctx.Builder.Append(ctx.LineBreak);
		}
	}
}
