using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//このスクリプト及びオブジェクトは選択されたカードを表示する場所に対応する
public class SelectedDisplayScript : MonoBehaviour
{
    public static int MaxSelectable = 3;　//選択できるカードの最大数
    GameObject Card;
    static CardScript cs;
    CardGenerator1[] cg1 = new CardGenerator1[MaxSelectable];
    static bool setUp = true;　//手札のカードをシーン間で保持するために必要
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
        /* 
        if(Input.GetKeyDown(KeyCode.Return)){　//エンターで選択済みのカードの情報(CardID)をもとにインスタンスを生成
            SetCards();
            AllChildDestroy();　//使用したカードを破棄
            ActivateAll(); //生成したインスタンスのメソッドを実行　カードの効果発動に対応
        }
        */
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
    GameObject Player;
    Player player;
    GameObject Floor;
    Floor fs;
    Enemy[] enemies = new Enemy[100];
    public CardGenerator1(int Type, int Level, int Property){
        type = Type;
        level = Level;
        property = Property;
        Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        Floor = GameObject.Find("Generator");
        fs = Floor.GetComponent<Floor>();
        for(int i = 0; i < fs.enemies.Count; i++){
            enemies[i] = fs.enemies[i].GetComponent<Enemy>();
        }
    }
    public void Activate(){
        Vector2 a;
        for(int i = 0; i < fs.enemies.Count; i++){
            a = Modify(enemies[i].x, enemies[i].z);
            Debug.Log("........");
            Debug.Log((int)a.x);
            Debug.Log((int)a.y);
            Debug.Log("........");
            //if(type == 1){
                if(a.x == 0 && a.y == 1){
                    enemies[i].nowHP -= 24;
                    Debug.Log("Hit! 敵のHP↓");
                    Debug.Log(enemies[i].nowHP);
                }
            //}
        }
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
}
