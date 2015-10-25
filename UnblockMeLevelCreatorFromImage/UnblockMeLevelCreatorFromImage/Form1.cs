using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnblockMeLevelCreatorFromImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadImage();
        }

        private Bitmap myBitmap;
        private List<int> VerticalLines { get; set; } = new List<int>();
        private List<int> HorizontalLines { get; set; } = new List<int>();
        private List<int> VerticalLines2 { get; set; } = new List<int>();
        private List<int> HorizontalLines2 { get; set; } = new List<int>();

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetVertical());
        }

        private string[] files;
        public void LoadImage()
        {
            files = System.IO.Directory.GetFiles(".//Pictures", "*.png");
            myBitmap = new Bitmap(files[0]);
            pictureBox1.Image = myBitmap;
            myBitmap = new Bitmap(pictureBox1.Image);
            this.Height += myBitmap.Height - 500;
        }

        private string GetVertical()
        {

            VerticalLines.Sort();
            HorizontalLines.Sort();
            bool found = false;
            string foundinf = "";
            string answer = "";

            for (int i = 0; i < VerticalLines.Count; i++)
            {
                for (int j = 0; j < HorizontalLines.Count; j++)
                {
                    Color pixelColor = myBitmap.GetPixel(VerticalLines[i], HorizontalLines[j]);
                    //     MessageBox.Show(pixelColor.R + " " + pixelColor.G + " " + pixelColor.B);
                    if (pixelColor.R < 140 && pixelColor.R > 60 || pixelColor.R < 60 && pixelColor.R > 0)
                    {
                        //   MessageBox.Show("Empty");
                        if (found)
                        {
                            foundinf = "v2" + foundinf;
                            answer += foundinf;
                            found = false;
                        }
                    }
                    else
                    {
                        if (pixelColor.G > 30)
                        {

                            //     MessageBox.Show("Block");
                            if (found)
                            {
                                foundinf = "v3" + foundinf;
                                answer += foundinf;
                                found = false;
                            }
                            else
                            {
                                found = true;
                                foundinf = (i + 1).ToString() + (j + 1).ToString();
                            }
                        }
                        else
                        {
                            answer += "h2" + (i + 1).ToString() + (j + 1).ToString();
                            found = false;
                            //     MessageBox.Show("RedBlock");
                        }
                    }


                }

            }

            return answer;
        }

        private string GetHorizontal()
        {
            VerticalLines2.Sort();
            HorizontalLines2.Sort();
            bool found = false;
            string foundinf = "";
            string answer = "";
            for (int j = 0; j < HorizontalLines2.Count; j++)
            {
                for (int i = 0; i < VerticalLines2.Count; i++)
                {
                    Color pixelColor = myBitmap.GetPixel(VerticalLines2[i], HorizontalLines2[j]);
                    //     MessageBox.Show(pixelColor.R + " " + pixelColor.G + " " + pixelColor.B);
                    if (pixelColor.R < 140 && pixelColor.R > 60 || pixelColor.R < 60 && pixelColor.R > 0)
                    {
                        //   MessageBox.Show("Empty");
                        if (found)
                        {
                            foundinf = "g2" + foundinf;
                            answer += foundinf;
                            found = false;
                        }
                    }
                    else
                    {
                        if (pixelColor.G > 30)
                        {

                            //     MessageBox.Show("Block");
                            if (found)
                            {
                                foundinf = "g3" + foundinf;
                                answer += foundinf;
                                found = false;
                            }
                            else
                            {
                                found = true;
                                foundinf = (i + 1).ToString() + (j + 1).ToString();
                            }
                        }
                        else
                        {
                            answer += "h2" + (i + 1).ToString() + (j + 1).ToString();
                            found = false;
                            //     MessageBox.Show("RedBlock");
                        }
                    }
                }
            }
            return answer;
        }
        private void click(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                pictureBox1.CreateGraphics().DrawLine(new Pen(Color.Blue), 0, e.Y, pictureBox1.Width, e.Y);
                if (!checkBox1.Checked)
                    HorizontalLines.Add(e.Y);
                else
                {
                    HorizontalLines2.Add(e.Y);
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                pictureBox1.CreateGraphics().DrawLine(new Pen(Color.DarkRed), e.X, 0, e.X, pictureBox1.Height);
                if (!checkBox1.Checked)
                    VerticalLines.Add(e.X);
                else
                {
                    VerticalLines2.Add(e.X);
                }
            }
        }

        private void DrawLines(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetHorizontal());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadImage();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            File.WriteAllText("levels", "");
            foreach (var path in files)
            {
                myBitmap = new Bitmap(path);
                File.AppendAllText("levels","levels.Add(\""+GetHorizontal()+GetVertical()+"\");"+Environment.NewLine);
            }
            MessageBox.Show("Done!");
        }
    }
}
