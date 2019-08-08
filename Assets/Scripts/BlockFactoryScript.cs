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
    // Start is called before the first frame update
    void Start()
    {
        FillBlock();
        DivisionGenerator(2, (int)Floor.transform.localScale.x - 3, 2, (int)Floor.transform.localScale.z - 3);
        bool f = (Random.Range(0,2)==0);
        for(int i = 0; i < 5; i++){
            SplitDivision(f);
            f = !f;
        }
        divListSize = divList.Count;
        for(int i = 0; i < divListSize; i++){
            RoomSet(divList[i]);
        }
        //FillBlock();
    }
   void FillBlock()
   {
        float xfloorSize = Floor.transform.localScale.x - 1;
        float zfloorSize = Floor.transform.localScale.z - 1;
        Vector3 tp = new Vector3(-xfloorSize/2, 0.5f, -zfloorSize/2);
        for(int i = 0; i <= xfloorSize; i++)
        {
            tp.x = -xfloorSize/2 + i;
            for(int j = 0; j <= zfloorSize; j++)
            {
                tp.z = -zfloorSize/2 + j;
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
        //区画の幅が部屋の辺の最小値よりも小さいならそれ以上分割しない
        if(HorizontalOrVerticle && Parent.Outer.width <= MIN_ROOM)
        {
           //divList.Add(Parent);
           return;
        }
        if(!HorizontalOrVerticle && Parent.Outer.height <= MIN_ROOM)
        {
           //divList.Add(Parent);
           return;
        }
        Division Child = new Division();
        //区間の幅を部屋が作れるように余裕を持たせて設定する
        if(HorizontalOrVerticle)
        {
            r = Parent.Outer.right - MIN_ROOM - MERGIN;
            l = Parent.Outer.left + MIN_ROOM + MERGIN;
            int point = Random.Range(l, r);
            Child.Outer.SetRect(point, Parent.Outer.left, Parent.Outer.top, Parent.Outer.bottom);
            Parent.Outer.left = point;
        }
        else
        {
            r = Parent.Outer.top - MIN_ROOM - MERGIN;
            l = Parent.Outer.bottom + MIN_ROOM + MERGIN;
            int point = Random.Range(l, r);
            Child.Outer.SetRect(Parent.Outer.right, Parent.Outer.left, point, Parent.Outer.bottom);
            Parent.Outer.bottom = point;
        }
        divList.Add(Child);
        divList.Add(Parent);
    }
    void RoomSet(Division div){

        int RoomWidthMax = div.Outer.width - MERGIN * 2;
        int RoomHeightMax = div.Outer.height - MERGIN * 2;

        int RoomWidth = Random.Range(MIN_ROOM, RoomWidthMax + 1);
        int RoomHeight = Random.Range(MIN_ROOM, RoomHeightMax + 1);


        RoomWidth = Mathf.Min(RoomWidth, MAX_ROOM);
        RoomHeight = Mathf.Min(RoomHeight, MAX_ROOM);

        int left = div.Outer.left + MERGIN + Random.Range(0, div.Outer.width - RoomWidth - MERGIN + 1); 
        int bottom = div.Outer.bottom + MERGIN + Random.Range(0, div.Outer.height - RoomHeight - MERGIN + 1);

        div.Room.SetRect(left + RoomWidth, left, bottom + RoomHeight, bottom);
    }
    public Vector4 CreatRoom(int i){
        return new Vector4(divList[i].Room.left, divList[i].Room.right, divList[i].Room.bottom, divList[i].Room.top);
    }

    public int ListSize(){
        return divListSize;
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
        public int top, bottom, right, left, width, height, area;
        public SubDivision()
        {
        }
        public void SetRect(int Right, int Left, int Top, int Bottom)
        {
            right = Right;
            left = Left;
            top = Top;
            bottom = Bottom;
            width = right - left;
            height = top - bottom;
            area = width * height;
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
}