namespace Mntone.RWinRT.Generators.CSharp.UnitWriters
{
	// // "{value}"
	// {accessor} const string {preferred(name)} = "{name}";
	public sealed class ConstVariable : Invokable<ConstVariable>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
		{
			var accessor = ctx.AsCSharp().Accessor;
			var name = vals[0];
			var value = vals[1];
			var preferredName = ctx.PreferredNameConverter(name);
			ctx.Builder.Append($"{ctx.CurrentIndent()}// \"{value}\"{ctx.LineBreak}");
			ctx.Builder.Append($"{ctx.CurrentIndent()}{accessor} const string {preferredName} = \"{name}\";{ctx.LineBreak}");
		}
	}
}
