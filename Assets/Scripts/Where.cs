using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Where : MonoBehaviour
{
    public Text position;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position.text = "("+player.transform.position.x+","+player.transform.position.z+")";
    }
}
