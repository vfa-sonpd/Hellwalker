using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lean
{
	// Token: 0x020001BD RID: 445
	[AddComponentMenu("Lean/Pool")]
	public class LeanPool : MonoBehaviour
	{
		// Token: 0x06000D98 RID: 3480 RVA: 0x00022906 File Offset: 0x00020D06
		public static T Spawn<T>(T prefab) where T : Component
		{
			return LeanPool.Spawn<T>(prefab, Vector3.zero, Quaternion.identity, null);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00022919 File Offset: 0x00020D19
		public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
		{
			return LeanPool.Spawn<T>(prefab, position, rotation, null);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00022924 File Offset: 0x00020D24
		public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Component
		{
			GameObject prefab2 = (!(prefab != null)) ? null : prefab.gameObject;
			GameObject gameObject = LeanPool.Spawn(prefab2, position, rotation, parent);
			return (!(gameObject != null)) ? ((T)((object)null)) : gameObject.GetComponent<T>();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0002297D File Offset: 0x00020D7D
		public static GameObject Spawn(GameObject prefab)
		{
			return LeanPool.Spawn(prefab, Vector3.zero, Quaternion.identity, null);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00022990 File Offset: 0x00020D90
		public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
		{
			return LeanPool.Spawn(prefab, position, rotation, null);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002299C File Offset: 0x00020D9C
		public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
		{
			if (prefab != null)
			{
				LeanPool leanPool = LeanPool.AllPools.Find((LeanPool p) => p.Prefab == prefab);
				if (leanPool == null)
				{
					leanPool = new GameObject(prefab.name + " Pool").AddComponent<LeanPool>();
					leanPool.Prefab = prefab;
				}
				GameObject gameObject = leanPool.FastSpawn(position, rotation, parent);
				if (gameObject != null)
				{
					LeanPool.AllLinks.Add(gameObject, leanPool);
					return gameObject.gameObject;
				}
			}
			else
			{
				Debug.LogError("Attempting to spawn a null prefab");
			}
			return null;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00022A4F File Offset: 0x00020E4F
		public static void Despawn(Component clone, float delay = 0f)
		{
			if (clone != null)
			{
				LeanPool.Despawn(clone.gameObject, 0f);
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00022A70 File Offset: 0x00020E70
		public static void Despawn(GameObject clone, float delay = 0f)
		{
			if (clone != null)
			{
				LeanPool leanPool = null;
				if (LeanPool.AllLinks.TryGetValue(clone, out leanPool))
				{
					LeanPool.AllLinks.Remove(clone);
					leanPool.FastDespawn(clone, delay);
				}
				else
				{
					UnityEngine.Object.Destroy(clone);
				}
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00022AC1 File Offset: 0x00020EC1
		public int Total
		{
			get
			{
				return this.total;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00022AC9 File Offset: 0x00020EC9
		public int Cached
		{
			get
			{
				return this.cache.Count;
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00022AD8 File Offset: 0x00020ED8
		public GameObject FastSpawn(Vector3 position, Quaternion rotation, Transform parent = null)
		{
			if (this.Prefab != null)
			{
				while (this.cache.Count > 0)
				{
					int index = this.cache.Count - 1;
					GameObject gameObject = this.cache[index];
					this.cache.RemoveAt(index);
					if (gameObject != null)
					{
						Transform transform = gameObject.transform;
						transform.localPosition = position;
						transform.localRotation = rotation;
						transform.SetParent(parent, false);
						gameObject.SetActive(true);
						this.SendNotification(gameObject, "OnSpawn");

                        PooledParticle pooledObj = gameObject.GetComponent<PooledParticle>();
                        if (pooledObj)
                        {
                            pooledObj.originPool = this;
                        }
                        return gameObject;
					}
					Debug.LogError("The " + base.name + " pool contained a null cache entry");
				}
				if (this.Capacity <= 0 || this.total < this.Capacity)
				{
					GameObject gameObject2 = this.FastClone(position, rotation, parent);
					this.SendNotification(gameObject2, "OnSpawn");

                    PooledParticle pooledObj = gameObject.GetComponent<PooledParticle>();
                    if (pooledObj)
                    {
                        pooledObj.originPool = this;
                    }
                    return gameObject2;
				}
			}
			else
			{
				Debug.LogError("Attempting to spawn null");
			}
			return null;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00022BD0 File Offset: 0x00020FD0
		public void FastDespawn(GameObject clone, float delay = 0f)
		{
			if (clone != null)
			{
				if (delay > 0f)
				{
					if (!this.delayedDestructions.Exists((LeanPool.DelayedDestruction m) => m.Clone == clone))
					{
						LeanPool.DelayedDestruction delayedDestruction = LeanClassPool<LeanPool.DelayedDestruction>.Spawn() ?? new LeanPool.DelayedDestruction();
						delayedDestruction.Clone = clone;
						delayedDestruction.Life = delay;
						this.delayedDestructions.Add(delayedDestruction);
					}
				}
				else
				{
					this.cache.Add(clone);
					this.SendNotification(clone, "OnDespawn");

                    clone.SetActive(false);
					clone.transform.SetParent(base.transform, false);
				}
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00022CA4 File Offset: 0x000210A4
		public void FastPreload()
		{
			if (this.Prefab != null)
			{
				GameObject gameObject = this.FastClone(Vector3.zero, Quaternion.identity, null);
				this.cache.Add(gameObject);
				gameObject.SetActive(false);
				gameObject.transform.SetParent(base.transform, false);
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00022CF9 File Offset: 0x000210F9
		protected virtual void Awake()
		{
			this.UpdatePreload();
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00022D01 File Offset: 0x00021101
		protected virtual void OnEnable()
		{
			LeanPool.AllPools.Add(this);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00022D0E File Offset: 0x0002110E
		protected virtual void OnDisable()
		{
			LeanPool.AllPools.Remove(this);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00022D1C File Offset: 0x0002111C
		protected virtual void Update()
		{
			for (int i = this.delayedDestructions.Count - 1; i >= 0; i--)
			{
				LeanPool.DelayedDestruction delayedDestruction = this.delayedDestructions[i];
				if (delayedDestruction.Clone != null)
				{
					delayedDestruction.Life -= Time.deltaTime;
					if (delayedDestruction.Life <= 0f)
					{
						this.RemoveDelayedDestruction(i);
						this.FastDespawn(delayedDestruction.Clone, 0f);
					}
				}
				else
				{
					this.RemoveDelayedDestruction(i);
				}
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00022DAC File Offset: 0x000211AC
		private void RemoveDelayedDestruction(int index)
		{
			LeanPool.DelayedDestruction instance = this.delayedDestructions[index];
			this.delayedDestructions.RemoveAt(index);
			LeanClassPool<LeanPool.DelayedDestruction>.Despawn(instance);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00022DD8 File Offset: 0x000211D8
		private void UpdatePreload()
		{
			if (this.Prefab != null)
			{
				for (int i = this.total; i < this.Preload; i++)
				{
					this.FastPreload();
				}
			}
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00022E18 File Offset: 0x00021218
		private GameObject FastClone(Vector3 position, Quaternion rotation, Transform parent)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prefab, position, rotation);
			this.total++;
			gameObject.name = this.Prefab.name + " " + this.total;
			gameObject.transform.SetParent(parent, false);
			return gameObject;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00022E78 File Offset: 0x00021278
		private void SendNotification(GameObject clone, string messageName)
		{
			LeanPool.NotificationType notification = this.Notification;
			if (notification != LeanPool.NotificationType.SendMessage)
			{
				if (notification == LeanPool.NotificationType.BroadcastMessage)
				{
					clone.BroadcastMessage(messageName, SendMessageOptions.DontRequireReceiver);
				}
			}
			else
			{
				clone.SendMessage(messageName, SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x040004E4 RID: 1252
		public static List<LeanPool> AllPools = new List<LeanPool>();

		// Token: 0x040004E5 RID: 1253
		public static Dictionary<GameObject, LeanPool> AllLinks = new Dictionary<GameObject, LeanPool>();

		// Token: 0x040004E6 RID: 1254
		[Tooltip("The prefab the clones will be based on")]
		public GameObject Prefab;

		// Token: 0x040004E7 RID: 1255
		[Tooltip("Should this pool preload some clones?")]
		public int Preload;

		// Token: 0x040004E8 RID: 1256
		[Tooltip("Should this pool have a maximum amount of spawnable clones?")]
		public int Capacity;

		// Token: 0x040004E9 RID: 1257
		[Tooltip("Should this pool send messages to the clones when they're spawned/despawned?")]
		public LeanPool.NotificationType Notification = LeanPool.NotificationType.SendMessage;

		// Token: 0x040004EA RID: 1258
		private List<GameObject> cache = new List<GameObject>();

		// Token: 0x040004EB RID: 1259
		private List<LeanPool.DelayedDestruction> delayedDestructions = new List<LeanPool.DelayedDestruction>();

		// Token: 0x040004EC RID: 1260
		private int total;

		// Token: 0x020001BE RID: 446
		public class DelayedDestruction
		{
			// Token: 0x040004ED RID: 1261
			public GameObject Clone;

			// Token: 0x040004EE RID: 1262
			public float Life;
		}

		// Token: 0x020001BF RID: 447
		public enum NotificationType
		{
			// Token: 0x040004F0 RID: 1264
			None,
			// Token: 0x040004F1 RID: 1265
			SendMessage,
			// Token: 0x040004F2 RID: 1266
			BroadcastMessage
		}
	}
}
