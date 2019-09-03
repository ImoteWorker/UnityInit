using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data : MonoBehaviour
{
    public Text data;
    public GameObject player;
    public GameObject fg;
    public TurnManager tm;
    // Start is called before the first frame update
    void Start()
    {
        tm = fg.GetComponent<TurnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        data.text = "("+player.transform.position.x+","+player.transform.position.z+") ";
        data.text += tm.getTurn()+"ターン";
    }
}
