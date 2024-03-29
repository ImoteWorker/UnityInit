﻿namespace MyDungeon
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStript : MonoBehaviour
{
    static int xcount = 0, zcount = -1;
    public int FloorSize = 32;
    int x, z;
    BlockStript(){
        x = xcount;
        z = zcount;
        zcount++;
        if(zcount >= FloorSize){
            zcount = 0;
            xcount++;
        }
    }

    // Start is called before the first frame update
    public void check()
    {
        /*Debug.Log(x);
        Debug.Log(transform.position.x);
        Debug.Log(z);
        Debug.Log(transform.position.z);
        Debug.Log("--------------");*/
        List<Division> divList = BlockFactoryScript.divList;
        int[,] WallLocation = BlockFactoryScript.WallLocation;
        for(int i = 0; i < divList.Count; i++){
            if(divList[i].Room.left <= x && x <= divList[i].Room.right && divList[i].Room.bottom <= z && z <= divList[i].Room.top){
                Destroy(gameObject);
                WallLocation[x, z] = 1;
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
            Debug.Log("----------");
            if(divList[i].Room.left < divList[i].Outer.left || divList[i].Room.right > divList[i].Outer.right || divList[i].Room.bottom < divList[i].Outer.bottom || divList[i].Room.top > divList[i].Outer.top){
                Debug.Log("warning A");
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
            }
            if(divList[i].Room.right >= 49 || divList[i].Room.top >= 49){
                Debug.Log("warning B");
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
            }*/
        }
        List<Road> RoadList = BlockFactoryScript.RoadList;
        for(int i = 0; i < RoadList.Count; i++){
            if(RoadList[i].HorizontalOrVerticle){
                if(RoadList[i].start == z && RoadList[i].left <= x && x <= RoadList[i].right){
                    Destroy(gameObject);
                    WallLocation[x, z] = 2;
                }
            }
            else{
                if(RoadList[i].start == x && RoadList[i].bottom <= z && z <= RoadList[i].top){
                    Destroy(gameObject);
                    WallLocation[x, z] = 2;
                }
            }
        }
        BlockFactoryScript.setList(WallLocation);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
}