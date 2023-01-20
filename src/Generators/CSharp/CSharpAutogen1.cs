﻿using Mntone.RWinRT.Generators.CSharp.BlockWriters;
using Mntone.RWinRT.Generators.CSharp.UnitWriters;
using Mntone.RWinRT.Generators.UnitWriters;
using System;

namespace Mntone.RWinRT.Generators.CSharp
{
	public static class CSharpAutogen1

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
			"",
			"// R/WinRT (C#)",
			"//",
			$"// Copyright (C) {DateTime.UtcNow.Year} {System.Diagnostics.FileVersionInfo.GetVersionInfo( typeof(Program).Assembly.Location).CompanyName}.",
			"// Licensed under the MIT License.",
			"",
			"using System;",
		};

		private static void WriteSection(CSharpWriterContext ctx, ResourcesData data)
		{
			using (StaticClass.Block(ctx, "R"))
			{
				foreach (var resource in data.Resources)
				{
					ConstVariable.Write(ctx, resource.Name, resource.Value);
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
				WriteSection(ctx, defaultResource);
				foreach (var resource in resources)
				{
					using (Namespace.Block(ctx, resource.ResourceType))
					{
						WriteSection(ctx, resource);
					}
				}
			}
			return ctx.Builder.ToString();
		}
	}
}
