using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyDungeon;

public class FloorAnnounceScript : MonoBehaviour
{
    public Text Announce;
    // Start is called before the first frame update
    void Start()
    {
        Announce.text = Floor.whatFloor + "F";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
