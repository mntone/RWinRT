using System;

#if NETFRAMEWORK
using Mntone.RWinRT.Helpers;
#endif

namespace Mntone.RWinRT.Generators.UnitWriters
{
	public sealed class Raw : Invokable<Raw>, ICodeUnitWriter
	{
		private const string MARK_INDENT = "<indent>";
		private const string MARK_OUTDENT = "<outdent>";

#pragma warning disable IDE0057 // Disable suggestion for .NET Framework
		public void WriteCore(ICodeWriterContext ctx, params string[] vals)
		{
			foreach (var val in vals)
			{
				var span = val.AsSpan();
				if (span.StartsWith(MARK_OUTDENT.AsSpan()))
				{
					span = span.Slice(9);
					ctx.Builder.Append(span);
					ctx.Builder.Append(ctx.LineBreak);
					continue;
				}

				var indent = 0;
				while (span.StartsWith(MARK_INDENT.AsSpan()))
				{
					span = span.Slice(8);
					++indent;
				}

				ctx.Builder.Append(ctx.CurrentIndent());
				for (; indent > 0; --indent)
				{
					ctx.Builder.Append(ctx.Indent);
				}
				ctx.Builder.Append(span);
				ctx.Builder.Append(ctx.LineBreak);
			}
		}
#pragma warning restore IDE0057
	}
}
