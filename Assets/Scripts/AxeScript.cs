using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour
{
    Animator animator;
    public static bool AxeMotionStart;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AxeMotionStart){
            animator.SetBool("AxeAttack",true);
            AxeMotionStart = false;
        }
    }
    void SwingStart(){

    }
    void SwingEnd(){
        animator.SetBool("AxeAttack", false);
        this.gameObject.SetActive(false);
    }
}
