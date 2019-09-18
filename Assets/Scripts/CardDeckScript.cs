using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDeckScript : MonoBehaviour
{
    public static int NumOfCards = 0;
    static int MaxNumOfCards = 80;
    public static int[] Type = new int[100];
    public static int[] Level = new int[100];
    public static int[] Property = new int[100];
    public static int NumOfPramater = 3;
    public Text text;

    public void GetCard(int type, int level, int property){
        if(NumOfCards > MaxNumOfCards){
            return;
        }
        Type[NumOfCards] = type;
        Level[NumOfCards] = level;
        Property[NumOfCards] = property;
        NumOfCards++;
        //Debug.Log(NumOfCards);
    }
    // Start is called before the first frame update
    void Start()
    {
        //仮の山札
        GetCard(2, 1, 3);
        GetCard(1, 2, 1);
        GetCard(6, 4, 1);
        GetCard(4, 1, 1);
        GetCard(5, 2, 8);
        GetCard(8, 2, 1);
        GetCard(7, 3, 2);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = NumOfCards + "枚";
    }
    bool isOkay(){
        if(NumOfCards > MaxNumOfCards){
            return false;
        }
        else{
            return true;
        }
    }
}