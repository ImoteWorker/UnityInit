using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMove : MonoBehaviour
{
    public float moveFrame = 5f;
    public float nanameFrame = 7f;
    public float turnFrame = 5f;
    public float masu=1f;
    public Vector2 Area = new Vector2(100,100);
    private bool moving;
    private float moveTime;
    private bool naname;
    private bool turning;
    private float turnTime;
    private float x;
    private float z;
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        moveTime = 0;
        naname = false;
        turning = false;
        turnTime = 0;
        x=0;
        z=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTime<=0 && turnTime<=0){
            
            if((x = Input.GetAxisRaw("Horizontal"))!=0){
                turnTime = turnFrame;
                if(naname) naname = false;
                else naname = true;
                if(x>0) x=1;
                else x=-1;
            }
            
            else if((z = Input.GetAxisRaw("Vertical"))!=0){
                moveTime = moveFrame;
                if(z>0) z=1;
                else z=-1;
            }

            transform.position = new Vector3(Mathf.Round(transform.position.x),0.5f,Mathf.Round(transform.position.z));
            transform.eulerAngles = new Vector3(0f,Mathf.Round(transform.eulerAngles.y/45)*45,0f);
        }
        
        else if(moveTime>0){
            //transform.Translate(masu/moveFrame*Time.deltaTime*x,0,masu/moveFrame*Time.deltaTime*z);
            if(!naname) transform.Translate(0f,0f,z*masu/moveFrame*Time.deltaTime);
            else transform.Translate(0f,0f,z*masu*Mathf.Sqrt(2f)/nanameFrame*Time.deltaTime);
            moveTime-=Time.deltaTime;
        }
        else if(turnTime>0){
            transform.Rotate(0f,x*45f*Time.deltaTime/turnFrame,0f);
            turnTime-=Time.deltaTime;
        }
    }
}
