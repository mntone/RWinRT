namespace Mntone.RWinRT.Generators.Cpp.UnitWriters
{
	// [Content]
	// // "{content}"
	// {key},
	public static class EnumValue
	{
		private static void ContentCore(ICodeWriterContext ctx, string type, string key, string content)
		{
			ctx.Builder.Append($"{ctx.CurrentIndent()}/// <summary>\"{content}\"</summary>{ctx.LineBreak}");
			if (ctx.Language.ToCppVersion() >= CppVersion.Cpp11)
			{
				ctx.Builder.Append($"{ctx.CurrentIndent()}{key},{ctx.LineBreak}");
			}
			else
			{
				ctx.Builder.Append($"{ctx.CurrentIndent()}{type}__{key},{ctx.LineBreak}");
			}
		}

		private static void FooterCore(ICodeWriterContext ctx, string type)
		{
			if (ctx.Language.ToCppVersion() >= CppVersion.Cpp11)
			{
				ctx.Builder.Append($"{ctx.CurrentIndent()}__MaxCount,{ctx.LineBreak}");
			}
			else
			{
				ctx.Builder.Append($"{ctx.CurrentIndent()}__{type}__MaxCount,{ctx.LineBreak}");
			}
		}

		public static void Build(ICodeWriterContext ctx, ResourcesData data)
		{
			foreach (var resource in data.Resources)
			{
				ContentCore(ctx, data.ResourceType, ctx.PreferredNameConverter(resource.Name), resource.Value);
			}
			FooterCore(ctx, data.ResourceType);
		}
	}
}
