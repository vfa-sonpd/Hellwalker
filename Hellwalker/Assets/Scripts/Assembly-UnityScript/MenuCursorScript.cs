using System;
using UnityEngine;

// Token: 0x02000079 RID: 121
[Serializable]
public class MenuCursorScript : MonoBehaviour
{
	// Token: 0x060002F9 RID: 761 RVA: 0x0001AC1C File Offset: 0x00018E1C
	public MenuCursorScript()
	{
		this.cursorSizeX = 16;
		this.cursorSizeY = 16;
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0001AC34 File Offset: 0x00018E34
	public virtual void Start()
	{
		Cursor.visible = false;
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0001AC3C File Offset: 0x00018E3C
	public virtual void OnGUI()
	{
		float num = (float)Screen.width / 1920f;
		float num2 = (float)this.cursorSizeX * num;
		float num3 = (float)this.cursorSizeY * num;
		GUI.DrawTexture(new Rect(Event.current.mousePosition.x - num2 / (float)2, Event.current.mousePosition.y - num3 / (float)2, num2, num3), this.yourCursor);
	}

	// Token: 0x060002FC RID: 764 RVA: 0x0001ACAC File Offset: 0x00018EAC
	public virtual void Main()
	{
	}

	// Token: 0x0400035B RID: 859
	public Texture2D yourCursor;

	// Token: 0x0400035C RID: 860
	public int cursorSizeX;

	// Token: 0x0400035D RID: 861
	public int cursorSizeY;
}
