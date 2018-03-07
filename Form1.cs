using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace demo2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int counter = 1;
        Bitmap myBitmap;

        private void button1_Click(object sender, EventArgs e)
        {
            //插入圖片
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "所有檔案|*.*|BMP File| *.bmp|JPEG File|*.jpg| GIF File|*.gif";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)   ////由對話框選取圖檔
            {
                myBitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = myBitmap;
            }
            pictureBox2.Image = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //色彩轉灰階
            try
            {
                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap newbitmap = new Bitmap(Width, Height);
                Bitmap oldbitmap = (Bitmap)this.pictureBox1.Image;
                Color pixel;
                int r, g, b, Result = 0;
                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {
                        pixel = oldbitmap.GetPixel(x, y);
                        //int r, g, b, Result = 0;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        //平均值法產生黑白圖像
                        Result = ((r + g + b) / 3);
                        newbitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    }
                this.pictureBox2.Image = newbitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //輸出圖片
            if (this.pictureBox2.Image != null)
            {
                string output = "output" + counter + ".bmp";
                this.pictureBox2.Image.Save(output);
                counter = counter + 1;
                MessageBox.Show("成功輸出圖片!!!", "success");
            }
            else
                MessageBox.Show("請選取圖片!!!", "error");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //底片效果
            try
            {
                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap newbitmap = new Bitmap(Width, Height);
                Bitmap oldbitmap = (Bitmap)this.pictureBox1.Image;
                Color pixel;
                int r = 0, g = 0, b = 0;
                for (int x = 1; x < Width; x++)
                {
                    for (int y = 1; y < Height; y++)
                    {
                        //int r, g, b;
                        pixel = oldbitmap.GetPixel(x, y);
                        r = 255 - pixel.R;
                        g = 255 - pixel.G;
                        b = 255 - pixel.B;
                        newbitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                this.pictureBox2.Image = newbitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //浮雕
            try
            {
                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap newbitmap = new Bitmap(Width, Height);
                Bitmap oldbitmap = (Bitmap)this.pictureBox1.Image;
                Color pixel1, pixel2;
                int r = 0, g = 0, b = 0;
                for (int x = 0; x < Width - 1; x++)
                {
                    for (int y = 0; y < Height - 1; y++)
                    {
                        //int r = 0, g = 0, b = 0;
                        pixel1 = oldbitmap.GetPixel(x, y);
                        pixel2 = oldbitmap.GetPixel(x + 1, y + 1);
                        r = Math.Abs(pixel1.R - pixel2.R + 128);
                        g = Math.Abs(pixel1.G - pixel2.G + 128);
                        b = Math.Abs(pixel1.B - pixel2.B + 128);
                        if (r > 255)
                            r = 255;
                        if (r < 0)
                            r = 0;
                        if (g > 255)
                            g = 255;
                        if (g < 0)
                            g = 0;
                        if (b > 255)
                            b = 255;
                        if (b < 0)
                            b = 0;
                        newbitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                this.pictureBox2.Image = newbitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        private void button7_Click(object sender, EventArgs e)
        {
            //魚眼
            try
            {
                int Width = this.pictureBox1.Image.Width;
                int Height = this.pictureBox1.Image.Height;
                int maxHW = Width;
                int minHW = Height;
                if (Height > Width)
                {
                    maxHW = Height;
                    minHW = Width;
                }
                int midH = Height / 2;
                int midW = Width / 2;
                Bitmap fish = new Bitmap(Width, Height);
                Bitmap ImgData = new Bitmap(Width, Height);
                Bitmap oldBitmap = (Bitmap)this.pictureBox1.Image;

                Color pixel1;
                int r1 = 0, g1 = 0, b1 = 0;

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        pixel1 = oldBitmap.GetPixel(x, y);
                        r1 = pixel1.R;
                        g1 = pixel1.G;
                        b1 = pixel1.B;
                        ImgData.SetPixel(x, y, Color.FromArgb(r1, g1, b1));
                        if (Math.Sqrt((double)((x - midW) * (x - midW) + (y - midH) * (y - midH))) > (double)1 / 2 * minHW)
                        {
                            r1 = 0; g1 = 0; b1 = 0;

                            ImgData.SetPixel(x, y, Color.FromArgb(r1, g1, b1));
                            fish.SetPixel(x, y, Color.FromArgb(r1, g1, b1));
                        }
                        else
                        {
                            pixel1 = ImgData.GetPixel(x, y);
                            r1 = pixel1.R;
                            g1 = pixel1.G;
                            b1 = pixel1.B;
                            fish.SetPixel(x, y, Color.FromArgb(r1, g1, b1));
                        }
                    }
                }
                //位移 改image data , fish 拿來被參考
                double r, rp;
                int tempx = 0, tempy = 0;
                int r2 = 0, g2 = 0, b2 = 0;
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (Math.Sqrt((double)((x - midW) * (x - midW) + (y - midH) * (y - midH))) < (double)1 / 2 * minHW)
                        {
                            rp = Math.Sqrt((midW - x) * (midW - x) + (midH - y) * (midH - y));
                            r = minHW / 2 * Math.Sqrt(1 - Math.Sqrt(1 - rp * 2 / minHW * rp * 2 / minHW * rp * 2 / minHW));

                            tempx = (int)(((x - midW) * r / rp) + midW);
                            tempy = (int)(((y - midH) * r / rp) + midH);
                            if (tempx >= 0 && tempx < Width && tempy >= 0 && tempy < Height)
                            {
                                pixel1 = fish.GetPixel(tempx, tempy);
                                r2 = pixel1.R;
                                g2 = pixel1.G;
                                b2 = pixel1.B;
                                ImgData.SetPixel(x, y, Color.FromArgb(r2, g2, b2));
                            }
                        }
                    }
                }
                //pictureBox1.Image = myBitmap;
                this.pictureBox2.Image = ImgData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //馬賽克
            try
            {
                //int effectWidth = 10;                
                int effectWidth = int.Parse(textBox2.Text);
                int Width = this.pictureBox1.Image.Width;
                int Height = this.pictureBox1.Image.Height;
                int maxHW = Width;
                int minHW = Height;
                Bitmap oldbitmap = (Bitmap)this.pictureBox1.Image;
                Bitmap newbitmap = new Bitmap(maxHW, minHW);
                // 差異最多的就是以照一定範圍取樣 玩之後直接去下一個範圍
                for (int heightOfffset = 0; heightOfffset < minHW; heightOfffset += effectWidth)
                {
                    for (int widthOffset = 0; widthOffset < maxHW; widthOffset += effectWidth)
                    {
                        int avgR = 0, avgG = 0, avgB = 0;
                        int blurPixelCount = 0;

                        for (int x = widthOffset; (x < widthOffset + effectWidth && x < maxHW); x++)
                        {
                            for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < minHW); y++)
                            {
                                System.Drawing.Color pixel = oldbitmap.GetPixel(x, y);

                                avgR += pixel.R;
                                avgG += pixel.G;
                                avgB += pixel.B;

                                blurPixelCount++;
                            }
                        }
                        // 計算範圍平均
                        avgR = avgR / blurPixelCount;
                        avgG = avgG / blurPixelCount;
                        avgB = avgB / blurPixelCount;


                        // 所有範圍內都設定此值
                        for (int x = widthOffset; (x < widthOffset + effectWidth && x < maxHW); x++)
                        {
                            for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < minHW); y++)
                            {
                                System.Drawing.Color newColor = System.Drawing.Color.FromArgb(avgR, avgG, avgB);
                                newbitmap.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                                //newbitmap.SetPixel(x, y, newColor);
                            }
                        }


                    }
                }
                this.pictureBox2.Image = newbitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //暖色
            try
            {
                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap newbitmap = new Bitmap(Width, Height);
                Bitmap oldbitmap = (Bitmap)this.pictureBox1.Image;
                Color pixel1;
                int r = 0, g = 0, b = 0;
                for (int x = 0; x < Width - 1; x++)
                {
                    for (int y = 0; y < Height - 1; y++)
                    {
                        //int r = 0, g = 0, b = 0;
                        pixel1 = oldbitmap.GetPixel(x, y);
                        r = Math.Abs(pixel1.R + 100);
                        if (r >= 255)
                        {
                            r = 255;
                        }
                        g = Math.Abs(pixel1.G);
                        b = Math.Abs(pixel1.B);

                        newbitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                this.pictureBox2.Image = newbitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //冷色
            try
            {
                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap newbitmap = new Bitmap(Width, Height);
                Bitmap oldbitmap = (Bitmap)this.pictureBox1.Image;
                Color pixel1;
                int r = 0, g = 0, b = 0;
                for (int x = 0; x < Width - 1; x++)
                {
                    for (int y = 0; y < Height - 1; y++)
                    {
                        //int r = 0, g = 0, b = 0;
                        pixel1 = oldbitmap.GetPixel(x, y);
                        r = Math.Abs(pixel1.R);
                        g = Math.Abs(pixel1.G);
                        b = Math.Abs(pixel1.B + 100);
                        if (b >= 255)
                        {
                            b = 255;
                        }

                        newbitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                this.pictureBox2.Image = newbitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //右轉90度
            try
            {
                Bitmap bitmap = (Bitmap)this.pictureBox1.Image;           
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                this.pictureBox2.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try//亮度調亮
            {
                Bitmap bitmap = (Bitmap)this.pictureBox1.Image;
                //int valBrightness = 50;
                int valBrightness = int.Parse(textBox1.Text);
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        // 取得每一個 pixel
                        var pixel = bitmap.GetPixel(x, y);

                        // 判斷 如果處理過後 255 就設定為 255 如果小於則設定為 0
                            var pR = ((pixel.R + valBrightness > 255) ? 255 : pixel.R + valBrightness) < 0 ? 0 : ((pixel.R + valBrightness > 255) ? 255 : pixel.R + valBrightness);
                            var pG = ((pixel.G + valBrightness > 255) ? 255 : pixel.G + valBrightness) < 0 ? 0 : ((pixel.G + valBrightness > 255) ? 255 : pixel.G + valBrightness);
                            var pB = ((pixel.B + valBrightness > 255) ? 255 : pixel.B + valBrightness) < 0 ? 0 : ((pixel.B + valBrightness > 255) ? 255 : pixel.B + valBrightness);

                            // 將改過的 RGB 寫回
                            bitmap.SetPixel(x, y, Color.FromArgb(pR, pG, pB));
                    }
                }
                // 回傳結果
                this.pictureBox2.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try//亮度調暗
            {
                Bitmap bitmap = (Bitmap)this.pictureBox1.Image;
                //int valBrightness = -50;
                int valBrightness = -(int.Parse(textBox3.Text));
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        // 取得每一個 pixel
                        var pixel = bitmap.GetPixel(x, y);

                        // 判斷 如果處理過後 255 就設定為 255 如果小於則設定為 0
                        var pR = ((pixel.R + valBrightness > 255) ? 255 : pixel.R + valBrightness) < 0 ? 0 : ((pixel.R + valBrightness > 255) ? 255 : pixel.R + valBrightness);
                        var pG = ((pixel.G + valBrightness > 255) ? 255 : pixel.G + valBrightness) < 0 ? 0 : ((pixel.G + valBrightness > 255) ? 255 : pixel.G + valBrightness);
                        var pB = ((pixel.B + valBrightness > 255) ? 255 : pixel.B + valBrightness) < 0 ? 0 : ((pixel.B + valBrightness > 255) ? 255 : pixel.B + valBrightness);

                        // 將改過的 RGB 寫回
                        bitmap.SetPixel(x, y, Color.FromArgb(pR, pG, pB));
                    }
                }
                // 回傳結果
                this.pictureBox2.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
