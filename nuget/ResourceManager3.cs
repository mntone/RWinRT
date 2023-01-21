// R/WinRT (C#)
//
// Copyright (C) mntone.
// Licensed under the MIT License.

#nullable enable

using Microsoft.Windows.ApplicationModel.Resources;

namespace RWinRT
{
	internal abstract class ResourceManager
	{
		private ResourceMap Resources { get; }

		protected ResourceManager(string resourceName)
		{
			Resources = Native.MainResourceMap.GetSubtree(resourceName);
		}

		public string GetValueCore(string key)
		{
			return Resources.GetValue(key, Context).ValueAsString;
		}

		public string GetValueCore(string key, ResourceContext context)
		{
			return Resources.GetValue(key, context).ValueAsString;
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

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
		public string GetFormatValueCore(string key, ResourceContext context, params object?[] args)
#else
		public string GetFormatValueCore(string key, ResourceContext context, params object[] args)
#endif
		{
			var format = Resources.GetValue(key, context).ValueAsString;
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

	internal struct ResourceObject
	{
		public ResourceManager Manager { get; }

		public string Key { get; }

		internal ResourceObject(ResourceManager manager, string key)
		{
			Manager = manager;
			Key = key;
		}

		public string Value
		{
			get { return Manager.GetValueCore(Key); }
		}

		public string ValueIn(ResourceContext context)
		{
			return Manager.GetValueCore(Key, context);
		}

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
		public string Format(params object?[] args)
#else
		public string Format(params object[] args)
#endif
		{
			return Manager.GetFormatValueCore(Key, args);
		}

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
		public string Format(ResourceContext context, params object?[] args)
#else
		public string Format(ResourceContext context, params object[] args)
#endif
		{
			return Manager.GetFormatValueCore(Key, context, args);
		}
	}
}
