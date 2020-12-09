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
            var asyncLoadLevel = SceneManager.LoadSceneAsync("GreyboxScene", LoadSceneMode.Single);

            while (!asyncLoadLevel.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            PlayerFactory<PlayerView> playerFactory = new PlayerFactory<PlayerView>();
            PlayerView playerView = (PlayerView)playerFactory.Create(new Context(Vector3.zero));

            playerView.ControllerOverride(InputControllerOverride.FORWARD);

            yield return new WaitForSeconds(1);

            playerView.ControllerOverride(InputControllerOverride.NONE);

            yield return new WaitForSeconds(3000);
        }
        static bool[] CanRagdoll = new bool[] { true,false };
        [UnityTest]
        public IEnumerator SoldierPoolingTest([ValueSource("CanRagdoll")] bool value)
        {
            Debug.Log("value value " + value);
            var asyncLoadLevel = SceneManager.LoadSceneAsync("GreyboxScene", LoadSceneMode.Single);

            while (!asyncLoadLevel.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            // Setup factories...
            SoldierFactory<SoldierView> factory = new SoldierFactory<SoldierView>();
            List<SoldierView> list = new List<SoldierView>();


            for (int j = 0; j < 5; j++)
            {
                float xPos = 3;

                // Start spawning soldiers...
                for (int i = 0; i < 5; i++)
                {
                    list.Add(factory.Create(new Context(new Vector3(xPos, 1, 5))) as SoldierView);
                    xPos -= 1;
                }

                yield return new WaitForSeconds(0.5f);

                // Start forcing them to suicide...
                foreach (SoldierView soldier in list)
                {
                    soldier.Suicide(value);
                    yield return new WaitForSeconds(0.5f);
                }

                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(3000);
        }

        [UnityTest]
        public IEnumerator MassSpawnTest()
        {
            var asyncLoadLevel = SceneManager.LoadSceneAsync("GreyboxScene", LoadSceneMode.Single);

            while (!asyncLoadLevel.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            // Setup factories...
            SoldierFactory<SoldierView> factory = new SoldierFactory<SoldierView>();

            for (int j = 0; j < 7; j++)
            {
                float xPos = 3;

                // Start spawning soldiers...
                for (int i = 0; i < 5; i++)
                {
                    factory.Create(new Context(new Vector3(xPos, 1, 5)));
                    xPos -= 1;

                    yield return new WaitForSeconds(0.5f);
                }

            }

            yield return new WaitForSeconds(3000);
        }
    }
}
