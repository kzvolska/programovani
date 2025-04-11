using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static class Global
        {
            public static int firstNumber = 0;
            public static int result;
            public static int secondNumber = 0;
            public static string buttonClicked;
        }

        public void Plus(int firstNumber, int secondNumber)
        {
            Global.result = firstNumber + secondNumber;
        }
        public void Minus(int firstNumber, int secondNumber)
        {
            Global.result = firstNumber - secondNumber;
        }
        public void Multiply(int firstNumber, int secondNumber)
        {
            Global.result = firstNumber * secondNumber;
        }
        public void Devide(int firstNumber, int secondNumber)
        {
            Global.result = firstNumber / secondNumber;
        }
        private void buttonA_Click(object sender, EventArgs e)
        {
            buttonA.Text = "Ahoj";
            buttonA.BackColor = Color.Aquamarine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += 6;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += 7;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += 8;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += 9;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += 0;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Global.firstNumber = int.Parse(textBox1.Text);
            textBox1.Clear();
            Global.buttonClicked = "+";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Global.secondNumber = int.Parse(textBox1.Text);
            textBox1.Clear();
            if (Global.buttonClicked == "+")
            {
                Plus(Global.firstNumber, Global.secondNumber);
            }
            else if (Global.buttonClicked == "-")
            {
                Minus(Global.firstNumber, Global.secondNumber);
            }
            else if (Global.buttonClicked == "*")
            {
                Multiply(Global.firstNumber, Global.secondNumber);
            }
            else
            {
                Devide(Global.firstNumber, Global.secondNumber);
            }

            textBox1.Text = Global.result.ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Global.firstNumber = int.Parse(textBox1.Text);
            textBox1.Clear();
            Global.buttonClicked = "-";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Global.firstNumber = int.Parse(textBox1.Text);
            textBox1.Clear();
            Global.buttonClicked = "*";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Global.firstNumber = int.Parse(textBox1.Text);
            textBox1.Clear();
            Global.buttonClicked = "/";
        }
    }
}
