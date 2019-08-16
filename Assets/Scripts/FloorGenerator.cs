namespace MyDungeon
{
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Floor;
    public int AreaX=32;//全体のx方向サイズ
    public int AreaZ=32;//全体のz方向サイズ
    public int minX=3;//部屋のx方向最小
    public int minZ=3;//部屋のz方向最小
    public int minRoom=3;//部屋の最小個数※大きいと死ぬ
    public int maxRoom=5;//通路の生成試行回数最大
    public float branchRate=0.5f;//1部屋からの通路生成確率
    int[,] Area;
    List<Vector2Int> Zone = new List<Vector2Int>();//区画の左上位置
    List<Vector2Int> Gen = new List<Vector2Int>();//通路生成開始位置
    SortedSet<int> SetX = new SortedSet<int>();//すでに生成した縦の通路のx
    SortedSet<int> SetZ = new SortedSet<int>();//すでに生成した横の通路のz
    public static int[,] WallLocation = new int[100, 100];
    // Start is called before the first frame update
    void Start()
    {
        generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cut(int[,] field, int dire, int num){
        bool end=false;
        for(int i=0;end==false;i++){
            switch(dire){
                case 0:
                    if(field[num,i+1]!=0){
                        end = true;
                        Zone.Add(new Vector2Int(num+1,1));
                        Gen.Add(new Vector2Int(num,1));
                        SetX.Add(num);
                        break;
                    }
                    field[num,i+1] = 2;
                    break;
                case 1:
                    if(field[num,AreaZ-i]!=0){
                        end = true;
                        Zone.Add(new Vector2Int(num+1,AreaZ-i+1));
                        Gen.Add(new Vector2Int(num,AreaZ));
                        SetX.Add(num);
                        break;
                    }
                    field[num,AreaZ-i] = 2;
                    break;
                case 2:
                    if(field[i+1,num]!=0){
                        end = true;
                        Zone.Add(new Vector2Int(1,num+1));
                        Gen.Add(new Vector2Int(1,num));
                        SetZ.Add(num);
                        break;
                    }
                    field[i+1,num] = 2;
                    break;
                case 3:
                    if(field[AreaX-i,num]!=0){
                        end = true;
                        Zone.Add(new Vector2Int(AreaX-i+1,num+1));
                        Gen.Add(new Vector2Int(AreaX,num));
                        SetZ.Add(num);
                        break;
                    }
                    field[AreaX-i,num] = 2;
                    break;
                default: break;
            }
        }

    }
    //From... 0:Up, 1:Down, 2:Left, 3:Right
    //共用通路生成
    public void makeRoom(int[,] field, Vector2Int v){
        int AvalableX;
        int AvalableZ;
        int checkX;
        int checkZ;
        for(int i=0;;i++){
            if(field[v.x+i,v.y]!=0){
                AvalableX=i;
                checkX=field[v.x+i,v.y];
                break;
            }
        }
        for(int i=0;;i++){
            if(field[v.x,v.y+i]!=0){
                AvalableZ=i;
                checkZ=field[v.x,v.y+i];
                break;
            }
        }
        int sizeX = UnityEngine.Random.Range(minX,AvalableX-1);
        int sizeZ = UnityEngine.Random.Range(minZ,AvalableZ-1);
        int edgeX = (int)v.x+UnityEngine.Random.Range(1,AvalableX-sizeX);
        int edgeZ = (int)v.y+UnityEngine.Random.Range(1,AvalableZ-sizeZ);
        for(int i=0;i<sizeX;i++){
            for(int j=0;j<sizeZ;j++){
                field[edgeX+i,edgeZ+j] = 1;
            }
        }
        //
        int[] makeBranch = new int[4];
        for(int i=0;i<4;i++){
            makeBranch[i]=2;
        }
        if((int)v.y==1) makeBranch[0]=0;
        if(checkZ==9) makeBranch[1]=0;
        if((int)v.x==1) makeBranch[2]=0;
        if(checkX==9) makeBranch[3]=0;
        int choice;
        while(true){
            choice = UnityEngine.Random.Range(0,4);
            if(makeBranch[choice]!=0){
                makeBranch[choice]=1;
                break;
            }
        }
        for(int i=0;i<4;i++){
            if(makeBranch[i]!=2) continue;
            if(UnityEngine.Random.Range(0f,1f)<branchRate) makeBranch[i]=1;
            else makeBranch[i]=0;
        }
        int point;
        if(makeBranch[0]==1){
            point = edgeX+UnityEngine.Random.Range(0,sizeX);
            for(int i=0;;i++){
                field[point,edgeZ-i]=3;
                if(field[point,edgeZ-i-1]!=0){
                    field[point,edgeZ-i-1]=3;
                    break;
                }
            }
        }
        if(makeBranch[1]==1){
            point = edgeX+UnityEngine.Random.Range(0,sizeX);
            for(int i=0;;i++){
                field[point,edgeZ+sizeZ+i]=3;
                if(field[point,edgeZ+sizeZ+i+1]!=0){
                    field[point,edgeZ+sizeZ+i+1]=3;
                    break;
                }
            }
        }
        if(makeBranch[2]==1){
            point = edgeZ+UnityEngine.Random.Range(0,sizeZ);
            for(int i=0;;i++){
                field[edgeX-i,point]=3;
                if(field[edgeX-i-1,point]!=0){
                    field[edgeX-i-1,point]=3;
                    break;
                }
            }
        }
        if(makeBranch[3]==1){
            point = edgeZ+UnityEngine.Random.Range(0,sizeZ);
            for(int i=0;;i++){
                field[edgeX+sizeX+i,point]=3;
                if(field[edgeX+sizeX+i+1,point]!=0){
                    field[edgeX+sizeX+i+1,point]=3;
                    break;
                }
            }
        }
    }
    //通路の間に部屋生成
    public void cutBranch(int[,] field,Vector2Int v){
        bool buryMode = true;
        bool cont = true;
        if(v.x==1){
            for(int i=0;cont;i++){
                if(field[v.x+i+1,v.y]==0||field[v.x+i+1,v.y]==9){
                    cont = false;
                    if(field[v.x+i+1,v.y]==0 && !buryMode){
                        field[v.x+i,v.y]=3;
                    }
                }
                if(buryMode && field[v.x+i,v.y]==3){
                    buryMode=false;
                }
                if(buryMode && cont) field[v.x+i,v.y]=0;
            }
        }
        if(v.x==AreaX){
            for(int i=0;cont;i++){
                if(field[v.x-i-1,v.y]==0||field[v.x-i-1,v.y]==9){
                    cont = false;
                    if(field[v.x-i-1,v.y]==0 && !buryMode){
                        field[v.x-i,v.y]=3;
                    }
                }
                if(buryMode && field[v.x-i,v.y]==3){
                    buryMode=false;
                }
                if(buryMode && cont) field[v.x-i,v.y]=0;
            }
        }
        if(v.y==1){
            for(int i=0;cont;i++){
                if(field[v.x,v.y+i+1]==0||field[v.x,v.y+i+1]==9){
                    cont = false;
                    if(field[v.x,v.y+i+1]==0 && !buryMode){
                        field[v.x,v.y+i]=3;
                    }
                }
                if(buryMode && field[v.x,v.y+i]==3){
                    buryMode=false;
                }
                if(buryMode && cont) field[v.x,v.y+i]=0;
            }
        }
        if(v.y==AreaZ){
            for(int i=0;cont;i++){
                if(field[v.x,v.y-i-1]==0||field[v.x,v.y-i-1]==9){
                    cont = false;
                    if(field[v.x,v.y-i-1]==0 && !buryMode){
                        field[v.x,v.y-i]=3;
                    }
                }
                if(buryMode && field[v.x,v.y-i]==3){
                    buryMode=false;
                }
                if(buryMode && cont) field[v.x,v.y-i]=0;
            }
        }
    }
    //余計な通路の削除
    public void generate(){
        Area = new int[AreaX+2,AreaZ+2];
        for(int i=1;i<AreaX+1;i++){
            for(int j=1;j<AreaZ+1;j++){
                Area[i,j] = 0;
            }
        }
        for(int i=0;i<AreaX+2;i++){
            Area[i,0]=9;
            Area[i,AreaZ+1]=9;
        }
        for(int i=0;i<AreaZ+2;i++){
            Area[0,i]=9;
            Area[AreaX+1,i]=9;
        }
        //全体を壁に
        Zone.Add(new Vector2Int(1,1));

        int times = UnityEngine.Random.Range(minRoom,maxRoom);
        int dire = UnityEngine.Random.Range(0,2);
        dire*=2;
        int num;
        bool line;
        int lineNum=0;
        int mx = minX+2;
        int mz = minZ+2;
        
        for(int i=0;i<times || lineNum<minRoom;i++){
            line=true;
            dire = (dire+1)%4;
            if(dire<=1)num=UnityEngine.Random.Range(mx,AreaX-4);
            else num=UnityEngine.Random.Range(mz,AreaZ-4);
            int x=0,z=0;
            switch(dire){
                case 0:
                    x=num;
                    z=1;
                    break;
                case 1:
                    x=num;
                    z=AreaZ;
                    break;
                case 2:
                    x=1;
                    z=num;
                    break;
                case 3:
                    x=AreaX;
                    z=num;
                    break;
                default: break;
            }
            //通路生成開始位置の設定
            if(dire<=1){
                for(int j=-mx;j<=mx;j++){
                    if(Area[x+j,z]!=0){
                        line=false;
                        if(i<=4) dire = (dire+3)%4;
                        break;
                    }
                }
                if(SetX.Contains(num)){
                    line=false;
                    if(i<=4) dire = (dire+3)%4;
                }
            }
            else{
                 for(int j=-mz;j<=mz;j++){
                    if(Area[x,z+j]!=0){
                        line=false;
                        if(i<=4) dire = (dire+3)%4;
                        break;
                    }   
                }
                if(SetZ.Contains(num)){
                    line=false;
                    if(i<=4) dire = (dire+3)%4;
                }
            }
            //部屋が作れるようにするためのチェック
            if(line){
                if(lineNum==0){
                    if(dire==1) Gen.Add(new Vector2Int(num,1));
                    if(dire==3) Gen.Add(new Vector2Int(1,num));
                }
                //初回のみ開始位置の逆側も保存
                cut(Area,dire,num);
                lineNum++;
            }
            //通路生成
        }
        Zone = Zone.OrderBy(a=>Guid.NewGuid()).ToList();//ランダムに並び替え
        int roomNum = UnityEngine.Random.Range(minRoom,Zone.Count+1);
        
        for(int i=0;i<roomNum;i++){
            makeRoom(Area,Zone[i]);
        }
        //区画をランダム選択し部屋生成
        lineNum = Gen.Count();
        for(int i=0;i<lineNum;i++){
            cutBranch(Area,Gen[lineNum-1-i]);
        }
        for(int i = 0; i < AreaX;i++){
            for(int j = 0; j < AreaZ; j++){
                WallLocation[i, j] = 0;
            }
        }

        //最後に生成した通路から枝切り
        for(int i=0;i<AreaX+2;i++){
            for(int j=0;j<AreaZ+2;j++){
                if(Area[i,j]==0 || Area[i,j]==9){
                    Instantiate(Wall,transform.position+(new Vector3(i,0,j)),transform.rotation);
                    WallLocation[i, j] = 1;
                }
            }
        }
        //インスタンスとして配置
    }
    //壁:0 部屋:1 通路:2 部屋からの通路:3 外壁:9
}
}