namespace Mntone.RWinRT.Generators
{
    public interface ICodeBlockWriter
    {
        byte NestLevel { get; }

        void StartCore(ICodeWriterContext ctx, params string[] vals);

        void EndCore(ICodeWriterContext ctx, string val);
    }
}
