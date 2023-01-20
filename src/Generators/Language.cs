namespace Mntone.RWinRT.Generators
{
	public enum Language
	{
		Cpp14,
		Cpp17,

		CSharpLegacy,
		CSharp8,
		CSharp9,
		CSharp10,
		CSharp11,
	}

	public static class LanguageExtensions
	{
		public static bool GetIsCpp(this Language language)
		{
			switch (language)
			{
				case Language.Cpp14:
				case Language.Cpp17:
					return true;
				default:
					return false;
			}
		}
	}
}
