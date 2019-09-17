using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{   
    GameObject SelectedDisplay;
    //GameObject SelectedDisplay = GameObject.Find("SelectedDisplay");
    bool f = false;
    // Start is called before the first frame update
    void Start()
    {
        SelectedDisplay = GameObject.Find("SelectedDisplay");
    }

    // Update is called once per frame
    void Update()
    {
        if(f){
            selectAnimation();
        }
    }
    public void OnClick(){
        f = true;
    }
    void selectAnimation(){
        if(transform.position.y <= 75f){
            transform.position += new Vector3(0f, 10f, 0f);
        }
        else{
            transform.parent = null;
            transform.parent = SelectedDisplay.transform;
            f = false;
            return;
        }
    }
}
