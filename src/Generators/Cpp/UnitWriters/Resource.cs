namespace Mntone.RWinRT.Generators.Cpp.UnitWriters
{
	// template<>
	// inline constexpr ::std::wstring_view res_v<{type}::{preferred(name)}> { L"{name}" };
	public sealed class Resource : Invokable<Resource>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
		{
			var type = vals[0];
			var name = vals[1];
			var preferredName = ctx.PreferredNameConverter(name);
			var keyword = ctx.Language.ToCppVersion() >= CppVersion.Cpp17 ? "inline constexpr ::std::wstring_view" : "const wchar_t const*";
			ctx.Builder.Append($"{ctx.CurrentIndent()}template<> {keyword} res_v<{type}::{preferredName}> {{ L\"{name}\" }};{ctx.LineBreak}");
		}
	}
}


