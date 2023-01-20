namespace Mntone.RWinRT.Generators.Cpp.BlockWriters
{
	// enum class {val}: {type} {
	//   :
	//   :
	// };
	public sealed class Enum : Blockable<Enum>, ICodeBlockWriter
	{
		public byte NestLevel => 1;

		public void StartCore(ICodeWriterContext ctx, params string[] vals)
		{
			string keyword = ctx.Language.ToCppVersion() >= CppVersion.Cpp11 ? "enum class" : "enum";
			ctx.Builder.Append($"{ctx.CurrentIndent()}{keyword} {vals[0]}: {vals[1]} {{{ctx.LineBreak}");
		}

		public void EndCore(ICodeWriterContext ctx, string val)
		{
			string keyword = ctx.Language.ToCppVersion() >= CppVersion.Cpp11 ? "enum class" : "enum";
			ctx.Builder.Append($"{ctx.CurrentIndent()}}}; // ^^^ {keyword} {val} ^^^{ctx.LineBreak}{ctx.LineBreak}");
		}
	}
}
