using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
[Serializable]
public class LightTriggerMASTERSCRIPT : MonoBehaviour
{
	// Token: 0x0600028A RID: 650 RVA: 0x00018BB0 File Offset: 0x00016DB0
	public virtual void Start()
	{
		RenderSettings.ambientLight = Color.black;
		this.makelightarray();
	}

	// Token: 0x0600028B RID: 651 RVA: 0x00018BC4 File Offset: 0x00016DC4
	public virtual void Update()
	{
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00018BC8 File Offset: 0x00016DC8
	public virtual void makelightarray()
	{
		Light[] array = ((Light[])UnityEngine.Object.FindObjectsOfType(typeof(Light))) as Light[];
		LightTriggerNODE[] array2 = ((LightTriggerNODE[])UnityEngine.Object.FindObjectsOfType(typeof(LightTriggerNODE))) as LightTriggerNODE[];
		this.triggerlocation = new Vector3[array.Length + array2.Length];
		this.triggercolor = new Color[array.Length + array2.Length];
		this.triggerrange = new float[array.Length + array2.Length];
		this.triggersun = new bool[array.Length + array2.Length];
		this.overridesun = new bool[array.Length + array2.Length];
		int num = 0;
		int i = 0;
		Light[] array3 = array;
		int length = array3.Length;
		while (i < length)
		{
			if (array3[i].alreadyLightmapped && array3[i].gameObject.tag != "IgnoreThisInTriggerLighting")
			{
				this.triggerlocation[num] = array3[i].gameObject.transform.position;
				this.triggercolor[num] = array3[i].color;
				this.triggerrange[num] = array3[i].range;
				if (array3[i].gameObject.tag == "IgnoreSunLightTag")
				{
					this.overridesun[num] = true;
				}
			}
			num++;
			i++;
		}
		int j = 0;
		LightTriggerNODE[] array4 = array2;
		int length2 = array4.Length;
		while (j < length2)
		{
			this.triggerlocation[num] = array4[j].gameObject.transform.position;
			this.triggercolor[num] = array4[j].mycolor;
			this.triggerrange[num] = array4[j].myrange;
			if (array4[j].sun)
			{
				this.triggersun[num] = true;
			}
			if (array4[j].overridesun)
			{
				this.overridesun[num] = true;
			}
			num++;
			j++;
		}
	}

	// Token: 0x0600028D RID: 653 RVA: 0x00018E14 File Offset: 0x00017014
	public virtual void Main()
	{
	}

	// Token: 0x04000302 RID: 770
	[HideInInspector]
	public Vector3[] triggerlocation;

	// Token: 0x04000303 RID: 771
	[HideInInspector]
	public Color[] triggercolor;

	// Token: 0x04000304 RID: 772
	[HideInInspector]
	public float[] triggerrange;

	// Token: 0x04000305 RID: 773
	[HideInInspector]
	public bool[] triggersun;

	// Token: 0x04000306 RID: 774
	[HideInInspector]
	public bool[] overridesun;

	// Token: 0x04000307 RID: 775
	public LayerMask blockinglayers;

	// Token: 0x04000308 RID: 776
	public bool useambient;

	// Token: 0x04000309 RID: 777
	public Color ambientcolor;
}
