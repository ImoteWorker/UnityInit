using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxScript : MonoBehaviour
{
    List<int[]> ItemList = new List<int[]>();
    int ItemX, ItemZ;
    GameObject CardDeck;
    CardDeckScript cds;
    ModifyPosition mp;
    // Start is called before the first frame update
    public void setting()
    {
        CardDeck = GameObject.Find("CardDeck");
        cds = CardDeck.GetComponent<CardDeckScript>();
        mp = new ModifyPosition();
        int RateOfNum = (int)UnityEngine.Random.Range(0, 100);
        int Num;
        if(RateOfNum < 40){
            Num = 3;
        }
        else if(RateOfNum < 70){
            Num = 2;
        }
        else if(RateOfNum < 90){
            Num = 4;
        }
        else{
            Num = 1;
        }
        int RateOfType, RateOfLevel, RateOfProperty;
        for(int i = 0; i < Num; i++){
            RateOfType = (int)UnityEngine.Random.Range(0, 100);
            RateOfLevel = (int)UnityEngine.Random.Range(0, 100);
            RateOfProperty = (int)UnityEngine.Random.Range(0, 100);
            int type, level, property;
            int[] card = new int[3];
            //Type
            if(RateOfType < 50){
                type = 1;
            }
            else if(RateOfType < 70){
                type = 2;
            }
            else if(RateOfType < 80){
                type = 3;
            }
            else if(RateOfType < 90){
                type = 4;
            }
            else{
                type = 5;
            }
            //Level
            if(RateOfLevel < 50){
                level = 1;
            }
            else if(RateOfLevel < 80){
                level = 2;
            }
            else if(RateOfLevel < 92){
                level = 3;
            }
            else if(RateOfLevel < 97){
                level = 4;
            }
            else{
                level = 5;
            }
            //Property
            if(RateOfProperty < 40){
                property = 1;
            }
            else if(RateOfProperty < 55){
                property = 2;
            }
            else if(RateOfProperty < 70){
                property = 3;
            }
            else if(RateOfProperty < 85){
                property = 4;
            }
            else{
                property = 5;
            }
            card[0] = type;
            card[1] = level;
            card[2] = property;
            ItemList.Add(card);
        }
        int x, z;
        for(;;){
            x = UnityEngine.Random.Range(0, Floor.x);
            z = UnityEngine.Random.Range(0, Floor.z);
            if(Floor.Map[x, z] == 1){
                ItemX = x;
                ItemZ = z;
                transform.position = new Vector3(x, 0f, z);
                Floor.Map[x, z] += 30;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.I) && ItemList != null){
            bool f = false;
            for(int i = -1; i <= 1; i++){
                for(int j = -1; j <= 1; j++){
                    if(Floor.Map[i + ItemX, j + ItemZ] == 11 || Floor.Map[i + ItemX, j + ItemZ] == 12){
                        f = true;
                    }
                }
            }
            if(f){
                Vector2 a = mp.Modify(ItemX, ItemZ);
                if(!(a.x == 0 && a.y == 1)){
                    f = false;
                }
            }
            if(f){
                for(int i = 0; i < ItemList.Count; i++){
                    cds.GetCard(ItemList[i][0], ItemList[i][1], ItemList[i][2]);
                    //ItemList.Remove(item);
                    Debug.Log("カードを手に入れた");
                    Debug.Log(ItemList[i][0]);
                    Debug.Log(ItemList[i][1]);
                    Debug.Log(ItemList[i][2]);
                }
                Debug.Log("以上");
                ItemList = null;
                Destroy(gameObject);
            }
        }
    }
}
