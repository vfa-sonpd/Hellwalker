using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SoldierPoolingTest());
    }

    public IEnumerator SoldierPoolingTest()
    {
        // Setup factories...
        SoldierFactory<SoldierView> factory = new SoldierFactory<SoldierView>();
        List<SoldierView> list = new List<SoldierView>();


        for(int j = 0; j < 5; j++)
        {
            float xPos = -83f;

            // Start spawning soldiers...
            for (int i = 0; i < 5; i++)
            {
                list.Add(factory.Create(new Context(new Vector3(xPos, -175f, 396))) as SoldierView);
                xPos -= 2;
            }

            yield return new WaitForSeconds(0.5f);

            // Start forcing them to suicide...
            foreach (SoldierView soldier in list)
            {
                soldier.Suicide();
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(0.5f);
        }




        yield return new WaitForSeconds(3000);
    }
}
