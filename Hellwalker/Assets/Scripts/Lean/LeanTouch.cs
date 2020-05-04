using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lean
{
	// Token: 0x020001D0 RID: 464
	[ExecuteInEditMode]
	[AddComponentMenu("Lean/Touch")]
	public class LeanTouch : MonoBehaviour
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00024560 File Offset: 0x00022960
		public static float ScalingFactor
		{
			get
			{
				float result = 1f;
				int num = 200;
				if (LeanTouch.Instance != null)
				{
					num = LeanTouch.Instance.ReferenceDpi;
				}
				if (Screen.dpi > 0f && num > 0)
				{
					result = Mathf.Sqrt((float)num) / Mathf.Sqrt(Screen.dpi);
				}
				return result;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x000245BE File Offset: 0x000229BE
		public static Vector2 ScaledDragDelta
		{
			get
			{
				return LeanTouch.DragDelta * LeanTouch.ScalingFactor;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x000245CF File Offset: 0x000229CF
		public static Vector2 ScaledSoloDragDelta
		{
			get
			{
				return LeanTouch.SoloDragDelta * LeanTouch.ScalingFactor;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x000245E0 File Offset: 0x000229E0
		public static Vector2 ScaledMultiDragDelta
		{
			get
			{
				return LeanTouch.MultiDragDelta * LeanTouch.ScalingFactor;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x000245F4 File Offset: 0x000229F4
		public static bool AnyMouseButtonSet
		{
			get
			{
				for (int i = 0; i < LeanTouch.highestMouseButton; i++)
				{
					if (Input.GetMouseButton(i))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00024628 File Offset: 0x00022A28
		public static bool GuiInUse
		{
			get
			{
				if (GUIUtility.hotControl > 0)
				{
					return true;
				}
				for (int i = LeanTouch.Fingers.Count - 1; i >= 0; i--)
				{
					if (LeanTouch.Fingers[i].IsOverGui)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00024678 File Offset: 0x00022A78
		public static Vector2 GetCenterOfFingers()
		{
			Vector2 vector = Vector2.zero;
			int count = LeanTouch.Fingers.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					vector += LeanTouch.Fingers[i].ScreenPosition;
				}
				vector /= (float)count;
			}
			return vector;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000246D0 File Offset: 0x00022AD0
		public static Vector2 GetLastCenterOfFingers()
		{
			Vector2 vector = Vector2.zero;
			int count = LeanTouch.Fingers.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					vector += LeanTouch.Fingers[i].LastScreenPosition;
				}
				vector /= (float)count;
			}
			return vector;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00024728 File Offset: 0x00022B28
		public static float GetAverageFingerDistance(Vector2 referencePoint)
		{
			float num = 0f;
			int count = LeanTouch.Fingers.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					num += LeanTouch.Fingers[i].GetDistance(referencePoint);
				}
				num /= (float)count;
			}
			return num;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0002477C File Offset: 0x00022B7C
		public static float GetLastAverageFingerDistance(Vector2 referencePoint)
		{
			float num = 0f;
			int count = LeanTouch.Fingers.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					num += LeanTouch.Fingers[i].GetLastDistance(referencePoint);
				}
				num /= (float)count;
			}
			return num;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x000247D0 File Offset: 0x00022BD0
		public static void MoveObject(Transform transform, Vector2 deltaPosition, Camera camera = null)
		{
			if (transform != null && (deltaPosition.x != 0f || deltaPosition.y != 0f))
			{
				RectTransform rectTransform = transform as RectTransform;
				if (rectTransform != null)
				{
					rectTransform.anchoredPosition += deltaPosition;
				}
				else
				{
					transform.position = LeanTouch.MoveObject(transform.position, deltaPosition, camera);
				}
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00024848 File Offset: 0x00022C48
		public static Vector3 MoveObject(Vector3 worldPosition, Vector2 deltaPosition, Camera camera = null)
		{
			if (camera == null)
			{
				camera = Camera.main;
			}
			if (camera != null)
			{
				Vector3 vector = camera.WorldToScreenPoint(worldPosition);
                print(deltaPosition);
				//vector += deltaPosition;
				worldPosition = camera.ScreenToWorldPoint(vector);
			}
			return worldPosition;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00024893 File Offset: 0x00022C93
		public static void RotateObject(Transform transform, float deltaRotation, Camera camera = null)
		{
			if (transform != null && deltaRotation != 0f)
			{
				transform.rotation = LeanTouch.RotateObject(transform.rotation, deltaRotation, camera);
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000248C0 File Offset: 0x00022CC0
		public static Quaternion RotateObject(Quaternion worldRotation, float deltaRotation, Camera camera = null)
		{
			if (deltaRotation != 0f)
			{
				if (camera == null)
				{
					camera = Camera.main;
				}
				if (camera != null)
				{
					worldRotation = Quaternion.AngleAxis(deltaRotation, camera.transform.forward) * worldRotation;
				}
			}
			return worldRotation;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00024911 File Offset: 0x00022D11
		public static void ScaleObject(Transform transform, float scale)
		{
			if (transform != null && scale != 1f)
			{
				transform.localScale *= scale;
			}
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002493C File Offset: 0x00022D3C
		public static void ScaleObjectRelative(Transform transform, float scale, Vector2 referencePoint, Camera camera = null)
		{
			if (transform != null && scale != 1f)
			{
				if (camera == null)
				{
					camera = Camera.main;
				}
				if (camera != null)
				{
					Vector3 position = camera.WorldToScreenPoint(transform.position);
					position.x = referencePoint.x + (position.x - referencePoint.x) * scale;
					position.y = referencePoint.y + (position.y - referencePoint.y) * scale;
					transform.position = camera.ScreenToWorldPoint(position);
					transform.localScale *= scale;
				}
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000249EC File Offset: 0x00022DEC
		public static void RotateObjectRelative(Transform transform, float deltaRotation, Vector2 referencePoint, Camera camera = null)
		{
			if (transform != null && deltaRotation != 0f)
			{
				if (camera == null)
				{
					camera = Camera.main;
				}
				if (camera != null)
				{
					transform.RotateAround(camera.ScreenToWorldPoint(referencePoint), camera.transform.forward, deltaRotation);
				}
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00024A50 File Offset: 0x00022E50
		protected virtual void OnEnable()
		{
			if (LeanTouch.Instance != null && LeanTouch.Instance != this)
			{
				Debug.LogWarning("Your scene already contains a " + typeof(LeanTouch).Name + ", destroying the old one...");
				UnityEngine.Object.DestroyImmediate(LeanTouch.Instance.gameObject);
			}
			LeanTouch.Instance = this;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00024AB5 File Offset: 0x00022EB5
		protected virtual void Update()
		{
			this.UpdateAllInputs();
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00024AC0 File Offset: 0x00022EC0
		protected virtual void OnGUI()
		{
			if (this.FingerTexture != null && Input.touchCount == 0 && LeanTouch.Fingers.Count > 1)
			{
				for (int i = LeanTouch.Fingers.Count - 1; i >= 0; i--)
				{
					LeanTouch.Fingers[i].Show(this.FingerTexture);
				}
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00024B2B File Offset: 0x00022F2B
		private void UpdateAllInputs()
		{
			this.UpdateFingers();
			this.UpdateMultiTap();
			this.UpdateGestures();
			this.UpdateEvents();
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00024B48 File Offset: 0x00022F48
		private void UpdateFingers()
		{
			for (int i = LeanTouch.InactiveFingers.Count - 1; i >= 0; i--)
			{
				LeanTouch.InactiveFingers[i].Age += Time.unscaledDeltaTime;
			}
			for (int j = LeanTouch.Fingers.Count - 1; j >= 0; j--)
			{
				LeanFinger leanFinger = LeanTouch.Fingers[j];
				if (leanFinger.Up)
				{
					LeanTouch.Fingers.RemoveAt(j);
					LeanTouch.InactiveFingers.Add(leanFinger);
					leanFinger.Age = 0f;
					leanFinger.ClearSnapshots(-1);
				}
				else
				{
					leanFinger.LastSet = leanFinger.Set;
					leanFinger.LastHeldSet = leanFinger.HeldSet;
					leanFinger.LastScreenPosition = leanFinger.ScreenPosition;
					leanFinger.Set = false;
					leanFinger.HeldSet = false;
					leanFinger.Tap = false;
				}
			}
			if (Input.touchCount > 0)
			{
				for (int k = 0; k < Input.touchCount; k++)
				{
					Touch touch = Input.GetTouch(k);
					this.AddFinger(touch.fingerId, touch.position);
				}
			}
			else if (LeanTouch.AnyMouseButtonSet)
			{
				Rect rect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
				Vector2 vector = Input.mousePosition;
				if (rect.Contains(vector))
				{
					this.AddFinger(0, vector);
					if (this.SimulateMultiFingers)
					{
						if (Input.GetKey(this.PinchTwistKey))
						{
							Vector2 vector2 = new Vector2((float)Screen.width * 0.5f, (float)Screen.height * 0.5f);
							this.AddFinger(1, vector2 - (vector - vector2));
						}
						else if (Input.GetKey(this.MultiDragKey))
						{
							this.AddFinger(1, vector);
						}
					}
				}
			}
			for (int l = LeanTouch.Fingers.Count - 1; l >= 0; l--)
			{
				LeanFinger leanFinger2 = LeanTouch.Fingers[l];
				if (leanFinger2.Up)
				{
					if (leanFinger2.Age <= this.TapThreshold && leanFinger2.ScaledTotalDeltaMagnitude < this.SwipeThreshold)
					{
						leanFinger2.Tap = true;
						leanFinger2.TapCount++;
					}
					else
					{
						leanFinger2.TapCount = 0;
					}
				}
				else if (!leanFinger2.Down)
				{
					leanFinger2.Age += Time.unscaledDeltaTime;
					if (leanFinger2.Age >= this.HeldThreshold)
					{
						leanFinger2.HeldSet = true;
					}
				}
				leanFinger2.TotalDeltaMagnitude += leanFinger2.DeltaScreenPosition.magnitude;
			}
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00024E04 File Offset: 0x00023204
		private void UpdateMultiTap()
		{
			int count = LeanTouch.Fingers.Count;
			if (count >= 1)
			{
				this.multiFingerTime += Time.unscaledDeltaTime;
				this.multiFingerCount = Mathf.Max(this.multiFingerCount, count);
				if (this.lastFingerCount == 0)
				{
					this.multiFingerTime = 0f;
					this.multiFingerCount = 0;
				}
			}
			if (count == 0 && this.lastFingerCount > 0 && this.multiFingerTime <= this.TapThreshold && LeanTouch.OnMultiTap != null)
			{
				LeanTouch.OnMultiTap(this.multiFingerCount);
			}
			this.lastFingerCount = count;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00024EA8 File Offset: 0x000232A8
		private void UpdateGestures()
		{
			int count = LeanTouch.Fingers.Count;
			LeanTouch.DragDelta = Vector3.zero;
			LeanTouch.SoloDragDelta = Vector2.zero;
			LeanTouch.MultiDragDelta = Vector2.zero;
			LeanTouch.PinchScale = 1f;
			LeanTouch.TwistRadians = 0f;
			LeanTouch.TwistDegrees = 0f;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					LeanTouch.DragDelta += LeanTouch.Fingers[i].DeltaScreenPosition;
				}
				if (count == 1)
				{
					LeanTouch.SoloDragDelta = LeanTouch.Fingers[0].DeltaScreenPosition;
				}
				else
				{
					Vector2 lastCenterOfFingers = LeanTouch.GetLastCenterOfFingers();
					Vector2 centerOfFingers = LeanTouch.GetCenterOfFingers();
					float lastAverageFingerDistance = LeanTouch.GetLastAverageFingerDistance(lastCenterOfFingers);
					float averageFingerDistance = LeanTouch.GetAverageFingerDistance(centerOfFingers);
					if (lastAverageFingerDistance > 0f && averageFingerDistance > 0f)
					{
						LeanTouch.PinchScale = averageFingerDistance / lastAverageFingerDistance;
					}
					for (int j = 0; j < count; j++)
					{
						LeanTouch.TwistRadians += LeanTouch.Fingers[j].GetDeltaRadians(lastCenterOfFingers, centerOfFingers);
						LeanTouch.TwistDegrees += LeanTouch.Fingers[j].GetDeltaDegrees(lastCenterOfFingers, centerOfFingers);
					}
					for (int k = 0; k < count; k++)
					{
						LeanTouch.MultiDragDelta += LeanTouch.Fingers[k].DeltaScreenPosition;
					}
				}
				LeanTouch.TwistRadians /= (float)count;
				LeanTouch.TwistDegrees /= (float)count;
				LeanTouch.DragDelta /= (float)count;
				LeanTouch.MultiDragDelta /= (float)count;
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0002505C File Offset: 0x0002345C
		private void UpdateEvents()
		{
			for (int i = 0; i < LeanTouch.Fingers.Count; i++)
			{
				LeanFinger leanFinger = LeanTouch.Fingers[i];
				if (leanFinger.Down && LeanTouch.OnFingerDown != null)
				{
					LeanTouch.OnFingerDown(leanFinger);
				}
				if (leanFinger.Set && LeanTouch.OnFingerSet != null)
				{
					LeanTouch.OnFingerSet(leanFinger);
				}
				if (leanFinger.Up && LeanTouch.OnFingerUp != null)
				{
					LeanTouch.OnFingerUp(leanFinger);
				}
				if (leanFinger.Tap && LeanTouch.OnFingerTap != null)
				{
					LeanTouch.OnFingerTap(leanFinger);
				}
				if (leanFinger.HeldDown && LeanTouch.OnFingerHeldDown != null)
				{
					LeanTouch.OnFingerHeldDown(leanFinger);
				}
				if (leanFinger.HeldSet && LeanTouch.OnFingerHeldSet != null)
				{
					LeanTouch.OnFingerHeldSet(leanFinger);
				}
				if (leanFinger.HeldUp && LeanTouch.OnFingerHeldUp != null)
				{
					LeanTouch.OnFingerHeldUp(leanFinger);
				}
				if (leanFinger.DeltaScreenPosition != Vector2.zero && LeanTouch.OnFingerDrag != null)
				{
					LeanTouch.OnFingerDrag(leanFinger);
				}
				if (leanFinger.Up && leanFinger.GetScaledSnapshotDelta(this.TapThreshold).magnitude >= this.SwipeThreshold && LeanTouch.OnFingerSwipe != null)
				{
					LeanTouch.OnFingerSwipe(leanFinger);
				}
			}
			if (LeanTouch.DragDelta != Vector2.zero && LeanTouch.OnDrag != null)
			{
				LeanTouch.OnDrag(LeanTouch.DragDelta);
			}
			if (LeanTouch.SoloDragDelta != Vector2.zero && LeanTouch.OnSoloDrag != null)
			{
				LeanTouch.OnSoloDrag(LeanTouch.SoloDragDelta);
			}
			if (LeanTouch.MultiDragDelta != Vector2.zero && LeanTouch.OnMultiDrag != null)
			{
				LeanTouch.OnMultiDrag(LeanTouch.MultiDragDelta);
			}
			if (LeanTouch.PinchScale != 1f && LeanTouch.OnPinch != null)
			{
				LeanTouch.OnPinch(LeanTouch.PinchScale);
			}
			if (LeanTouch.TwistDegrees != 0f)
			{
				if (LeanTouch.OnTwistDegrees != null)
				{
					LeanTouch.OnTwistDegrees(LeanTouch.TwistDegrees);
				}
				if (LeanTouch.OnTwistRadians != null)
				{
					LeanTouch.OnTwistRadians(LeanTouch.TwistRadians);
				}
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000252CC File Offset: 0x000236CC
		private void AddFinger(int index, Vector2 screenPosition)
		{
			LeanFinger leanFinger = LeanTouch.Fingers.Find((LeanFinger t) => t.Index == index);
			if (leanFinger == null)
			{
				int num = LeanTouch.InactiveFingers.FindIndex((LeanFinger t) => t.Index == index);
				if (num >= 0)
				{
					leanFinger = LeanTouch.InactiveFingers[num];
					LeanTouch.InactiveFingers.RemoveAt(num);
					if (leanFinger.Age > this.TapThreshold)
					{
						leanFinger.TapCount = 0;
					}
					leanFinger.Age = 0f;
					leanFinger.LastSet = false;
					leanFinger.Set = false;
					leanFinger.LastHeldSet = false;
					leanFinger.HeldSet = false;
					leanFinger.Tap = false;
				}
				else
				{
					leanFinger = new LeanFinger();
					leanFinger.Index = index;
				}
				leanFinger.StartScreenPosition = screenPosition;
				leanFinger.LastScreenPosition = screenPosition;
				leanFinger.ScreenPosition = screenPosition;
				leanFinger.StartedOverGui = leanFinger.IsOverGui;
				leanFinger.TotalDeltaMagnitude = 0f;
				LeanTouch.Fingers.Add(leanFinger);
			}
			leanFinger.Set = true;
			leanFinger.ScreenPosition = screenPosition;
			if (this.RecordFingers)
			{
				if (this.RecordLimit > 0f && leanFinger.SnapshotDuration > this.RecordLimit)
				{
					int lowerSnapshotIndex = leanFinger.GetLowerSnapshotIndex(leanFinger.Age - this.RecordLimit);
					leanFinger.ClearSnapshots(lowerSnapshotIndex);
				}
				if (this.RecordThreshold > 0f)
				{
					if (leanFinger.Snapshots.Count == 0 || leanFinger.LastSnapshotDelta.magnitude >= this.RecordThreshold)
					{
						leanFinger.RecordSnapshot();
					}
				}
				else
				{
					leanFinger.RecordSnapshot();
				}
			}
		}

		// Token: 0x04000512 RID: 1298
		public static LeanTouch Instance;

		// Token: 0x04000513 RID: 1299
		public static List<LeanFinger> Fingers = new List<LeanFinger>();

		// Token: 0x04000514 RID: 1300
		public static List<LeanFinger> InactiveFingers = new List<LeanFinger>();

		// Token: 0x04000515 RID: 1301
		public static Vector2 DragDelta;

		// Token: 0x04000516 RID: 1302
		public static Vector2 SoloDragDelta;

		// Token: 0x04000517 RID: 1303
		public static Vector2 MultiDragDelta;

		// Token: 0x04000518 RID: 1304
		public static float TwistDegrees;

		// Token: 0x04000519 RID: 1305
		public static float TwistRadians;

		// Token: 0x0400051A RID: 1306
		public static float PinchScale = 1f;

		// Token: 0x0400051B RID: 1307
		public static Action<LeanFinger> OnFingerDown;

		// Token: 0x0400051C RID: 1308
		public static Action<LeanFinger> OnFingerSet;

		// Token: 0x0400051D RID: 1309
		public static Action<LeanFinger> OnFingerUp;

		// Token: 0x0400051E RID: 1310
		public static Action<LeanFinger> OnFingerDrag;

		// Token: 0x0400051F RID: 1311
		public static Action<LeanFinger> OnFingerTap;

		// Token: 0x04000520 RID: 1312
		public static Action<LeanFinger> OnFingerSwipe;

		// Token: 0x04000521 RID: 1313
		public static Action<LeanFinger> OnFingerHeldDown;

		// Token: 0x04000522 RID: 1314
		public static Action<LeanFinger> OnFingerHeldSet;

		// Token: 0x04000523 RID: 1315
		public static Action<LeanFinger> OnFingerHeldUp;

		// Token: 0x04000524 RID: 1316
		public static Action<int> OnMultiTap;

		// Token: 0x04000525 RID: 1317
		public static Action<Vector2> OnDrag;

		// Token: 0x04000526 RID: 1318
		public static Action<Vector2> OnSoloDrag;

		// Token: 0x04000527 RID: 1319
		public static Action<Vector2> OnMultiDrag;

		// Token: 0x04000528 RID: 1320
		public static Action<float> OnPinch;

		// Token: 0x04000529 RID: 1321
		public static Action<float> OnTwistDegrees;

		// Token: 0x0400052A RID: 1322
		public static Action<float> OnTwistRadians;

		// Token: 0x0400052B RID: 1323
		public float TapThreshold = 0.5f;

		// Token: 0x0400052C RID: 1324
		public float SwipeThreshold = 50f;

		// Token: 0x0400052D RID: 1325
		public float HeldThreshold = 1f;

		// Token: 0x0400052E RID: 1326
		public int ReferenceDpi = 200;

		// Token: 0x0400052F RID: 1327
		public bool RecordFingers = true;

		// Token: 0x04000530 RID: 1328
		public float RecordThreshold = 5f;

		// Token: 0x04000531 RID: 1329
		public float RecordLimit;

		// Token: 0x04000532 RID: 1330
		public bool SimulateMultiFingers = true;

		// Token: 0x04000533 RID: 1331
		public KeyCode PinchTwistKey = KeyCode.LeftControl;

		// Token: 0x04000534 RID: 1332
		public KeyCode MultiDragKey = KeyCode.LeftAlt;

		// Token: 0x04000535 RID: 1333
		public Texture2D FingerTexture;

		// Token: 0x04000536 RID: 1334
		private static int highestMouseButton = 7;

		// Token: 0x04000537 RID: 1335
		private int lastFingerCount;

		// Token: 0x04000538 RID: 1336
		private float multiFingerTime;

		// Token: 0x04000539 RID: 1337
		private int multiFingerCount;
	}
}
