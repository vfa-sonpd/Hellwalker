using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lean
{
	// Token: 0x020001CE RID: 462
	public class LeanFinger
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00023D0E File Offset: 0x0002210E
		public bool IsActive
		{
			get
			{
				return LeanTouch.Fingers.Contains(this);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00023D1B File Offset: 0x0002211B
		public float SnapshotDuration
		{
			get
			{
				if (this.Snapshots.Count > 0)
				{
					return this.Age - this.Snapshots[0].Age;
				}
				return 0f;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00023D4C File Offset: 0x0002214C
		public bool IsOverGui
		{
			get
			{
				EventSystem current = EventSystem.current;
				if (current != null)
				{
					PointerEventData pointerEventData = new PointerEventData(current);
					pointerEventData.position = new Vector2(this.ScreenPosition.x, this.ScreenPosition.y);
					LeanFinger.tempRaycastResults.Clear();
					current.RaycastAll(pointerEventData, LeanFinger.tempRaycastResults);
					return LeanFinger.tempRaycastResults.Count > 0;
				}
				return false;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00023DB8 File Offset: 0x000221B8
		public bool HeldDown
		{
			get
			{
				return this.HeldSet && !this.LastHeldSet;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00023DD1 File Offset: 0x000221D1
		public bool HeldUp
		{
			get
			{
				return !this.HeldSet && this.LastHeldSet;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00023DEA File Offset: 0x000221EA
		public bool Down
		{
			get
			{
				return this.Set && !this.LastSet;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00023E03 File Offset: 0x00022203
		public bool Up
		{
			get
			{
				return !this.Set && this.LastSet;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00023E1C File Offset: 0x0002221C
		public Vector2 LastSnapshotDelta
		{
			get
			{
				int count = this.Snapshots.Count;
				if (count > 0)
				{
					LeanFinger.Snapshot snapshot = this.Snapshots[count - 1];
					if (snapshot != null)
					{
						return this.ScreenPosition - snapshot.ScreenPosition;
					}
				}
				return Vector2.zero;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00023E68 File Offset: 0x00022268
		public Vector2 DeltaScreenPosition
		{
			get
			{
				return this.ScreenPosition - this.LastScreenPosition;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00023E7B File Offset: 0x0002227B
		public Vector2 ScaledDeltaScreenPosition
		{
			get
			{
				return this.DeltaScreenPosition * LeanTouch.ScalingFactor;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00023E8D File Offset: 0x0002228D
		public Vector2 TotalDeltaScreenPosition
		{
			get
			{
				return this.ScreenPosition - this.StartScreenPosition;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00023EA0 File Offset: 0x000222A0
		public Vector2 ScaledTotalDeltaScreenPosition
		{
			get
			{
				return this.TotalDeltaScreenPosition * LeanTouch.ScalingFactor;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00023EB2 File Offset: 0x000222B2
		public Vector2 SwipeDelta
		{
			get
			{
				if (LeanTouch.Instance != null)
				{
					return this.GetSnapshotDelta(LeanTouch.Instance.TapThreshold);
				}
				return Vector2.zero;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00023EDA File Offset: 0x000222DA
		public Vector2 ScaledSwipeDelta
		{
			get
			{
				return this.SwipeDelta * LeanTouch.ScalingFactor;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00023EEC File Offset: 0x000222EC
		public float ScaledTotalDeltaMagnitude
		{
			get
			{
				return this.TotalDeltaMagnitude * LeanTouch.ScalingFactor;
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00023EFC File Offset: 0x000222FC
		public Ray GetRay(Camera camera = null)
		{
			if (camera == null)
			{
				camera = Camera.main;
			}
			if (camera != null)
			{
				return camera.ScreenPointToRay(this.ScreenPosition);
			}
			return default(Ray);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00023F44 File Offset: 0x00022344
		public Ray GetStartRay(Camera camera = null)
		{
			if (camera == null)
			{
				camera = Camera.main;
			}
			if (camera != null)
			{
				return camera.ScreenPointToRay(this.StartScreenPosition);
			}
			return default(Ray);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00023F8B File Offset: 0x0002238B
		public Vector2 GetSnapshotDelta(float deltaTime)
		{
			return this.ScreenPosition - this.GetSnapshotScreenPosition(this.Age - deltaTime);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00023FA6 File Offset: 0x000223A6
		public Vector2 GetScaledSnapshotDelta(float deltaTime)
		{
			return this.GetSnapshotDelta(deltaTime) * LeanTouch.ScalingFactor;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00023FBC File Offset: 0x000223BC
		public Vector2 GetSnapshotScreenPosition(float targetAge)
		{
			int lowerSnapshotIndex = this.GetLowerSnapshotIndex(targetAge);
			float num = 0f;
			Vector2 vector = default(Vector2);
			this.GetSnapshot(lowerSnapshotIndex, out num, out vector);
			if (targetAge <= num)
			{
				return vector;
			}
			int index = lowerSnapshotIndex + 1;
			float num2 = 0f;
			Vector2 vector2 = default(Vector2);
			this.GetSnapshot(index, out num2, out vector2);
			if (targetAge >= num2)
			{
				return vector2;
			}
			return Vector2.Lerp(vector, vector2, Mathf.InverseLerp(num, num2, targetAge));
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00024034 File Offset: 0x00022434
		public void GetSnapshot(int index, out float age, out Vector2 screenPosition)
		{
			if (index >= 0 && index < this.Snapshots.Count)
			{
				LeanFinger.Snapshot snapshot = this.Snapshots[index];
				age = snapshot.Age;
				screenPosition = snapshot.ScreenPosition;
			}
			else
			{
				age = this.Age;
				screenPosition = this.ScreenPosition;
			}
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00024093 File Offset: 0x00022493
		public float GetRadians(Vector2 referencePoint)
		{
			return Mathf.Atan2(this.ScreenPosition.x - referencePoint.x, this.ScreenPosition.y - referencePoint.y);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x000240C0 File Offset: 0x000224C0
		public float GetDegrees(Vector2 referencePoint)
		{
			return this.GetRadians(referencePoint) * 57.29578f;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x000240CF File Offset: 0x000224CF
		public float GetLastRadians(Vector2 referencePoint)
		{
			return Mathf.Atan2(this.LastScreenPosition.x - referencePoint.x, this.LastScreenPosition.y - referencePoint.y);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x000240FC File Offset: 0x000224FC
		public float GetLastDegrees(Vector2 referencePoint)
		{
			return this.GetLastRadians(referencePoint) * 57.29578f;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0002410B File Offset: 0x0002250B
		public float GetDeltaRadians(Vector2 referencePoint)
		{
			return this.GetDeltaRadians(referencePoint, referencePoint);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00024118 File Offset: 0x00022518
		public float GetDeltaRadians(Vector2 lastReferencePoint, Vector2 referencePoint)
		{
			float lastRadians = this.GetLastRadians(lastReferencePoint);
			float radians = this.GetRadians(referencePoint);
			float num = Mathf.Repeat(lastRadians - radians, 6.2831855f);
			if (num > 3.1415927f)
			{
				num -= 6.2831855f;
			}
			return num;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00024157 File Offset: 0x00022557
		public float GetDeltaDegrees(Vector2 referencePoint)
		{
			return this.GetDeltaRadians(referencePoint, referencePoint) * 57.29578f;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00024167 File Offset: 0x00022567
		public float GetDeltaDegrees(Vector2 lastReferencePoint, Vector2 referencePoint)
		{
			return this.GetDeltaRadians(lastReferencePoint, referencePoint) * 57.29578f;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00024177 File Offset: 0x00022577
		public float GetLastDistance(Vector2 referencePoint)
		{
			return Vector2.Distance(this.LastScreenPosition, referencePoint);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00024185 File Offset: 0x00022585
		public float GetDistance(Vector2 referencePoint)
		{
			return Vector2.Distance(this.ScreenPosition, referencePoint);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00024194 File Offset: 0x00022594
		public Vector3 GetWorldPosition(float distance, Camera camera = null)
		{
			if (camera == null)
			{
				camera = Camera.main;
			}
			if (camera != null)
			{
				Vector3 position = new Vector3(this.ScreenPosition.x, this.ScreenPosition.y, distance);
				return camera.ScreenToWorldPoint(position);
			}
			return default(Vector3);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x000241F0 File Offset: 0x000225F0
		public Vector3 GetLastWorldPosition(float distance, Camera camera = null)
		{
			if (camera == null)
			{
				camera = Camera.main;
			}
			if (camera != null)
			{
				Vector3 position = new Vector3(this.ScreenPosition.x, this.ScreenPosition.y, distance);
				return camera.ScreenToWorldPoint(position);
			}
			return default(Vector3);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0002424B File Offset: 0x0002264B
		public Vector3 GetDeltaWorldPosition(float distance, Camera camera = null)
		{
			return this.GetDeltaWorldPosition(distance, distance, camera);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00024258 File Offset: 0x00022658
		public Vector3 GetDeltaWorldPosition(float lastDistance, float distance, Camera camera = null)
		{
			if (camera == null)
			{
				camera = Camera.main;
			}
			if (camera != null)
			{
				return this.GetWorldPosition(distance, camera) - this.GetLastWorldPosition(lastDistance, camera);
			}
			return default(Vector3);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000242A4 File Offset: 0x000226A4
		public void Show(Texture texture)
		{
			if (texture != null && this.Set)
			{
				GUI.DrawTexture(new Rect(0f, 0f, (float)texture.width, (float)texture.height)
				{
					center = new Vector2(this.ScreenPosition.x, (float)Screen.height - this.ScreenPosition.y)
				}, texture);
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00024318 File Offset: 0x00022718
		public void ClearSnapshots(int count = -1)
		{
			if (count > 0 && count <= this.Snapshots.Count)
			{
				for (int i = 0; i < count; i++)
				{
					LeanFinger.tempSnapshots.Add(this.Snapshots[i]);
				}
				this.Snapshots.RemoveRange(0, count);
			}
			else if (count < 0)
			{
				LeanFinger.tempSnapshots.AddRange(this.Snapshots);
				this.Snapshots.Clear();
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0002439C File Offset: 0x0002279C
		public void RecordSnapshot()
		{
			LeanFinger.Snapshot snapshot = null;
			int count = LeanFinger.tempSnapshots.Count;
			if (count > 0)
			{
				snapshot = LeanFinger.tempSnapshots[count - 1];
				LeanFinger.tempSnapshots.RemoveAt(count - 1);
			}
			if (snapshot == null)
			{
				snapshot = new LeanFinger.Snapshot();
			}
			snapshot.Age = this.Age;
			snapshot.ScreenPosition = this.ScreenPosition;
			this.Snapshots.Add(snapshot);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00024408 File Offset: 0x00022808
		public int GetLowerSnapshotIndex(float targetAge)
		{
			int count = this.Snapshots.Count;
			if (count > 0)
			{
				float age = this.Snapshots[0].Age;
				if (targetAge > age)
				{
					for (int i = 1; i < count; i++)
					{
						if (this.Snapshots[i].Age > targetAge)
						{
							return i - 1;
						}
					}
					return count - 1;
				}
			}
			return 0;
		}

		// Token: 0x04000500 RID: 1280
		public int Index;

		// Token: 0x04000501 RID: 1281
		public float Age;

		// Token: 0x04000502 RID: 1282
		public bool Set;

		// Token: 0x04000503 RID: 1283
		public bool HeldSet;

		// Token: 0x04000504 RID: 1284
		public bool Tap;

		// Token: 0x04000505 RID: 1285
		public int TapCount;

		// Token: 0x04000506 RID: 1286
		public Vector2 StartScreenPosition;

		// Token: 0x04000507 RID: 1287
		public Vector2 LastScreenPosition;

		// Token: 0x04000508 RID: 1288
		public float TotalDeltaMagnitude;

		// Token: 0x04000509 RID: 1289
		public bool LastSet;

		// Token: 0x0400050A RID: 1290
		public bool LastHeldSet;

		// Token: 0x0400050B RID: 1291
		public Vector2 ScreenPosition;

		// Token: 0x0400050C RID: 1292
		public bool StartedOverGui;

		// Token: 0x0400050D RID: 1293
		public List<LeanFinger.Snapshot> Snapshots = new List<LeanFinger.Snapshot>();

		// Token: 0x0400050E RID: 1294
		private static List<RaycastResult> tempRaycastResults = new List<RaycastResult>();

		// Token: 0x0400050F RID: 1295
		private static List<LeanFinger.Snapshot> tempSnapshots = new List<LeanFinger.Snapshot>();

		// Token: 0x020001CF RID: 463
		public class Snapshot
		{
			// Token: 0x06000E14 RID: 3604 RVA: 0x00024494 File Offset: 0x00022894
			public Vector3 GetWorldPosition(float distance, Camera camera = null)
			{
				if (camera == null)
				{
					camera = Camera.main;
				}
				if (camera != null)
				{
					Vector3 position = new Vector3(this.ScreenPosition.x, this.ScreenPosition.y, distance);
					return camera.ScreenToWorldPoint(position);
				}
				return default(Vector3);
			}

			// Token: 0x04000510 RID: 1296
			public float Age;

			// Token: 0x04000511 RID: 1297
			public Vector2 ScreenPosition;
		}
	}
}
