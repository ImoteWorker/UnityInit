using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySample : Enemy
{ 
    // Start is called before the first frame update

    public override void action(){
        moveRandom();
    }
    public override void Start(){
        setStatus(50);
    }
}
