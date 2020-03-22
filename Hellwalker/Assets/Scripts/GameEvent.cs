
public class GameEvent {

    public static string pickUpWeapon = "game:pick_up_weapon";

}

public class WeaponPickupData
{
    // Token: 0x040005A5 RID: 1445
    public int weaponcontent;

    // Token: 0x040005A6 RID: 1446
    public int ammocontent;

    // Token: 0x040005A7 RID: 1447
    public string message;

    public WeaponPickupData(int weaponcontent, int ammocontent,string message )
    {
        this.weaponcontent = weaponcontent;
        this.ammocontent = ammocontent;
        this.message = message;
    }
}
