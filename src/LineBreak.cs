using System;

namespace Mntone.RWinRT
{
	public enum LineBreak : byte
	{
		LF,
		CR,
		CRLF,
	}

	public static class LineBreakExtensions
	{
		public static string Actual(this LineBreak lineBreak)
		{
			string actual;
			switch (lineBreak)
			{
				case LineBreak.LF:
					actual = "\n";
					break;
				case LineBreak.CR:
					actual = "\r";
					break;
				case LineBreak.CRLF:
					actual = "\r\n";
					break;
				default:
					throw new ArgumentException(nameof(lineBreak));
			}
			return actual;
		}
	}
}
