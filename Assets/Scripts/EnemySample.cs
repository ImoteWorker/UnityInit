using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySample : Enemy
{ 
    // Start is called before the first frame update

    public override void action(){
        look();
        if (frontPlayer) attackFront(7, 1);
        else move();
    }

    public override void Start(){
        setStatus(50);
    }

    protected override void move()
    {
        if (findPlayer) moveChase();
        else moveRandom();
    }
}
