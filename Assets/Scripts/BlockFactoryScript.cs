namespace MyDungeon
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockFactoryScript : MonoBehaviour
{
    public GameObject Block;
    public int MAX_ROOM, MIN_ROOM, MERGIN, FloorX = 50, FloorZ = 50;
    static public List<Division> divList = new List<Division>();
    static public List<Road> RoadList = new List<Road>();  
    public System.Random rd = new System.Random();
    static public int[,] WallLocation = new int[100, 100];
    // Start is called before the first frame update
    void Start()
    {
        FillBlock();
        DivisionGenerator(0, (int)FloorX - 1, 0, (int)FloorZ - 1);
        //Debug.Log(CreatRoad(0));
        bool f = (rd.Next(0,2)==0);
        
        SplitDivision(f);
    
        for(int i = 0; i < divList.Count; i++){
            RoomSet(divList[i]);
            //Debug.Log(CreatRoad(i));
        }
        CreatRoad();
        //FillBlock();
    }
   void FillBlock()
   {
        float xfloorSize = FloorX - 1;
        float zfloorSize = FloorZ - 1;
        Vector3 tp = new Vector3(-xfloorSize/2, 0.5f, -zfloorSize/2);
        for(int i = 0; i <= (int)xfloorSize; i++)
        {
            tp.x = -xfloorSize/2 + (float)i;
            for(int j = 0; j <= (int)zfloorSize; j++)
            {
                tp.z = -zfloorSize/2 + (float)j;
                Instantiate(Block, tp, transform.rotation);
                WallLocation[i, j] = 1;
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

        int left = div.Outer.left + MERGIN + rd.Next(0, div.Outer.width - RoomWidth - MERGIN * 2 + 1); 
        int bottom = div.Outer.bottom + MERGIN + rd.Next(0, div.Outer.height - RoomHeight - MERGIN * 2 + 1);

        div.Room.SetRect(left + RoomWidth, left, bottom + RoomHeight, bottom);
    }
    public void CreatRoad(){
        for(int i = 1; i < divList.Count; i++){
            if(divList[i-1].Outer.right == divList[i].Outer.left){
                Road road1 = new Road();
                road1.HorizontalOrVerticle = true;
                road1.start = rd.Next(divList[i-1].Room.bottom, divList[i-1].Room.top + 1);
                road1.right = divList[i-1].Outer.right;
                road1.left = divList[i-1].Room.right;
                Road road2 = new Road();
                road2.HorizontalOrVerticle = true;
                road2.start = rd.Next(divList[i].Room.bottom, divList[i].Room.top + 1);
                road2.right = divList[i].Room.left;
                road2.left = divList[i].Outer.left;
                Road road3 = new Road();
                road3.HorizontalOrVerticle = false;
                road3.start = divList[i].Outer.left;
                road3.bottom = Mathf.Min(road1.start, road2.start);
                road3.top = Mathf.Max(road1.start, road2.start);
                RoadList.Add(road1); 
                RoadList.Add(road2); 
                RoadList.Add(road3);
            }
            else if(divList[i-1].Outer.left == divList[i].Outer.right){
                Road road1 = new Road();
                road1.HorizontalOrVerticle = true;
                road1.start = rd.Next(divList[i-1].Room.bottom, divList[i-1].Room.top + 1);
                road1.left = divList[i-1].Outer.left;
                road1.right = divList[i-1].Room.left;
                Road road2 = new Road();
                road2.HorizontalOrVerticle = true;
                road2.start = rd.Next(divList[i].Room.bottom, divList[i].Room.top + 1);
                road2.left = divList[i].Room.right;
                road2.right = divList[i].Outer.right;
                Road road3 = new Road();
                road3.HorizontalOrVerticle = false;
                road3.start = divList[i].Outer.right;
                road3.bottom = Mathf.Min(road1.start, road2.start);
                road3.top = Mathf.Max(road1.start, road2.start);
                RoadList.Add(road1); 
                RoadList.Add(road2); 
                RoadList.Add(road3);
            }
            else if(divList[i-1].Outer.top == divList[i].Outer.bottom){
                Road road1 = new Road();
                road1.HorizontalOrVerticle = false;
                road1.start = rd.Next(divList[i-1].Room.left, divList[i-1].Room.right + 1);
                road1.top = divList[i-1].Outer.top;
                road1.bottom = divList[i-1].Room.top;
                Road road2 = new Road();
                road2.HorizontalOrVerticle = false;
                road2.start = rd.Next(divList[i].Room.left, divList[i].Room.right + 1);
                road2.top = divList[i].Room.bottom;
                road2.bottom = divList[i].Outer.bottom;
                Road road3 = new Road();
                road3.HorizontalOrVerticle = true;
                road3.start = divList[i].Outer.bottom;
                road3.left = Mathf.Min(road1.start, road2.start);
                road3.right = Mathf.Max(road1.start, road2.start);
                RoadList.Add(road1); 
                RoadList.Add(road2); 
                RoadList.Add(road3);
            }
            else if(divList[i-1].Outer.bottom == divList[i].Outer.top){
                Road road1 = new Road();
                road1.HorizontalOrVerticle = false;
                road1.start = rd.Next(divList[i-1].Room.left, divList[i-1].Room.right + 1);
                road1.bottom = divList[i-1].Outer.bottom;
                road1.top = divList[i-1].Room.bottom;
                Road road2 = new Road();
                road2.HorizontalOrVerticle = false;
                road2.start = rd.Next(divList[i].Room.left, divList[i].Room.right + 1);
                road2.bottom = divList[i].Room.top;
                road2.top = divList[i].Outer.top;
                Road road3 = new Road();
                road3.HorizontalOrVerticle = true;
                road3.start = divList[i].Outer.top;
                road3.left = Mathf.Min(road1.start, road2.start);
                road3.right = Mathf.Max(road1.start, road2.start);
                RoadList.Add(road1); 
                RoadList.Add(road2); 
                RoadList.Add(road3);
            }

        }
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
public class Road
{        
    public bool HorizontalOrVerticle;
    public int left, right, bottom, top, start, end;
}

}