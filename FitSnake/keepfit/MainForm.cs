using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace keepFit
{
    public partial class MainForm : Form
    {
        //Size of mainform:495 * 495
        //Size of block:15 * 15
        //34 * 15 = 495

        private Action objAction;
        private Snake objSnake;
        private bool start;
        private bool oneKey = true;  //防在timetick间隙输入多个命令        

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer.Interval = 50;                                         //调节snake的速度
        }      

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (objAction != null)
            {
                objAction.ClearSnake();
            }
            objSnake = new Snake();　　　　　　　　　　　　　　　　　　　　　//snake.cs中的Snake类的引用
            objAction = new Action(panel, objSnake);　　　　　　　　//Action.cs中Action类的重载，方便在panel上画蛇身
            objAction.InitializeSnake();　　　　　　　　　　　　　　　　//调用蛇身的初始化函数
            start = true;
            timer.Enabled = true;
            this.btnStart.Enabled = false;
            this.btnHelp.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            objAction.Move();
            textBox1.Text = objSnake.Length.ToString();
            if (objAction.IsGameOver)
            {
                timer.Enabled = false;
                MessageBox.Show("游戏结束");

                btnStart.Enabled = true;
                btnHelp.Enabled = true;
            }
            oneKey = true;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (oneKey)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (objSnake.Direction != 'D')
                    {
                        objSnake.Direction = 'U';
                        oneKey = false;
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (objSnake.Direction != 'U')
                    {
                        objSnake.Direction = 'D';
                        oneKey = false;
                    }
                }
                if (e.KeyCode == Keys.Left)
                {
                    if (objSnake.Direction != 'R')
                    {
                        objSnake.Direction = 'L';
                        oneKey = false;
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    if (objSnake.Direction != 'L')
                    {
                        objSnake.Direction = 'R';
                        oneKey = false;
                    }
                }
            }         
        }
        
        private void btnHelp_Click(object sender, EventArgs e)
        {
            Form help = new Help();
            help.ShowDialog();
        }        

    }
}
