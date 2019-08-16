using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveFrame = 5f;
    public float masu=1f;
    public Vector2 Area = new Vector2(100,100);
    private int moving;
    private float moveTime;
    private float x;
    private float z;
    // Start is called before the first frame update
    void Start()
    {
        moving = 0;
        moveTime = 0;
        x=0;
        z=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTime<=0){
            /* 
            if(Input.GetAxisRaw("Horizontal")==1){
                moveTime = moveFrame;
                moving = 1;
                x=1;
            }
            else if(Input.GetAxisRaw("Horizontal")==-1){
                moveTime = moveFrame;
                moving = 2;
                x=-1;
            }
            else if(Input.GetAxisRaw("Vertical")==1){
                moveTime = moveFrame;
                moving = 3;
                z=1;
            }
            else if(Input.GetAxisRaw("Vertical")==-1){
                moveTime = moveFrame;
                moving = 4;
                z=-1;
            }
            else moving = 0;
            */
            if((x = Input.GetAxisRaw("Horizontal"))!=0){
                moveTime = moveFrame;
                if(x>0) x=1;
                else x=-1;
            }
            if((z = Input.GetAxisRaw("Vertical"))!=0){
                moveTime = moveFrame;
                if(z>0) z=1;
                else z=-1;
            }

            transform.position = new Vector3(Mathf.Round(transform.position.x),0.5f,Mathf.Round(transform.position.z));
        }
        
        else{
            /* 
            switch(moving){
                case 1:
                    transform.Translate(masu/moveFrame*Time.deltaTime,0,0);
                    break;
                case 2:
                    transform.Translate(-masu/moveFrame*Time.deltaTime,0,0);
                    break;
                case 3:
                    transform.Translate(0,0,masu/moveFrame*Time.deltaTime);
                    break;
                case 4:
                    transform.Translate(0,0,-masu/moveFrame*Time.deltaTime);
                    break;
                default: break;
            }
            */
            transform.Translate(masu/moveFrame*Time.deltaTime*x,0,masu/moveFrame*Time.deltaTime*z);
            moveTime-=Time.deltaTime;
        }
    }
}
