using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlatersMapCreatScript : MonoBehaviour
{
    public Text map;
    public GameObject player;
    int[,] Paint = new int[100,100];
    public float timeOut = 3000f;
    private float timeTrigger;
    // Start is called before the first frame update
    public void setting()
    {
        for(int i = 0; i < Floor.x; i++){
            for(int j = 0; j < Floor.z; j++){
                Paint[i, j] = 0;
            }
        }
        map.text = "";
        //write();
    }

    // Update is called once per frame
    public void write(int playerX, int playerZ)
    {
        //timeTrigger += Time.deltaTime;
        //if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0){
            RoadPaint(playerX,playerZ);
            RoomPaint(playerX,playerZ);
            CreateMap(playerX,playerZ);
        //}
    }
    void RoadPaint(int x, int z){
        if(Floor.Map[x,z]%10 == 2 && Paint[x,z] == 0){
            Paint[x,z] = 1;
            if(Floor.Map[x-1,z]%10 == 2){
                for(int i = 1;;i++){
                    if(Floor.Map[x-i,z]%10 == 2){
                        Paint[x-i,z] = 1;
                    }else{
                        return;
                    }
                }
            }else if(Floor.Map[x,z-1]%10 == 2){
                for(int i = 1;;i++){
                    if(Floor.Map[x,z-i]%10 == 2){
                        Paint[x,z-i] = 1;
                    }else{
                        return;
                    }
                }
            }
            if(Floor.Map[x+1,z]%10 == 2){
                for(int i = 1;;i++){
                    if(Floor.Map[x+i,z]%10 == 2){
                        Paint[x+i,z] = 1;
                    }else{
                        return;
                    }
                }
            }else if(Floor.Map[x,z+1]%10 == 2){
                for(int i = 1;;i++){
                    if(Floor.Map[x,z+i]%10 == 2){
                        Paint[x,z+i] = 1;
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
        if(Floor.Map[x,z]%10 == 1 && Paint[x,z] == 0){
            Paint[x,z] = 1;
            RoomPaint(x-1,z);
            RoomPaint(x,z-1);
            RoomPaint(x+1,z);
            RoomPaint(x,z+1);
        }
        else if(Floor.Map[x,z] == 3 && Paint[x,z] != 3){
            Paint[x,z] = 3;
        }
        else{
            return;
        }
    }
    void CreateMap(int x, int z){
        map.text = "";
        for(int i = 0; i < Floor.x; i++){
            for(int j = 0; j < Floor.z; j++){
                if(i == x && j == z){
                    map.text += "@";
                }
                else if(Paint[i,j] == 3){
                    map.text += "x";
                }
                else if(Paint[i,j] == 1){
                    map.text += ".";
                }
                else{
                    map.text += " ";
                }
            }
            map.text += "\n";
        }
    }
}
