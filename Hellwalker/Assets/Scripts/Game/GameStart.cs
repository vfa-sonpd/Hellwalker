using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameStart : MonoBehaviour {

    PlayerView playerView;

    // Use this for initialization
    void Start () {
        CreatePlayer();
        StartCoroutine(GoForwardUnitTest());
    }

    public IEnumerator GoForwardUnitTest()
    {
        playerView.ControllerOverride(InputControllerOverride.FORWARD);

        yield return new WaitForSeconds(1);

        playerView.ControllerOverride(InputControllerOverride.NONE);
    }

    void CreatePlayer()
    {
        PlayerFactory<PlayerView> playerFactory = new PlayerFactory<PlayerView>();
        PlayerView playerView = (PlayerView)playerFactory.Create();
    }
}
