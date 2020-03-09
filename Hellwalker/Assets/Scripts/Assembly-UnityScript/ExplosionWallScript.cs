using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
[Serializable]
public class ExplosionWallScript : MonoBehaviour
{
	// Token: 0x06000180 RID: 384 RVA: 0x0000FD38 File Offset: 0x0000DF38
	public virtual void Start()
	{
		this.sav = (SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript));
		this.origcoors = this.transform.position;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000FD7C File Offset: 0x0000DF7C
	public virtual void Update()
	{
		this.dosavestuff();
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0000FD84 File Offset: 0x0000DF84
	public virtual void die()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mygibs, this.transform.position, Quaternion.Euler((float)-90, (float)0, (float)0));
		UnityEngine.Object.Destroy(this.transform.gameObject);
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
	public virtual void dosavestuff()
	{
		string rhs = null;
		if (this.sav.dosave || this.sav.doload)
		{
			rhs = "?tag=" + this.transform.gameObject.name + this.origcoors.ToString();
		}
		if (this.sav.dosave)
		{
			ES2.Save<Transform>(this.transform, this.sav.filename + rhs + "tr4n5orm");
		}
		if (this.sav.doload)
		{
			if (ES2.Exists(this.sav.filename + rhs + "tr4n5orm"))
			{
				ES2.Load<Transform>(this.sav.filename + rhs + "tr4n5orm", this.transform);
			}
			else
			{
				UnityEngine.Object.Destroy(this.transform.gameObject);
			}
		}
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
	public virtual void Main()
	{
	}

	// Token: 0x04000256 RID: 598
	public GameObject mygibs;

	// Token: 0x04000257 RID: 599
	[HideInInspector]
	public SaveManagerScript sav;

	// Token: 0x04000258 RID: 600
	[HideInInspector]
	public Vector3 origcoors;
}
