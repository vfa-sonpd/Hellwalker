using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory :  ObjectFactory {
    public override ObjectView Create(Context context)
    {
        //Load character
        PlayerView playerView = InstantiateView<PlayerView>("Prefabs/Characters/Player");
        //Load context
        playerView.OnCreate(context);

        return playerView;
    }

    public override ObjectView Create()
    {
        //Load character
        PlayerView playerView = InstantiateView<PlayerView>("Prefabs/Characters/Player");

        return playerView;
    }
}
