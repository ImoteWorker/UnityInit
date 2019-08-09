namespace appleboy
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockFactoryScript : MonoBehaviour
{
    public GameObject Block, Floor;
    public int MAX_ROOM, MIN_ROOM, MERGIN;
    static public List<Division> divList = new List<Division>();
    static public int divListSize;
    public System.Random rd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        FillBlock();
        DivisionGenerator(0, (int)Floor.transform.localScale.x - 1, 0, (int)Floor.transform.localScale.z - 1);
        //Debug.Log(CreatRoad(0));
        bool f = (rd.Next(0,2)==0);
        
        SplitDivision(f);
    
        divListSize = divList.Count;
        for(int i = 0; i < divListSize; i++){
            RoomSet(divList[i]);
            //Debug.Log(CreatRoad(i));
        }
        //FillBlock();
    }
   void FillBlock()
   {
        float xfloorSize = Floor.transform.localScale.x - 1;
        float zfloorSize = Floor.transform.localScale.z - 1;
        Vector3 tp = new Vector3(-xfloorSize/2, 0.5f, -zfloorSize/2);
        for(int i = 0; i <= 49; i++)
        {
            tp.x = -24.5f + i;
            for(int j = 0; j <= 49; j++)
            {
                tp.z = -24.5f + j;
                //if(!(divList[0].Room.left < i && i < divList[0].Room.right && divList[0].Room.bottom < j && j < divList[0].Room.top)){
                    Instantiate(Block, tp, transform.rotation);
                //}
            }
        }
   }
    void DivisionGenerator(int left, int right, int bottom, int top)
    {
        Division div = new Division();
        div.Outer.SetRect(right, left, top, bottom);
        divList.Add(div);
    } 
    void SplitDivision(bool HorizontalOrVerticle)
    {
        int r, l;
        Division Parent = divList[divList.Count-1];
        divList.Remove(Parent);
        int width = Parent.Outer.right - Parent.Outer.left;
        int height = Parent.Outer.top - Parent.Outer.bottom;

        if(HorizontalOrVerticle)
        {
            if(Parent.Outer.width <= (MIN_ROOM + MERGIN * 2) * 2 + 1 || Parent.Outer.height <= (MIN_ROOM + MERGIN * 2) * 2 + 1){
                divList.Add(Parent);
                return;
            }
            r = Parent.Outer.right - MERGIN - MIN_ROOM * 2;
            l = Parent.Outer.left + MERGIN + MIN_ROOM * 2;
            int point = rd.Next(l, r);
            Division Child = new Division();
            Child.Outer.SetRect(point, Parent.Outer.left, Parent.Outer.top, Parent.Outer.bottom);
            Parent.Outer.left = point;
            if(Child.Outer.area < Parent.Outer.area){
                divList.Add(Child);
                divList.Add(Parent);
            }
            else{
                divList.Add(Parent);
                divList.Add(Child);
            }
        }
        else
        {
            if(Parent.Outer.height <= (MIN_ROOM + MERGIN * 2) * 2 + 1 || Parent.Outer.width <= (MIN_ROOM + MERGIN * 2) * 2 + 1){
                divList.Add(Parent);
                return;
            }
            r = Parent.Outer.top - MERGIN * 2 -MIN_ROOM;
            l = Parent.Outer.bottom + MERGIN * 2 + MIN_ROOM;
            int point = rd.Next(l, r);
            Division Child = new Division();
            Child.Outer.SetRect(Parent.Outer.right, Parent.Outer.left, point, Parent.Outer.bottom);
            Parent.Outer.bottom = point;
            if(Child.Outer.area < Parent.Outer.area){
                divList.Add(Child);
                divList.Add(Parent);
            }
            else{
                divList.Add(Parent);
                divList.Add(Child);
            }
        }
        SplitDivision(!HorizontalOrVerticle);
        
    }
    void RoomSet(Division div){

        //Debug.Log(div.Outer.width);
        //Debug.Log(div.Outer.height);
        int RoomWidthMax = div.Outer.width - MERGIN * 2;
        int RoomHeightMax = div.Outer.height - MERGIN * 2;

        //Debug.Log(RoomWidthMax);
        //Debug.Log(RoomHeightMax);
        int RoomWidth = rd.Next(MIN_ROOM, RoomWidthMax + 1);
        int RoomHeight = rd.Next(MIN_ROOM, RoomHeightMax + 1);

        RoomWidth = Mathf.Min(RoomWidth, MAX_ROOM);
        RoomHeight = Mathf.Min(RoomHeight, MAX_ROOM);

        int left = div.Outer.left + MERGIN + rd.Next(0, div.Outer.width - RoomWidth - MERGIN + 1); 
        int bottom = div.Outer.bottom + MERGIN + rd.Next(0, div.Outer.height - RoomHeight - MERGIN + 1);

        div.Room.SetRect(left + RoomWidth, left, bottom + RoomHeight, bottom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Division
{
    public class SubDivision
    {
        public int top, bottom, right, left; //width, height, area;
        public SubDivision()
        {
        }
        public void SetRect(int Right, int Left, int Top, int Bottom)
        {
            right = Right;
            left = Left;
            top = Top;
            bottom = Bottom;
            //width = Right - Left;
            //height = Top - Bottom;
            //area = width * height;
        }
        public int width{
           get {return right - left;}
        }
        public int height{
            get {return top - bottom;}
        }
        public int area{
            get {return width * height;}
        }
        
    }
    public SubDivision Outer;
    public SubDivision Room;
    public Division(){
        Outer = new SubDivision();
        Room = new SubDivision();
    }
    public void Debug(){
        
    }
}
    public class DivisionList
    {
        public List<Division> divList = new List<Division>();
    }

}