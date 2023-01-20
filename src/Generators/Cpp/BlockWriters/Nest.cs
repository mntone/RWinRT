namespace Mntone.RWinRT.Generators.Cpp.BlockWriters
{
	public sealed class Nest : Blockable<Nest>, ICodeBlockWriter
	{
		public byte NestLevel => 1;

		public void StartCore(ICodeWriterContext ctx, params string[] vals) { }

		public void EndCore(ICodeWriterContext ctx, string val) { }
	}
}
