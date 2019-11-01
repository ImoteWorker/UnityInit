using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyDungeon;

public class Player : MonoBehaviour
{
    public float moveFrame = 5f;
    public float nanameFrame = 7f;
    public float turnFrame = 5f;
    public float masu=1f;
    public Vector2 Area = new Vector2(100,100);
    private bool moving;
    private float moveTime;
    private bool naname;
    private bool turning;
    private float turnTime;
    public int dire;
    private float x;
    private float z;
    private int actNum;

    public int posX;
    public int posZ;

    public static int maxHP = 50;
    public static int nowHP = 50;
    public static int level=1;
    public static int atk = 5;
    public static int def = 5;
    public static int EXP=0;
    public static int type = 1;

    public Floor fs;
    public GameObject generator;
    PlatersMapCreatScript pmcs;
    public GameObject map;
    SelectedDisplayScript sds;
    GameObject sd;
    public GameObject Sword; //アニメ追加
    public GameObject Knife;
    public GameObject Axe;
    public GameObject Bow;
    public GameObject Arrow;
    public GameObject Staff;
    CardScript Card;
    bool AnimeNumFlag = true;
    bool FirstWeaponAnime = true;
    bool SecondWeaponAnime;
    bool ThirdWeaponAnime;
    bool AllMotionStart;
    float TimeCount = 0f;
    //Animator animator;
    //CardHolderScript chs;
    //public GameObject cardholder;

    public Slider HPBar;
    public Text HPUI;
    public Text LvUI;
    public Text GameOver;
    
    void StartPoint()
    {  
        int[,] WallLocation = BlockFactoryScript.WallLocation;
        List<Division> divList = BlockFactoryScript.divList;
        //Debug.Log(divList.Count);
        int rd = UnityEngine.Random.Range(0,divList.Count);
        posX = (divList[0].Room.right + divList[0].Room.left)/2;
        posZ = (divList[0].Room.top + divList[0].Room.bottom)/2;
        //transform.Translate(posX, 0f, posZ);
        transform.position = new Vector3(posX,0.5f,posZ);
    }

    public void setting()
    {
        moving = false;
        moveTime = 0;
        naname = false;
        turning = false;
        turnTime = 0;
        dire = 0;
        x=0;
        z=0;
        fs = generator.GetComponent<Floor>();
        if(!fs.type) StartPoint();
        else{
            while(true){
                posX = Random.Range(1,Floor.x);
                posZ = Random.Range(1,Floor.z);
                if(fs.startable(posX,posZ)) break;
            }
            transform.position = new Vector3(posX,0.5f,posZ);
        }
        fs.setChara(posX,posZ,1);

        pmcs = map.GetComponent<PlatersMapCreatScript>();
        pmcs.write(posX,posZ);

        actNum = 0;

        HPBar.maxValue = maxHP;
        HPBar.minValue = 0;
        HPBar.value = nowHP;

        HPUI.text = nowHP + "/" + maxHP;
        LvUI.text = "Lv." + level;

        GameOver.enabled = false;

        sd = GameObject.Find("SelectedDisplay");
        sds = sd.GetComponent<SelectedDisplayScript>();
        //animator = GetComponent<Animator>();
        //chs = cardholder.GetComponent<CardHolderScript>();
        //chs.CardSelect();
    }

    // Update is called once per frame
    void Update()
    {   
        if(moveTime>0){
            if(!naname) transform.Translate(0f,0f,z*masu/moveFrame*Time.deltaTime);
            else transform.Translate(0f,0f,z*masu*Mathf.Sqrt(2f)/nanameFrame*Time.deltaTime);
            moveTime-=Time.deltaTime;
        }
        else if(turnTime>0){
            transform.Rotate(0f,x*45f*Time.deltaTime/turnFrame,0f);
            turnTime-=Time.deltaTime;
        }
        HPBar.maxValue = maxHP;
        //HPBar.minValue = 0;
        HPBar.value = nowHP;
        HPUI.text = nowHP + "/" + maxHP;
        LvUI.text = "Lv." + level;
        ItemAnimation();
        if(AllMotionStart){
            if(FirstWeaponAnime){
                if(sds.cg1[0] != null){
                    sds.cg1[0].WeaponsMotion();
                }
                FirstWeaponAnime = false;
                SecondWeaponAnime = true;
                TimeCount = 0f;
            }
            else if(SecondWeaponAnime && TimeCount >= 1.0f){
                if(sds.cg1[1] != null){
                    Debug.Log("こんにちは");
                    WeaponActivate(sds.cg1[1].type);
                    sds.cg1[1].WeaponsMotion();
                }
                SecondWeaponAnime = false;
                ThirdWeaponAnime = true;
                TimeCount = 0f;
            }
            else if(ThirdWeaponAnime && TimeCount >= 1.0f){
                if(sds.cg1[2] != null){
                    WeaponActivate(sds.cg1[2].type);
                    sds.cg1[2].WeaponsMotion();
                }
                ThirdWeaponAnime = false;
                FirstWeaponAnime = true;
                AllMotionStart = false;
                sds.cg1[0] = null; sds.cg1[1] = null; sds.cg1[2] = null;
            }
            TimeCount += Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.E)){
            AllWeaponInactivate();
        }
    }

    public bool action(){
        if(moveTime<=0 && turnTime<=0){
            transform.position = new Vector3(Mathf.Round(transform.position.x),0.5f,Mathf.Round(transform.position.z));
            transform.eulerAngles = new Vector3(0f,Mathf.Round(transform.eulerAngles.y/45)*45,0f);
            if (nowHP <= 0)
            {
                GameOver.enabled = true;
                return false;
            }

            if((x = Input.GetAxisRaw("Horizontal"))!=0){
                turnTime = turnFrame;
                if(naname) naname = false;
                else naname = true;
                if(x>0){
                    x=1;
                    dire = (dire+1)%8;
                }
                else{
                    x=-1;
                    dire = (dire+7)%8;
                }
            }
            
            else if((z = Input.GetAxisRaw("Vertical"))!=0){
                int toX;
                int toZ;
                if(dire>=1 && dire<=3) toX=1;
                else if(dire%4==0) toX=0;
                else toX=-1;
                if(dire>=3 && dire<=5) toZ=-1;
                else if(dire%4==2) toZ=0;
                else toZ=1;

                if(z>0) z=1;
                else z=-1;

                if(fs.available((int)transform.position.x+toX*(int)z,(int)transform.position.z+toZ*(int)z)){
                    if(!naname){
                        moveTime = moveFrame;
                        fs.moveChara(posX,posZ,(int)transform.position.x+toX*(int)z,(int)transform.position.z+toZ*(int)z,1);
                        posX = (int)transform.position.x+toX*(int)z;
                        posZ = (int)transform.position.z+toZ*(int)z;
                        pmcs.write(posX,posZ);
                        //chs.CardSelect();
                        actNum++;
                        if (actNum % 5 == 0 && nowHP < maxHP) nowHP++;
                        return true;
                    }
                    else{
                        if(fs.availableNaname((int)transform.position.x+toX*(int)z,(int)transform.position.z) &&fs.availableNaname((int)transform.position.x,(int)transform.position.z+toZ*(int)z)){
                            moveTime = nanameFrame;
                            fs.moveChara(posX,posZ,(int)transform.position.x+toX*(int)z,(int)transform.position.z+toZ*(int)z,1);
                            posX = (int)transform.position.x+toX*(int)z;
                            posZ = (int)transform.position.z+toZ*(int)z;
                            pmcs.write(posX,posZ);
                            //chs.CardSelect();
                            actNum++;
                            if (actNum % 5 == 0 && nowHP < maxHP) nowHP++;
                            return true;
                        }
                    }
                }
            }

            else if(Input.GetKeyDown(KeyCode.Return)){
                sds.UseCard();
                AllMotionStart = true;
                actNum++;
                if (actNum % 5 == 0 && nowHP < maxHP) nowHP++;
                return true;
            }
        }
        return false;
    }
    void ItemAnimation()
    {
        if(sd.transform.childCount != 0 && AnimeNumFlag)
        {
            Card = sd.GetComponentInChildren<CardScript>();
            WeaponActivate(Card.CardID[0]);
            AnimeNumFlag = false;
        }
        else if(sd.transform.childCount == 0 && !AllMotionStart)
        {
            AnimeNumFlag = true;
        }
    }
    void WeaponActivate(int type){
        if(type == 1){
            Knife.gameObject.SetActive(true);
        }
        else if(type == 2){
            Axe.gameObject.SetActive(true);
        }
        else if(type == 3){
            Sword.gameObject.SetActive(true);
        }
        else if(type == 4){          
            Arrow.gameObject.SetActive(true);         
            Bow.gameObject.SetActive(true);
        }
        else if(type == 5){
            Staff.gameObject.SetActive(true);
        }
    }
    void AllWeaponInactivate(){
        Sword.gameObject.SetActive(false);
        Knife.gameObject.SetActive(false);
        Axe.gameObject.SetActive(false);
        Arrow.gameObject.SetActive(false);
        Bow.gameObject.SetActive(false);
        Staff.gameObject.SetActive(false);
    }
    public void damage(int pow, int atkType)
    {
        double reduce = pow * pow / (pow + def);
        if(type != 1)
        {
            if (atkType < type) atkType += 4;
            if (atkType - type == 3) reduce *= 0.8;
            else if (atkType - type == 1) reduce *= 1.2;
        }
        nowHP -= (int)reduce;

        /*
        if (type == 1)
        {
            nowHP -= pow - def;
        }
        else
        {
            if (atkType < type) atkType += 4;
            if (atkType - type == 3) nowHP -= (int)((pow - def) * 0.8);
            else if (atkType - type == 1) nowHP -= (int)((pow - def) * 1.2);
            else nowHP -= pow - def;
        }
        こちらは計算式の初期案、上のはネットを参考にした
        */
    }

    public void getExp(int exp){
        EXP+=exp;
        if(EXP>=100){
            level+=1;
            atk += 1;
            def += 1;
            maxHP+=5;
        }//レベルアップ処理だけど適当なんであとでちゃんと直す
    }
}
