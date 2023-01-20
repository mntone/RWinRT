using System;

namespace Mntone.RWinRT.Generators.Cpp
{
	public enum CppVersion : byte
	{
		Cpp03 = 3,
		Cpp11 = 11,
		Cpp14 = 14,
		Cpp17 = 17,
	}

	public static class CppLanguageExtensions
	{
		public static Language AsLanguage(this CppVersion version)
		{
			switch (version)
			{
				case CppVersion.Cpp17: return Language.Cpp17;
				case CppVersion.Cpp14: return Language.Cpp14;
				default: throw new ArgumentException(nameof(version));
			}
		}

		public static CppVersion ToCppVersion(this Language language)
		{
			switch (language)
			{
				case Language.Cpp14: return CppVersion.Cpp14;
				case Language.Cpp17: return CppVersion.Cpp17;
				default: throw new ArgumentException(nameof(language));
			}
		}
	}
}
