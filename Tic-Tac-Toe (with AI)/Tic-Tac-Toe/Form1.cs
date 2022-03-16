using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        #region 宣告內部使用的欄位
        private bool turn; // 紀錄輪到 O(true) or X(false)
        private int turnCount; // 記錄按幾次按鈕，若是平手 turnCount == 9
        private Button[] myBtn; // 存放九宮格按鈕
        private bool againstToComputer;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region Form_Load 欄位初始化
        private void Form1_Load(object sender, EventArgs e)
        {
            // 欄位初始化
            turn = true; // 固定 O 先下
            turnCount = 0;
            myBtn = new Button[] { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };
            // initialGame();
            againstToComputer = false;
        }
        #endregion

        #region 按鈕事件
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit(); // 結束遊戲
        }

        private void whenBtnClick(object sender, EventArgs e)
        {
            if (txtPlayer2.Text != "COMPUTER" || txtPlayer2.Text != "電腦")
            {
                Button tempBtn = (Button)sender;

                if (turn == true)
                {
                    tempBtn.Text = "O";
                    tempBtn.BackColor = Color.LightGreen;
                }
                else if (turn == false)
                {
                    tempBtn.Text = "X";
                    tempBtn.BackColor = Color.LightYellow;
                }

                turnCount++; // 按按鈕次數+1
                tempBtn.Enabled = false; // 讓按鈕，按過就不能再按
                checkForWinner(); // 按完按鈕後先檢查勝利，再換人
                turn = !turn; // 換人
            }

            if ((!turn) && (againstToComputer))
            {
                computerMakeMove();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            initialGame();
            txtPlayer2.Text = "Player 2";
            btnStart.Text = "開始遊戲"; // 讓顯示從 再來一局! 變回 開始遊戲 
            btnStart.Enabled = false;
            btnSinglePlayer.Text = "單人模式";
            btnSinglePlayer.Enabled = false;
        }

        private void whenMousEnter(object sender, EventArgs e)
        {
            Button tempBtn = (Button)sender;
            if (tempBtn.Enabled == true)
            {
                if (turn == true)
                {
                    tempBtn.Text = "O";
                }
                else if (turn == false)
                {
                    tempBtn.Text = "X";
                }
            }
        }

        private void WhenMousLeave(object sender, EventArgs e)
        {
            Button tempBtn = (Button)sender;
            if (tempBtn.Enabled == true)
            {
                tempBtn.Text = "";
            }
        }

        private void btnResetCount_Click(object sender, EventArgs e)
        {
            lblDrawCount.Text = "0";
            lblOWinCount.Text = "0";
            lblXWinCount.Text = "0";
        }

        #endregion

        #region 自定義方法
        private void checkForWinner()
        {
            bool isThereAWinner = false;

            #region 勝利條件
            // 水平檢查
            if ((btn1.Text == btn2.Text) && (btn2.Text == btn3.Text) && (!btn1.Enabled))
            {
                btn1.BackColor = btn2.BackColor = btn3.BackColor = Color.LightCoral;
                isThereAWinner = true;
            }
            else if ((btn4.Text == btn5.Text) && (btn5.Text == btn6.Text) && (!btn4.Enabled))
            {
                btn4.BackColor = btn5.BackColor = btn6.BackColor = Color.LightCoral;
                isThereAWinner = true;
            }
            else if ((btn7.Text == btn8.Text) && (btn8.Text == btn9.Text) && (!btn7.Enabled))
            {
                btn7.BackColor = btn8.BackColor = btn9.BackColor = Color.LightCoral;
                isThereAWinner = true;
            }

            // 垂直檢查
            else if ((btn1.Text == btn4.Text) && (btn4.Text == btn7.Text) && (!btn1.Enabled))
            {
                btn1.BackColor = btn4.BackColor = btn7.BackColor = Color.LightCoral;
                isThereAWinner = true;
            }
            else if ((btn2.Text == btn5.Text) && (btn5.Text == btn8.Text) && (!btn2.Enabled))
            {
                btn2.BackColor = btn5.BackColor = btn8.BackColor = Color.LightCoral;
                isThereAWinner = true;
            }
            else if ((btn3.Text == btn6.Text) && (btn6.Text == btn9.Text) && (!btn3.Enabled))
            {
                btn3.BackColor = btn6.BackColor = btn9.BackColor = Color.LightCoral;
                isThereAWinner = true;
            }

            // 斜向檢查
            else if ((btn1.Text == btn5.Text) && (btn5.Text == btn9.Text) && (!btn1.Enabled))
            {
                btn1.BackColor = btn5.BackColor = btn9.BackColor = Color.LightCoral;
                isThereAWinner = true;
            }
            else if ((btn3.Text == btn5.Text) && (btn5.Text == btn7.Text) && (!btn7.Enabled))
            {
                btn3.BackColor = btn5.BackColor = btn7.BackColor = Color.LightCoral;
                isThereAWinner = true;
            }
            #endregion

            // 如果有人獲勝
            if (isThereAWinner == true)
            {
                disableButtons();
                string theWinner = "";
                if (turn == true)
                {
                    theWinner = txtPlayer1.Text;
                    lblOWinCount.Text = (int.Parse(lblOWinCount.Text) + 1).ToString();
                }
                else if (turn == false)
                {
                    theWinner = txtPlayer2.Text;
                    lblXWinCount.Text = (int.Parse(lblXWinCount.Text) + 1).ToString();
                }

                DialogResult = MessageBox.Show($"{theWinner} 獲勝!", "遊戲結束", MessageBoxButtons.OK);
                if (DialogResult == DialogResult.OK)
                {
                    btnStart.Enabled = true;
                    if (!againstToComputer)
                    {
                        btnStart.Text = "再來一局 !";
                    }
                    btnSinglePlayer.Enabled = true;
                }
            }
            else if (turnCount == 9)
            {
                lblDrawCount.Text = (int.Parse(lblDrawCount.Text) + 1).ToString();
                DialogResult = MessageBox.Show($"X O 平手!", "遊戲結束", MessageBoxButtons.OK);
                if (DialogResult == DialogResult.OK)
                {
                    btnStart.Enabled = true;
                    if (!againstToComputer)
                    {
                        btnStart.Text = "再來一局 !";
                    }
                    btnSinglePlayer.Enabled = true;
                }
            }

        } // end checkForWinner

        private void disableButtons() // 當贏家出現時，讓剩下的按鈕不能按
        {
            foreach (Button btn in myBtn)
            {
                btn.Enabled = false;
            }
        } // end disableButtons

        private void initialGame() // 初始化遊戲
        {
            turnCount = 0;

            foreach (Button btn in myBtn)
            {
                btn.Text = "";
                btn.Enabled = true;
                btn.BackColor = Color.LightBlue;
            }
        } // end initialGame

        private void computerMakeMove()
        {
            // priority 1. 獲勝
            // priority 2. 阻擋
            // priority 3. 挑中間
            // priority 4. 挑角落
            // priority 5. 找還沒按過的按鈕

            Button move = null;

            // 尋找獲勝機會
            move = lookForWinOrBlock("X"); // 獲勝
            if (move == null)
            {
                move = lookForWinOrBlock("O"); // 阻擋
                if (move == null)
                {
                    move = lookForCenter(); // 挑中間
                    if (move == null)
                    {
                        move = lookForCorner(); // 挑角落
                        if (move == null)
                        {
                            move = lookForAvailableButton(); // 找還沒按過的按鈕
                        } // end if
                    } // end if
                } // end if
            } // end if

            move.PerformClick(); // 觸發按鈕事件(whenBtnClick)
        } // end computerMakeMove

        private Button lookForWinOrBlock(string mark)
        {
            Console.WriteLine("Looking for win or block");
            #region 策略 水平檢查
            // 水平檢查
            if ((btn1.Text == mark) && (btn2.Text == mark) && (btn3.Text == ""))
            {
                return btn3;
            }
            if ((btn1.Text == mark) && (btn2.Text == "") && (btn3.Text == mark))
            {
                return btn2;
            }
            if ((btn1.Text == "") && (btn2.Text == mark) && (btn3.Text == mark))
            {
                return btn1;
            }


            if ((btn4.Text == mark) && (btn5.Text == mark) && (btn6.Text == ""))
            {
                return btn6;
            }
            if ((btn4.Text == mark) && (btn5.Text == "") && (btn6.Text == mark))
            {
                return btn5;
            }
            if ((btn4.Text == "") && (btn5.Text == mark) && (btn6.Text == mark))
            {
                return btn4;
            }



            if ((btn7.Text == mark) && (btn8.Text == mark) && (btn9.Text == ""))
            {
                return btn9;
            }
            if ((btn7.Text == mark) && (btn8.Text == "") && (btn9.Text == mark))
            {
                return btn8;
            }
            if ((btn7.Text == "") && (btn8.Text == mark) && (btn9.Text == mark))
            {
                return btn7;
            }
            #endregion

            #region 策略 垂直檢查
            // 垂直檢查
            if ((btn1.Text == mark) && (btn4.Text == mark) && (btn7.Text == ""))
            {
                return btn7;
            }
            if ((btn1.Text == mark) && (btn4.Text == "") && (btn7.Text == mark))
            {
                return btn4;
            }
            if ((btn1.Text == "") && (btn4.Text == mark) && (btn7.Text == mark))
            {
                return btn1;
            }


            if ((btn2.Text == mark) && (btn5.Text == mark) && (btn8.Text == ""))
            {
                return btn8;
            }
            if ((btn2.Text == mark) && (btn5.Text == "") && (btn8.Text == mark))
            {
                return btn5;
            }
            if ((btn2.Text == "") && (btn5.Text == mark) && (btn8.Text == mark))
            {
                return btn2;
            }


            if ((btn3.Text == mark) && (btn6.Text == mark) && (btn9.Text == ""))
            {
                return btn9;
            }
            if ((btn3.Text == mark) && (btn6.Text == "") && (btn9.Text == mark))
            {
                return btn6;
            }
            if ((btn3.Text == "") && (btn6.Text == mark) && (btn9.Text == mark))
            {
                return btn3;
            }
            #endregion

            #region 策略 斜向檢查
            // 斜向檢查
            if ((btn1.Text == mark) && (btn5.Text == mark) && (btn9.Text == ""))
            {
                return btn9;
            }
            if ((btn1.Text == mark) && (btn5.Text == "") && (btn9.Text == mark))
            {
                return btn5;
            }
            if ((btn1.Text == "") && (btn5.Text == mark) && (btn9.Text == mark))
            {
                return btn1;
            }


            if ((btn3.Text == mark) && (btn5.Text == mark) && (btn7.Text == ""))
            {
                return btn7;
            }
            if ((btn3.Text == mark) && (btn5.Text == "") && (btn7.Text == mark))
            {
                return btn5;
            }
            if ((btn3.Text == "") && (btn5.Text == mark) && (btn7.Text == mark))
            {
                return btn3;
            }
            #endregion

            return null;
        } // lookForWinOrBlock

        private Button lookForCenter()
        {
            #region 策略 挑中間
            Console.WriteLine("Looking for center");
            if (btn5.Text == "")
            {
                return btn5;
            }
            return null;
            #endregion
        } // end lookForCenter

        private Button lookForCorner()
        {
            #region 策略 挑角落
            Console.WriteLine("Looking for corner");
            if (btn1.Text == "X")
            {
                if (btn3.Text == "")
                {
                    return btn3;
                }
                if (btn9.Text == "")
                {
                    return btn9;
                }
                if (btn7.Text == "")
                {
                    return btn7;
                }
            }

            if (btn3.Text == "X")
            {
                if (btn9.Text == "")
                {
                    return btn9;
                }
                if (btn7.Text == "")
                {
                    return btn7;
                }
                if (btn1.Text == "")
                {
                    return btn1;
                }
            }

            if (btn9.Text == "X")
            {
                if (btn7.Text == "")
                {
                    return btn7;
                }
                if (btn1.Text == "")
                {
                    return btn1;
                }
                if (btn3.Text == "")
                {
                    return btn3;
                }
            }

            if (btn7.Text == "X")
            {
                if (btn1.Text == "")
                {
                    return btn1;
                }
                if (btn3.Text == "")
                {
                    return btn3;
                }
                if (btn9.Text == "")
                {
                    return btn9;
                }
            }

            if (btn1.Text == "")
            {
                return btn1;
            }
            if (btn3.Text == "")
            {
                return btn3;
            }
            if (btn9.Text == "")
            {
                return btn9;
            }
            if (btn7.Text == "")
            {
                return btn7;
            }

            return null;
            #endregion
        } // end lookForCorner

        private Button lookForAvailableButton()
        {
            #region 策略 找還沒按過的按鈕
            Console.WriteLine("Looking for available button");
            foreach (Button btn in myBtn)
            {
                if (btn.Text == "")
                {
                    return btn;
                }
            }

            return btn1;
            #endregion
        } // end lookForAvailableButton

        #endregion

        private void txtPlayer2_TextChanged(object sender, EventArgs e)
        {
            if (txtPlayer2.Text.ToUpper() == "COMPUTER" || txtPlayer2.Text == "電腦")
            {
                againstToComputer = true;
            }
            else
            {
                againstToComputer = false;
            }
        }

        private void btnSinglePlayer_Click(object sender, EventArgs e)
        {
            txtPlayer2.Text = "電腦";
            btnStart.Text = "開始遊戲";
            initialGame();
            btnStart.Enabled = false;
            btnSinglePlayer.Enabled = false;
            turn = true;
        }
    }
}
