using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public int[] CardID;
    GameObject SelectedDisplay;
    bool isClicked = false;
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
    public int[] DrowCard(){
        int[] CardID = new int[CardDeckScript.NumOfPramater];
        int ID = UnityEngine.Random.Range(0, CardDeckScript.NumOfCards);
        CardID[0] = CardDeckScript.Type[ID];
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
        isClicked = true;
    }
    void selectAnimation(){
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