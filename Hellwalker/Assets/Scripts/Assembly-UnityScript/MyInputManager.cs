using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
[Serializable]
public class MyInputManager : MonoBehaviour
{
	// Token: 0x0600032B RID: 811 RVA: 0x0001D13C File Offset: 0x0001B33C
	public virtual void Start()
	{
		this.lastaxisstate = new bool[10];
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0001D14C File Offset: 0x0001B34C
	public virtual void Update()
	{
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0001D150 File Offset: 0x0001B350
	public virtual void OnGUI()
	{
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0001D154 File Offset: 0x0001B354
	public virtual bool GetKeyInput(string action, int keystate)
	{
		int num = this.FindIndex(action);
        bool result;
		if (num != -1)
		{
			if (keystate == 0)
			{
				if (this.keycodes[num] == KeyCode.LeftShift && (Input.GetKey(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)))
				{
					return true;
				}
				if (Input.GetKey(this.keycodes[num]))
				{
					return true;
				}
				if (this.GetJoystickInput(action, 0) == (float)1)
				{
					return true;
				}
			}
			if (keystate == 1)
			{
				if (this.keycodes[num] == KeyCode.LeftShift && (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)))
				{
					return true;
				}
				if (Input.GetKeyDown(this.keycodes[num]))
				{
					return true;
				}
				if (this.GetJoystickInput(action, 1) == (float)1)
				{
					return true;
				}
			}
			if (keystate == 2)
			{
				if (this.keycodes[num] == KeyCode.LeftShift && (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)))
				{
					return true;
				}
				if (Input.GetKeyUp(this.keycodes[num]))
				{
					return true;
				}
				if (this.GetJoystickInput(action, 2) == (float)1)
				{
					return true;
				}
			}
			result = false;
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0001D2B4 File Offset: 0x0001B4B4
	public virtual int FindIndex(string name)
	{
		int i = 0;
		int result = -1;
		while (i < this.actionnames.Length)
		{
			if (this.actionnames[i] == name)
			{
				result = i;
			}
			i++;
		}
		return result;
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0001D2F8 File Offset: 0x0001B4F8
	public virtual bool CheckJoystickAxis(int i, float input)
	{
		if (Mathf.Abs(input) > (float)0)
		{
			float num = this.joystickaxisthresholds[i];
			if (num < (float)0 && input <= num)
			{
				return true;
			}
			if (num > (float)0 && input >= num)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0001D348 File Offset: 0x0001B548
	public virtual float HorribleAxisStuff(string axisname, int i, int lastaxisarrayindex, int keystate)
	{
		bool flag = this.CheckJoystickAxis(i, Input.GetAxis(axisname));
		float result;
		if (keystate == 0 && flag)
		{
			result = (float)1;
		}
		else if (keystate == 1 && !this.lastaxisstate[lastaxisarrayindex] && flag)
		{
			this.lastaxisstate[lastaxisarrayindex] = true;
			result = (float)1;
		}
		else if (keystate == 2 && this.lastaxisstate[lastaxisarrayindex] && !flag)
		{
			this.lastaxisstate[lastaxisarrayindex] = false;
			result = (float)1;
		}
		else
		{
			result = (float)0;
		}
		return result;
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0001D400 File Offset: 0x0001B600
	public virtual float GetJoystickInput(string name, int keystate)
	{
		int num = this.FindIndex(name);
		float result;
		if (this.joystickinputassignments[num] == 1)
		{
			result = this.HorribleAxisStuff("joystickaxis1", num, 0, keystate);
		}
		else if (this.joystickinputassignments[num] == 2)
		{
			result = this.HorribleAxisStuff("joystickaxis2", num, 1, keystate);
		}
		else if (this.joystickinputassignments[num] == 3)
		{
			result = this.HorribleAxisStuff("joystickaxis3", num, 2, keystate);
		}
		else if (this.joystickinputassignments[num] == 4)
		{
			result = this.HorribleAxisStuff("joystickaxis4", num, 3, keystate);
		}
		else if (this.joystickinputassignments[num] == 5)
		{
			result = this.HorribleAxisStuff("joystickaxis5", num, 4, keystate);
		}
		else if (this.joystickinputassignments[num] == 6)
		{
			result = this.HorribleAxisStuff("joystickaxis6", num, 5, keystate);
		}
		else if (this.joystickinputassignments[num] == 7)
		{
			result = this.HorribleAxisStuff("joystickaxis7", num, 6, keystate);
		}
		else if (this.joystickinputassignments[num] == 8)
		{
			result = this.HorribleAxisStuff("joystickaxis8", num, 7, keystate);
		}
		else
		{
			if (this.joystickinputassignments[num] == 9)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton0"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton0"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton0"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 10)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton1"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton1"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton1"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 11)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton2"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton2"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton2"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 12)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton3"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton3"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton3"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 13)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton4"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton4"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton4"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 14)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton5"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton5"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton5"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 15)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton6"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton6"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton6"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 16)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton7"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton7"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton7"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 18)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton8"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton8"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton8"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 19)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton9"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton9"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton9"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 20)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton10"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton10"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton10"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 21)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton11"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton11"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton11"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 22)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton12"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton12"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton12"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 23)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton13"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton13"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton13"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 24)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton14"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton14"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton14"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 25)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton15"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton15"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton15"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 26)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton16"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton16"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton16"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 27)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton17"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton17"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton17"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 28)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton18"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton18"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton18"))
				{
					return (float)1;
				}
			}
			if (this.joystickinputassignments[num] == 29)
			{
				if (keystate == 0 && Input.GetButton("joystickbutton19"))
				{
					return (float)1;
				}
				if (keystate == 1 && Input.GetButtonDown("joystickbutton19"))
				{
					return (float)1;
				}
				if (keystate == 2 && Input.GetButtonUp("joystickbutton19"))
				{
					return (float)1;
				}
			}
			result = ((this.joystickinputassignments[num] != 30) ? ((this.joystickinputassignments[num] != 31) ? ((float)0) : this.HorribleAxisStuff("joystickaxis10", num, 9, keystate)) : this.HorribleAxisStuff("joystickaxis9", num, 8, keystate));
		}
		return result;
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0001DD48 File Offset: 0x0001BF48
	public virtual void Main()
	{
	}

	// Token: 0x040003C0 RID: 960
	public string[] actionnames;

	// Token: 0x040003C1 RID: 961
	public KeyCode[] keycodes;

	// Token: 0x040003C2 RID: 962
	public int[] joystickinputassignments;

	// Token: 0x040003C3 RID: 963
	public float[] joystickaxisthresholds;

	// Token: 0x040003C4 RID: 964
	[HideInInspector]
	public bool[] lastaxisstate;
}
