using System;

namespace Mntone.RWinRT.Generators.CSharp
{
	public enum CSharpVersion : byte
	{
		CSharp7_3 = 73,
		CSharp8 = 80,
		CSharp9 = 90,
		CSharp10 = 100,
		CSharp11 = 110,
	}

	public static class CSharpLanguageExtensions
	{
		public static Language AsLanguage(this CSharpVersion version)
		{
			switch (version)
			{
				case CSharpVersion.CSharp11: return Language.CSharp11;
				case CSharpVersion.CSharp10: return Language.CSharp10;
				case CSharpVersion.CSharp9: return Language.CSharp9;
				case CSharpVersion.CSharp8: return Language.CSharp8;
				default: return Language.CSharpLegacy;
			}
		}

		public static CSharpVersion ToCSharpVersion(this Language language)
		{
			switch (language)
			{
				case Language.CSharpLegacy: return CSharpVersion.CSharp7_3;
				case Language.CSharp8: return CSharpVersion.CSharp8;
				case Language.CSharp9: return CSharpVersion.CSharp9;
				case Language.CSharp10: return CSharpVersion.CSharp10;
				case Language.CSharp11: return CSharpVersion.CSharp11;
				default: throw new ArgumentException(nameof(language));
			}
		}
	}
}
