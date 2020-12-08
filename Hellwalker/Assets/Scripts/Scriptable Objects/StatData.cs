using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "Stat/Stat Data", order = 2)]
public class StatData : ScriptableObject
{
    public string levelname;
    public string episodeandlevel;
    public string partime;
    public float difficultymultiplier;
    public int difficulty;
    public int addtomultiplier;
    public float multiplierdecayspeed;
    public int multiplierlimit;
    // Start is called before the first frame update
    void Start()
    {
        this.levelname = "Unnamed";
        this.episodeandlevel = "E#M#";
        this.partime = "00.00.00";
        this.difficultymultiplier = (float)1;
        this.difficulty = 1;
        this.addtomultiplier = 1;
        this.multiplierdecayspeed = (float)4;
        this.multiplierlimit = 99;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
