namespace appleboy
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStript : MonoBehaviour
{
    static int xcount = 0, zcount = 0;
    int x, z;
    BlockStript(){
        x = xcount;
        z = zcount;
        zcount++;
        if(zcount >= 50){
            zcount = 0;
            xcount++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        List<Division> divList = BlockFactoryScript.divList;
        for(int i = 0; i < divList.Count; i++){
            if(divList[i].Room.left <= x && x <= divList[i].Room.right && divList[i].Room.bottom <= z && z <= divList[i].Room.top){
                Destroy(gameObject);
            }
            /*Debug.Log("区画");
            Debug.Log(divList[i].Outer.left);
            Debug.Log(divList[i].Outer.right);
            Debug.Log(divList[i].Outer.bottom);
            Debug.Log(divList[i].Outer.top);
            Debug.Log("間取り");
            Debug.Log(divList[i].Outer.width);
            Debug.Log(divList[i].Outer.height);
            Debug.Log("部屋");
            Debug.Log(divList[i].Room.left);
            Debug.Log(divList[i].Room.right);
            Debug.Log(divList[i].Room.bottom);
            Debug.Log(divList[i].Room.top);
            Debug.Log("間取り");
            Debug.Log(divList[i].Room.width);
            Debug.Log(divList[i].Room.height);
            Debug.Log("----------");*/
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
}