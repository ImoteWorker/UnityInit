using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int type;
    public static GameObject player;
    public static GameObject generator;
    public static Floor floor;
    public static Player ps;
    public int maxHP;
    public int nowHP;
    public int atk;
    public int def;
    public int EXP;
    protected bool findPlayer = false;
    protected bool frontPlayer = false;
    protected bool linePlayer = false;
    public int x;
    public int z;
    protected float moveTime;
    protected float moveFrame = 0.2f;
    public int masu=1;
    protected int toX;
    protected int toZ;
    protected int lostTurn = 0;

    protected int atkPow;
    protected int atkType;

    public bool moving;
    public bool waiting;
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
            moving = false;
        }
        if (acting)
        {
            ps.damage(atkPow, atkType);
            acting = false;
            atkPow = 0;
            atkType = 1;
        }
        if(nowHP <= 0){
            floor.removeChara(x,z,2);
            floor.enemies.Remove(this.gameObject);
            player.GetComponent<Player>().getExp(EXP);
            Destroy(this.gameObject);
        }
    }
    //演出がメイン

    public void changeAct()
    {
        waiting = false;
        acting = true;
    }

    public void setStatus(int MaxHP){
        nowHP = MaxHP;
    }

    public void setting()
    {
        player = GameObject.Find("Player");
        ps = player.GetComponent<Player>();
        generator = GameObject.Find("Generator");
        floor = generator.GetComponent<Floor>();
        while(true){
            x = Random.Range(1,Floor.x);
            z = Random.Range(1,Floor.z);
            if(floor.available(x,z)) break;
        }
        transform.position = new Vector3(x,0.5f,z);
        floor.setChara(x,z,2);
        moving = false;
        waiting = false;
        acting = false;
        atkPow = 0;
        atkType = 1;
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
                if(ps.posX == x || ps.posZ == z)
                {
                    linePlayer = true;
                }

                if (Mathf.Abs(ps.posX - x) <= 1 && Mathf.Abs(ps.posZ - z) <= 1)
                {
                    frontPlayer = true;
                }
                else frontPlayer = false;
            }
            else
            {
                lostTurn--;
                if (lostTurn <= 0) findPlayer = false;
                linePlayer = false;
                frontPlayer = false;
            }
        }
    }
    //敵とプレイヤーの間に障害があるかを判定、3ターン隠れると見失う
    //現状では敵も障害扱いだから2体並んでると後ろのやつが見失ったりする
    //敵のプレイヤーとの位置関係も判定する

    protected virtual void move(){

    }
    //これをオーバーライドして移動パターンを作る...つもりだけどactionでまとめてやってもいいかも

    protected void attackFront(int pow, int type)
    {
        waiting = true;
        atkPow = pow;
        atkType = type;
        //ps.damage(pow, type);
    }
    //前方攻撃

    protected void attackLine(int pow, int type)
    {
        waiting = true;
        atkPow = pow;
        atkType = type;
        //ps.damage(pow, type);
    }
    //直線攻撃

    //現状ではあまり分ける意味がないけど攻撃エフェクトを変える場合を考えて分けておいた

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
        moving = true;
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
        moving = true;
    }
    //追いかけるように移動

    protected void moveEscape(){
        List<Vector2Int> vec2s = new List<Vector2Int>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (floor.available(x + i, z + j)) vec2s.Add(new Vector2Int(i, j));
            }
        }

        Vector2Int playerPos = new Vector2Int((int)player.transform.position.x, (int)player.transform.position.z);
        Vector2Int farest = new Vector2Int(0, 0);
        double distance = 0;

        Vector2 dist;
        foreach (Vector2Int v in vec2s)
        {
            dist = playerPos - (new Vector2Int(x, z) + v);
            if (dist.magnitude > distance)
            {
                distance = dist.magnitude;
                farest = v;
            }
        }

        toX = farest.x;
        toZ = farest.y;
        floor.moveChara(x, z, x + toX, z + toZ, 2);
        x += toX;
        z += toZ;
        moveTime = moveFrame;
        moving = true;
    }
    //逃げるように移動
}
