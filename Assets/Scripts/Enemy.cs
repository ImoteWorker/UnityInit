using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int type;
    public static GameObject player;
    public static GameObject generator;
    public static Floor floor;
    public int maxHP;
    public int nowHP;
    public int atk;
    public int def;
    public int EXP;
    protected bool findPlayer = false;
    public int x;
    public int z;
    protected float moveTime;
    protected float moveFrame = 0.2f;
    public int masu=1;
    protected int toX;
    protected int toZ;

    public bool acting;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTime>0){
            transform.Translate(toX*masu/moveFrame*Time.deltaTime,0f,toZ*masu/moveFrame*Time.deltaTime);
            moveTime-=Time.deltaTime;
        }
        else{
            transform.position = new Vector3(x,0.5f,z);
            acting = false;
        }
        if(nowHP <= 0){
            floor.removeChara(x,z,2);
            floor.enemies.Remove(this.gameObject);
            player.GetComponent<Player>().getExp(EXP);
            Destroy(this.gameObject);
        }
    }
    public void setStatus(int MaxHP){
        nowHP = MaxHP;
    }
    public void setting()
    {
        player = GameObject.Find("Player");
        generator = GameObject.Find("Generator");
        floor = generator.GetComponent<Floor>();
        while(true){
            x = Random.Range(1,Floor.x);
            z = Random.Range(1,Floor.z);
            if(floor.available(x,z)) break;
        }
        transform.position = new Vector3(x,0.5f,z);
        floor.setChara(x,z,2);
        acting = false;
    }

    public virtual void action(){

    }

    protected void move(){

    }

    protected void attack(){

    }

    protected void special(){

    }

    public void damage(int atk, int atkType){

    }

    protected void moveAround(){

    }

    protected void moveRandom(){
        while(true){
            toX = Random.Range(-1,2);
            toZ = Random.Range(-1,2);
            if(floor.available(x+toX,z+toZ)){
                if(toX==0 || toZ==0) break;
                else{
                    if(floor.availableNaname(x+toX,z) && floor.availableNaname(x,z+toZ)) break;
                }
            }
            else if(toX==0 && toZ==0) break;
        }
        floor.moveChara(x,z,x+toX,z+toZ,2);
        x += toX;
        z += toZ;
        moveTime = moveFrame;
        acting = true;
        //transform.position = new Vector3(x,0.5f,z);
    }

    protected void moveChase(){

    }

    protected void moveEscape(){

    }
}
