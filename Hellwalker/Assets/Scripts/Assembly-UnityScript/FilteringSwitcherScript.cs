using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x02000047 RID: 71
[Serializable]
public class FilteringSwitcherScript : MonoBehaviour
{
	// Token: 0x0600019C RID: 412 RVA: 0x000102B8 File Offset: 0x0000E4B8
	public virtual void Start()
	{
	}

	// Token: 0x0600019D RID: 413 RVA: 0x000102BC File Offset: 0x0000E4BC
	public virtual void switching()
	{
		if (this.menubuttons == null)
		{
			this.menubuttons = (GameMenuButtonsScript)GameObject.Find("DasMenu").GetComponent(typeof(GameMenuButtonsScript));
		}
		Texture2D[] array = (Texture2D[])Resources.FindObjectsOfTypeAll(typeof(Texture2D));
		for (int i = 0; i < Extensions.get_length(array); i++)
		{
			if (this.menubuttons.LoadConfigInt("bfilter") == 0)
			{
				array[i].filterMode = FilterMode.Point;
			}
			else
			{
				array[i].filterMode = FilterMode.Bilinear;
			}
			if (array[i].name.Contains("Lightmap"))
			{
				array[i].filterMode = FilterMode.Bilinear;
			}
		}
		for (int i = 0; i < Extensions.get_length(this.exceptions); i++)
		{
			this.exceptions[i].filterMode = FilterMode.Bilinear;
		}
		Font[] array2 = (Font[])Resources.FindObjectsOfTypeAll(typeof(Font));
		int j = 0;
		Font[] array3 = array2;
		int length = array3.Length;
		while (j < length)
		{
			array3[j].material.mainTexture.filterMode = FilterMode.Bilinear;
			j++;
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x000103F0 File Offset: 0x0000E5F0
	public virtual Texture2D[] texarray(object[] obs)
	{
		Texture2D[] array = null;
		for (int i = 0; i < obs.Length; i++)
		{
			Texture2D[] array2 = array;
			int num = i;
			object obj2;
			object obj = obj2 = obs[i];
			if (!(obj is Texture2D))
			{
				obj2 = RuntimeServices.Coerce(obj, typeof(Texture2D));
			}
			array2[num] = (Texture2D)obj2;
		}
		return array;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00010440 File Offset: 0x0000E640
	public virtual void Main()
	{
	}

	// Token: 0x04000266 RID: 614
	public Texture2D[] exceptions;

	// Token: 0x04000267 RID: 615
	[HideInInspector]
	public GameMenuButtonsScript menubuttons;
}
