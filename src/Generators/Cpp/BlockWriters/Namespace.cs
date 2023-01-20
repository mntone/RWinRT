using System;
using System.Linq;

namespace Mntone.RWinRT.Generators.Cpp.BlockWriters
{
	// namespace {val} {
	//   :
	//   :
	// }
	public sealed class Namespace : Blockable<Namespace>, ICodeBlockWriter
	{
		public byte NestLevel => 1;

		public void StartCore(ICodeWriterContext ctx, params string[] vals)
		{
			var name = vals[0];
			if (ctx.Language.ToCppVersion() >= CppVersion.Cpp17)
			{
				ctx.Builder.Append($"{ctx.CurrentIndent()}namespace {name} {{{ctx.LineBreak}{ctx.LineBreak}");
			}
			else
			{
				var nss = string.Concat(name
					.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries)
					.Select(ns => $"namespace {ns} {{ "))
					.TrimEnd(new[] { ' ' });
				ctx.Builder.Append($"{ctx.CurrentIndent()}{nss}{ctx.LineBreak}{ctx.LineBreak}");
			}
		}

		public void EndCore(ICodeWriterContext ctx, string val)
		{
			ctx.Builder.Append(ctx.LineBreak);
			if (ctx.Language.ToCppVersion() >= CppVersion.Cpp17)
			{
				ctx.Builder.Append($"{ctx.CurrentIndent()}}} // ^^^ namespace {val} ^^^{ctx.LineBreak}{ctx.LineBreak}");
			}
			else
			{
				var closing = new string('}', val.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries).Length);
				ctx.Builder.Append($"{ctx.CurrentIndent()}{closing} // ^^^ namespace {val} ^^^{ctx.LineBreak}{ctx.LineBreak}");
			}
		}
	}
}
