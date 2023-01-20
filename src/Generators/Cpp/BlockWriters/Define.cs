namespace Mntone.RWinRT.Generators.Cpp.BlockWriters
{
	// #define {val}
	//   :
	//   :
	// #undef {val}
	public sealed class Define : Blockable<Define>, ICodeBlockWriter
	{
		public byte NestLevel => 0;

		public void StartCore(ICodeWriterContext ctx, params string[] vals)
			=> ctx.Builder.Append($"#define {vals[0]} {vals[1]}{ctx.LineBreak}");

		public void EndCore(ICodeWriterContext ctx, string val)
			=> ctx.Builder.Append($"#undef {val}{ctx.LineBreak}");
	}
}
