namespace Mntone.RWinRT.Generators
{
	public interface ICodeUnitWriter
	{
		void WriteCore(ICodeWriterContext ctx, params string[] vals);
	}
}
