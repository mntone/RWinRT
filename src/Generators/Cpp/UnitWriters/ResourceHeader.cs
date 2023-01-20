namespace Mntone.RWinRT.Generators.Cpp.UnitWriters
{
	// template<{type} T>
	// inline constexpr ::std::wstring_view res_v;
	public sealed class ResourceHeader : Invokable<ResourceHeader>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
		{
			var type = vals[0];
			var keyword = ctx.Language.ToCppVersion() >= CppVersion.Cpp17 ? "inline constexpr ::std::wstring_view" : "const wchar_t const*";
			ctx.Builder.Append($"{ctx.CurrentIndent()}template<{type} val> {keyword} res_v;{ctx.LineBreak}");
		}
	}
}
