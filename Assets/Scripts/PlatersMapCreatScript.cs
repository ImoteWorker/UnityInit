using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlatersMapCreatScript : MonoBehaviour
{
    public Text map;
    public GameObject player;
    bool[,] Paint = new bool[100,100];
    public float timeOut = 3000f;
    private float timeTrigger;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Floor.x; i++){
            for(int j = 0; j < Floor.z; j++){
                Paint[i, j] = false;
            }
        }
        map.text = "";
        write();
    }

    // Update is called once per frame
    public void write()
    {
        //timeTrigger += Time.deltaTime;
        //if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0){
            RoadPaint((int)player.transform.position.x, (int)player.transform.position.z);
            RoomPaint((int)player.transform.position.x, (int)player.transform.position.z);
            CreateMap();
        //}
    }
    void RoadPaint(int x, int z){
        if(Floor.Map[x,z]%10 == 2 && !Paint[x,z]){
            Paint[x,z] = true;
            if(Floor.Map[x-1,z]%10 == 2){
                for(int i = 1;;i++){
                    if(Floor.Map[x-i,z]%10 == 2){
                        Paint[x-i,z] = true;
                    }else{
                        return;
                    }
                }
            }else if(Floor.Map[x,z-1]%10 == 2){
                for(int i = 1;;i++){
                    if(Floor.Map[x,z-i]%10 == 2){
                        Paint[x,z-i] = true;
                    }else{
                        return;
                    }
                }
            }
            if(Floor.Map[x+1,z]%10 == 2){
                for(int i = 1;;i++){
                    if(Floor.Map[x+i,z]%10 == 2){
                        Paint[x+i,z] = true;
                    }else{
                        return;
                    }
                }
            }else if(Floor.Map[x,z+1]%10 == 2){
                for(int i = 1;;i++){
                    if(Floor.Map[x,z+i]%10 == 2){
                        Paint[x,z+i] = true;
                    }else{
                        return;
                    }
                }
            }
        }else{
            return;
        }
    }
    void RoomPaint(int x, int z){
        if(Floor.Map[x,z]%10 == 1 && !Paint[x,z]){
            Paint[x,z] = true;
            RoomPaint(x-1,z);
            RoomPaint(x,z-1);
            RoomPaint(x+1,z);
            RoomPaint(x,z+1);
        }else{
            return;
        }
    }
    void CreateMap(){
        map.text = "";
        for(int i = 0; i < Floor.x; i++){
            for(int j = 0; j < Floor.z; j++){
                if(i == player.transform.position.x && j == player.transform.position.z){
                    map.text += "@";
                }
                else if(Paint[i,j]){
                    map.text += ".";
                }else{
                    map.text += " ";
                }
            }
            map.text += "\n";
        }
    }
}
