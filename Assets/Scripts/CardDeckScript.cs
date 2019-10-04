using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//このスクリプトおよびオブジェクトは山札に対応する
public class CardDeckScript : MonoBehaviour
{
    public static int NumOfCards = 0;　//現在の山札にあるカードの枚数
    static int MaxNumOfCards = 80;　//山札に入れられるカードの最大数
    public static int[] Type = new int[100];　//山札にあるカードの種類を保存
    public static int[] Level = new int[100];　//山札にあるカードのレベルを保存
    public static int[] Property = new int[100];　//山札にあるカードの属性を保存
    public static int NumOfPramater = 3;　//カードを決定するパラメータの数（種類、レベル、属性）
    public Text text;　//今はカードの枚数表示に使用
    public static int NumOfType = 5;
    public static int NumOfLevel = 9;
    public static int NumOfProperty = 5;

    public void GetCard(int type, int level, int property){ //手に入れたカードのパラメータを配列に保存して山札に加えるを実現
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
        //GetCard(2, 1, 3);
        //GetCard(1, 2, 1);
        //GetCard(4, 2, 1);
        //GetCard(4, 1, 1);
        //GetCard(3, 1, 8);
        //GetCard(5, 2, 1);
        //GetCard(5, 3, 2);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = NumOfCards + "枚";　
    }
    bool isOkay(){　//未使用のメソッド
        if(NumOfCards > MaxNumOfCards){
            return false;
        }
        else{
            return true;
        }
    }
}