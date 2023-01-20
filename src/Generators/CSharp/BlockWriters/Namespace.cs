using System;

namespace Mntone.RWinRT.Generators.CSharp.BlockWriters
{
	// namespace {val}
	// {
	//   :
	//   :
	// }
	public sealed class Namespace : Blockable<Namespace>, ICodeBlockWriter
	{
		public byte NestLevel => 1;

		public void StartCore(ICodeWriterContext ctx, params string[] vals)
		{
			var name = vals[0];
			ctx.Builder.Append($"{ctx.CurrentIndent()}namespace {name}{ctx.LineBreak}");
			ctx.Builder.Append($"{ctx.CurrentIndent()}{{{ctx.LineBreak}{ctx.LineBreak}");
		}

		public void EndCore(ICodeWriterContext ctx, string val)
		{
			ctx.Builder.Append($"{ctx.CurrentIndent()}}} // ^^^ namespace {val} ^^^{ctx.LineBreak}{ctx.LineBreak}");
		}
	}
}
