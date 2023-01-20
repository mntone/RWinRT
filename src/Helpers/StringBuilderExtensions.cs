#if NETFRAMEWORK
using System;
using System.Text;

namespace Mntone.RWinRT.Helpers
{
	internal static class StringBuilderExtensions
	{
		public static void Append(this StringBuilder builder, ReadOnlySpan<char> value)
		{
			builder.Append(value.ToArray(), 0, value.Length);
		}
	}

}
#endif
