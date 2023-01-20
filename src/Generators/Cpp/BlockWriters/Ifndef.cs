namespace Mntone.RWinRT.Generators.Cpp.BlockWriters
{
    // #ifndef {val}
    //   :
    //   :
    // #endif
    public sealed class Ifndef : Blockable<Ifndef>, ICodeBlockWriter
    {
        public byte NestLevel => 0;

        public void StartCore(ICodeWriterContext ctx, params string[] vals)
            => ctx.Builder.Append($"#ifndef {vals[0]}{ctx.LineBreak}");

        public void EndCore(ICodeWriterContext ctx, string val)
            => ctx.Builder.Append($"#endif // ^^^ ifndef {val} ^^^{ctx.LineBreak}{ctx.LineBreak}");
    }
}
