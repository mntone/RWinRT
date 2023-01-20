namespace Mntone.RWinRT.Generators.CSharp.UnitWriters
{
	// case {type}.{preferred(name)}: return "{name}";
	public sealed class EnumCase : Invokable<EnumCase>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
		{
			var type = vals[0];
			var name = vals[1];
			var preferredName = ctx.PreferredNameConverter(name);
			ctx.Builder.Append($"{ctx.CurrentIndent()}case {type}.{preferredName}: return \"{name}\";{ctx.LineBreak}");
		}
	}
}
