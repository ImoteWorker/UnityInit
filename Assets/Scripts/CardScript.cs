using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//このスクリプトおよびオブジェクトは個々のカードに対応する
public class CardScript : MonoBehaviour
{
    public int[] CardID;　//このカードが何の種類でレベルで属性なのかを保存
    GameObject SelectedDisplay;
    bool isClicked = false;　//クリックでカードを選択できるクリックされるとtrue完了でfalseに戻る
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        SelectedDisplay = GameObject.Find("SelectedDisplay");
        CardID = DrowCard();
        //Debug.Log(CardID[0]);
        text.text = CardID[0] + "," + CardID[1] + "," + CardID[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(isClicked){
            selectAnimation();
        }
    }
    public int[] DrowCard(){　　//山札の配列に保存してある情報をもとにカードの種類、レベル、属性をランダムに決定する
        int[] CardID = new int[CardDeckScript.NumOfPramater];  //これは山札からカードを引く操作に対応
        int ID = UnityEngine.Random.Range(0, CardDeckScript.NumOfCards);
        CardID[0] = CardDeckScript.Type[ID];　//Type,Level,Property配列は同じindexにある値を総合してある１つのカードの情報を表す
        CardID[1] = CardDeckScript.Level[ID];
        CardID[2] = CardDeckScript.Property[ID];
        for(int i = ID; i < CardDeckScript.NumOfCards-1; i++){
            CardDeckScript.Type[i] = CardDeckScript.Type[i+1];
            CardDeckScript.Level[i] = CardDeckScript.Level[i+1];
            CardDeckScript.Property[i] = CardDeckScript.Property[i+1];
        }
        CardDeckScript.NumOfCards--;
        return CardID;
    }
    public void OnClick(){
        if(SelectedDisplayScript.MaxSelectable > SelectedDisplay.transform.childCount){
            isClicked = true;
        }
    }
    void selectAnimation(){　//CardDisplayにあるものと同じ
        if(transform.position.y <= 75f){
            transform.position += new Vector3(0f, 10f, 0f);
        }
        else{
            transform.SetParent(null, true);
            transform.SetParent(SelectedDisplay.transform, true);
            isClicked = false;
            return;
        }
    }
}