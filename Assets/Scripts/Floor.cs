﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDungeon;

public class Floor : MonoBehaviour
{
    public static int[,] Map;
    //
    //
    //0:壁, 1:部屋, 2:通路, 3:階段
    //+10:プレイヤー, +20:敵, +30:アイテム
    //
    public bool type = true;

    static BlockFactoryScript bfs;
    static BlockStript bs;
    static FloorGenerator fg;
    static Player ps;
    static PlatersMapCreatScript pmsc;
    public GameObject player;
    public GameObject bf;
    public GameObject map;
    List<GameObject> blockList = new List<GameObject>();
    public static int x;
    public static int z;
    public static int whatFloor = 0; //現在の階層
    public GameObject enemy1;
    public int enemyNum;
    public List<GameObject> enemies = new List<GameObject>();
    public int ItemBoxNum;
    public GameObject itemBox;
    void Start()
    {
        bfs = bf.GetComponent<BlockFactoryScript>();
        blockList = bfs.blockList;
        fg = GetComponent<FloorGenerator>();
        ps = player.GetComponent<Player>();
        whatFloor += 1;
        if(whatFloor == 1){　/*ダンジョンの切り替えができるか実験 */
            type = false;
        }else{
            type = true;
        }
        pmsc = map.GetComponent<PlatersMapCreatScript>();
        pmsc.setting();
        generate();
    }
    public void generate(){
        if(type){
            fg.generate();
            Map = FloorGenerator.WallLocation;
            x = fg.AreaX;
            z = fg.AreaZ;
        }
        else{
            bfs.generate();
            for(int i=0;i<blockList.Count;i++){
                bs = blockList[i].GetComponent<BlockStript>();
                bs.check();
            }
            Map = BlockFactoryScript.WallLocation;
            x = bfs.FloorX;
            z = bfs.FloorZ;
        }
        ps.setting();
        GameObject en;
        for(int i=0;i<enemyNum;i++){
            en = Instantiate(enemy1,transform.position,transform.rotation);
            en.GetComponent<Enemy>().setting();
            enemies.Add(en);
        }
        GameObject ib;
        for(int i = 0; i < ItemBoxNum; i++){
            ib = Instantiate(itemBox, transform.position, transform.rotation);
            ib.GetComponent<ItemBoxScript>().setting();
        }
    }

    public bool startable(int x,int z){
        if(type){
            if(x<0 || x>fg.AreaX ||z<0 || z>fg.AreaZ) return false;
            else if(Map[x,z]!=1) return false;
        }
        else{
            if(x<0 || x>bfs.FloorX || z<0 || z>bfs.FloorZ) return false;
            else if(Map[x,z]!=1) return false;
        }
        return true;
    }

    public bool available(int x,int z){
        if(type){
            if(x<0 || x>fg.AreaX ||z<0 || z>fg.AreaZ) return false;
            else if(Map[x,z]==0) return false;
            else if(Map[x,z]>=10 && Map[x,z]<30) return false;
        }
        else{
            if(x<0 || x>bfs.FloorX || z<0 || z>bfs.FloorZ) return false;
            else if(Map[x,z]==0) return false;
            else if(Map[x,z]>=10 && Map[x,z]<30) return false;
        }
        return true;
    }

    public bool availableNaname(int x,int z){
        if(type){
            if(x<0 || x>fg.AreaX ||z<0 || z>fg.AreaZ) return false;
            else if(Map[x,z]==0) return false;
        }
        else{
            if(x<0 || x>bfs.FloorX || z<0 || z>bfs.FloorZ) return false;
            else if(Map[x,z]==0) return false;
        }
        return true;
    }

    public void moveChara(int oldX, int oldZ, int newX, int newZ, int charaType){
        Map[oldX,oldZ] -=10*charaType;
        Map[newX,newZ] +=10*charaType;
    }

    public void setChara(int x, int z, int charaType){
        Map[x,z] += charaType*10;
    }

    public void removeChara(int x, int z, int charaType){
        Map[x,z] -= charaType*10;
    }

}
