namespace appleboy
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactoryScript : MonoBehaviour
{
    public GameObject Block, Floor;
    public int MAX_ROOM = 15, MIN_ROOM = 5, MERGIN = 3;
    public List<Division> divList = new List<Division>();
    // Start is called before the first frame update
    void Start()
    {
        
        DivisionGenerator(0, 29, 0, 29);
        bool f = (Random.Range(0,2)==0);
        for(int i = 0; i < 5; i++){
            SplitDivision(f);
            f = !f;
        }
        for(int i = 0; i < divList.Count; i++){
            RoomSet(divList[i]);
        }
        FillBlock();
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
                if(!(divList[0].Room.left < i && i < divList[0].Room.right && divList[0].Room.bottom < j && j < divList[0].Room.top)){
                    Instantiate(Block, tp, transform.rotation);
                }
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
          // divList.Add(Parent);
           return;
        }
        //区間の幅を部屋が作れるように余裕を持たせて設定する
        if(HorizontalOrVerticle)
        {
            r = Parent.Outer.right - MAX_ROOM - MERGIN;
            l = Parent.Outer.left + MAX_ROOM + MERGIN;
        }
        else
        {
            r = Parent.Outer.top - MAX_ROOM - MERGIN;
            l = Parent.Outer.bottom + MAX_ROOM + MERGIN;
        }
        int length = r - l + 1;
        int point = l + Random.Range(0, length);
        Division Child = new Division();
        if(HorizontalOrVerticle)
        {
            Child.Outer.SetRect(point, Parent.Outer.left, Parent.Outer.top, Parent.Outer.bottom);
            Parent.Outer.left = point;
        }
        else
        {
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
    void CreatRoom(Division div){
        
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