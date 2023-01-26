// R/WinRT (C#) / CSharpAutogen V2
//
// Copyright (C) mntone.
// Licensed under the MIT License.

#nullable enable

using Microsoft.Windows.ApplicationModel.Resources;

namespace RWinRT
{
	internal abstract class ResourceManager
	{
		protected ResourceMap Resources { get; }

		protected ResourceManager(string resourceName)
		{
			Resources = Native.MainResourceMap.GetSubtree(resourceName);
		}

		public string GetValueCore(string key)
		{
			return Resources.GetValue(key, Context).ValueAsString;
		}

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
		public string GetFormatValueCore(string key, params object?[] args)
#else
		public string GetFormatValueCore(string key, params object[] args)
#endif
		{
			var format = Resources.GetValue(key, Context).ValueAsString;
			return string.Format(format, args);
		}

		protected static Microsoft.Windows.ApplicationModel.Resources.ResourceManager Native { get; }

		protected static ResourceContext Context { get; }

		static ResourceManager()
		{
			Native = new Microsoft.Windows.ApplicationModel.Resources.ResourceManager();
			Context = Native.CreateResourceContext();
		}

		public static void ChangeLanguage(string language)
		{
			Context.QualifierValues["Language"] = language;
		}
	}
}
