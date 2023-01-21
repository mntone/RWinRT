﻿using Mntone.RWinRT.Generators.CSharp.BlockWriters;
using Mntone.RWinRT.Generators.UnitWriters;
using System.Linq;

namespace Mntone.RWinRT.Generators.CSharp
{
	public static class CSharpAutogen3

	{
		public static string[] Headers { get; } =
		{
			"//------------------------------------------------------------------------------",
			"// <auto-generated>",
			$"//     This file was generated by R/WinRT v{typeof(Program).Assembly.GetName().Version}",
			"//",
			"//     Changes to this file may cause incorrect behavior and will be lost if",
			"//     the code is regenerated.",
			"// </auto-generated>",
			"//------------------------------------------------------------------------------",
			"#nullable enable",
			"",
			"// R/WinRT (C#) / CSharpAutogen V3",
			"//",
			$"// Copyright (C) {System.DateTime.UtcNow.Year} {System.Diagnostics.FileVersionInfo.GetVersionInfo( typeof(Program).Assembly.Location).CompanyName}.",
			"// Licensed under the MIT License.",
			"",
		};

		public static string[] Body { get; } =
		{
			"private sealed class __<0>_ResourceManager : global::RWinRT.ResourceManager",
			"{",
			"<indent>public static __<0>_ResourceManager Instance { get; } = new __<0>_ResourceManager();",
			"<indent>public __<0>_ResourceManager() : base(\"<0>\") { }",
			"}",
			"",
		};

		private static void WriteSection(CSharpWriterContext ctx, string type, ResourcesData data)
		{
			using (StaticClass.Block(ctx, "R"))
			{
				Raw.Write(ctx, Body.Select(v => v.Replace("<0>", type)).ToArray());

				foreach (var resource in data.Resources)
				{
					var name = resource.Name;
					var preferredName = ctx.PreferredNameConverter(name);
					Raw.Write(ctx,
						$"/// <summary>\"{resource.Value}\"</summary>",
						$"public static global::RWinRT.ResourceObject {preferredName} {{ get; }} = new global::RWinRT.ResourceObject(__{type}_ResourceManager.Instance, \"{name}\");");
				}
			}
		}

		public static string Build(CSharpWriterContext ctx, ResourcesData defaultResource, ResourcesData[] resources)
		{
			// Write headers
			Raw.Write(ctx, Headers);

			// Write root namespace
			using (Namespace.Block(ctx, ctx.RootNamespace))
			{
				WriteSection(ctx, defaultResource.ResourceType, defaultResource);
				foreach (var resource in resources)
				{
					using (Namespace.Block(ctx, resource.ResourceType))
					{
						WriteSection(ctx, resource.ResourceType, resource);
					}
				}
			}
			return ctx.Builder.ToString();
		}
	}
}
