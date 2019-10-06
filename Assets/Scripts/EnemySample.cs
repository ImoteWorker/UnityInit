using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySample : Enemy
{ 
    // Start is called before the first frame update

    public override void action(){
        look();
        if (findPlayer) moveChase();
        else moveRandom();
    }
    protected override void attack(){
        
    }

    public override void Start(){
        setStatus(50);
    }
}
