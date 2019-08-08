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
        if(zcount >= 30){
            zcount = 0;
            xcount++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BlockFactoryScript bf = new BlockFactoryScript();
        for(int i = 0; i < bf.ListSize(); i++){
            Vector4 rect = bf.CreatRoom(i);
            if(rect.x <= x && x <= rect.y && rect.z <= z && z <= rect.w){
                Destroy(gameObject);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
}