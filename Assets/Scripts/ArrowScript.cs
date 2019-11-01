using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    Animator animator;
    public static bool ArrowMotionStart;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ArrowMotionStart){
            animator.SetBool("ArrowAttack", true);
            ArrowMotionStart = false;
        }
    }
    void SwingStart(){

    }
    void SwingEnd(){
        animator.SetBool("ArrowAttack", false);
        this.gameObject.SetActive(false);
    }
}
