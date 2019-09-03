using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject player;
    public FirstPersonMove fpm;

    public int turnNum;
    // Start is called before the first frame update
    void Start()
    {
        fpm = player.GetComponent<FirstPersonMove>();
        turnNum=1;
    }

    // Update is called once per frame
    void Update()
    {
        //2回行動はここで処理

        if(fpm.action() == false) return;//プレイヤーが行動してないときはここで終了
        turnNum++;
        //敵の行動

        //ターン終了時の効果など
    }
    
    public int getTurn(){
        return turnNum;
    }
}
