using System;
using UnityEngine;

// Token: 0x0200008A RID: 138
[Serializable]
public class PlayMenuShotgunSound : MonoBehaviour
{
	// Token: 0x06000369 RID: 873 RVA: 0x000200D8 File Offset: 0x0001E2D8
	public virtual void Start()
	{
		GameObject gameObject = GameObject.Find("PERSIST");
		if (gameObject && ((PersistScript)gameObject.GetComponent(typeof(PersistScript))).domenusound)
		{
			((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
			((PersistScript)gameObject.GetComponent(typeof(PersistScript))).domenusound = false;
		}
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00020150 File Offset: 0x0001E350
	public virtual void Update()
	{
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00020154 File Offset: 0x0001E354
	public virtual void Main()
	{
	}
}
