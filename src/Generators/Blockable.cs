using Mntone.RWinRT.Helpers;
using System;

namespace Mntone.RWinRT.Generators
{
	public abstract class Blockable<T> where T : class, ICodeBlockWriter, new()
	{
		public static T Instance { get; } = new T();

		public static IDisposable Block(ICodeWriterContext ctx, params string[] vals)
		{
			Instance.StartCore(ctx, vals);
			ctx.NestLevel += Instance.NestLevel;

			return new DisposableAction(() =>
			{
				ctx.NestLevel -= Instance.NestLevel;
				if (vals.Length != 0)
				{
					Instance.EndCore(ctx, vals[0]);
				}
			});

		}
	}
}
