using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    Animator animator;
    public static bool KnifeMotionStart;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(KnifeMotionStart){
            animator.SetBool("KnifeAttack",true);
            KnifeMotionStart = false;
        }
    }
    void SwingStart(){

    }
    void SwingEnd(){
        animator.SetBool("KnifeAttack", false);
        this.gameObject.SetActive(false);
    }
}
