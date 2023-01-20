using Mntone.RWinRT.Generators.Cpp.BlockWriters;

namespace Mntone.RWinRT.Generators.Cpp.UnitWriters
{
	// template<>
	// struct __namespace_of<Resources> {
	//   inline static constexpr ::std::wstring_view type_v { L"{type}" };
	//
	//   template<Resources val>
	//   inline static constexpr ::std::wstring_view res_of() noexcept
	//   {
	//     return __res_impl::res_v<val>;
	//   }
	// };
	public sealed class NamespaceOf : Invokable<NamespaceOf>, ICodeUnitWriter
	{
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
		{
			var name = vals[0];
			var type = vals[1];
			var impl = vals[2];
			var isDefault = bool.Parse(vals[3]);
			var typeSyntax = !isDefault ? type + "::" : string.Empty;
			ctx.Builder.Append($"{ctx.CurrentIndent()}template<> struct __namespace_of<{typeSyntax}{name}> {{{ctx.LineBreak}");
			using (Nest.Block(ctx))
			{
				ctx.Builder.Append($"{ctx.CurrentIndent()}inline static constexpr ::std::wstring_view type_v {{ L\"{type}\" }};{ctx.LineBreak}");
				ctx.Builder.Append($"{ctx.CurrentIndent()}template<{typeSyntax}{name} Key> inline static constexpr ::std::wstring_view res_of(::std::nullptr_t) noexcept {{ return {typeSyntax}{impl}::res_v<Key>; }}{ctx.LineBreak}");
			}
			ctx.Builder.Append($"{ctx.CurrentIndent()}}};{ctx.LineBreak}");
		}
	}
}
