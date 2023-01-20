namespace Mntone.RWinRT.Generators.CSharp.UnitWriters
{
	// // "{value}"
	// {preferred(name)};
	public sealed class EnumValue : Invokable<EnumValue>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
		{
			var name = vals[0];
			var value = vals[1];
			var preferredName = ctx.PreferredNameConverter(name);
			ctx.Builder.Append($"{ctx.CurrentIndent()}/// <summary>\"{value}\"</summary>{ctx.LineBreak}");
			ctx.Builder.Append($"{ctx.CurrentIndent()}{preferredName},{ctx.LineBreak}");
		}
	}
}
