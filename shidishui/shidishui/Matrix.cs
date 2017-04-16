using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shidishui
{
    using System;
    
    public class Matrix
    {

        /// <summary>
        /// 枚举类型，用于代表方向
        /// </summary>
        public enum DIRECTION { _none, _left = 1, _right = 2, _up = 3, _down = 4 };
        /// <summary>
        /// 水滴信息结构体
        /// </summary>        
        public struct DropInfo
        {
            public int SN;//次序，从1开始
            public DIRECTION FromDirection;
            /*   DropInfo(int Sn,DIRECTION Fromdirection)
               {
                   SN = Sn;//
                   FromDirection = Fromdirection;
               }*/
        }
        /// <summary>
        /// 坐标信息结构体
        /// </summary>
        public struct CoordInfo
        {
            public int row;	 //横坐标
            public int col;	 //纵坐标
            public int value;	 //当前值
            public int dropnum; //已接收水滴数
            public bool shot;//当前是否接收水滴
            public DropInfo[] dropinfo;//接收的水滴的信息，一个位置最多接收10滴水？？
            /*        public CoordInfo()
                    {//默认构造函数
                        row = col = -1;
                        value = 0;
                        dropnum = 0;
                        shot = false;
                    }*/
        }
        /// <summary>
        /// 坐标结构体
        /// </summary>
        public struct Coord
        {
            public int row ;
            public int col ;
        }
        /// <summary>
        /// 判断坐标是否有效函数
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static bool coord_valid(int row, int col)
        {
            if (row >= 0 && row <= 5 && col >= 0 && col <= 5) return true;
            else return false;

        }

        public Form3 f3;
        int Row, Col;//横、纵坐标
        int[,] A = new int[6, 6];//矩阵
        int ProgressCount;//记录水滴飞溅的进展
        bool lastshot;
        int burstnum;//连锁爆炸的个数，为3时药水数加1
        int leftdrops;//剩余药水滴数
        CoordInfo[] coordinfo = new CoordInfo[36];//将矩阵用一维数组存储
        CoordInfo[] notblank = new CoordInfo[36];//有水球的坐标位置
        int Notblanknum;//当前水球数
        int Depth;//当前深度

        

        /*        Matrix* Child;//指向后继的指针
                Matrix* Father;//指向父亲的指针*/
        /// <summary>
        /// 用一个一维数组存储当前矩阵元素
        /// </summary>
        void transf()
        {
            int n;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    n = 6 * i + j;
                    coordinfo[n].row = i;
                    coordinfo[n].col = j;
                    coordinfo[n].value = A[i, j];
/*                    coordinfo[n].dropinfo = new DropInfo[18];
                    for (int k = 0; k < 18; k++)
                    {
                        coordinfo[n].dropinfo[k].SN = 0;
                        coordinfo[n].dropinfo[k].FromDirection = DIRECTION._none;
                    }*/
                }
        }
/// <summary>
/// 水滴飞溅函数
/// </summary>
/// <param name="row"></param>
/// <param name="col"></param>
/// <param name="ProgressCount"></param>
        void splash(int row, int col, int ProgressCount)
        {
            int num;
            int n;
            n = 6 * row + col;
            if (coord_valid(row, col - 1))
            {
                num = ++coordinfo[n - 1].dropnum;
                coordinfo[n - 1].dropinfo[num - 1].SN = ProgressCount;//接收的水滴次序
                coordinfo[n - 1].dropinfo[num - 1].FromDirection = DIRECTION._right;//方向来自右边	
                coordinfo[n - 1].shot = true;//当前接收到了水滴
            }
            if (coord_valid(row, col + 1))
            {
                num = ++coordinfo[n + 1].dropnum;
                coordinfo[n + 1].dropinfo[num - 1].SN = ProgressCount;//接收的水滴次序
                coordinfo[n + 1].dropinfo[num - 1].FromDirection = DIRECTION._left;//方向来自左边
                coordinfo[n + 1].shot = true;	//当前接收到了水滴
            }
            if (coord_valid(row - 1, col))
            {
                num = ++coordinfo[n - 6].dropnum;
                coordinfo[n - 6].dropinfo[num - 1].SN = ProgressCount;//接收的水滴次序
                coordinfo[n - 6].dropinfo[num - 1].FromDirection = DIRECTION._down;//方向来自下边		
                coordinfo[n - 6].shot = true;	//当前接收到了水滴
            }

            if (coord_valid(row + 1, col))
            {
                num = ++coordinfo[n + 6].dropnum;
                coordinfo[n + 6].dropinfo[num - 1].SN = ProgressCount;//接收的水滴次序
                coordinfo[n + 6].dropinfo[num - 1].FromDirection = DIRECTION._up;//方向来自下边		
                coordinfo[n + 6].shot = true;//当前接收到了水滴
            }
        }
        public bool burst(int x, int y, int NOTBLANKNUM_CONST, CoordInfo Coordinfo, int Place)
        {
            int row, col;
            int n;
            int num;
            int drops;
            row = Coordinfo.row;
            col = Coordinfo.col;
            n = 6 * row + col;
            if (row < 0 || row > 5 || col < 0 || col > 5)
            {
                return false;
            }

            if (A[row, col] == 0)
            {
                if (coordinfo[n].dropnum <= ProgressCount)
                    coordinfo[n].shot = Coordinfo.shot = false;
                switch (Coordinfo.dropinfo[Place].FromDirection)
                {
                    case DIRECTION._left:
                        {
                            if (coord_valid(row, col + 1))
                            {
                                num = ++coordinfo[n + 1].dropnum;
                                coordinfo[n + 1].dropinfo[num - 1].SN = ProgressCount + 1;
                                coordinfo[n + 1].dropinfo[num - 1].FromDirection = DIRECTION._left;//方向来自左边		
                                coordinfo[n + 1].shot = true;//当前接收到了水滴

                            }
                            break;
                        }
                    case DIRECTION._right:
                        {
                            if (coord_valid(row, col - 1))
                            {
                                num = ++coordinfo[n - 1].dropnum;
                                coordinfo[n - 1].dropinfo[num - 1].SN = ProgressCount + 1;
                                coordinfo[n - 1].dropinfo[num - 1].FromDirection = DIRECTION._right;//方向来自右边		
                                coordinfo[n - 1].shot = true;//当前接收到了水滴

                            }
                            break;
                        }
                    case DIRECTION._up:
                        {
                            if (coord_valid(row + 1, col))
                            {
                                num = ++coordinfo[n + 6].dropnum;
                                coordinfo[n + 6].dropinfo[num - 1].SN = ProgressCount + 1;
                                coordinfo[n + 6].dropinfo[num - 1].FromDirection = DIRECTION._up;//方向来自上边	
                                coordinfo[n + 6].shot = true;//当前接收到了水滴

                            }
                            break;
                        }
                    case DIRECTION._down:
                        {
                            if (coord_valid(row - 1, col))
                            {
                                num = ++coordinfo[n - 6].dropnum;
                                coordinfo[n - 6].dropinfo[num - 1].SN = ProgressCount + 1;
                                coordinfo[n - 6].dropinfo[num - 1].FromDirection = DIRECTION._down;//方向来自下边		
                                coordinfo[n - 6].shot = true;//当前接收到了水滴

                            }
                            break;
                        }

                    default:
                        {//此情况为起爆点在第一轮过程中遭遇的情况

                            break;
                        }
                }
                return false;
            }
            if (A[row, col] < 4)
            {
                drops = coordinfo[n].value = A[row, col] = ++Coordinfo.value;
                for (int i = 0; i < NOTBLANKNUM_CONST; i++)
                    if ((notblank[i].row == row) && (notblank[i].col == col) && ((x != row) || (y != col))) notblank[i].value = Coordinfo.value;
                if (coordinfo[n].dropnum <= ProgressCount)
                    coordinfo[n].shot = Coordinfo.shot = false;
 //               Picture_Refresh(row, col, drops);
                return false;
            }
            if (A[row, col] == 4)
            {
                burstnum++;
                if (burstnum == 3)
                {
                    leftdrops++;
                    burstnum = 0;
                }
                drops = coordinfo[n].value = A[row, col] = Coordinfo.value = 0;
                for (int i = 0; i < NOTBLANKNUM_CONST; i++)
                    if ((notblank[i].row == row) && (notblank[i].col == col) && ((x != row) || (y != col)))
                    {
                        notblank[i].value = 0;
                        Notblanknum--;
                    }
                if (coordinfo[n].dropnum <= ProgressCount)
                    coordinfo[n].shot = Coordinfo.shot = false;
  //              Thread.Sleep(250);
  //              picturebox[row, col].Image = Resource1.Boomb;
  //              picturebox[row, col].Refresh();
  //              Picture_Refresh(row, col, drops);
                splash(row, col, ProgressCount + 1);//调用水滴飞溅函数		
                return true;
            }
            return false;

        }
/// <summary>
/// 起爆函数
/// </summary>
/// <param name="Coordinfo"></param>
        void burst0(int NOTBLANKNUM_CONST, CoordInfo Coordinfo)
        {
            leftdrops--;//剩余药水数减1

            int row, col;//暂存横纵坐标
            int drops;//？？
            int num;//暂存的接收水滴数
            int n;//数组下标
            int StartBurst;//在当下能对该坐标起作用的水滴
            int i = 0;
            row = Coordinfo.row;//获取坐标
            col = Coordinfo.col;//获取坐标
            n = 6 * row + col;//将横纵坐标转换为一维数组下标
            if (row < 0 || row > 5 || col < 0 || col > 5)
            {//坐标无效
                return;
            }
            coordinfo[n].shot = Coordinfo.shot = true;//当前接收到了水滴
            num = ++coordinfo[n].dropnum;//接收的水滴数加1
            coordinfo[n].dropinfo[num - 1].SN++;//接收的水滴次序加1
            coordinfo[n].dropinfo[num - 1].FromDirection = DIRECTION._none;//方向不是来自四周

            if (A[row, col] == 0)
            {//该处空白，此时不必考虑水滴传播问题
                coordinfo[n].shot = Coordinfo.shot = false;
                Coordinfo.value++;
                drops = coordinfo[n].value = A[row, col] = Coordinfo.value;

   //             Picture_Refresh(row, col, drops);
                Clicking = true;
                return;
            }
            if (A[row, col] < 4)
            {
                Coordinfo.value++;
                drops = coordinfo[n].value = A[row, col] = Coordinfo.value;
                coordinfo[n].shot = Coordinfo.shot = false;
                coordinfo[n].dropinfo[num - 1].SN = 0;//水滴次序归零，防止之后误判
    //            Picture_Refresh(row, col, drops);
                Clicking = true;
                return;
            }
/////////////////////第一轮轰炸////////////////////////////
            if (A[row, col] == 4)
            {		
                drops = coordinfo[n].value = A[row, col] = Coordinfo.value = 0;
                coordinfo[n].shot = Coordinfo.shot = false;
                coordinfo[n].dropinfo[num - 1].SN = 0;//水滴次序归零，防止之后误判
                ProgressCount++;//水滴飞溅进展加1
 //               picturebox[row, col].Image = Resource1.Boomb;
 //               picturebox[row, col].Refresh();
  //              Thread.Sleep(250);
  //              Picture_Refresh(row, col, drops);
                splash(row, col, ProgressCount);//调用水滴飞溅函数	
                burstnum++;
                if (burstnum == 3)
                {
                    leftdrops++;
                    burstnum = 0;
                }
                Notblanknum--;
            }
            while (true)
            {
/////////////////////////第二轮轰炸////////////////////////
                for (i = 0; i < 36; i++)
                {
                    if (coordinfo[i].shot)
                    {

                        for (StartBurst = 0; StartBurst < coordinfo[i].dropnum; StartBurst++)
                        {//找出本轮轰炸下应该起作用的起始水滴
                            if (coordinfo[i].dropinfo[StartBurst].SN == ProgressCount) break;

                        }

                        for (int j = StartBurst; j < coordinfo[i].dropnum; j++)
                        {
                            if (coordinfo[i].dropinfo[j].SN == ProgressCount)
                            {

                                burst(row, col, NOTBLANKNUM_CONST, coordinfo[i], j);
                                lastshot = true;
                                coordinfo[i].dropinfo[j].SN = 0;
                                if (Notblanknum == 0)
                                {
                                    //	cout<<"所有水球已被干掉！"<<endl;
                                    for (int i1 = 0; i1 < 36; i1++) notblank[i1].value = 0;
                                    return;
                                }
                            }

                        }



                    }

                }
                ProgressCount++;
                if (!lastshot) { burstnum = 0; break; }
                lastshot = false;
            }
            Clicking = true;
            return;
        }
    }

}
