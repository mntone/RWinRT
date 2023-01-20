using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mntone.RWinRT
{
	public enum ResourceMode
	{
		Windows8,
		Windows8_1,
		WindowsPhone,
		Windows8_2,
		UAP,
	}

	public enum ResourceQualifierContrast
	{
		Standard,
		High,
		Black,
		White,
	}

	public enum ResourceQualifierLayoutDirection
	{
		LTR,
		RTL,
		TTBLTR,
		TTBRTL,
	}

	public enum ResourceQualifierDXFeatureLevel
	{
		DX9 = 9,
		DX10 = 10,
		DX11 = 11,
		DX12 = 12,
	}

	public enum ResourceQualifierTheme
	{
		None,
		Light,
		Dark,
	}

	// https://learn.microsoft.com/en-us/uwp/api/windows.applicationmodel.resources.core.resourcecontext.qualifiervalues?view=winrt-22621#windows-applicationmodel-resources-core-resourcecontext-qualifiervalues
#if NET6_0_OR_GREATER
	public record struct ResourceQualifiers
#else
	public struct ResourceQualifiers
#endif
	{
		public string Language;
		public ResourceQualifierContrast Contrast;
		public string HomeRegion; // BCP-47 region tag
		public int Scale; // Default: 200 in UAP, 240 in WP, otherwise 100
		public int TargetSize;
		public ResourceQualifierLayoutDirection LayoutDirection;
		public ResourceQualifierDXFeatureLevel DXFeatureLevel;
		public ResourceQualifierTheme Theme;
		public string Configuration;
		public string AlternateForm;
		public string Platform;
		public Dictionary<string, string> Custom;

		public static ResourceQualifiers Parse(string filename, ResourceMode mode, string defaultLanguage)
		{
			var bag = CreateDefault(mode, defaultLanguage);
			var qualifiers = Path.GetDirectoryName(filename)
				.Split(new[] { '.' })
				.Where(s => s.Contains("-"))
				.Reverse()
				.Concat(Path.GetFileNameWithoutExtension(filename).Split(new[] { '.' }).Skip(1));
			foreach (var qualifier in qualifiers)
			{
				var keyValue = qualifier.Split(new[] { '-' }, 2);
				switch (keyValue[0].ToLowerInvariant())
				{
					case "lang":
					case "language":
						bag.Language = keyValue[1];
						break;
					case "contrast":
						switch (keyValue[1].ToLowerInvariant())
						{
							case "standard":
								bag.Contrast = ResourceQualifierContrast.Standard;
								break;
							case "high":
								bag.Contrast = ResourceQualifierContrast.High;
								break;
							case "black":
								bag.Contrast = ResourceQualifierContrast.Black;
								break;
							case "white":
								bag.Contrast = ResourceQualifierContrast.White;
								break;
						}
						break;
					case "homeregion":
						bag.HomeRegion = keyValue[1];
						break;
					case "scale":
						bag.Scale = int.Parse(keyValue[1]);
						break;
					case "targetsize":
						bag.TargetSize = int.Parse(keyValue[1]);
						break;
					case "layoutdir":
					case "layoutdirection":
						switch (keyValue[1].ToUpperInvariant())
						{
							case "LTR":
								bag.LayoutDirection = ResourceQualifierLayoutDirection.LTR;
								break;
							case "RTL":
								bag.LayoutDirection = ResourceQualifierLayoutDirection.RTL;
								break;
							case "TTBLTR":
								bag.LayoutDirection = ResourceQualifierLayoutDirection.TTBLTR;
								break;
							case "TTBRTL":
								bag.LayoutDirection = ResourceQualifierLayoutDirection.TTBRTL;
								break;
						}
						break;
					case "dxfl":
					case "dxfeaturelevel":
						switch (keyValue[1].ToUpperInvariant())
						{
							case "DX9":
								bag.DXFeatureLevel = ResourceQualifierDXFeatureLevel.DX9;
								break;
							case "DX10":
								bag.DXFeatureLevel = ResourceQualifierDXFeatureLevel.DX10;
								break;
							case "DX11":
								bag.DXFeatureLevel = ResourceQualifierDXFeatureLevel.DX11;
								break;
							case "DX12":
								bag.DXFeatureLevel = ResourceQualifierDXFeatureLevel.DX12;
								break;
						}
						break;
					case "theme":
						switch (keyValue[1].ToLowerInvariant())
						{
							case "light":
								bag.Theme = ResourceQualifierTheme.Light;
								break;
							case "dark":
								bag.Theme = ResourceQualifierTheme.Dark;
								break;
						}
						break;
					case "config":
					case "configuration":
						bag.Configuration = keyValue[1];
						break;
					case "altform":
					case "alternateform":
						bag.AlternateForm = keyValue[1];
						break;
					case "platform":
						bag.Platform = keyValue[1];
						break;
					default:
						bag.Custom.Add(keyValue[0], keyValue[1]);
						break;
				}
			}
			return bag;
		}

		public static ResourceQualifiers CreateDefault(string defaultLanguage)
			=> CreateDefault(ResourceMode.UAP, defaultLanguage);

		public static ResourceQualifiers CreateDefault(ResourceMode mode, string defaultLanguage)
		{
			var res = new ResourceQualifiers()
			{
				Language = defaultLanguage,
				Contrast = ResourceQualifierContrast.Standard,
				HomeRegion = "001",
				Scale = mode == ResourceMode.UAP ? 200 : mode == ResourceMode.WindowsPhone ? 240 : 100,
				TargetSize = 256,
				LayoutDirection = ResourceQualifierLayoutDirection.LTR,
				DXFeatureLevel = ResourceQualifierDXFeatureLevel.DX9,
				Theme = mode == ResourceMode.WindowsPhone ? ResourceQualifierTheme.Dark : ResourceQualifierTheme.None,
				Custom = new Dictionary<string, string>(),
			};
			return res;
		}
	}
}
