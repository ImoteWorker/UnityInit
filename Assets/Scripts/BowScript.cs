using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    Animator animator;
    public static bool BowMotionStart;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BowMotionStart){
            animator.SetBool("BowAttack", true);
            BowMotionStart = false;
        }
    }
    void SwingStart(){

    }
    void SwingEnd(){
        animator.SetBool("BowAttack", false);
        this.gameObject.SetActive(false);
    }
}
