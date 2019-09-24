using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDungeon;
using UnityEngine.SceneManagement;

public class StairsScript : MonoBehaviour
{
    public Floor fs;
    public GameObject generator; 
    public GameObject player;
    public GameObject Canvas;
    public int StairPosX;
    public int StairPosZ;
    static bool setUp = true;
    void Awake(){
        if(setUp){
            DontDestroyOnLoad(Canvas);
        }
        setUp = false;
    }
    // Start is called before the first frame update
    void Start(){
        fs = generator.GetComponent<Floor>();
        if(!fs.type) StartPoint();
        else{
            int posX;
            int posZ;
            while(true){
                posX = Random.Range(1,Floor.x);
                posZ = Random.Range(1,Floor.z);
                if(fs.startable(posX,posZ)) break;
            }
            transform.position = new Vector3(posX,0.05f,posZ);
            StairPosX = posX;
            StairPosZ = posZ;
        }
    }
    void StartPoint()
    {    
        List<Division> divList = BlockFactoryScript.divList;
        int rd = UnityEngine.Random.Range(0,divList.Count);
        //Debug.Log(divList.Count);
        int xLocation = (divList[divList.Count-1].Room.right + divList[divList.Count-1].Room.left)/2;
        int zLocation = (divList[divList.Count-1].Room.top + divList[divList.Count-1].Room.bottom)/2;
        //Debug.Log(xLocation);
        //Debug.Log(zLocation);
        transform.Translate(xLocation, 0f, zLocation);
        StairPosX = xLocation;
        StairPosZ = zLocation;
    }

    // Update is called once per frame
    void Update()
    {
        if(StairPosX == player.transform.position.x && StairPosZ == player.transform.position.z){
            Destroy(GameObject.Find("PlayersMap"));
            Destroy(GameObject.Find("FloorAnnounce"));
            Destroy(GameObject.Find("Status"));
            Destroy(GameObject.Find("CardDeck"));
            SceneManager.LoadScene("Main");
        }
    }
}

