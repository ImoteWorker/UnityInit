using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    //Animation anim;
    Animator animator;
    public static bool SwordMotionStart;
    public static bool SwordMotionEnd;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animation>();
       // anim.Play("NormalSword");
       animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SwordMotionStart)
        {
            //anim.Play("SwingSword");
            SwordMotionEnd = false;
            animator.SetBool("Attack", true);
            SwordMotionStart = false;
        }
    }
    void SwingStart(){
        
    }
    void SwingEnd(){
        animator.SetBool("Attack", false);
        this.gameObject.SetActive(false);
        SwordMotionEnd = true;
    }
}
