using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(GoForwardUnitTest());
    }

    public IEnumerator GoForwardUnitTest()
    {
        PlayerFactory playerFactory = new PlayerFactory();
        PlayerView playerView = (PlayerView)playerFactory.Create();

        float seconds = 1;

        playerView.ControllerOverride(InputControllerOverride.FORWARD);

        while (seconds > 0)
        {
            seconds -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        playerView.ControllerOverride(InputControllerOverride.NONE);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
