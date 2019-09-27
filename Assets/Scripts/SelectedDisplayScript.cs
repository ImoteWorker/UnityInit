using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//このスクリプト及びオブジェクトは選択されたカードを表示する場所に対応する
public class SelectedDisplayScript : MonoBehaviour
{
    public static int MaxSelectable = 3;　//選択できるカードの最大数
    GameObject Card;
    static CardScript cs;
    CardGenerator1[] cg1 = new CardGenerator1[MaxSelectable];
    static bool setUp = true;　//手札のカードをシーン間で保持するために必要
    // Start is called before the first frame update
    void Start()
    {
        if(!setUp){
            this.gameObject.SetActive(false);　//CardDisplayのものと同じ
        }
        setUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){　//エンターで選択済みのカードの情報(CardID)をもとにインスタンスを生成
            SetCards();
            AllChildDestroy();　//使用したカードを破棄
            ActivateAll(); //生成したインスタンスのメソッドを実行　カードの効果発動に対応
        }

    }
    void SetCards(){
        for(int i = 0; i < transform.childCount; i++){
            Card = transform.GetChild(i).gameObject;
            cs = Card.GetComponent<CardScript>();
            cg1[i] = new CardGenerator1(cs.CardID[0], cs.CardID[1], cs.CardID[2]);
            Debug.Log(cg1[i].type);
        }
    }
    void ActivateAll(){
        for(int i = 0; i < transform.childCount; i++){
            cg1[i].Activate();
        }
    }
    void AllChildDestroy(){
        for(int i = 0; i < transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
    }
   
}

public class CardGenerator1{　//ここにカードの効果を書いていくことになる予定
    public int type, level, property;
    public CardGenerator1(int Type, int Level, int Property){
        type = Type;
        level = Level;
        property = Property;
    }
    public void Activate(){

    }
}