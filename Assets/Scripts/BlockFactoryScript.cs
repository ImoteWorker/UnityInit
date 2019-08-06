using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactoryScript : MonoBehaviour
{
    public GameObject Block;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 tp = new Vector3(-9.5f, 0.5f, -9.5f);
        for(int i = 0; i < 20; i++){
            tp.x = -9.5f + i;
            for(int j = 0; j < 20; j++){
                tp.z = -9.5f + j;
                SpawnBlock(tp);
            }
        }
    }
    void SpawnBlock(Vector3 tp){
        Instantiate(Block, tp, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
