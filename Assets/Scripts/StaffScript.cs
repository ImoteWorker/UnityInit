using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffScript : MonoBehaviour
{
    Animator animator;
    public static bool StaffMotionstart;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(StaffMotionstart){
            animator.SetBool("StaffAttack", true);
            StaffMotionstart = false;
        }
    }
    void SwingStart(){

    }
    void SwingEnd(){
        animator.SetBool("StaffAttack", false);
        this.gameObject.SetActive(false);
    }
}
