using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDrawingProject
{
    public partial class baseForm : Form
    {
        public int WWidth  = Screen.PrimaryScreen.Bounds.Width;//화면 좌표 가져오기
        public int HHeight = Screen.PrimaryScreen.Bounds.Height;
        public int dist = 10;

        public int myWidth; //Width / dist;
        public int myHeight; //Height / dist;
        mySquare[][] myArr;

        public baseForm()
        {
            InitializeComponent();
    
        }
        
        struct mySquare
        {
            public int x1, y1,
                       x2, y2,
                       x3, y3,
                       x4, y4;
        }

        private void baseForm_Load(object sender, EventArgs e)
        {
            //이미지 불러오기
            timer1.Start();
            timer1.Interval = 1;

 

            myWidth = WWidth / dist;
            myHeight = HHeight / dist;
            


            //동적 2차원 배열 얘는 따로 delete를 안해줘도 되나?
            myArr = new mySquare[myHeight][];
            for (int i = 0; i < myHeight; i++)
                myArr[i] = new mySquare[myWidth];
           
            for(int i = 0; i< myHeight; i++)
            {
                for(int j = 0; j< myWidth; j++)
                {
                    myArr[i][j].x1 = j * dist;
                    myArr[i][j].y1 = i * dist;

                    myArr[i][j].x4 = myArr[i][j].x1 + dist;
                    myArr[i][j].y4 = myArr[i][j].y1 + dist;
  
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form2 myGrid = new Form2();
           // myGrid.Show();

            //선그리기
            Graphics myLine = CreateGraphics();//그림을 그릴 패널의 그래픽을 가져온다.
            Pen myPen = new Pen(Color.Black);


            for(int i = 0; i<= HHeight; i+=dist)        
                myLine.DrawLine(myPen, WWidth, i, 0, i);
            for (int i = 0; i <= WWidth; i += dist)
                myLine.DrawLine(myPen, i,0 , i,HHeight );


            myLine.Dispose();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            button1.Text = Control.MousePosition.X.ToString();
            button2.Text = Control.MousePosition.Y.ToString();
        }

        private void baseForm_Click(object sender, EventArgs e)
        {
            //버튼클릭이 일어나면 마우스가 있는 사각형을 그려주자
            //이분탐색을 하면 더 빠를거같은데 그렇게 까지 할 필요는 없나?
            int cur_x = Control.MousePosition.X;
            int cur_y = Control.MousePosition.Y;

            int yy = 0;
            int xx = 0;
            for (int i = 0; i < myHeight; i++)
            {
                if (myArr[i][0].y4 <= cur_y) yy = i;
                else break;
            }

            for (int i = 0; i < myWidth; i++)
            {
                if (myArr[0][i].x4 <= cur_x) xx = i;
                else break;
            }

    

            Graphics g = CreateGraphics();
            Rectangle rect = new Rectangle(myArr[yy][xx].x1, myArr[yy][xx].y1, dist,dist);

            g.FillRectangle(Brushes.Red, rect);
            g.Dispose();


        }

     
    }
}
