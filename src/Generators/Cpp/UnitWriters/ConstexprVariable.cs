namespace Mntone.RWinRT.Generators.Cpp.UnitWriters
{
	// // "{value}"
	// constexpr ::std::wstring_view {preferred(name)} { L"{name}" };
	public sealed class ConstexprVariable : Invokable<ConstexprVariable>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
		{
			var name = vals[0];
			var value = vals[1];
			var preferredName = ctx.PreferredNameConverter(name);
			var keyword = ctx.Language.ToCppVersion() >= CppVersion.Cpp17 ? "inline constexpr ::std::wstring_view" : "const wchar_t const*";
			ctx.Builder.Append($"{ctx.CurrentIndent()}// \"{value}\"{ctx.LineBreak}");
			ctx.Builder.Append($"{ctx.CurrentIndent()}{keyword} {preferredName} {{ L\"{name}\" }};{ctx.LineBreak}");
		}
	}
}

