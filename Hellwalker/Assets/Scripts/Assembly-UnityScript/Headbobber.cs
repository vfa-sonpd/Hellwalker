using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
[Serializable]
public class Headbobber : MonoBehaviour
{
	// Token: 0x0600022F RID: 559 RVA: 0x00015328 File Offset: 0x00013528
	public Headbobber()
	{
		this.bobbingSpeed = 0.18f;
		this.bobbingAmount = 0.2f;
	}

	// Token: 0x06000230 RID: 560 RVA: 0x00015348 File Offset: 0x00013548
	public virtual void Start()
	{
		this.inputmanager = Essential.Instance.inputManager;
        this.cont = (MyControllerScript)GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
	}

	// Token: 0x06000231 RID: 561 RVA: 0x000153A0 File Offset: 0x000135A0
	public virtual void Update()
	{
		float num = (float)0;
		if (this.cont.SecondCheckGrounded())
		{
			if (this.inputmanager.GetKeyInput("right", 0) || this.inputmanager.GetKeyInput("left", 0) || this.inputmanager.GetKeyInput("forward", 0) || this.inputmanager.GetKeyInput("backward", 0))
			{
				this.horizontal = (float)1;
				this.vertical = (float)1;
			}
			else
			{
				this.horizontal -= Time.deltaTime * (float)5;
				this.vertical -= Time.deltaTime * (float)5;
			}
		}
		else
		{
			this.horizontal = (float)0;
			this.vertical = (float)0;
		}
		if (this.inputmanager.GetKeyInput("zoom", 0))
		{
			this.horizontal = (float)0;
			this.vertical = (float)0;
		}
		if (this.horizontal < (float)0)
		{
			this.horizontal = (float)0;
		}
		if (this.vertical < (float)0)
		{
			this.vertical = (float)0;
		}
		if (Mathf.Abs(this.horizontal) == (float)0 && Mathf.Abs(this.vertical) == (float)0)
		{
			this.timer = (float)0;
		}
		else
		{
			num = Mathf.Sin(this.timer);
			this.timer += this.bobbingSpeed * (Time.deltaTime * (float)60);
			if (this.timer > 6.2831855f)
			{
				this.timer -= 6.2831855f;
			}
		}
		if (num != (float)0)
		{
			float num2 = num * this.bobbingAmount;
			float num3 = Mathf.Abs(this.horizontal) + Mathf.Abs(this.vertical);
			num3 = Mathf.Clamp(num3, (float)0, 1f);
			num2 = num3 * num2;
			if (this.doz)
			{
				float z = this.midpoint + num2;
				Vector3 localPosition = this.transform.localPosition;
				float num4 = localPosition.z = z;
				Vector3 vector = this.transform.localPosition = localPosition;
			}
			if (this.doy)
			{
				float y = this.midpoint + num2;
				Vector3 localPosition2 = this.transform.localPosition;
				float num5 = localPosition2.y = y;
				Vector3 vector2 = this.transform.localPosition = localPosition2;
			}
			if (this.dox)
			{
				float x = this.midpoint + num2;
				Vector3 localPosition3 = this.transform.localPosition;
				float num6 = localPosition3.x = x;
				Vector3 vector3 = this.transform.localPosition = localPosition3;
			}
		}
		else
		{
			if (this.doz)
			{
				float z2 = this.midpoint;
				Vector3 localPosition4 = this.transform.localPosition;
				float num7 = localPosition4.z = z2;
				Vector3 vector4 = this.transform.localPosition = localPosition4;
			}
			if (this.dox)
			{
				float x2 = this.midpoint;
				Vector3 localPosition5 = this.transform.localPosition;
				float num8 = localPosition5.x = x2;
				Vector3 vector5 = this.transform.localPosition = localPosition5;
			}
			if (this.doy)
			{
				float y2 = this.midpoint;
				Vector3 localPosition6 = this.transform.localPosition;
				float num9 = localPosition6.y = y2;
				Vector3 vector6 = this.transform.localPosition = localPosition6;
			}
		}
	}

	// Token: 0x06000232 RID: 562 RVA: 0x00015728 File Offset: 0x00013928
	public virtual void Main()
	{
	}

	// Token: 0x0400029A RID: 666
	private float timer;

	// Token: 0x0400029B RID: 667
	public float bobbingSpeed;

	// Token: 0x0400029C RID: 668
	public float bobbingAmount;

	// Token: 0x0400029D RID: 669
	public float midpoint;

	// Token: 0x0400029E RID: 670
	public bool dox;

	// Token: 0x0400029F RID: 671
	public bool doy;

	// Token: 0x040002A0 RID: 672
	public bool doz;

	// Token: 0x040002A1 RID: 673
	[HideInInspector]
	public MyControllerScript cont;

	// Token: 0x040002A2 RID: 674
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x040002A3 RID: 675
	[HideInInspector]
	public float horizontal;

	// Token: 0x040002A4 RID: 676
	[HideInInspector]
	public float vertical;
}
