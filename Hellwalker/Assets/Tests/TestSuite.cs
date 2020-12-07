using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayerGoForward()
        {
            var asyncLoadLevel = SceneManager.LoadSceneAsync("RefractorScene", LoadSceneMode.Single);

            while (!asyncLoadLevel.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            PlayerFactory playerFactory = new PlayerFactory();
            PlayerView playerView = (PlayerView)playerFactory.Create();

            playerView.ControllerOverride(InputControllerOverride.FORWARD);

            yield return new WaitForSeconds(1);

            playerView.ControllerOverride(InputControllerOverride.NONE);

            yield return new WaitForSeconds(3000);
        }
    }
}
