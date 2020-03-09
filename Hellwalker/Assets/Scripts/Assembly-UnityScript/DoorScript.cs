using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
[Serializable]
public class DoorScript : MonoBehaviour
{
	// Token: 0x0600013F RID: 319 RVA: 0x0000E094 File Offset: 0x0000C294
	public DoorScript()
	{
		this.soundelay = (float)1;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
	public virtual void Start()
	{
		this.disbebuttonscript = (ButtonScript)this.mybutton.GetComponent(typeof(ButtonScript));
		this.laststate = this.disbebuttonscript.buttonstate;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
	public virtual void Update()
	{
		this.soundelay -= Time.deltaTime;
		if (this.soundelay < (float)0)
		{
			this.soundelay = (float)0;
		}
		if (this.disbebuttonscript.buttonstate != this.laststate)
		{
			if (this.soundelay <= (float)0)
			{
				((AudioSource)this.mysound.GetComponent(typeof(AudioSource))).Play();
			}
			this.laststate = this.disbebuttonscript.buttonstate;
		}
		if (this.disbebuttonscript.buttonstate)
		{
			this.movecoors = this.opencoordinates;
		}
		else
		{
			this.movecoors = this.closedcoordinates;
		}
		if (this.transform.position.y < this.movecoors.y)
		{
			float y = this.transform.position.y + Time.deltaTime * this.myspeed;
			Vector3 position = this.transform.position;
			float num = position.y = y;
			Vector3 vector = this.transform.position = position;
			if (this.transform.position.y > this.movecoors.y)
			{
				float y2 = this.movecoors.y;
				Vector3 position2 = this.transform.position;
				float num2 = position2.y = y2;
				Vector3 vector2 = this.transform.position = position2;
			}
		}
		if (this.transform.position.y > this.movecoors.y)
		{
			float y3 = this.transform.position.y - Time.deltaTime * this.myspeed;
			Vector3 position3 = this.transform.position;
			float num3 = position3.y = y3;
			Vector3 vector3 = this.transform.position = position3;
			if (this.transform.position.y < this.movecoors.y)
			{
				float y4 = this.movecoors.y;
				Vector3 position4 = this.transform.position;
				float num4 = position4.y = y4;
				Vector3 vector4 = this.transform.position = position4;
			}
		}
		if (this.transform.position.x < this.movecoors.x)
		{
			float x = this.transform.position.x + Time.deltaTime * this.myspeed;
			Vector3 position5 = this.transform.position;
			float num5 = position5.x = x;
			Vector3 vector5 = this.transform.position = position5;
			if (this.transform.position.x > this.movecoors.x)
			{
				float x2 = this.movecoors.x;
				Vector3 position6 = this.transform.position;
				float num6 = position6.x = x2;
				Vector3 vector6 = this.transform.position = position6;
			}
		}
		if (this.transform.position.x > this.movecoors.x)
		{
			float x3 = this.transform.position.x - Time.deltaTime * this.myspeed;
			Vector3 position7 = this.transform.position;
			float num7 = position7.x = x3;
			Vector3 vector7 = this.transform.position = position7;
			if (this.transform.position.x < this.movecoors.x)
			{
				float x4 = this.movecoors.x;
				Vector3 position8 = this.transform.position;
				float num8 = position8.x = x4;
				Vector3 vector8 = this.transform.position = position8;
			}
		}
		if (this.transform.position.z < this.movecoors.z)
		{
			float z = this.transform.position.z + Time.deltaTime * this.myspeed;
			Vector3 position9 = this.transform.position;
			float num9 = position9.z = z;
			Vector3 vector9 = this.transform.position = position9;
			if (this.transform.position.z > this.movecoors.z)
			{
				float z2 = this.movecoors.z;
				Vector3 position10 = this.transform.position;
				float num10 = position10.z = z2;
				Vector3 vector10 = this.transform.position = position10;
			}
		}
		if (this.transform.position.z > this.movecoors.z)
		{
			float z3 = this.transform.position.z - Time.deltaTime * this.myspeed;
			Vector3 position11 = this.transform.position;
			float num11 = position11.z = z3;
			Vector3 vector11 = this.transform.position = position11;
			if (this.transform.position.z < this.movecoors.z)
			{
				float z4 = this.movecoors.z;
				Vector3 position12 = this.transform.position;
				float num12 = position12.z = z4;
				Vector3 vector12 = this.transform.position = position12;
			}
		}
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
	public virtual void Main()
	{
	}

	// Token: 0x04000211 RID: 529
	public Vector3 closedcoordinates;

	// Token: 0x04000212 RID: 530
	public Vector3 opencoordinates;

	// Token: 0x04000213 RID: 531
	public float myspeed;

	// Token: 0x04000214 RID: 532
	public GameObject mybutton;

	// Token: 0x04000215 RID: 533
	public GameObject mysound;

	// Token: 0x04000216 RID: 534
	[HideInInspector]
	public Vector3 movecoors;

	// Token: 0x04000217 RID: 535
	[HideInInspector]
	public bool laststate;

	// Token: 0x04000218 RID: 536
	[HideInInspector]
	public float soundelay;

	// Token: 0x04000219 RID: 537
	[HideInInspector]
	public ButtonScript disbebuttonscript;
}
