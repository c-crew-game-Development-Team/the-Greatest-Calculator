using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingFightBarScript : MonoBehaviour
{
    public int fighthbar;
    bool once;
    
    public GameObject fighthbarObject;

    void Start()
    {
        once = false;
        fighthbar = 5;
        fighthbarObject.GetComponent<Slider>().value = 5;
    }

    void Update() 
    {
        if(true || GameObject.Find("NumberBundle").GetComponent<RankingNumberBundleScript>().going == 1)
            fighthbarObject.GetComponent<Slider>().value = fighthbar;

        if (fighthbarObject.GetComponent<Slider>().value >= 10 && once == false)
        {
            GameObject.Find("Stage").GetComponent<RankingStage>().stage = 3;
            GameObject.Find("Stage").GetComponent<RankingStage>().stagemove = false;
        
            GameObject.Find("Stage").GetComponent<RankingStage>().Win();
            once = true;
        }

        if (fighthbarObject.GetComponent<Slider>().value <= 0 && once == false)
        {
            GameObject.Find("Stage").GetComponent<RankingStage>().stage = 3;
            GameObject.Find("Stage").GetComponent<RankingStage>().stagemove = false;
            
            GameObject.Find("Stage").GetComponent<RankingStage>().Lose();
            once = true;
        }
    }
}
