using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory<T> :  ObjectFactory<T>
{
    public override ObjectView Create(Context context)
    {
        //Load character
        PlayerView playerView = GameObject.FindObjectOfType<PlayerView>();
        if (!playerView)
        {
            playerView = InstantiateView<PlayerView>("Prefabs/Characters/Player", true);
        }
        //Load context
        playerView.OnCreate(context);

        return playerView;
    }

    public override ObjectView Create()
    {
        //Load character
        PlayerView playerView = GameObject.FindObjectOfType<PlayerView>();
        if(!playerView)
        {
            playerView = InstantiateView<PlayerView>("Prefabs/Characters/Player", true);
        }

        return playerView;
    }
}
