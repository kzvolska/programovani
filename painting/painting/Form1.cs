using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace painting
{
    public partial class Form1 : Form 
    {
        Point lastPosition;     //zavedeni globalnich promennych, ktere pouzivam napric funkcemi
        Graphics panelGraphics;
        bool mousePressed;
        Pen pen = new Pen(Color.Black);  //nastaveni pera
        int penWidth = 1;
        string tool;    //v prubehu si vzdy nastavim, jaky nastroj pouzivam
        int lastY = 0;
        int shapeHeight = 5;        
        int shapeWidth = 5;
        int firstX = 0;
        int firstY = 0;
        bool colorDialogOpened = false;
        Color color = new Color();

        public Form1()
        {
            InitializeComponent();
            panelGraphics = panel1.CreateGraphics();
        }

        private void buttonPencil_Click(object sender, EventArgs e)  //nastavim, ze nastroj je tuzka
        {
            tool = "p";
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) //pokud zmacknuta mys
        {
            firstX = e.X; //zapamatuje si souradnice doby, kdyz se zmackla
            firstY = e.Y;
            lastPosition = e.Location;  //zapamatuju si polohu mysi    
            mousePressed = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            int currentY = e.Y; 
            pen.Width = penWidth;
            if (mousePressed)   //pokud mys zmacknuta
            {
                if (tool == "c") //v pripade kaligrafickeho pera
                {
                    if (currentY > lastY) //zkoumam, jestli je akttualni poloha souradnice y vetsi nebo mensi nez predchozi
                    {
                        if (pen.Width < 9)
                        {
                            pen.Width = penWidth + penWidth; //pokud jedu dolu, bude pero tlustsi
                        }
                        else
                        {
                            pen.Width = penWidth + penWidth / 2; //rozdeleni, aby to pri vsech tloustkach vypadalo co nejpodobneji kaligraf. peru
                        }
                    }
                    else if (currentY < lastY)
                    {
                        pen.Width = penWidth;
                    }
                    panelGraphics.DrawLine(pen, lastPosition, e.Location); //vykresleni cary
                    panelGraphics.FillEllipse(new SolidBrush(pen.Color), e.X - (pen.Width / 2), e.Y - (pen.Width / 2), pen.Width, pen.Width); //vyplneni kruhem, aby byla cara plynula
                }
                else if (tool == "b") //stetec (normalni pero)
                {
                    pen.Width = penWidth + 3; //je trochu tlustsi nez hodnotz na slideru, aby se dalo rozeznat mezi perem a tuzkou
                    panelGraphics.DrawLine(pen, lastPosition, e.Location);
                    panelGraphics.FillEllipse(new SolidBrush(pen.Color), e.X - (pen.Width / 2), e.Y - (pen.Width / 2), pen.Width, pen.Width);
                }
                else if (tool == "p") //tuzka (nakonec nevypada uplne podle predstav, ale aspon takhle:))
                {
                    pen.Width = 1;  //porad je sirka pera stejna, nejmensi - aby vypadala jako tuzka
                    panelGraphics.DrawEllipse(pen, e.X, e.Y, pen.Width, pen.Width); //predstavovala jsem si, ze tuzka ma mezi sebou diry, neni plynula
                }
                else if (tool == "g")
                {
                    pen.Color = Color.White; //guma - barva se zmeni na bilou
                    pen.Width = penWidth + 3; //zase tlustsi, prijde mi ze guma o velikosti 1 neni prilis prakticka...
                    panelGraphics.DrawLine(pen, lastPosition, e.Location); //jinak stejny princip jako u pera
                    panelGraphics.FillEllipse(new SolidBrush(pen.Color), e.X - (pen.Width / 2), e.Y - (pen.Width / 2), pen.Width, pen.Width);
                }
            }
            lastPosition = e.Location; //zmenim aktualni polohu na minulou
            lastY = currentY;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            int secondX = e.X; //souradnice mysi v dobe, kdy ji postim
            int secondY = e.Y;
            pen.Width = penWidth;
            mousePressed = false;
            shapeHeight = Math.Abs(firstY - secondY); //rozdil souradnic v dobe, kdy mys mackam a kdy poustim, mi da rozmery obrazce/obrazku
            shapeWidth = Math.Abs(secondX - firstX);
            if (tool == "i") //pokud vkladam obrazek, muzu vybrat ze souboru
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    panelGraphics.DrawImage(Image.FromFile(open.FileName), firstX, firstY, shapeWidth, shapeHeight);
                }
            }
            else if (tool == "e") //kresleni elipsy
            {
                panelGraphics.DrawEllipse(pen, firstX, firstY - shapeHeight, shapeWidth, shapeHeight);
            }
            else if (tool == "l") //kresleni cary
            {
                panelGraphics.DrawLine(pen, firstX, firstY, secondX, secondY);
            }
            else if (tool == "r") //kresleni obdelniku
            {
                panelGraphics.DrawRectangle(pen, firstX, firstY, shapeWidth, secondY - firstY);
            }
            else if (tool == "fr") //kresleni full rectangle
            {
                panelGraphics.FillRectangle(new SolidBrush(pen.Color), firstX, firstY, shapeWidth, secondY - firstY);
            }
            else if (tool == "fe") //kresleni full ellipse
            {
                panelGraphics.FillEllipse(new SolidBrush(pen.Color), firstX, firstY - shapeHeight, shapeWidth, shapeHeight);
            }
            else if (tool == "g") //pote, co prestanu pouzivat gumu, se mi barva vrati opet na puvodni - vybranou dialog oknem nebo zakladni cernou
            {
                if (colorDialogOpened)
                {
                    pen.Color = color;
                }
                else
                {
                    pen.Color = Color.Black;
                }
            }
        }

        private void buttonRubber_Click(object sender, EventArgs e)
        {
            tool = "g";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            panel1.Refresh(); //umozni mi smazat vse, co jsem oposud nakeslila
        } 

        private void buttonColors_Click(object sender, EventArgs e) //dialogove okno pro vyber barev
        {
            colorDialogOpened = true;
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.Color = pen.Color;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog.Color; //ulozim barvu do promenne, zapamatovani pro pripad meneni barvy z gumy na pero
                pen.Color = colorDialog.Color; //nastavim barvu, kterou jsem si vybrala, jako barvu pera
            }
        }
        private void buttonCaligraphy_Click(object sender, EventArgs e)
        {
            tool = "c";
        }

        private void buttonBrush_Click(object sender, EventArgs e)
        {
            tool = "b";
        }
        private void Using_TrackBar_Load(object sender, EventArgs e)
        {
            trackBar1.Value = int.Parse(label1.Text);
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();  //slider - hodnota se ukazuje v popisku
            penWidth = trackBar1.Value; //nastavuje se hodnota na sirku pera
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            tool = "e";
        }

        private void buttonImage_Click(object sender, EventArgs e)
        {
            tool = "i";
        }

        private void buttonRectangle_Click(object sender, EventArgs e)
        {
            tool = "r";
        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            tool = "l";
        }

        private void buttonFullRectangle_Click(object sender, EventArgs e)
        {
            tool = "fr";
        }

        private void buttonFullCircle_Click(object sender, EventArgs e)
        {
            tool = "fe";
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}


