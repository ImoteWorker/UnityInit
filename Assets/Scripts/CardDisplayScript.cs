using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//このスクリプトおよびオブジェクトは手札に対応する
public class CardDisplayScript : MonoBehaviour
{
    public GameObject Card;
    bool[] changed = new bool[15];
    public GameObject SelectedDisplay;
    List<GameObject> selectedList = new List<GameObject>();
    static bool setUp = true;
    int NumOfAvailableCard = 7;
    // Start is called before the first frame update
    void Start() 
    {
        if(!setUp){
            this.gameObject.SetActive(false);　//手札をシーン間で保持するための手間　2回目以降に読み込まれたDisplayを非表示化
        }                                      //一回目に読み込まれたDisplayをずっと使用する
        setUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelectable()){
            WhatSelected(); //キー入力で選ばれたカードをListに追加
        }
        if(selectedList.Count != 0){
            selectAnimation();　//Listにあるカードを少し動き（選択したときにカードがy軸方向に動く）を付けて、
        }                       //そのカードをSelectedDisplayの子オブジェクトにする（このときカードが表示される場所が変わる）
    }
    void WhatSelected(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            subWhatSelected(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            subWhatSelected(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            subWhatSelected(3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            subWhatSelected(4);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            subWhatSelected(5);
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)){
            subWhatSelected(6);
        }
        if(Input.GetKeyDown(KeyCode.Alpha7)){
            subWhatSelected(7);
        }
        if(Input.GetKeyDown(KeyCode.Alpha8)){
            subWhatSelected(8);
        }
        if(Input.GetKeyDown(KeyCode.Alpha9)){
            subWhatSelected(9);
        }
        if(Input.GetKeyDown(KeyCode.Alpha0) && CardDeckScript.NumOfCards != 0　&& transform.childCount + SelectedDisplay.transform.childCount < NumOfAvailableCard){
            if(CardDeckScript.NumOfCards < NumOfAvailableCard){
                for(int i = 0; i < CardDeckScript.NumOfCards; i++){
                    GameObject childObject = Instantiate(Card) as GameObject;
                    childObject.transform.SetParent(this.transform, true);
                    if(transform.childCount + SelectedDisplay.transform.childCount == NumOfAvailableCard){
                        break;
                    }
                }
            }
            else{
                for(int i = 0; i < NumOfAvailableCard - (transform.childCount + SelectedDisplay.transform.childCount); i++){
                    GameObject childObject = Instantiate(Card) as GameObject;
                    childObject.transform.SetParent(this.transform, true);
                if(transform.childCount + SelectedDisplay.transform.childCount == NumOfAvailableCard){
                        break;
                    }
                }
            }
        }
    }
    void subWhatSelected(int key){
        GameObject card;
        for(int i = 0; i < transform.childCount; i++){
            card = transform.GetChild(i).gameObject;
            //Debug.Log(card.transform.localPosition.x);
            if(((key-1)*40-165) == card.transform.localPosition.x){
                //ChangeColor(card, false);
                selectedList.Add(card);
                break;
            }
        }
    }
    bool ChangeColor(GameObject obj, bool changed){　//未使用のメソッド
        var card = obj.GetComponent<Button>();
        var colors = card.colors;
        if(!changed){
            colors.normalColor = new Color(130f / 255f, 120f / 255f, 192f / 255f, 150f / 255f);
            colors.highlightedColor = new Color(165f / 255f, 220f / 255f, 192f / 255f, 255f / 255f);
            colors.pressedColor = new Color(165f / 255f, 220f / 255f, 192f / 255f, 255f / 255f);
            colors.disabledColor = new Color(165f / 255f, 220f / 255f, 192f / 255f, 255f / 255f);
            card.colors = colors;
            return true;
        }
        else{
            colors.normalColor = new Color(1f, 1f, 1f, 1f);
            colors.highlightedColor = new Color(1f, 1f, 1f, 1f);
            colors.pressedColor = new Color(1f, 1f, 1f, 1f);
            colors.disabledColor = new Color(1f, 1f, 1f, 1f);
            card.colors = colors;
            return false;
        }
    }
    void selectAnimation(){
        foreach(GameObject card in selectedList){
            if(card.transform.position.y <= 75f){
                card.transform.position += new Vector3(0f, 10f, 0f);
            }
            else{
                card.transform.SetParent(null, true);
                card.transform.SetParent(SelectedDisplay.transform, true);
                selectedList.Remove(card);
                return;
            }
        }
    }
    bool isSelectable(){

        if(SelectedDisplayScript.MaxSelectable <= (SelectedDisplay.transform.childCount+selectedList.Count)){
            return false;
        }
        else return true;
    }
}