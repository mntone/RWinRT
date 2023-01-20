namespace Mntone.RWinRT.Generators.CSharp.UnitWriters
{
	// throw new {type}(nameof({name});
	public sealed class ThrowNew : Invokable<ThrowNew>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
		{
			var type = vals[0];
			var name = vals[1];
			ctx.Builder.Append($"{ctx.CurrentIndent()}throw new {type}(nameof({name}));{ctx.LineBreak}");
		}
	}
}
