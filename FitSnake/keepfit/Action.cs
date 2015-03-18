using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace keepFit
{
    public class Action
    {
        private Snake objSnake;
        private System.Windows.Forms.Panel objPanel;
        private Graphics g;
        private Pen p;
        private SolidBrush b;
        private SolidBrush bb;
        private SolidBrush B;
        private Point objFoodPoint;
        private int recWidth = 15;
        private int fillRecWidth = 15 - 1;
        

        public Action(System.Windows.Forms.Panel panel, Snake snake)//为什么要对两个参数进行重载
        {　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　//如果不重载则不能在MainForm上的panel上画图　
            objPanel = panel;
            objSnake = snake;
        }

        public void InitializeSnake()
        {
            g = objPanel.CreateGraphics();
            p = new Pen(Color.Blue, 1);
            b = new SolidBrush(Color.Yellow);
            bb = new SolidBrush(Color.Green);
            B = new SolidBrush(Color.White);

            foreach (Point point in objSnake.SnakePointList)//蛇身的一个集合的引用，此在snake.cs中定义
            {
                drawBlock(point);
            }
            generateFood();
        }

        public void ClearSnake()//游戏重新开始把上一次的蛇给擦掉
        {
            foreach (Point point in objSnake.SnakePointList)
            {
                clearBlock(point);
            }

            clearBlock(objFoodPoint);
        }

        private void drawBlock(Point point)//蛇的前进是在前面画一个方块再在后面清除一个方块
        {
            g.DrawRectangle(p, (15 * point.X), (15 * point.Y), recWidth, recWidth);            //画边框
            g.FillRectangle(b, (15 * point.X + 1), (15 * point.Y + 1), fillRecWidth, fillRecWidth);     //填充矩形
            
        }
        private void drawFood(Point point)
        {
            g.DrawRectangle(p, (15 * point.X), (15 * point.Y), recWidth, recWidth);            //画边框
            g.FillRectangle(bb, (15 * point.X + 1), (15 * point.Y + 1), fillRecWidth, fillRecWidth);     //填充矩形

        }
        private void clearBlock(Point point)//清除方块
        {
            g.FillRectangle(B, (15 * point.X), (15 * point.Y), recWidth + 1, recWidth + 1);
        }

        public void Move()
        {
            Point objNewPoint = new Point();//定义新的方块
            if (objSnake.canMove(ref objNewPoint))//这个函数在snake.cs里有定义，如果可以移动则不会GameOver
            {
                objSnake.Move(objNewPoint);//插入list头
                drawBlock(objNewPoint);//一边移动一边在前面画方格
                if (objSnake.IsGetFood(objFoodPoint))
                {
                    clearBlock(objSnake.SnakePointList[objSnake.SnakePointList.Count - 1]);//移去最后两个方格
                    objSnake.SnakePointList.Remove(objSnake.SnakePointList[objSnake.SnakePointList.Count - 1]);
                    clearBlock(objSnake.SnakePointList[objSnake.SnakePointList.Count - 1]);
                    objSnake.SnakePointList.Remove(objSnake.SnakePointList[objSnake.SnakePointList.Count - 1]);
                    objSnake.Length--;
                    if (objSnake.Length == 15)
                    {
                        System.Windows.Forms.MessageBox.Show("减肥成功!");
                        createFile();                        
                        isGameOver = true;
                    }
                    generateFood();
                }
                else
                {
                    clearBlock(objSnake.SnakePointList[objSnake.SnakePointList.Count - 1]);//移去最后一个方格
                    objSnake.SnakePointList.Remove(objSnake.SnakePointList[objSnake.SnakePointList.Count - 1]);//把蛇身的最后一项去掉
                }
            }
            else
            {
                isGameOver = true;//不然的话就GameOver
            }

        }

        private void createFile()
        {
            StreamWriter sw = File.AppendText(".\\g_tcs.dat");
            sw.WriteLine("finished");
            sw.Flush();
            sw.Close(); 
        }
        
        
        private void generateFood()//随机产生一个食物
        {
            Random random = new Random();
            Point objPoint;
            int tempX, tempY;
            for (int i = 0; ; i++)
            {
                tempX = random.Next(1000) % 33;
                tempY = random.Next(1000) % 33;
                if (tempX == 0 || tempY == 0)
                {
                    continue;
                }
                else
                {
                    objPoint = new Point(tempX, tempY);

                    if (objSnake.SnakePointList.Contains(objPoint))
                    {
                        continue;
                    }
                    else
                    {
                        objFoodPoint = objPoint;
                        break;
                    }
                }
            }
            drawFood(objFoodPoint);//画出食物
        }

        //Properties Read Only
        private bool isGameOver = false;
        public bool IsGameOver
        {
            get
            {
                return isGameOver;
            }
        } 
    }
}
