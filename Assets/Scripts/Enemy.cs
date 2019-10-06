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
    protected int lostTurn = 0;

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
    //演出がメイン

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
    //開始時の処理

    public virtual void action(){

    }
    //これをオーバーライドして行動パターンを作る

    protected void look()
    {
        RaycastHit hit;
        Vector3 temp = player.transform.position - this.transform.position;
        Vector3 normal = temp.normalized;
        if(Physics.Raycast(this.transform.position, normal, out hit, 10))
        {
            if(hit.transform.gameObject == player)
            {
                findPlayer = true;
                lostTurn = 3;
            }
            else
            {
                lostTurn--;
                if (lostTurn <= 0) findPlayer = false;
            }
        }
    }
    //敵とプレイヤーの間に障害があるかを判定、3ターン隠れると見失う
    //現状では敵も障害扱いだから2体並んでると後ろのやつが見失ったりする

    protected virtual void move(){

    }
    //これをオーバーライドして移動パターンを作る...つもりだけどactionでまとめてやってもいいかも

    protected virtual void attack(){

    }
    //これをオーバーライドして攻撃パターンを作る...つもりだけど(ry

    protected virtual void special(){

    }
    //特殊な行動

    public void damage(int atk, int atkType){

    }
    //使ってないね

    protected void moveAround(){

    }
    //ここに各部屋を回るような移動の方法をかければいいなぁ

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
    //ランダムに移動、動かない時もある

    protected void moveChase(){
        List<Vector2Int> vec2s = new List<Vector2Int>();
        for(int i= -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if(floor.available(x+i,z+j)) vec2s.Add(new Vector2Int(i,j));
            }
        }

        Vector2Int playerPos = new Vector2Int((int)player.transform.position.x, (int)player.transform.position.z);
        Vector2Int nearest = new Vector2Int(0, 0);
        double distance = double.MaxValue;

        Vector2 dist;
        foreach(Vector2Int v in vec2s)
        {
            dist = playerPos - (new Vector2Int(x,z)+v);
            if (dist.magnitude < distance)
            {
                distance = dist.magnitude;
                nearest = v;
            }
        }

        toX = nearest.x;
        toZ = nearest.y;
        floor.moveChara(x, z, x + toX, z + toZ, 2);
        x += toX;
        z += toZ;
        moveTime = moveFrame;
        acting = true;
    }
    //追いかけるように移動

    protected void moveEscape(){

    }
    //逃げるように移動

    protected void attackFront(int pow, int type){

    }
    //前に攻撃

    protected void attackLine(int pow, int type){

    }
    //一直線上に攻撃
}
