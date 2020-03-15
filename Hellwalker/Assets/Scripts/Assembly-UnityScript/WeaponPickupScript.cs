using com.ootii.Messages;
using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
[Serializable]
public class WeaponPickupScript : MonoBehaviour
{
	// Token: 0x060004B5 RID: 1205 RVA: 0x0002AD8C File Offset: 0x00028F8C
	public virtual void Start()
	{
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0002AD90 File Offset: 0x00028F90
	public virtual void Update()
	{
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x0002AD94 File Offset: 0x00028F94
	public virtual void Main()
	{
	}

	// Token: 0x040005A4 RID: 1444
	public bool giveweapon;

	// Token: 0x040005A5 RID: 1445
	public int weaponcontent;

	// Token: 0x040005A6 RID: 1446
	public int ammocontent;

	// Token: 0x040005A7 RID: 1447
	public string message;

    public WeaponPickupData weaponPickupData;

    // Token: 0x04000472 RID: 1138
    public LayerMask pickupblockinglayers;

    public virtual void OnTriggerStay(Collider hit)
    {
        //     if (!Physics.Raycast(this.transform.position, (hit.transform.position - this.transform.position).normalized, Vector3.Distance(this.transform.position, hit.transform.position), this.pickupblockinglayers))
        //     {
        //GameObject gameObject = hit.transform.gameObject;
        ////if (gameObject.tag == "Weapon Pickup")
        ////{
        ////	this.pickupweapon(gameObject);
        ////}
        //     }

        // Try to raycast to see if this is not blocked by wall
        if (Physics.Raycast(this.transform.position, (hit.transform.position - this.transform.position).normalized, Vector3.Distance(this.transform.position, hit.transform.position)))
        {
            // Send a pick up weapon event
            MessageDispatcher.SendMessageData(GameEvent.pickUpWeapon,new WeaponPickupData(weaponcontent, ammocontent, message));

            // Destroy afterwards
            Destroy(gameObject);
        }
    }

}
