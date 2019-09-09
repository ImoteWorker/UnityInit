using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHolderScript : MonoBehaviour
{
    public GameObject Card;
    float offset = 90f;
    public float radius = 50f;
    bool motion = false;
    float terminal = 90f;
    bool f;
    int selectedID;
    public Vector3 offsetPosition = new Vector3(250f, -15f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        Arrange();
        selectedID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(motion){
            if((int)terminal == (int)offset){
                motion = false;
                WhatSelected();
            }
            else if(f){
                float AngleSplit = 360 / transform.childCount;
                offset += AngleSplit / 8f;
                Arrange();
            }
            else{
                float AngleSplit = 360 / transform.childCount;
                offset -= AngleSplit / 8f;
                Arrange();
            }
           // Debug.Log(offset);
            //Debug.Log(terminal);
        }
        else if(Input.GetKeyDown(KeyCode.Z)){
            float AngleSplit = 360 / transform.childCount;
            terminal += AngleSplit;
            motion = true;
            f = true;
        }
        else if(Input.GetKeyDown(KeyCode.C)){
            float AngleSplit = 360 / transform.childCount;
            terminal -= AngleSplit;
            motion = true;
            f = false;
        }
        else if(Input.GetKeyDown(KeyCode.X)){
            GameObject selsectedCard = transform.GetChild(selectedID).gameObject;
            selsectedCard.SetActive(false);
        }
    }
    void Arrange(){
        float AngleSplit = 360 / transform.childCount;
        for(int elementID = 0; elementID < transform.childCount; elementID++){
            var Card = transform.GetChild(elementID) as RectTransform;
            float currentAngle = AngleSplit * elementID + offset;
            Card.anchoredPosition = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0f) * radius + offsetPosition;
            //Debug.Log(elementID);
            Debug.Log(currentAngle);
        }
    }
    void WhatSelected(){
        if(f){
            if(selectedID == 0){
                selectedID = transform.childCount - 1;
            }
            else{
                selectedID--;
            }
        }
        else{
            if(selectedID == transform.childCount - 1){
                selectedID = 0;
            }
            else{
                selectedID++;
            }
        }
    }
    /*void ChangeColor(){
        GameObject obj = transform.GetChild(selectedID).gameObject;
        var card = obj.GetComponent<Button>();
        var colors = card.colors;
        colors.normalColor = new Color(165f / 255f, 220f / 255f, 192f / 255f, 255f / 255f);

    }*/
    /*void Motion(){
        for(int elementID = 0; elementID < transform.childCount; elementID++){
            GameObject childCard = transform.GetChild(elementID).gameObject;
        }
    }*/
}
