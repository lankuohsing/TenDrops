using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace shidishui
{
/// <summary>
/// 游戏界面窗口Form3
/// </summary>
    
    public partial class Form3 : Form
    {
        
/// <summary>
/// 代表水球含水量的图片
/// 共5个等级，无水->饱满
/// </summary>
        public Image Drop_0 = Resource1.Drop_0;
        public Image Drop_1 = Resource1.Drop_1;
        public Image Drop_2 = Resource1.Drop_2;
        public Image Drop_3 = Resource1.Drop_3;
        public Image Drop_4 = Resource1.Drop_4;

        public static Image[] left_drops = new Image[32];
        
/// <summary>
/// 构建6X6方格
/// </summary>
        public static PictureBox[,] picturebox = new PictureBox[6, 6];
        public static PictureBox picturebox_37=new PictureBox();
        public static TextBox textbox_1 = new TextBox();
/// <summary>
/// 用于判断玩家和电脑谁来玩游戏
/// </summary>
        public bool I_play=false;//我来玩
        public bool Auto_Search = false;//电脑来玩，自动搜索最优解
        public bool confirmed = false;//确认键
        static public bool Clicking = false;//??
        public int NOTBLANKNUM_CONST;
        public static void Scores_Refresh(int LeftDrops)
        {
            textbox_1.Text = LeftDrops.ToString() + "分";
            if (LeftDrops <= 31&&LeftDrops>=0) picturebox_37.Image = left_drops[LeftDrops];
        }
        Matrix matrix,matrix1;
/// <summary>
/// Form3默认构造函数
/// </summary>
        public Form3()
        {
            InitializeComponent();

            left_drops[0] = Resource1.scores_0;
            left_drops[1] = Resource1.scores_1;
            left_drops[2] = Resource1.scores_2;
            left_drops[3] = Resource1.scores_3;
            left_drops[4] = Resource1.scores_4;
            left_drops[5] = Resource1.scores_5;
            left_drops[6] = Resource1.scores_6;
            left_drops[7] = Resource1.scores_7;
            left_drops[8] = Resource1.scores_8;
            left_drops[9] = Resource1.scores_9;
            left_drops[10] = Resource1.scores_10;
            left_drops[11] = Resource1.scores_11;
            left_drops[12] = Resource1.scores_12;
            left_drops[13] = Resource1.scores_13;
            left_drops[14] = Resource1.scores_14;
            left_drops[15] = Resource1.scores_15;
            left_drops[16] = Resource1.scores_16;
            left_drops[17] = Resource1.scores_17;
            left_drops[18] = Resource1.scores_18;
            left_drops[19] = Resource1.scores_19;
            left_drops[20] = Resource1.scores_20;
            left_drops[21] = Resource1.scores_21;
            left_drops[22] = Resource1.scores_22;
            left_drops[23] = Resource1.scores_23;
            left_drops[24] = Resource1.scores_24;
            left_drops[25] = Resource1.scores_25;
            left_drops[26] = Resource1.scores_26;
            left_drops[27] = Resource1.scores_27;
            left_drops[28] = Resource1.scores_28;
            left_drops[29] = Resource1.scores_29;
            left_drops[30] = Resource1.scores_30;
            left_drops[31] = Resource1.scores_31;
            
////////////////用36个pictureBox构造6X6方格////////////////
            picturebox[0, 0] = this.pictureBox0;
            picturebox[0, 1] = this.pictureBox1;
            picturebox[0, 2] = this.pictureBox2;
            picturebox[0, 3] = this.pictureBox3;
            picturebox[0, 4] = this.pictureBox4;
            picturebox[0, 5] = this.pictureBox5;
            picturebox[1, 0] = this.pictureBox6;
            picturebox[1, 1] = this.pictureBox7;
            picturebox[1, 2] = this.pictureBox8;
            picturebox[1, 3] = this.pictureBox9;
            picturebox[1, 4] = this.pictureBox10;
            picturebox[1, 5] = this.pictureBox11;
            picturebox[2, 0] = this.pictureBox12;
            picturebox[2, 1] = this.pictureBox13;
            picturebox[2, 2] = this.pictureBox14;
            picturebox[2, 3] = this.pictureBox15;
            picturebox[2, 4] = this.pictureBox16;
            picturebox[2, 5] = this.pictureBox17;
            picturebox[3, 0] = this.pictureBox18;
            picturebox[3, 1] = this.pictureBox19;
            picturebox[3, 2] = this.pictureBox20;
            picturebox[3, 3] = this.pictureBox21;
            picturebox[3, 4] = this.pictureBox22;
            picturebox[3, 5] = this.pictureBox23;
            picturebox[4, 0] = this.pictureBox24;
            picturebox[4, 1] = this.pictureBox25;
            picturebox[4, 2] = this.pictureBox26;
            picturebox[4, 3] = this.pictureBox27;
            picturebox[4, 4] = this.pictureBox28;
            picturebox[4, 5] = this.pictureBox29;
            picturebox[5, 0] = this.pictureBox30;
            picturebox[5, 1] = this.pictureBox31;
            picturebox[5, 2] = this.pictureBox32;
            picturebox[5, 3] = this.pictureBox33;
            picturebox[5, 4] = this.pictureBox34;
            picturebox[5, 5] = this.pictureBox35;

            picturebox_37 = this.pictureBox37;
            textbox_1 = this.textBox1;
    //        left_drops[0]=
            matrix = new Matrix();
            matrix1 = new Matrix();
        }
/// <summary>
/// Form3_Load函数
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        
        private void Form3_Load(object sender, EventArgs e)
        { 
            matrix.transf();
            textBox1.Text = "10分";
        }
/// <summary>
/// 关闭窗口函数
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();//关闭应用程序
        }
       
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
        }
/// <summary>
/// 坐标结构体
/// </summary>
        public struct Coord
        {
            public int row;
            public int col;
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

        public void Intialize_Picture(ref PictureBox [,]picturebox,ref int [,]A)
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    switch (A[i, j])
                    {
                        case 0:
                            {
                                picturebox[i, j].Image = Drop_0;
                                break;
                            }
                        case 1:
                            {
                                picturebox[i, j].Image = Drop_1;
                                break;
                            }
                        case 2:
                            {
                                picturebox[i, j].Image = Drop_2;
                                break;
                            }
                        case 3:
                            {
                                picturebox[i, j].Image = Drop_3;
                                break;
                            }
                        case 4:
                            {
                                picturebox[i, j].Image = Drop_4;
                                break;
                            }
                        default:break;

                    }
                }
        }

/// <summary>
/// 图片刷新函数
/// </summary>
/// <param name="row"></param>
/// <param name="col"></param>
/// <param name="drops"></param>
        public static void Picture_Refresh(int row, int col, int drops)
        {
            
            switch (drops)
            {
                case 0:
                    {
                        picturebox[row, col].Image = Resource1.Drop_0;
                        picturebox[row, col].Refresh();
                        break;
                    }
                case 1:
                    {
                        picturebox[row, col].Image = Resource1.Drop_1;
                        picturebox[row, col].Refresh();
                        break;
                    }
                case 2:
                    {
                        picturebox[row, col].Image = Resource1.Drop_2;
                        picturebox[row, col].Refresh();
                        break; ;
                    }
                case 3:
                    {
                        picturebox[row, col].Image = Resource1.Drop_3;
                        picturebox[row, col].Refresh();
                        break; ;
                    }
                case 4:
                    {
                        picturebox[row, col].Image = Resource1.Drop_4;
                        picturebox[row, col].Refresh();
                        break;
                    }
                default: break;
            }
        }


        void ID_search(ref Coord []pathcoord1,ref Coord []pathcoord2,ref Matrix []treenode,ref Matrix A,ref int scores1,ref int scores2,ref int RecurDepth,ref int path_count,ref int NOTBLANKNUM_CONST,ref bool Bingo,int Search_Depth){
         
            if ((RecurDepth >= Search_Depth)||(A.Notblanknum == 0))
            {
                if (A.Notblanknum == 0)
                {
                    Bingo = true;
                    if (A.leftdrops > scores1)
                    {
                        scores1 = A.leftdrops;//获取当前得分
                        scores2 = scores1;
                        for (int i = 0; i < 11; i++) { pathcoord2[i].row = pathcoord1[i].row; pathcoord2[i].col = pathcoord1[i].col; }
                    }
                }
                
		        
		        
		        if(RecurDepth>0){//返回上一结点
			        A.assign(treenode[RecurDepth].Father);
			        RecurDepth--;//当前递归深度减1
			        path_count--;//
			        pathcoord1[path_count].row=pathcoord1[path_count].col=-1;
		        }
		        return;//搜索深度超出设定值	
	        }

	        int n;	 
            n=NOTBLANKNUM_CONST;//当前水球数
	        do {if(n==0)return;n--;} while (A.notblank[n].value==0);

	        //////////////////记录当前加水的坐标///////////////////
	        pathcoord1[path_count].row=A.notblank[n].row;
	        pathcoord1[path_count].col=A.notblank[n].col;	
	        //////////////////记录当前加水的坐标///////////////////

	        path_count++;//路径坐标结点加1
	        A.burst0_ID(ref NOTBLANKNUM_CONST,ref A.notblank[n]);//对当前对标调用起爆函数
	        if(A.Notblanknum==0){for(int i=path_count;i<11;i++){pathcoord1[i].row=-1;pathcoord1[i].col=-1;}}
	        RecurDepth++;//当前递归深度加1
	        //起爆后的状态A赋给树数组的下一层
	        treenode[RecurDepth].assign(A);

        /////////////////////在上一状态与当前状态之间建立纽带/////////////////	
	        treenode[RecurDepth-1].Child=treenode[RecurDepth];
	        A.Father=treenode[RecurDepth].Father=treenode[RecurDepth-1];
        /////////////////////在上一状态与当前状态之间建立纽带/////////////////

	        ID_search(ref pathcoord1,ref pathcoord2,ref treenode,ref A,ref scores1,ref scores2,ref RecurDepth,ref path_count,ref NOTBLANKNUM_CONST,ref Bingo,Search_Depth);	
	        for(int i=1;(i<NOTBLANKNUM_CONST)&&(A.notblank[NOTBLANKNUM_CONST-1-i]).value!=0;i++){	
		        //////////////////记录当前加水的坐标///////////////////
		        pathcoord1[path_count].row=A.notblank[NOTBLANKNUM_CONST-1-i].row;
		        pathcoord1[path_count].col=A.notblank[NOTBLANKNUM_CONST-1-i].col;
		
		        //////////////////记录当前加水的坐标///////////////////

		        path_count++;//路径坐标结点加1
		        A.burst0_ID(ref NOTBLANKNUM_CONST,ref A.notblank[NOTBLANKNUM_CONST-1-i]);
		        
		        if((A.Notblanknum==0)&&(A.leftdrops>scores1)){
                    for (int j = path_count; j < 11; j++)
                    {
                        pathcoord1[j].row = pathcoord2[j].row = -1;
                        pathcoord1[j].col = pathcoord2[j].col = -1;
                    }
                    /* int j=path_count;
                    while (pathcoord1[j].row != 0 || pathcoord2[j].row != 0)
			        {
				        pathcoord1[j].row=pathcoord2[j].row=-1;
				        pathcoord1[j].col=pathcoord2[j].col=-1;
                        j++;
			        }*/
		        }
		        //	treenode[RecurDepth]=A;
		        RecurDepth++;//当前递归深度加1
		        //起爆后的状态A赋给树数组的下一层
		        treenode[RecurDepth].assign(A);

		        /////////////////////在上一状态与当前状态之间建立纽带/////////////////	
		        treenode[RecurDepth-1].Child=(treenode[RecurDepth]);
		        treenode[RecurDepth].Father=(treenode[RecurDepth-1]);
		        /////////////////////在上一状态与当前状态之间建立纽带/////////////////

		        ID_search(ref pathcoord1,ref pathcoord2,ref treenode,ref A,ref scores1,ref scores2,ref RecurDepth,ref path_count,ref NOTBLANKNUM_CONST,ref Bingo,Search_Depth);				
		
	        }
	        if(RecurDepth>0){//返回上一结点
		        A.assign(treenode[RecurDepth].Father);
		        RecurDepth--;//当前递归深度减1
		        path_count--;//
		        pathcoord1[path_count].row=pathcoord1[path_count].col=-1;
	        }
	        return;
        }
        public class Matrix
    {       
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
        
        public int[,] A = new int[6, 6];//矩阵
        public int ProgressCount;//记录水滴飞溅的进展
        public bool lastshot;
        public int burstnum;//连锁爆炸的个数，为3时药水数加1
        public int leftdrops;//剩余药水滴数
        public CoordInfo[] coordinfo = new CoordInfo[36];//将矩阵用一维数组存储
        public CoordInfo[] notblank = new CoordInfo[36];//有水球的坐标位置
        public Coord[] last_shot = new Coord[36];
        public Coord[] last_burst = new Coord[36];
        public int Notblanknum;//当前水球数
        public int Depth;//当前深度   
        public int k ;
        public int k2;
        public Matrix Child ;//后继
        public Matrix Father ;//父亲
/// <summary>
/// Matrix默认构造函数
/// </summary>
        public Matrix(){
            
            k = k2 = 0;

            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    A[i, j] = 0;
            for (int i = 0; i < 36; i++)
            {
                last_shot[i].row = -1;
                last_shot[i].col = -1;
                last_burst[i].row = -1;
                last_burst[i].col = -1;
            }
                for (int i = 0; i < 36; i++)
                {
                    coordinfo[i].row = coordinfo[i].col = -1;
                    coordinfo[i].value = 0;
                    coordinfo[i].dropnum = 0;
                    coordinfo[i].shot = false;
                    coordinfo[i].dropinfo = new DropInfo[15];
                    notblank[i].row = notblank[i].col = -1;
                    notblank[i].value = 0;
                    notblank[i].dropnum = 0;
                    notblank[i].shot = false;
                    notblank[i].dropinfo = new DropInfo[15];
                    for (int j = 0; j < 15; j++)
                    {
                        coordinfo[i].dropinfo[j].SN = 0;
                        coordinfo[i].dropinfo[j].FromDirection = DIRECTION._none;
                        notblank[i].dropinfo[j].SN = 0;
                        notblank[i].dropinfo[j].FromDirection = DIRECTION._none;
                    }
                }
            lastshot = false;
            ProgressCount = 0;
            burstnum = 0;
            leftdrops = 10;
            Scores_Refresh(leftdrops);
            
            Notblanknum = 0;
        }
        public void Intialize_Matrix()
        {
            
            k = k2 = 0;

            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    A[i, j] = 0;
            for (int i = 0; i < 36; i++)
            {
                last_shot[i].row = -1;
                last_shot[i].col = -1;
                last_burst[i].row = -1;
                last_burst[i].col = -1;
            }
            for (int i = 0; i < 36; i++)
            {
                coordinfo[i].row = coordinfo[i].col = -1;
                coordinfo[i].value = 0;
                coordinfo[i].dropnum = 0;
                coordinfo[i].shot = false;
                coordinfo[i].dropinfo = new DropInfo[15];
                notblank[i].row = notblank[i].col = -1;
                notblank[i].value = 0;
                notblank[i].dropnum = 0;
                notblank[i].shot = false;
                notblank[i].dropinfo = new DropInfo[15];
                for (int j = 0; j < 15; j++)
                {
                    coordinfo[i].dropinfo[j].SN = 0;
                    coordinfo[i].dropinfo[j].FromDirection = DIRECTION._none;
                    notblank[i].dropinfo[j].SN = 0;
                    notblank[i].dropinfo[j].FromDirection = DIRECTION._none;
                }
            }
            lastshot = false;
            ProgressCount = 0;
            burstnum = 0;
       
            Notblanknum = 0;
        }
        /// <summary>
        /// 用一个一维数组存储当前矩阵元素
        /// </summary>
        ///
        public void transf()
        {
            int n;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    n = 6 * i + j;
                    coordinfo[n].row = i;
                    coordinfo[n].col = j;
                    coordinfo[n].value = A[i, j];                   
                }
        }

/// <summary>
/// 水滴飞溅函数
/// </summary>
/// <param name="row"></param>
/// <param name="col"></param>
/// <param name="ProgressCount"></param>
        public void splash(int row, int col, int ProgressCount)
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
        public void assign(Matrix B){
	        
	        for(int i=0;i<6;i++)for(int j=0;j<6;j++)A[i,j]=B.A[i,j];
	        ProgressCount=B.ProgressCount;	
	        lastshot=B.lastshot;
	        burstnum=B.burstnum;
	        leftdrops=B.leftdrops;
	        Notblanknum=B.Notblanknum;
	        Depth=B.Depth;
	        Child=B.Child;
	        Father=B.Father;
	        for(int i=0;i<36;i++){
		        coordinfo[i].row=B.coordinfo[i].row;
		        coordinfo[i].col=B.coordinfo[i].col;
		        coordinfo[i].value=B.coordinfo[i].value;
		        coordinfo[i].shot=B.coordinfo[i].shot;
		        coordinfo[i].dropnum=B.coordinfo[i].dropnum;
		        for(int j=0;j<10;j++){
			        coordinfo[i].dropinfo[j].SN=B.coordinfo[i].dropinfo[j].SN;
			        coordinfo[i].dropinfo[j].FromDirection=B.coordinfo[i].dropinfo[j].FromDirection;
		        }
	        }
	        
	
	        for(int i=0;i<36;i++){
		        notblank[i].row=B.notblank[i].row;
		        notblank[i].col=B.notblank[i].col;
		        notblank[i].value=B.notblank[i].value;
		        notblank[i].shot=B.notblank[i].shot;
		        notblank[i].dropnum=B.notblank[i].dropnum;
		        for(int j=0;j<10;j++){
			        notblank[i].dropinfo[j].SN=B.notblank[i].dropinfo[j].SN;
			        notblank[i].dropinfo[j].FromDirection=B.notblank[i].dropinfo[j].FromDirection;
		        }
	        }

        }

        public bool burst(int x, int y, int NOTBLANKNUM_CONST, CoordInfo Coordinfo, int Place)
        {
            int row, col;//暂存横纵坐标
            int n;//数组下标
            int num;
            int drops;//当前水量
            
            row = Coordinfo.row;//暂存横坐标
            col = Coordinfo.col;//暂存纵坐标
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
                last_shot[k].row = row;
                last_shot[k].col = col;
                k++;
                drops = coordinfo[n].value = A[row, col] = ++Coordinfo.value;//水量加1
                for (int i = 0; i < NOTBLANKNUM_CONST; i++)
                    if ((notblank[i].row == row) && (notblank[i].col == col) && ((x != row) || (y != col))) notblank[i].value = Coordinfo.value;
                if (coordinfo[n].dropnum <= ProgressCount)
                    coordinfo[n].shot = Coordinfo.shot = false;
    //            Picture_Refresh(row, col, drops);
                return false;
            }
            if (A[row, col] == 4)
            {
                last_burst[k2].row = row;
                last_burst[k2].col = col;
                k2++;
                burstnum++;
                if (burstnum == 3)
                {
                    leftdrops++;
                    
                    burstnum = 0;
                    Scores_Refresh(leftdrops);
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
 /*               Thread.Sleep(250);
                picturebox[row, col].Image = Resource1.Boomb;
                picturebox[row, col].Refresh();
                Picture_Refresh(row, col, drops);*/
                splash(row, col, ProgressCount + 1);//调用水滴飞溅函数		
                return true;
            }
            return false;

        }
/// <summary>
/// 起爆函数
/// </summary>
/// <param name="Coordinfo"></param>
        public void burst0(int NOTBLANKNUM_CONST, CoordInfo Coordinfo)
        {
            leftdrops--;//剩余药水数减1
            Scores_Refresh(leftdrops);
            int row, col;//暂存横纵坐标
            int drops;//代表图片含的水量
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
                Coordinfo.value++;
                coordinfo[n].shot = Coordinfo.shot = false;               
                drops = coordinfo[n].value = A[row, col] = Coordinfo.value;
                coordinfo[n].dropinfo[num - 1].SN = 0;//水滴次序归零，防止之后误判
     //           Thread.Sleep(250);//延时函数
                Picture_Refresh(row, col, drops);
                Clicking = true;
                return;
            }
            if (A[row, col] < 4)
            {
                Coordinfo.value++;//水球水量加1              
                coordinfo[n].shot = Coordinfo.shot = false;
                drops = coordinfo[n].value = A[row, col] = Coordinfo.value;//更新水量
                coordinfo[n].dropinfo[num - 1].SN = 0;//水滴次序归零，防止之后误判
      //          Thread.Sleep(250);//延时函数
                Picture_Refresh(row, col, drops);//调用图片刷新函数
                Clicking = true;
                return;
            }
/////////////////////第一轮轰炸////////////////////////////
            if (A[row, col] == 4)
            {		
                drops = coordinfo[n].value = A[row, col] = Coordinfo.value = 0;//更新水量
                coordinfo[n].shot = Coordinfo.shot = false;
                coordinfo[n].dropinfo[num - 1].SN = 0;//水滴次序归零，防止之后误判
                ProgressCount++;//水滴飞溅进展加1
                picturebox[row, col].Image = Resource1.Boomb;//爆炸过程图片
                picturebox[row, col].Refresh();//图片刷新函数
                Thread.Sleep(250);//延时函数
                Picture_Refresh(row, col, drops);
                splash(row, col, ProgressCount);//调用水滴飞溅函数	
                burstnum++;//连锁爆炸的水球数加1
                if (burstnum == 3)//连续爆炸3个，得分加1
                {
                    leftdrops++;
                    Scores_Refresh(leftdrops);
                    burstnum = 0;//归零
                }
                Notblanknum--;//水球数减1
            }
            while (true)
            {
/////////////////////////第二轮轰炸////////////////////////
                for (i = 0; i < 36; i++)
                {
                    if (coordinfo[i].shot)//当前坐标被击中
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
               /*                 if (Notblanknum == 0)
                                {
                                    //	cout<<"所有水球已被干掉！"<<endl;
                                    for (int i1 = 0; i1 < 36; i1++) notblank[i1].value = 0;
                                    return;
                                }*/
                            }
                            else break;
                        }
                    }

                }
                int k3 = 0;
                int k4 = 0;
                scan_refresh();
                Thread.Sleep(250);//延时函数
                while (last_shot[k3].row != -1)
                {
                    last_shot[k3].row = -1;
                    last_shot[k3].col = -1;
                    k3++;
                }
                k = 0;
                while (last_burst[k4].row != -1)
                {
                    last_burst[k4].row = -1;
                    last_burst[k4].col = -1;
                    k4++;
                }
                
                k2 = 0;
                ProgressCount++;
                if (!lastshot) { burstnum = 0; break; }
                lastshot = false;
            }
            Clicking = true;
       //     if (Notblanknum == 0) NextMession();
            return;
        }

        public void scan_refresh()
        {
  //          Thread.Sleep(250);//延时函数
            int k3 = 0;
            int k4 = 0;
            while (last_shot[k3].row != -1)
            {
                Picture_Refresh(last_shot[k3].row, last_shot[k3].col, A[last_shot[k3].row, last_shot[k3].col]);
                k3++;
            }
            while (last_burst[k4].row != -1)
            {
                picturebox[last_burst[k4].row, last_burst[k4].col].Image = Resource1.Boomb;//爆炸过程图片
                picturebox[last_burst[k4].row, last_burst[k4].col].Refresh();//图片刷新函数
                Thread.Sleep(50);//延时函数
                Picture_Refresh(last_burst[k4].row, last_burst[k4].col, A[last_burst[k4].row, last_burst[k4].col]);
                last_shot[k].row = last_burst[k4].row;
                last_shot[k].col = last_burst[k4].col;
                k4++;
                
            }
        }

        public bool burst_ID(ref int x, ref int y, ref int NOTBLANKNUM_CONST, ref CoordInfo Coordinfo, ref int Place)
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
                
                splash(row, col, ProgressCount + 1);//调用水滴飞溅函数		
                return true;
            }
            return false;
        }

        public void burst0_ID(ref int NOTBLANKNUM_CONST, ref CoordInfo Coordinfo)
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
                return;
            }
            if (A[row, col] < 4)
            {
                Coordinfo.value++;
                drops = coordinfo[n].value = A[row, col] = Coordinfo.value;
                coordinfo[n].shot = Coordinfo.shot = false;
                coordinfo[n].dropinfo[num - 1].SN = 0;//水滴次序归零，防止之后误判                
                return;
            }
            /////////////////////第一轮轰炸////////////////////////////
            if (A[row, col] == 4)
            {
                drops = coordinfo[n].value = A[row, col] = Coordinfo.value = 0;
                coordinfo[n].shot = Coordinfo.shot = false;
                coordinfo[n].dropinfo[num - 1].SN = 0;//水滴次序归零，防止之后误判
                ProgressCount++;//水滴飞溅进展加1               
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

                                burst_ID(ref row, ref col, ref NOTBLANKNUM_CONST, ref coordinfo[i], ref j);
                                lastshot = true;
                                coordinfo[i].dropinfo[j].SN = 0;
                                if (Notblanknum == 0)
                                {
                                    
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
            return;
        }
        
        
     }

        
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }
                
        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e) 
        {
            
        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {             
            string str = (string)comboBox2.SelectedItem;           
            switch(str){
                case "第一关":
                    {
                        pictureBox39.Image = Resource1.Level_1;
                        matrix.Intialize_Matrix();
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 6;j++ )
                            {
                                matrix.A[i, j] = 0;
                                matrix.A[i, j] = 0;
                                matrix.A[i, j] = 0;
                                matrix.A[i, j] = 0;
                                matrix.A[i, j] = 0;
                                matrix.A[i, j] = 0;
                                
                            }

                        matrix.A[3, 0] = 3;
                        matrix.A[3, 1] = 3;
                        matrix.A[3, 2] = 4;
                        matrix.A[3, 3] = 4;
                        matrix.A[3, 4] = 3;
                        matrix.A[3, 5] = 3;

                        
                        for (int i = 4; i < 6; i++)
                            for (int j = 0; j < 6; j++)
                            {
                                matrix.A[i, j] = 0;
                                matrix.A[i, j] = 0;
                                matrix.A[i, j] = 0;
                                matrix.A[i, j] = 0;
                                matrix.A[i, j] = 0;
                                matrix.A[i, j] = 0;
                                
                            }
                        Intialize_Picture(ref picturebox, ref matrix.A); 
                        break;
                    }
                case "第二关":
                    {
                        pictureBox39.Image = Resource1.Level_3;
                        matrix.Intialize_Matrix();
                        matrix.A[0, 0] = 3;
                        matrix.A[0, 1] = 0;
                        matrix.A[0, 2] = 4;
                        matrix.A[0, 3] = 0;
                        matrix.A[0, 4] = 0;
                        matrix.A[0, 5] = 4;
                        matrix.A[1, 0] = 0;
                        matrix.A[1, 1] = 0;
                        matrix.A[1, 2] = 0;
                        matrix.A[1, 3] = 0;
                        matrix.A[1, 4] = 0;
                        matrix.A[1, 5] = 0;
                        matrix.A[2, 0] = 0;
                        matrix.A[2, 1] = 0;
                        matrix.A[2, 2] = 0;
                        matrix.A[2, 3] = 0;
                        matrix.A[2, 4] = 0;
                        matrix.A[2, 5] = 4;
                        matrix.A[3, 0] = 0;
                        matrix.A[3, 1] = 0;
                        matrix.A[3, 2] = 0;
                        matrix.A[3, 3] = 0;
                        matrix.A[3, 4] = 0;
                        matrix.A[3, 5] = 4;
                        matrix.A[4, 0] = 4;
                        matrix.A[4, 1] = 0;
                        matrix.A[4, 2] = 4;
                        matrix.A[4, 3] = 0;
                        matrix.A[4, 4] = 0;
                        matrix.A[4, 5] = 2;
                        matrix.A[5, 0] = 4;
                        matrix.A[5, 1] = 0;
                        matrix.A[5, 2] = 0;
                        matrix.A[5, 3] = 0;
                        matrix.A[5, 4] = 0;
                        matrix.A[5, 5] = 0;


                        Intialize_Picture(ref picturebox, ref matrix.A);

                        break;
                    }
                case "第三关":
                    {
                        pictureBox39.Image = Resource1.Level_2;
                        matrix.Intialize_Matrix();
                    //给各个坐标赋初始水滴 
                        matrix.A[0, 0] = 0;
                        matrix.A[0, 1] = 0;
                        matrix.A[0, 2] = 3;
                        matrix.A[0, 3] = 0;
                        matrix.A[0, 4] = 0;
                        matrix.A[0, 5] = 0;
                        matrix.A[1, 0] = 0;
                        matrix.A[1, 1] = 0;
                        matrix.A[1, 2] = 3;
                        matrix.A[1, 3] = 0;
                        matrix.A[1, 4] = 0;
                        matrix.A[1, 5] = 0;
                        matrix.A[2, 0] = 0;
                        matrix.A[2, 1] = 0;
                        matrix.A[2, 2] = 3;
                        matrix.A[2, 3] = 0;
                        matrix.A[2, 4] = 0;
                        matrix.A[2, 5] = 0;
                        matrix.A[3, 0] = 3;
                        matrix.A[3, 1] = 3;
                        matrix.A[3, 2] = 4;
                        matrix.A[3, 3] = 4;
                        matrix.A[3, 4] = 3;
                        matrix.A[3, 5] = 3;
                        matrix.A[4, 0] = 0;
                        matrix.A[4, 1] = 0;
                        matrix.A[4, 2] = 4;
                        matrix.A[4, 3] = 0;
                        matrix.A[4, 4] = 0;
                        matrix.A[4, 5] = 0;
                        matrix.A[5, 0] = 0;
                        matrix.A[5, 1] = 0;
                        matrix.A[5, 2] = 3;
                        matrix.A[5, 3] = 0;
                        matrix.A[5, 4] = 0;
                        matrix.A[5, 5] = 0;

                        Intialize_Picture(ref picturebox, ref matrix.A); 
                        
                    break;
                }
                
                case "第四关":
                    {
                        pictureBox39.Image = Resource1.Level_4;
                        matrix.Intialize_Matrix();
                        matrix.A[0, 0] = 3;
                        matrix.A[0, 1] = 0;
                        matrix.A[0, 2] = 1;
                        matrix.A[0, 3] = 0;
                        matrix.A[0, 4] = 1;
                        matrix.A[0, 5] = 3;
                        matrix.A[1, 0] = 2;
                        matrix.A[1, 1] = 4;
                        matrix.A[1, 2] = 3;
                        matrix.A[1, 3] = 4;
                        matrix.A[1, 4] = 2;
                        matrix.A[1, 5] = 3;
                        matrix.A[2, 0] = 3;
                        matrix.A[2, 1] = 3;
                        matrix.A[2, 2] = 2;
                        matrix.A[2, 3] = 4;
                        matrix.A[2, 4] = 0;
                        matrix.A[2, 5] = 1;
                        matrix.A[3, 0] = 1;
                        matrix.A[3, 1] = 3;
                        matrix.A[3, 2] = 3;
                        matrix.A[3, 3] = 4;
                        matrix.A[3, 4] = 4;
                        matrix.A[3, 5] = 3;
                        matrix.A[4, 0] = 2;
                        matrix.A[4, 1] = 2;
                        matrix.A[4, 2] = 0;
                        matrix.A[4, 3] = 0;
                        matrix.A[4, 4] = 0;
                        matrix.A[4, 5] = 3;
                        matrix.A[5, 0] = 2;
                        matrix.A[5, 1] = 3;
                        matrix.A[5, 2] = 0;
                        matrix.A[5, 3] = 4;
                        matrix.A[5, 4] = 1;
                        matrix.A[5, 5] = 2;


                        Intialize_Picture(ref picturebox, ref matrix.A);

                        break;
                    }
                case "随机生成":
                    {
                        pictureBox39.Image = null;
                        matrix.Intialize_Matrix();
                        int WaterRank = 0;
                        Random autoRand = new Random();
                        for (int i = 0; i < 6; i++)
                            for (int j = 0; j < 6; j++)
                            {

                                WaterRank = autoRand.Next(1, 150);
                                if (WaterRank >= 1 && WaterRank <= 60) matrix.A[i, j] = 0;
                                if (WaterRank > 60 && WaterRank <= 90) matrix.A[i, j] = 2;
                                if (WaterRank > 90 && WaterRank <= 120) matrix.A[i, j] = 3;
                                if (WaterRank > 120 && WaterRank <= 145) matrix.A[i, j] = 4;
                                if (WaterRank > 145 && WaterRank <= 150) matrix.A[i, j] = 1;
                            }
                        Intialize_Picture(ref picturebox, ref matrix.A);
                        break;
                    }
                default:
                    {
                        for (int i = 0; i < 6; i++)
                            for (int j = 0; j < 6; j++)
                            {
                                matrix.A[i, j] = 0;
                                picturebox[i, j].Image = Drop_0;
                            }
                        break;
                    }
            }

 

            picturebox[0, 0].Refresh();
            picturebox[0, 1].Refresh();
            picturebox[0, 2].Refresh();
            picturebox[0, 3].Refresh();
            picturebox[0, 4].Refresh();
            picturebox[0, 5].Refresh();
            picturebox[1, 0].Refresh();
            picturebox[1, 1].Refresh();
            picturebox[1, 2].Refresh();
            picturebox[1, 3].Refresh();
            picturebox[1, 4].Refresh();
            picturebox[1, 5].Refresh();
            picturebox[2, 0].Refresh();
            picturebox[2, 1].Refresh();
            picturebox[2, 2].Refresh();
            picturebox[2, 3].Refresh();
            picturebox[2, 4].Refresh();
            picturebox[2, 5].Refresh();
            picturebox[3, 0].Refresh();
            picturebox[3, 1].Refresh();
            picturebox[3, 2].Refresh();
            picturebox[3, 3].Refresh();
            picturebox[3, 4].Refresh();
            picturebox[3, 5].Refresh();
            picturebox[4, 0].Refresh();
            picturebox[4, 1].Refresh();
            picturebox[4, 3].Refresh();
            picturebox[4, 4].Refresh();
            picturebox[4, 5].Refresh();
            picturebox[5, 0].Refresh();
            picturebox[5, 0].Refresh();
            picturebox[5, 1].Refresh();
            picturebox[5, 2].Refresh();
            picturebox[5, 3].Refresh();
            picturebox[5, 4].Refresh();
            picturebox[5, 5].Refresh();
            matrix.transf();
            Clicking = true;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    if (matrix.A[i, j] != 0)
                    {
                        matrix.Notblanknum++;
                        matrix.notblank[matrix.Notblanknum - 1].row = i;
                        matrix.notblank[matrix.Notblanknum - 1].col = j;
                        matrix.notblank[matrix.Notblanknum - 1].value = matrix.A[i, j];
                    }
                }
        }

        private void pictureBox0_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 0;
                Coordinfo.col = 0;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 0;
                Coordinfo.col = 1;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 0;
                Coordinfo.col = 2;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 0;
                Coordinfo.col = 3;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 0;
                Coordinfo.col = 4;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 0;
                Coordinfo.col = 5;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 1;
                Coordinfo.col = 0;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 1;
                Coordinfo.col = 1;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 1;
                Coordinfo.col = 2;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 1;
                Coordinfo.col = 3;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 1;
                Coordinfo.col = 4;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 1;
                Coordinfo.col = 5;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 2;
                Coordinfo.col = 0;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 2;
                Coordinfo.col = 1;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 2;
                Coordinfo.col = 2;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 2;
                Coordinfo.col = 3;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 2;
                Coordinfo.col = 4;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 2;
                Coordinfo.col = 5;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 3;
                Coordinfo.col = 0;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 3;
                Coordinfo.col = 1;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 3;
                Coordinfo.col = 2;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 3;
                Coordinfo.col = 3;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 3;
                Coordinfo.col = 4;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 3;
                Coordinfo.col = 5;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 4;
                Coordinfo.col = 0;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }

        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 4;
                Coordinfo.col = 1;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 4;
                Coordinfo.col = 2;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 4;
                Coordinfo.col = 3;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 4;
                Coordinfo.col = 4;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 4;
                Coordinfo.col = 5;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 5;
                Coordinfo.col = 0;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 5;
                Coordinfo.col = 1;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 5;
                Coordinfo.col = 2;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox33_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 5;
                Coordinfo.col = 3;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox34_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 5;
                Coordinfo.col = 4;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void pictureBox35_Click(object sender, EventArgs e)
        {
            if (Clicking && I_play == true && confirmed == true)
            {
                CoordInfo Coordinfo = new CoordInfo();
                Coordinfo.row = 5;
                Coordinfo.col = 5;
                Coordinfo.shot = false;
                Coordinfo.dropnum = 0;
                Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                Coordinfo.dropinfo = new DropInfo[15];
                for (int i = 0; i < 15; i++)
                {
                    Coordinfo.dropinfo[i].SN = 0;
                    Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                }
                Clicking = false; matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
            }
        }

        private void panel0_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
                
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                I_play = true;
                Auto_Search = false;
            }
            if (radioButton2.Checked)
            {
                I_play = false;
                Auto_Search = true;

                int RecurDepth=0;
    	    	int path_count=0;
    	    	int scores1=-1;
                int scores2=-1;
                Coord[] pathcoord1 = new Coord[11];
                Coord[] pathcoord2 = new Coord[11];
                for (int i = 0; i < 11; i++)
                {
                    pathcoord2[i].row = pathcoord2[i].col = -1;
                    pathcoord1[i].row = pathcoord1[i].col = -1;
                }
                int NOTBLANKNUM_CONST = matrix.Notblanknum;//初始水球数
            	
            	Matrix []treenode=new Matrix[11];//树
                matrix1.assign(matrix);
                for (int i = 0; i < 11; i++)
                {
                    treenode[i] = new Matrix();
                }
                treenode[0].assign(matrix);
                bool Bingo = false;
                int Search_Depth = 4;
                ID_search(ref pathcoord1, ref pathcoord2, ref treenode, ref matrix1, ref scores1, ref scores2, ref RecurDepth, ref path_count, ref NOTBLANKNUM_CONST, ref Bingo, Search_Depth);
                while (!Bingo)
                {
                    MessageBox.Show("当前搜索深度不够，点击确认键增加搜索深度再次搜索！");
                    Search_Depth++;
                    
                    RecurDepth = 0;
                    path_count = 0;
                    scores1 = -1;
                    scores2 = -1;
                    for (int i = 0; i < 11; i++)
                    {
                        pathcoord2[i].row = pathcoord2[i].col = -1;
                        pathcoord1[i].row = pathcoord1[i].col = -1;
                    }
                    NOTBLANKNUM_CONST = matrix.Notblanknum;//初始水球数
                    
                    matrix1.assign(matrix);
                    for (int i = 0; i < 11; i++)
                    {
                        treenode[i] = new Matrix();
                    }
                    treenode[0].assign(matrix);
                    ID_search(ref pathcoord1, ref pathcoord2, ref treenode, ref matrix1, ref scores1, ref scores2, ref RecurDepth, ref path_count, ref NOTBLANKNUM_CONST, ref Bingo, Search_Depth);
                    
                }


                int Click_Count = 0;
                for (; pathcoord2[Click_Count].row != -1;Click_Count++ );
                MessageBox.Show("找到最优解，所需加水次数为："+Click_Count.ToString());

                int k = 0;
                CoordInfo Coordinfo = new CoordInfo();
                
                while (pathcoord2[k].row >= 0)
                {

                    Coordinfo.row = pathcoord2[k].row;
                    Coordinfo.col = pathcoord2[k].col;
                    Coordinfo.shot = false;
                    Coordinfo.dropnum = 0;
                    Coordinfo.value = matrix.A[Coordinfo.row, Coordinfo.col];
                    Coordinfo.dropinfo = new DropInfo[15];
                    for (int i = 0; i < 15; i++)
                    {
                        Coordinfo.dropinfo[i].SN = 0;
                        Coordinfo.dropinfo[i].FromDirection = DIRECTION._none;
                    }

                    matrix.burst0(NOTBLANKNUM_CONST, Coordinfo);
                    Thread.Sleep(1000);
                    k++;
                }
            }
            confirmed = true;
        }

        private void pictureBox37_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox39_Click(object sender, EventArgs e)
        {

        }

        
  
         
    }
}


