using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject player;
    public GameObject map;
    public Player ps;
    public PlatersMapCreatScript pmcs;
    public Floor floor;
    public int turnNum;
    // Start is called before the first frame update
    void Start()
    {
        ps = player.GetComponent<Player>();
        pmcs = map.GetComponent<PlatersMapCreatScript>();
        floor = GetComponent<Floor>();
        turnNum=1;
    }

    // Update is called once per frame
    void Update()
    {
        
        //pmcs.write();
        if(ps.action() == false) return;//プレイヤーが行動してないときはここで終了
        turnNum++;
        //pmcs.write();
        //敵の行動
        foreach(GameObject en in floor.enemies){
            en.GetComponent<Enemy>().action();
        }

        //ターン終了時の効果など
    }
    
    public int getTurn(){
        return turnNum;
    }
}
