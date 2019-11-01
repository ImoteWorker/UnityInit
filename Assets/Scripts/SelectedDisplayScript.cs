using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//このスクリプト及びオブジェクトは選択されたカードを表示する場所に対応する
public class SelectedDisplayScript : MonoBehaviour
{
    public static int MaxSelectable = 3;　//選択できるカードの最大数
    GameObject Card;
    static CardScript cs;
    public CardGenerator1[] cg1 = new CardGenerator1[MaxSelectable];
    static bool setUp = true;　//手札のカードをシーン間で保持するために必要
    public GameObject CardDisplay;
    // Start is called before the first frame update
    void Start()
    {
        if(!setUp){
            this.gameObject.SetActive(false);　//CardDisplayのものと同じ
        }
        setUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E)){
            foreach(Transform childtransform in this.transform){
                GameObject child = childtransform.gameObject;
                child.transform.SetParent(CardDisplay.transform, true);
            }
        }
    }

    public void UseCard(){
        Debug.Log("つかった");
        SetCards();
        AllChildDestroy();　//使用したカードを破棄
        ActivateAll(); //生成したインスタンスのメソッドを実行　カードの効果発動に対応
    }
    void SetCards(){
        for(int i = 0; i < transform.childCount; i++){
            Card = transform.GetChild(i).gameObject;
            cs = Card.GetComponent<CardScript>();
            cg1[i] = new CardGenerator1(cs.CardID[0], cs.CardID[1], cs.CardID[2]);
            Debug.Log(cg1[i].type);
        }
    }
    void ActivateAll(){
        for(int i = 0; i < transform.childCount; i++){
            cg1[i].Activate();
        }
    }
    void AllChildDestroy(){
        for(int i = 0; i < transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}

public class CardGenerator1
{　//ここにカードの効果を書いていくことになる予定
    public int type, level, property;
    GameObject player;
    Player pl;
    GameObject floor;
    Floor fs;
    Enemy[] enemies = new Enemy[100];
    //GameObject Sword;
    public CardGenerator1(int Type, int Level, int Property){
        type = Type;
        level = Level;
        property = Property;
        player = GameObject.Find("Player");
        pl = player.GetComponent<Player>();
        floor = GameObject.Find("Generator");
        fs = floor.GetComponent<Floor>();
        for(int i = 0; i < fs.enemies.Count; i++){
            enemies[i] = fs.enemies[i].GetComponent<Enemy>();
        }
    }
    public void Activate(){
        Vector2 a;
        ModifyPosition mp = new ModifyPosition();
        for(int i = 0; i < fs.enemies.Count; i++){
            a = mp.Modify(enemies[i].x, enemies[i].z);
            Debug.Log("........");
            Debug.Log((int)a.x);
            Debug.Log((int)a.y);
            Debug.Log("........");
            if(type == 1){
                if(a.x == 0 && a.y == 1){
                    enemies[i].nowHP -= (15 * level);     //１マス前方攻撃
                }
            }
            else if(type == 2){
                if(-1 <= a.x && a.x <= 1 && a.y == 1){
                    enemies[i].nowHP -= (20 * level);     //１マス前方,左右斜め１マス前攻撃
                }
            }
            else if(type == 3){
                if(-1 <= a.x && a.x <= 1 && -1 <= a.y && a.y <= 1){
                    enemies[i].nowHP -= (25 * level);     //１マス周囲攻撃
                }
            }
            else if(type == 4){
                Vector2 b = mp.WallVertical(pl.dire);
                b = mp.Modify((int)b.x, (int)b.y);
                if(a.x == 0 && a.y > 0 && a.y < b.y){
                    Vector2 c;
                    bool f = true;
                    for(int j = 0; j < fs.enemies.Count; j++){
                        c = mp.Modify(enemies[j].x, enemies[j].z);
                        if(c.x == 0 && c.y < a.y){
                            f = false;
                            break;
                        }
                    }
                    if(f) enemies[i].nowHP -= (30 * level);      //前方１列攻撃
                }
            }
            else if(type == 5){
                if(Floor.Map[pl.posX, pl.posZ] == 11){
                    Vector2 b = mp.WallVertical(0);
                    Vector2 c = mp.WallVertical(2);
                    Vector2 d = mp.WallVertical(4);
                    Vector2 e = mp.WallVertical(6);
                    if(e.x < enemies[i].x && enemies[i].x < c.x && d.y < enemies[i].z && enemies[i].z < b.y){
                        enemies[i].nowHP -= (40 * level);  　　　　　//部屋全体攻撃
                    }
                }
                else if(Floor.Map[pl.posX, pl.posZ] == 12){
                    if(a.x == 0 && a.y == 1){
                        enemies[i].nowHP -= (5 * level);
                    }
                }
            }
            else if(type == 6){
                int healQuantity = 20 * level;
                if((Player.maxHP - Player.nowHP) > healQuantity){
                    Player.nowHP += healQuantity;
                }
                else{
                    Player.nowHP = Player.maxHP;
                }
                healQuantity = 0; 
            }
            Debug.Log("敵のHP↓");
            Debug.Log(enemies[i].nowHP);
        }
    }
    public void WeaponsMotion(){
        if(type == 1){
            KnifeScript.KnifeMotionStart = true;
        }
        else if(type == 2){
            AxeScript.AxeMotionStart = true;
        }
        else if(type == 3){
            SwordScript.SwordMotionStart = true;
        }
        else if(type == 4){
            ArrowScript.ArrowMotionStart = true;
            BowScript.BowMotionStart = true;
        }
        else if(type == 5){
            StaffScript.StaffMotionstart = true;
        }
    }
}
public class ModifyPosition
{
    GameObject Player;
    Player player;
    GameObject floor;
    Floor fs;
    public ModifyPosition(){
        Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        floor = GameObject.Find("Generator");
        fs = floor.GetComponent<Floor>();
    }
    public Vector2 Modify(int enemyX, int enemyZ){
        if(player.dire == 0){
            return new Vector2(enemyX - player.posX, enemyZ - player.posZ);
        }
        else if(player.dire == 2){
            return new Vector2((-1) * (enemyZ - player.posZ), enemyX - player.posX);
        }
        else if(player.dire == 4){
            return new Vector2((-1) * (enemyX - player.posX), (-1) * (enemyZ - player.posZ));
        }
        else if(player.dire == 6){
            return new Vector2(enemyZ - player.posZ, (-1) * (enemyX - player.posX));
        }
        else{
            Vector2 a;
            if(player.dire == 1){
                a = new Vector2(enemyX - player.posX, enemyZ - player.posZ);
            }
            else if(player.dire == 3){
                a = subModify90(enemyX - player.posX, enemyZ - player.posZ);
            }
            else if(player.dire == 5){
                a = subModify180(enemyX - player.posX, enemyZ - player.posZ);
            }
            else{
                a = subModify270(enemyX - player.posX, enemyZ - player.posZ);
            }
            if(enemyX >= 0 && enemyZ >= 0){
                return DiagonalCase0((int)a.x, (int)a.y);
            }
            else if(enemyX >= 0 && enemyZ < 0){
                return DiagonalCase1((int)a.x, (int)a.y);
            }
            else if(enemyX < 0 && enemyZ < 0){
                return DiagonalCase2((int)a.x, (int)a.y);
            }
            else{
                return DiagonalCase3((int)a.x, (int)a.y);
            }
        }
    }
    private Vector2 DiagonalCase0(int enemyX, int enemyZ){
        if(enemyX > enemyZ){
                    return new Vector2(enemyX - enemyZ, enemyX);
                }
                else{
                    return new Vector2(enemyX - enemyZ, enemyZ);
                }
    }
    private Vector2 DiagonalCase1(int enemyX, int enemyZ){
        Vector2 a = subModify90(enemyX, enemyZ);
        Vector2 b = DiagonalCase0((int)a.x, (int)a.y);
        return subModify270((int)b.x, (int)b.y);
    }
    private Vector2 DiagonalCase2(int enemyX, int enemyZ){
        Vector2 a = subModify180(enemyX, enemyZ);
        Vector2 b = DiagonalCase0((int)a.x, (int)a.y);
        return subModify180((int)b.x, (int)b.y);
    }
    private Vector2 DiagonalCase3(int enemyX, int enemyZ){
        Vector2 a = subModify270(enemyX, enemyZ);
        Vector2 b = DiagonalCase0((int)a.x, (int)a.y);
        return subModify90((int)b.x, (int)b.y);
    }
    private Vector2 subModify90(int enemyX, int enemyZ){
        return new Vector2((-1) * enemyZ, enemyX);
    }
    private Vector2 subModify180(int enemyX, int enemyZ){
        return new Vector2((-1) * enemyX, (-1) * enemyZ);
    }
    private Vector2 subModify270(int enemyX, int enemyZ){
        return new Vector2(enemyZ, (-1) * enemyX);
    }
    public Vector2 WallVertical(int dire){
        //Vector2 a;
        if(dire == 0){
            return SUBisThereWall(0, 1);
            //return Modify((int)a.x, (int)a.y);
        }
        else if(dire == 1){
            return SUBisThereWall(1, 1);
            //return Modify((int)a.x, (int)a.y);
        }
        else if(dire == 2){
            return SUBisThereWall(1, 0);
            //return Modify((int)a.x, (int)a.y);
        }
        else if(dire == 3){
            return SUBisThereWall(1, -1);
            //return Modify((int)a.x, (int)a.y);
        }
        else if(dire == 4){
            return SUBisThereWall(0, -1);
            //return Modify((int)a.x, (int)a.y);
        }
        else if(dire == 5){
            return SUBisThereWall(-1, -1);
            //return Modify((int)a.x, (int)a.y);
        }
        else if(dire == 6){
            return SUBisThereWall(-1, 0);
            //return Modify((int)a.x, (int)a.y);
        }
        else{
            return SUBisThereWall(-1, 1);
            //return Modify((int)a.x, (int)a.y);
        }       
    }
    private Vector2 SUBisThereWall(int value1, int value2){
        for(int i = 0; ; i++){
            if(Floor.Map[player.posX + i * value1, player.posZ + i * value2] == 0){
                return new Vector2(player.posX + i * value1, player.posZ + i * value2);
            }
        }
    }
    
}
