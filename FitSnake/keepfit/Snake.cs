using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace keepFit
{
    public class Snake
    {
        private IList<Point> objSnakePointList; //Snake身体
        private char direction;
        static int length = 0;

        public Snake()
        {
            objSnakePointList = new List<Point>();
            createOriginalSnake( 50);
            direction = 'R';

        }
        
        private void createOriginalSnake(int difficulty)
        {
            length = 0;
            for (int i = 11; i > 5; i--)         //调整蛇初始长度和位置
            {
                Point objPoint = new Point(i, 25);
                objSnakePointList.Add(objPoint);
                length++;
            }
            for (int i = 25;i > 10; i--)
            {
                Point objPoint = new Point(5, i);
                objSnakePointList.Add(objPoint);
                length++;
            }
            for (int i = 5;i < 25; i++)
            {
                Point objPoint = new Point(i, 10);
                objSnakePointList.Add(objPoint);
                length++;
            }
            /*随机生成，存在绕圈bug
            if (difficulty > 0)
            {
                Random random = new Random();
                Point tail = new Point(9, 25);
                int tempX, tempY;
               
                for (int i = difficulty; i > 0; i--)
                {
                    for (int j = 0; ; j++)
                    {
                        tempX = random.Next(100) % 3 - 1;
                        tempY = random.Next(100) % 3 - 1;
                        if (tempX + tempY > 1 || tempX + tempY < -1)
                        {
                            continue;
                        }
                        else
                        {
                            Point objPoint = new Point(tail.X + tempX, tail.Y + tempY);

                            if (objSnakePointList.Contains(objPoint) || objPoint.X < 0 || objPoint.X > 32 || objPoint.Y < 0 || objPoint.Y > 32)
                            {
                                continue;
                            }
                            else
                            {
                                objSnakePointList.Add(objPoint);
                                tail.X = objPoint.X;
                                tail.Y = objPoint.Y;
                                break;
                            }
                        }
                    }
                }
             
            }*/

        }
       

        public void Move(Point point)
        {

            objSnakePointList.Insert(0, point);

        }

        public bool canMove(ref Point point)
        {
            point.X = objSnakePointList[0].X;
            point.Y = objSnakePointList[0].Y;

            switch (direction)
            {
                case 'R':
                    if (point.X == 32)
                    {
                        return false;

                    }
                    else
                    {
                        point.X += 1;

                    }
                    break;
                case 'L':
                    if (point.X == 0)
                    {
                        return false;

                    }
                    else
                    {
                        point.X -= 1;

                    }
                    break;
                case 'U':
                    if (point.Y == 0)
                    {
                        return false;
                    }
                    else
                    {
                        point.Y -= 1;

                    }
                    break;
                case 'D':
                    if (point.Y == 32)
                    {
                        return false;
                    }
                    else
                    {
                        point.Y += 1;

                    }
                    break;
            }

            if (biteItself(point))
                return false;
            else
                return true;
        }


        public bool IsGetFood(Point point)
        {
            return (objSnakePointList[0].X == point.X && objSnakePointList[0].Y == point.Y);

        }
        private bool biteItself(Point point)
        {
            return objSnakePointList.Contains(point);
        }


        //Properties

        public IList<Point> SnakePointList
        {
            get
            {
                return objSnakePointList;
            }
            set
            {
                objSnakePointList = value;
            }
        }

        public char Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

    }
}
