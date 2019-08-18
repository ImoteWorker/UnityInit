using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDungeon;

public class Floor : MonoBehaviour
{
    public static int[,] Map;
    public bool type = true;

    static BlockFactoryScript bfs;
    static BlockStript bs;
    static FloorGenerator fg;
    static FirstPersonMove fpm;
    public GameObject player;
    public GameObject bf;
    List<GameObject> blockList = new List<GameObject>();
    public static int x;
    public static int z;
    void Start()
    {
        bfs = bf.GetComponent<BlockFactoryScript>();
        blockList = bfs.blockList;
        fg = GetComponent<FloorGenerator>();
        fpm = player.GetComponent<FirstPersonMove>();
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
        fpm.setting();
    }

    public bool startable(int x,int z){
        if(type){
            if(x<0 || x>fg.AreaX ||z<0 || z>fg.AreaZ) return false;
            else if(Map[x,z]!=0) return false;
        }
        else{
            if(x<0 || x>bfs.FloorX || z<0 || z>bfs.FloorZ) return false;
            else if(Map[x,z]!=0) return false;
        }
        return true;
    }

    public bool available(int x,int z){
        if(type){
            if(x<0 || x>fg.AreaX ||z<0 || z>fg.AreaZ) return false;
            else if(Map[x,z]!=0) return false;
        }
        else{
            if(x<0 || x>bfs.FloorX || z<0 || z>bfs.FloorZ) return false;
            else if(Map[x,z]!=0) return false;
        }
        return true;
    }
}
