using System;
using System.Collections.Generic;

namespace Lean
{
	// Token: 0x020001BC RID: 444
	public static class LeanClassPool<T> where T : class
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x00022828 File Offset: 0x00020C28
		public static T Spawn()
		{
			return LeanClassPool<T>.Spawn(null, null);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00022831 File Offset: 0x00020C31
		public static T Spawn(Action<T> onSpawn)
		{
			return LeanClassPool<T>.Spawn(null, onSpawn);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0002283A File Offset: 0x00020C3A
		public static T Spawn(Predicate<T> match)
		{
			return LeanClassPool<T>.Spawn(match, null);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00022844 File Offset: 0x00020C44
		public static T Spawn(Predicate<T> match, Action<T> onSpawn)
		{
			int num = (match == null) ? (LeanClassPool<T>.cache.Count - 1) : LeanClassPool<T>.cache.FindIndex(match);
			if (num >= 0)
			{
				T t = LeanClassPool<T>.cache[num];
				LeanClassPool<T>.cache.RemoveAt(num);
				if (onSpawn != null)
				{
					onSpawn(t);
				}
				return t;
			}
			return (T)((object)null);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x000228A7 File Offset: 0x00020CA7
		public static void Despawn(T instance)
		{
			LeanClassPool<T>.Despawn(instance, null);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x000228B0 File Offset: 0x00020CB0
		public static void Despawn(T instance, Action<T> onDespawn)
		{
			if (instance != null)
			{
				if (onDespawn != null)
				{
					onDespawn(instance);
				}
				LeanClassPool<T>.cache.Add(instance);
			}
		}

		// Token: 0x040004E3 RID: 1251
		private static List<T> cache = new List<T>();
	}
}
