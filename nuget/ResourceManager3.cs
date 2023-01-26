// R/WinRT (C#) / CSharpAutogen V3
//
// Copyright (C) mntone.
// Licensed under the MIT License.

#nullable enable

using System.Globalization;

namespace RWinRT
{
	internal abstract class ResourceManager
	{
		public Microsoft.Windows.ApplicationModel.Resources.ResourceMap Resources { get; }

		protected ResourceManager(string resourceName)
		{
			Resources = Native.MainResourceMap.GetSubtree(resourceName);
		}

		internal protected static Microsoft.Windows.ApplicationModel.Resources.ResourceManager Native { get; }

		internal protected static Microsoft.Windows.ApplicationModel.Resources.ResourceContext Context { get; }

		static ResourceManager()
		{
			Native = new Microsoft.Windows.ApplicationModel.Resources.ResourceManager();
			Context = Native.CreateResourceContext();
		}

		public static void Change(CultureInfo culture)
		{
			Context.QualifierValues["Language"] = culture.Name;
		}

		public static void ChangeLanguage(string language)
		{
			Context.QualifierValues["Language"] = language;
		}
	}

	internal struct ResourceObject
	{
		public Microsoft.Windows.ApplicationModel.Resources.ResourceMap Resources { get; }

		/// <summary>
		/// Get the key related to this.
		/// </summary>
		public string Key { get; }

		internal ResourceObject(Microsoft.Windows.ApplicationModel.Resources.ResourceMap resources, string key)
		{
			Resources = resources;
			Key = key;
		}

		/// <summary>
		/// Get the localized text.
		/// </summary>
		public string Value
		{
			get { return ValueIn(ResourceManager.Context); }
		}

		/// <summary>
		/// Get the localized text related to <paramref name="context" />.
		/// </summary>
		/// <param name="context">The context to get the context specified.</param>
		/// <returns>A string containing the localized text.</returns>
		public string ValueIn(Microsoft.Windows.ApplicationModel.Resources.ResourceContext context)
		{
			return Resources.GetValue(Key, context).ValueAsString;
		}

		/// <summary>
		/// Get the localized text related to <paramref name="language" />.
		/// </summary>
		/// <param name="language">The language tag to get the target language.</param>
		/// <returns>A string containing the localized text.</returns>
		public string ValueIn(string language)
		{
			var context = ResourceManager.Native.CreateResourceContext();
			context.QualifierValues["Language"] = language;
			return ValueIn(context);
		}

		/// <summary>
		/// Get the localized text related to <paramref name="culture" />.
		/// </summary>
		/// <param name="culture">The culture to get the target language.</param>
		/// <returns>A string containing the localized text.</returns>
		public string ValueIn(CultureInfo culture)
		{
			if (string.IsNullOrEmpty(culture.Name))
			{
				return Resources.GetValue(Key).ValueAsString;
			}
			else
			{
				return ValueIn(culture.Name);
			}
		}

		/// <summary>
		/// Gets the localized text.
		/// </summary>
		/// <returns>A string containing the localized text.</returns>
		[System.Obsolete($"This method is obsolete. Use {nameof(Value)} instead.", false)]
		public string GetValue()
		{
			return Value;
		}

		/// <summary>
		/// Gets the formatted and localized text.
		/// </summary>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns>A string containing the formatted and localized text.</returns>
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
		public string Format(params object?[] args)
#else
		public string Format(params object[] args)
#endif
		{
			return Format(ResourceManager.Context, args);
		}

		/// <summary>
		/// Get the formatted and localized text related to <paramref name="context" />.
		/// </summary>
		/// <param name="context">The context to get the context specified.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns>A string containing the formatted and localized text.</returns>
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
		public string Format(Microsoft.Windows.ApplicationModel.Resources.ResourceContext context, params object?[] args)
#else
		public string Format(Microsoft.Windows.ApplicationModel.Resources.ResourceContext context, params object[] args)
#endif
		{
			var format = ValueIn(context);
			return string.Format(format, args);
		}

		/// <summary>
		/// Gets the formatted and localized text related to <paramref name="language" />.
		/// </summary>
		/// <param name="language">The language tag to get the target language.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns>A string containing the formatted and localized text.</returns>
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
		public string Format(string language, params object?[] args)
#else
		public string Format(string language, params object[] args)
#endif
		{
			var context = ResourceManager.Native.CreateResourceContext();
			context.QualifierValues["Language"] = language;
			return Format(context, args);
		}

		/// <summary>
		/// Gets the formatted and localized text related to <paramref name="culture" />.
		/// </summary>
		/// <param name="culture">The culture to get the target language.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns>A string containing the formatted and localized text.</returns>
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
		public string Format(CultureInfo culture, params object?[] args)
#else
		public string Format(CultureInfo culture, params object[] args)
#endif
		{
			if (string.IsNullOrEmpty(culture.Name))
			{
				var format = Resources.GetValue(Key).ValueAsString;
				return string.Format(format, args);
			}
			else
			{
				return Format(culture.Name, args);
			}
		}

		/// <summary>
		/// Gets the formatted and localized text.
		/// </summary>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns>A string containing the formatted and localized text.</returns>
		[System.Obsolete($"This method is obsolete. Call {nameof(Format)} instead.", false)]
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
		public string GetFormatValue(params object?[] args)
#else
		public string GetFormatValue(params object[] args)
#endif
		{
			return Format(args);
		}
	}
}
