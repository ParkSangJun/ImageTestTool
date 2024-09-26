//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Numerics;
//using System.Drawing.Drawing2D;
//using System.Diagnostics;

//namespace WindowsFormsApp4
//{
//    public partial class Form1 : Form
//    {
//        private Point Lastpoint;
//        private Rectangle bmprec;
//        private double ratio = 1.0F;
//        private Point bmppoint;
//        private Point Clickpoint;
//        Stopwatch sw = new Stopwatch();
//        private string ImageData;
//        private int angle = 0;
//        public Form1()
//        {
//            InitializeComponent();
//            pictureBox2.MouseWheel += new MouseEventHandler(MouseWheelEvent);
//            bmppoint = new Point(pictureBox2.Width / 2, pictureBox2.Height / 2);
//            bmprec = new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height);
//            ratio = 1.0;
//            Clickpoint = bmppoint;
//            pictureBox2.Invalidate();
//        }

//        private void MouseWheelEvent(object sender, MouseEventArgs e)
//        {
//            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

//            if (lines > 0)
//            {
//                ratio *= 1.1F;
//                if (ratio > 100.0)
//                {
//                    ratio = 100.0F;
//                }

//                bmprec.Width = (int)Math.Round(pictureBox2.Width * ratio);
//                bmprec.Height = (int)Math.Round(pictureBox2.Height * ratio);
//                bmprec.X = -(int)Math.Round(1.1F * (bmppoint.X - bmprec.X) - bmppoint.X);
//                bmprec.Y = -(int)Math.Round(1.1F * (bmppoint.Y - bmprec.Y) - bmppoint.Y);
//            }
//            else if (lines < 0)
//            {
//                ratio *= 0.9F;
//                if (ratio < 1)
//                {
//                    ratio = 1;
//                }

//                bmprec.Width = (int)Math.Round(pictureBox2.Width * ratio);
//                bmprec.Height = (int)Math.Round(pictureBox2.Height * ratio);
//                bmprec.X = -(int)Math.Round(0.9F * (bmppoint.X - bmprec.X) - bmppoint.X);
//                bmprec.Y = -(int)Math.Round(0.9F * (bmppoint.Y - bmprec.Y) - bmppoint.Y);
//            }

//            if (bmprec.X > 0)
//            {
//                bmprec.X = 0;
//            }
//            if (bmprec.Y > 0)
//            {
//                bmprec.Y = 0;
//            }
//            if (bmprec.X + bmprec.Width < pictureBox2.Width)
//            {
//                bmprec.X = pictureBox2.Width - bmprec.Width;
//            }
//            if (bmprec.Y + bmprec.Height < pictureBox2.Height)
//            {
//                bmprec.Y = pictureBox2.Height - bmprec.Height;
//            }
//            pictureBox2.Invalidate();
//        }
//        private void pictureBox2_Paint(object sender, PaintEventArgs e)
//        {
//            if (pictureBox2.Image != null)
//            {
//                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

//                e.Graphics.DrawImage(pictureBox2.Image, bmprec);
//                pictureBox2.Focus();
//            }
//        }
//        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
//        {
//            if (e.Button == MouseButtons.Left)
//            {
//                Clickpoint = new Point(e.X, e.Y);
//            }
//            pictureBox2.Invalidate();
//        }

//        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
//        {
//            if (e.Button == MouseButtons.Left)
//            {
//                bmprec.X = bmprec.X + (int)Math.Round((double)(e.X - Clickpoint.X) / 5);
//                if (bmprec.X >= 0)
//                {
//                    bmprec.X = 0;
//                }
//                if (Math.Abs(bmprec.X) >= Math.Abs(bmprec.Width - pictureBox2.Width))
//                {
//                    bmprec.X = -(bmprec.Width - pictureBox2.Width);
//                }
//                bmprec.Y = bmprec.Y + (int)Math.Round((double)(e.Y - Clickpoint.Y) / 5);
//                if (bmprec.Y >= 0)
//                {
//                    bmprec.Y = 0;
//                }
//                if (Math.Abs(bmprec.Y) >= Math.Abs(bmprec.Height - pictureBox2.Height))
//                {
//                    bmprec.Y = -(bmprec.Height - pictureBox2.Height);
//                }
//            }
//            else
//            {
//                Lastpoint = e.Location;
//            }
//            pictureBox2.Invalidate();
//        }
//        public unsafe int[,] ReadImage(Bitmap bmp)
//        {
//            Bitmap smp;
//            smp = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb);

//            BitmapData smpData = smp.LockBits(new Rectangle(0, 0, smp.Width, smp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

//            int[,] b_mp = new int[bmp.Width, bmp.Height];

//            for (int y = 0; y < smp.Height; y++)
//            {
//                byte* pit = (byte*)smpData.Scan0 + (y * smpData.Stride);
//                for (int x = 0; x < smp.Width; x++)
//                {
//                    byte r = (byte)((pit[0] + pit[1] + pit[2]) / 3);
//                    b_mp[x, y] = (int)r;

//                    pit += 3;
//                }
//            }
//            smp.UnlockBits(smpData);
//            return b_mp;
//        }

//        public unsafe Bitmap WriteImage(int[,] Input)
//        {
//            ;
//            Bitmap Write = new Bitmap(Input.GetLength(0), Input.GetLength(1));
//            BitmapData bmpd = Write.LockBits(new Rectangle(0, 0, Input.GetLength(0), Input.GetLength(1)), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);

//            for (int y = 0; y < Input.GetLength(1); y++)
//            {
//                byte* pit = (byte*)bmpd.Scan0 + (y * bmpd.Stride);
//                for (int x = 0; x < Input.GetLength(0); x++)
//                {
//                    pit[0] = pit[1] = pit[2] = (byte)Input[x, y];
//                    pit[3] = 255;
//                    pit += 4;
//                }
//            }

//            Write.UnlockBits(bmpd);

//            return Write;
//        }
//        public unsafe Bitmap WriteImage1(byte[,] Input)
//        {
//            Bitmap b_mp = (Bitmap)(pictureBox1.Image);
//            Bitmap Write = new Bitmap(b_mp);
//            BitmapData bmpd = Write.LockBits(new Rectangle(0, 0, b_mp.Width, b_mp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

//            for (int y = 0; y < b_mp.Height; y++)
//            {
//                byte* pit = (byte*)bmpd.Scan0 + (y * bmpd.Stride);
//                for (int x = 0; x < b_mp.Width; x++)
//                {
//                    pit[0] = pit[1] = pit[2] = (byte)Input[x, y];

//                    pit += 3;
//                }
//            }

//            Write.UnlockBits(bmpd);

//            return Write;
//        }

//        public unsafe Bitmap WriteImage2(double[,] Input)
//        {
//            Bitmap b_mp = (Bitmap)(pictureBox1.Image);
//            Bitmap Write = new Bitmap(b_mp);
//            BitmapData bmpd = Write.LockBits(new Rectangle(0, 0, b_mp.Width, b_mp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

//            for (int y = 0; y < b_mp.Height; y++)
//            {
//                double* pit = (double*)bmpd.Scan0 + (y * bmpd.Stride);
//                for (int x = 0; x < b_mp.Width; x++)
//                {
//                    pit[0] = pit[1] = pit[2] = (double)Input[x, y];

//                    pit += 3;
//                }
//            }

//            Write.UnlockBits(bmpd);

//            return Write;
//        }
//        public unsafe Bitmap WriteImage3(Complex[,] Input)
//        {
//            Bitmap b_mp = (Bitmap)(pictureBox1.Image);
//            Bitmap Write = new Bitmap(b_mp);
//            BitmapData bmpd = Write.LockBits(new Rectangle(0, 0, b_mp.Width, b_mp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

//            for (int y = 0; y < b_mp.Height; y++)
//            {
//                Complex* pit = (Complex*)bmpd.Scan0 + (y * bmpd.Stride);
//                for (int x = 0; x < b_mp.Width; x++)
//                {
//                    pit[0] = pit[1] = pit[2] = (Complex)Input[x, y];

//                    pit += 3;
//                }
//            }

//            Write.UnlockBits(bmpd);

//            return Write;
//        }
//        public unsafe int[] Histogram(Bitmap bmp)
//        {
//            Bitmap smp;
//            smp = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format24bppRgb);
//            BitmapData histo = smp.LockBits(new Rectangle(0, 0, smp.Width, smp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
//            int[] his = new int[256];
//            his.Initialize();
//            int width = bmp.Width;
//            int height = bmp.Height;


//            for (int z = 0; z < 256; z++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    byte* pit = (byte*)histo.Scan0 + (y * histo.Stride);
//                    for (int x = 0; x < width; x++)
//                    {
//                        if (z == pit[0])
//                        {
//                            his[z] += 1;
//                        }

//                        pit += 3;
//                    }
//                }
//            }
//            return his;
//        }

//        public unsafe byte[] Grayscale(Bitmap bmp)
//        {
//            Bitmap smp;
//            smp = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb);
//            BitmapData smpData = smp.LockBits(new Rectangle(0, 0, smp.Width, smp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
//            byte[] b_mp = new byte[bmp.Width * bmp.Height];
//            int width = bmp.Width;
//            int height = bmp.Height;

//            for (int y = 0; y < smp.Height; y++)
//            {
//                byte* pit = (byte*)smpData.Scan0 + (y * smpData.Stride);
//                for (int x = 0; x < smp.Width; x++)
//                {
//                    byte r = (byte)((pit[0] + pit[1] + pit[2]) / 3);
//                    b_mp[bmp.Width * y + x] = r;

//                    pit += 3;
//                }
//            }
//            smp.UnlockBits(smpData);
//            return b_mp;
//        }

//        public unsafe byte[,] Grayscale1(Bitmap bmp)
//        {
//            Bitmap smp;
//            smp = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb);
//            BitmapData smpData = smp.LockBits(new Rectangle(0, 0, smp.Width, smp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
//            byte[,] b_mp = new byte[bmp.Width, bmp.Height];
//            int width = bmp.Width;
//            int height = bmp.Height;

//            for (int y = 0; y < smp.Height; y++)
//            {
//                byte* pit = (byte*)smpData.Scan0 + (y * smpData.Stride);
//                for (int x = 0; x < smp.Width; x++)
//                {
//                    byte r = (byte)((pit[0] + pit[1] + pit[2]) / 3);
//                    b_mp[x, y] = r;

//                    pit += 3;
//                }
//            }
//            smp.UnlockBits(smpData);
//            return b_mp;
//        }

//        public unsafe byte[,] Binarization1(byte[,] b_mp, int width, int height, double T)
//        {
//            for (int y = 0; y < height; y++)
//            {
//                for (int x = 0; x < width; x++)
//                {
//                    if (b_mp[x, y] < T)
//                    {
//                        b_mp[x, y] = 0;
//                    }
//                    else if (b_mp[x, y] >= T)
//                    {
//                        b_mp[x, y] = 255;
//                    }
//                }
//            }
//            return b_mp;
//        }
//        public unsafe byte[] Binarization(byte[] b_mp, int width, int height)
//        {
//            int sum = 0;
//            for (int y = 0; y < height; y++)
//            {
//                for (int x = 0; x < width; x++)
//                {
//                    sum += b_mp[width * y + x];
//                }
//            }

//            sum = sum / width * height;
//            for (int y = 0; y < height; y++)
//            {
//                for (int x = 0; x < width; x++)
//                {
//                    if (b_mp[width * y + x] < sum)
//                    {
//                        b_mp[width * y + x] = 0;
//                    }
//                    else if (b_mp[width * y + x] >= sum)
//                    {
//                        b_mp[width * y + x] = 255;
//                    }
//                }
//            }
//            return b_mp;
//        }

//        public unsafe int T(int[] his)
//        {
//            double sum1 = 0;

//            for (int z = 0; z < 256; z++)
//            {
//                sum1 += his[z];
//            }
//            double[] di = new double[256];
//            di.Initialize();
//            for (int x = 0; x < 256; x++)
//            {
//                di[x] = his[x] / sum1;
//            }
//            int Max_Valuei = 0;
//            double Max_Value = 0;
//            for (int i = 0; i < 256; i++)
//            {
//                double p = 0;
//                double m1 = 0;
//                double m2 = 0;
//                for (int j = 0; j <= i; j++)
//                {

//                    p += di[j];

//                }
//                for (int j = 0; j <= i; j++)
//                {

//                    m1 += (di[j] * j) / p;

//                }
//                double q = 1 - p;
//                for (int k = i + 1; k < 256; k++)
//                {

//                    m2 += (di[k] * k) / q;

//                }
//                double sigma = (double)(p * q * Math.Pow(m1 - m2, 2));
//                if (Max_Value < sigma)
//                {
//                    Max_Value = sigma;
//                    Max_Valuei = i;
//                }
//            }
//            return Max_Valuei;
//        }

//        public unsafe Bitmap Dilation(Bitmap bmp)
//        {
//            Bitmap smp = new Bitmap(bmp.Width, bmp.Height);

//            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
//            BitmapData smpData = smp.LockBits(new Rectangle(0, 0, smp.Width, smp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

//            byte[,] hh = new byte[3, 3]
//            {
//                {0, 1, 0},
//                {1, 1, 1},
//                {0, 1, 0}
//            };
//            int size = 3;
//            int r = size / 2;
//            {
//                for (int y = r; y < smpData.Height - r; y++)
//                {
//                    byte* pit = (byte*)bmpData.Scan0 + (y * bmpData.Stride);
//                    byte* p_it = (byte*)smpData.Scan0 + (y * bmpData.Stride);
//                    for (int x = r; x < smpData.Width - r; x++)
//                    {
//                        byte max = 0;
//                        byte cValue = 0;
//                        for (int k = 0; k < 3; k++)
//                        {
//                            int i = k - r;
//                            byte* p__it = (byte*)bmpData.Scan0 + ((y + i) * bmpData.Stride);
//                            for (int t = 0; t < 3; t++)
//                            {
//                                int j = t - r;
//                                cValue = (byte)((p__it[x * 3 + j] + p__it[x * 3 + j + 1] + p__it[x * 3 + j + 2]) / 3);
//                                if (max < cValue)
//                                {
//                                    if (hh[k, t] != 0)
//                                        max = cValue;
//                                }
//                            }
//                        }
//                        p_it[0] = p_it[1] = p_it[2] = max;
//                        pit += 3;
//                        p_it += 3;
//                    }
//                }
//            }
//            smp.UnlockBits(bmpData);
//            bmp.UnlockBits(smpData);
//            return bmp;
//        }

//        public Bitmap Erosion(Bitmap bmp)
//        {
//            Bitmap smp = new Bitmap(bmp.Width, bmp.Height);
//            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
//            BitmapData smpData = smp.LockBits(new Rectangle(0, 0, smp.Width, smp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
//            byte[,] hh = new byte[3, 3]
//            {
//                { 0, 1, 0},
//                { 1, 1, 1},
//                { 0, 1, 0}
//            };
//            int size = 3;
//            int r = size / 2;
//            unsafe
//            {
//                for (int y = r; y < smpData.Height - r; y++)
//                {
//                    byte* pit = (byte*)bmpData.Scan0 + (y * bmpData.Stride);
//                    byte* p_it = (byte*)smpData.Scan0 + (y * bmpData.Stride);
//                    for (int x = r; x < smpData.Width - r; x++)
//                    {
//                        byte min = 255;
//                        byte cValue = 255;
//                        for (int k = 0; k < 3; k++)
//                        {
//                            int i = k - r;
//                            byte* p__it = (byte*)bmpData.Scan0 + ((y + i) * bmpData.Stride);
//                            for (int t = 0; t < 3; t++)
//                            {
//                                int j = t - r;
//                                cValue = (byte)((p__it[x * 3 + j] + p__it[x * 3 + j + 1] + p__it[x * 3 + j + 2]) / 3);
//                                if (min > cValue)
//                                {
//                                    if (hh[k, t] != 0)
//                                        min = cValue;
//                                }
//                            }
//                        }
//                        p_it[0] = p_it[1] = p_it[2] = min;
//                        pit += 3;
//                        p_it += 3;
//                    }
//                }
//                smp.UnlockBits(bmpData);
//                bmp.UnlockBits(smpData);
//            }
//            return bmp;
//        }

//        public unsafe int[,] Equalize(Bitmap bmp)
//        {
//            int[,] b_mp = ReadImage(bmp);
//            Bitmap b__mp = WriteImage(b_mp);
//            BitmapData histo = b__mp.LockBits(new Rectangle(0, 0, b__mp.Width, b__mp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
//            int[] his = new int[256];
//            his.Initialize();

//            for (int y = 0; y < bmp.Height; y++)
//            {
//                byte* pit = (byte*)histo.Scan0 + (y * histo.Stride);
//                for (int x = 0; x < bmp.Width; x++)
//                {
//                    his[pit[0]]++;
//                    pit += 3;
//                }
//            }
//            b__mp.UnlockBits(histo);
//            int sum = 0;
//            int[,] Histo = new int[bmp.Width, bmp.Height];
//            Histo.Initialize();
//            int sum2 = bmp.Width * bmp.Height;

//            for (int z = 0; z < 256; z++)
//            {
//                sum += his[z];

//                for (int y = 0; y < bmp.Height; y++)
//                {
//                    for (int x = 0; x < bmp.Width; x++)
//                    {
//                        if (z == b_mp[x, y])
//                        {
//                            Histo[x, y] = (int)Math.Round((double)(255 * sum / sum2));
//                        }
//                    }
//                }
//            }
//            return Histo;
//        }

//        public int[,] Gaussian(Bitmap bmp)
//        {
//            double Pi = Math.PI;
//            double sigma = 1;
//            double[,] f = new double[9, 9];

//            for (int y = 0; y < 9; y++)
//            {
//                for (int x = 0; x < 9; x++)
//                {
//                    f[x, y] = (double)(1 / (2 * Pi * Math.Pow(sigma, 2)) * (Math.Exp(-(Math.Pow(x - 4, 2) + Math.Pow(y - 4, 2))) / (2 * Math.Pow(sigma, 2))));
//                }
//            }
//            int[,] s__mp = ReadImage(bmp);
//            int[,] r__mp = (int[,])s__mp.Clone();
//            int size = 9;
//            double sum1 = 0;

//            for (int i = 0; i < size; i++)
//            {
//                for (int j = 0; j < size; j++)
//                {
//                    sum1 += f[j, i];
//                    if (sum1 < 1)
//                    {
//                        f[j, i] = f[j, i] + (double)0.008;
//                    }
//                }
//            }

//            for (int y = 0; y < (bmp.Height - size); y++)
//            {
//                for (int x = 0; x < (bmp.Width - size); x++)
//                {
//                    double sum = 0;
//                    for (int i = 0; i < size; i++)
//                    {
//                        for (int j = 0; j < size; j++)
//                        {
//                            sum += (double)(f[j, i] * r__mp[x + j, y + i]);
//                            for (int z = 0; z < size - 1; z++)
//                            {
//                                s__mp[x + z, y + z] = (int)sum;
//                            }
//                        }
//                    }
//                }
//            }
//            return s__mp;
//        }
//        public byte[,] Laplacian(Bitmap bmp)
//        {
//            Bitmap smp;
//            smp = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb);
//            byte[,] b_mp = Grayscale1(bmp);
//            byte[,] s_mp = new byte[bmp.Width, bmp.Height];

//            for (int y = 1; y < bmp.Height - 1; y++)
//            {
//                for (int x = 1; x < bmp.Width - 1; x++)
//                {
//                    byte a = b_mp[x - 1, y];
//                    byte b = b_mp[x + 1, y];
//                    byte c = b_mp[x, y - 1];
//                    byte d = b_mp[x, y + 1];
//                    byte f = b_mp[x, y];
//                    s_mp[x, y] = (byte)((a + b + c + d - 4 * f) / 16);
//                    if (s_mp[x, y] < 0)
//                    {
//                        s_mp[x, y] = 0;
//                    }
//                    if (s_mp[x, y] > 255)
//                    {
//                        s_mp[x, y] = 255;
//                    }
//                }
//            }
//            return s_mp;
//        }

//        public Bitmap rotate2(Bitmap bmp, int angle)
//        {
//            //int r = (int)Math.Sqrt(Math.Pow(bmp.Width, 2) + Math.Pow(bmp.Height, 2));
//            Bitmap smp = new Bitmap(bmp.Width, bmp.Height);
//            Graphics g = Graphics.FromImage((System.Drawing.Image)smp);
//            g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
//            g.RotateTransform(angle);
//            g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
//            g.DrawImage(bmp, new Point(0, 0));
//            return smp;
//        }
//        public double norm/*복소수 크기 구하는 함수 */(Complex z)
//        {
//            double n = Math.Sqrt(Math.Pow(z.Real, 2) + Math.Pow(z.Imaginary, 2));
//            return n;
//        }
//        public Bitmap resize(Bitmap bmp)
//        {
//            int W = bmp.Width;
//            int w = (int)Math.Log(W, 2);
//            int H = bmp.Height;
//            int h = (int)Math.Log(H, 2);
//            Rectangle res = new Rectangle(0, 0, (int)Math.Pow(2, w + 1), (int)Math.Pow(2, h + 1));
//            Bitmap smp = new Bitmap((int)Math.Pow(2, w + 1), (int)Math.Pow(2, h + 1));
//            Graphics G = Graphics.FromImage(smp);
//            G.FillRectangle(Brushes.Black, res);
//            G.DrawImage(bmp, res);
//            G.Dispose();

//            return smp;
//        }

//        public int[] Getcol(int[,] smp, int W, int i)
//        {
//            int[] a = new int[W];
//            for (int x = 0; x < W; x++)
//            {
//                a[x] = smp[x, i];
//            }
//            return a;
//        }
//        public Complex[] Getcol2(Complex[,] smp, int W, int i)
//        {
//            Complex[] a = new Complex[W];
//            for (int x = 0; x < W; x++)
//            {
//                a[x] = smp[x, i];
//            }
//            return a;
//        }
//        public Complex[] fft3(Complex[] f, int W) //순서 재배열
//        {
//            string c;
//            int d = 0;
//            Complex[] Gang = new Complex[W];
//            for (int x = 0; x < W; x++)
//            {
//                c = Convert.ToString(x, 2).PadLeft((int)Math.Log(W, 2), '0');
//                char[] A;
//                A = c.ToCharArray();
//                Array.Reverse(A);
//                string B = new string(A);
//                d = Convert.ToInt32(B, 2);
//                Gang[x] = f[d];
//            }
//            return Gang;
//        }

//        public Complex[] fft(int[] f, int W) //순서 재배열
//        {
//            string c;
//            int d = 0;
//            Complex[] Gang = new Complex[W];
//            for (int x = 0; x < W; x++)
//            {
//                c = Convert.ToString(x, 2).PadLeft((int)Math.Log(W, 2), '0');
//                char[] A;
//                A = c.ToCharArray();
//                Array.Reverse(A);
//                string B = new string(A);
//                d = Convert.ToInt32(B, 2);
//                Gang[x] = f[d];
//            }
//            return Gang;
//        }
//        public Complex[] change(Complex[] f, int H) //순서 재배열
//        {
//            string c;
//            int d;
//            Complex[] Gang = new Complex[H];
//            for (int x = 0; x < H; x++)
//            {
//                c = Convert.ToString(x, 2).PadLeft((int)Math.Log(H, 2), '0');
//                char[] A;
//                A = c.ToCharArray();
//                Array.Reverse(A);
//                string B = new string(A);
//                d = Convert.ToInt32(B, 2);
//                Gang[x] = f[d];
//            }
//            return Gang;
//        }
//        public Complex W2(int gsize, int half)
//        {
//            Complex i = new Complex(0, 1);
//            double Pi = Math.PI;
//            Complex w = Complex.Exp(-i * 2 * Pi * half / gsize);
//            return w;
//        }
//        public Complex W3(int gsize, int half)
//        {
//            Complex i = new Complex(0, 1);
//            double Pi = Math.PI;
//            Complex w = Complex.Exp(i * 2 * Pi * half / gsize);
//            return w;
//        }


//        public Complex[,] fft2(Bitmap smp, int W, int H) //행 계산 이 배열을 가지고 열 계산 진행
//        {
//            int[,] b_mp = ReadImage(smp);
//            Complex[,] hh = new Complex[W, H];
//            Complex[] t = new Complex[W];

//            for (int b = 0; b < H; b++)  //다음행으로 이동시킬 변수
//            {
//                int[] hmm = Getcol(b_mp, W, b);
//                Complex[] hyun = fft(hmm, W);

//                for (int k = 0; k < Math.Log(W, 2); k++)
//                {
//                    int gsize = (int)Math.Pow(2, k + 1);  //2,4,8,...,512 다음 묶음으로 이동 시키기 and 묶음당 연산 원소의 수
//                    int jump = (int)Math.Pow(2, (Math.Log(W, 2) - k - 1)); //256,128,...,1  묶음개수 (= W / gsize)
//                    int half = gsize / 2;
//                    t.Initialize();
//                    for (int a = 0; a < jump; a++)
//                    {
//                        for (int l = 0; l < half; l++)
//                        {
//                            t[l + gsize * a] = hyun[l + gsize * a] + W2(gsize, l) * hyun[l + half + gsize * a];
//                            t[l + half + gsize * a] = hyun[l + gsize * a] - W2(gsize, l) * hyun[l + half + gsize * a];
//                        }
//                    }
//                    for (int q = 0; q < W; q++)
//                    {
//                        hyun[q] = t[q];
//                    }
//                }
//                for (int p = 0; p < W; p++)
//                {
//                    hh[p, b] = hyun[p];
//                }
//            }
//            Complex[,] fu = new Complex[W, H];
//            Complex[] t2 = new Complex[H];
//            for (int b = 0; b < W; b++)  //다음행으로 이동시킬 변수
//            {
//                Complex[] hmm = Getrow(hh, W, H, b);
//                Complex[] hyun2 = change(hmm, H);
//                t2.Initialize();
//                for (int k = 0; k < Math.Log(H, 2); k++)
//                {
//                    int gsize = (int)Math.Pow(2, k + 1);  //2,4,8,...,512 다음 묶음으로 이동 시키기 and 묶음당 연산 원소의 수
//                    int jump = W / gsize; //256,128,...,1  묶음개수 (= W / gsize)
//                    int half = gsize / 2;
//                    for (int a = 0; a < jump; a++)
//                    {

//                        for (int l = 0; l < half; l++)
//                        {
//                            t2[l + gsize * a] = hyun2[l + gsize * a] + W2(gsize, l) * hyun2[l + half + gsize * a];
//                            t2[l + half + gsize * a] = hyun2[l + gsize * a] - W2(gsize, l) * hyun2[l + half + gsize * a];
//                        }
//                    }
//                    for (int q = 0; q < H; q++)
//                    {
//                        hyun2[q] = t2[q];
//                    }
//                }
//                for (int p = 0; p < H; p++)
//                {
//                    fu[b, p] = hyun2[p];
//                }
//            }
//            //for (int e = 0; e < H; e++)
//            //{
//            //    for (int f = 0; f < W; f++)
//            //    {
//            //        aa[f, e] = (int)norm(fu[f, e]);
//            //    }
//            //}
//            return fu;
//        }

//        public int[,] fftr(Complex[,] b_mp, int W, int H)
//        {
//            Complex[,] hh = new Complex[W, H];
//            Complex[] t = new Complex[W];
//            int[,] aa = new int[W, H];

//            for (int b = 0; b < H; b++)  //다음행으로 이동시킬 변수
//            {
//                Complex[] hmm = Getcol2(b_mp, W, b);
//                Complex[] hyun = change(hmm, W);

//                for (int k = 0; k < Math.Log(W, 2); k++)
//                {
//                    int gsize = (int)Math.Pow(2, k + 1);  //2,4,8,...,512 다음 묶음으로 이동 시키기 and 묶음당 연산 원소의 수
//                    int jump = (int)Math.Pow(2, (Math.Log(W, 2) - k - 1)); //256,128,...,1  묶음개수 (= W / gsize)
//                    int half = gsize / 2;
//                    t.Initialize();
//                    for (int a = 0; a < jump; a++)
//                    {
//                        for (int l = 0; l < half; l++)
//                        {
//                            t[l + gsize * a] = hyun[l + gsize * a] + W3(gsize, l) * hyun[l + half + gsize * a];
//                            t[l + half + gsize * a] = hyun[l + gsize * a] - W3(gsize, l) * hyun[l + half + gsize * a];
//                        }
//                    }
//                    for (int q = 0; q < W; q++)
//                    {
//                        hyun[q] = t[q];
//                    }
//                }
//                for (int p = 0; p < W; p++)
//                {
//                    hh[p, b] = hyun[p] / W;
//                }
//            }
//            Complex[,] fu = new Complex[W, H];
//            Complex[] t2 = new Complex[H];
//            for (int b = 0; b < W; b++)  //다음행으로 이동시킬 변수
//            {
//                Complex[] hmm = Getrow(hh, W, H, b);
//                Complex[] hyun2 = change(hmm, H);
//                t2.Initialize();
//                for (int k = 0; k < Math.Log(H, 2); k++)
//                {
//                    int gsize = (int)Math.Pow(2, k + 1);  //2,4,8,...,512 다음 묶음으로 이동 시키기 and 묶음당 연산 원소의 수
//                    int jump = W / gsize; //256,128,...,1  묶음개수 (= W / gsize)
//                    int half = gsize / 2;
//                    for (int a = 0; a < jump; a++)
//                    {

//                        for (int l = 0; l < half; l++)
//                        {
//                            t2[l + gsize * a] = hyun2[l + gsize * a] + W3(gsize, l) * hyun2[l + half + gsize * a];
//                            t2[l + half + gsize * a] = hyun2[l + gsize * a] - W3(gsize, l) * hyun2[l + half + gsize * a];
//                        }
//                    }
//                    for (int q = 0; q < H; q++)
//                    {
//                        hyun2[q] = t2[q];
//                    }
//                }
//                for (int p = 0; p < H; p++)
//                {
//                    fu[b, p] = hyun2[p] / H;
//                }
//            }
//            for (int e = 0; e < H; e++)
//            {
//                for (int f = 0; f < W; f++)
//                {
//                    aa[f, e] = (int)norm(fu[f, e]);
//                }
//            }
//            return aa;
//        }
//        public Complex[] Getrow(Complex[,] c, int W, int H, int i)
//        {
//            Complex[] a = new Complex[H];
//            for (int x = 0; x < H; x++)
//            {
//                a[x] = c[i, x];
//            }
//            return a;
//        }

//        public Complex[,] center(Complex[,] b_mp)
//        {
//            int W = b_mp.GetLength(0);
//            int H = b_mp.GetLength(1);
//            int m = W / 2;
//            int n = H / 2;
//            Complex[,] b__mp = new Complex[W, H];
//            for (int y = 0; y < n; y++)
//            {
//                for (int x = 0; x < m; x++)
//                {
//                    b__mp[x, y] = b_mp[x + m, y + n];
//                    b__mp[x + m, y] = b_mp[x, y + n];
//                }
//            }
//            for (int i = 0; i < n; i++)
//            {
//                for (int j = 0; j < m; j++)
//                {
//                    b__mp[j + m, i + n] = b_mp[j, i];
//                    b__mp[j, i + n] = b_mp[j + m, i];
//                }
//            }
//            return b__mp;
//        }

//        public Complex[,] remove(Complex[,] b_mp)
//        {
//            int W = b_mp.GetLength(0);
//            int H = b_mp.GetLength(1);
//            int r = 0;
//            Complex[,] b__mp = new Complex[W, H];
//            if (W >= H)
//            {
//                r = (int)W / 5;
//            }
//            else if (H > W)
//            {
//                r = (int)H / 5;
//            }
//            for (int y = 0; y < H; y++)
//            {
//                for (int x = 0; x < W; x++)
//                {
//                    if (Math.Pow(x - W / 2, 2) + Math.Pow(y - H / 2, 2) < Math.Pow(r, 2))
//                    {
//                        b__mp[x, y] = b_mp[x, y];
//                    }
//                    else
//                    {
//                        b__mp[x, y] = 0;
//                    }
//                }
//            }
//            return b__mp;
//        }

//        public int[,] cha(Complex[,] b_mp)
//        {
//            int W = b_mp.GetLength(0);
//            int H = b_mp.GetLength(1);
//            int[,] s_mp = new int[W, H];
//            for (int e = 0; e < H; e++)
//            {
//                for (int f = 0; f < W; f++)
//                {
//                    s_mp[f, e] = (int)norm(b_mp[f, e]);
//                }
//            }
//            return s_mp;
//        }

//        public byte[,] Temple(Bitmap bmp) //제곱화매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            int min = 120;
//            int m = 0;
//            int n = 0;

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    int k = 0;
//                    int sum = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum += (int)Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2] - b[j, i], 2);
//                        }
//                    }
//                    k = sum / (smp.Width * smp.Height);

//                    if (min > k)
//                    {
//                        min = k;
//                        m = x;
//                        n = y;
//                    }
//                }
//            }
//            for (int p = 0; p < smp.Width; p++)
//            {
//                for (int q = 0; q < smp.Height; q++)
//                {
//                    a[m - smp.Width / 2 + p, n - smp.Height / 2] = 255;
//                    a[m - smp.Width / 2, n - smp.Height / 2 + q] = 255;
//                    a[m - smp.Width / 2 + p, n + smp.Height / 2] = 255;
//                    a[m + smp.Width / 2, n - smp.Height / 2 + q] = 255;
//                }
//            }
//            return a;
//        }

//        public int[,] Tem(Bitmap Image) //제곱화매칭
//        {
//            Bitmap bmp = (Bitmap)Image;
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            int max = 1;
//            int k = 0;
//            int m = 0;
//            int n = 0;
//            int[,] c = new int[bmp.Width, bmp.Height];

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    int sum = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum += (int)Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2] - b[j, i], 2);
//                        }
//                    }
//                    k = sum / (smp.Width * smp.Height);
//                    if (max < k)
//                    {
//                        max = k;
//                        m = x;
//                        n = y;
//                    }
//                    c[x, y] = k * 255 / max;
//                }
//            }
//            return c;
//        }

//        public byte[,] Temple2(Bitmap bmp) //정규 제곱화 매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            double min = 1;
//            double k = 0;
//            int m = 0;
//            int n = 0;

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    int sum = 0;
//                    int sum2 = 0;
//                    int sum3 = 0;
//                    double sum4 = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum += (int)Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2] - b[j, i], 2);
//                            sum2 += (int)Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2], 2);
//                            sum3 += (int)Math.Pow(b[j, i], 2);
//                            sum4 = sum / (int)Math.Pow(sum2 * sum3, 1 / 2);
//                        }
//                    }
//                    k = sum4 / (smp.Width * smp.Height);

//                    if (min > k)
//                    {
//                        min = k;
//                        m = x;
//                        n = y;
//                    }
//                }
//            }
//            for (int x = 0; x < smp.Width; x++)
//            {
//                for (int y = 0; y < smp.Height; y++)
//                {
//                    a[m - smp.Width / 2 + x, n - smp.Height / 2] = 255;
//                    a[m - smp.Width / 2, n - smp.Height / 2 + y] = 255;
//                    a[m - smp.Width / 2 + x, n + smp.Height / 2] = 255;
//                    a[m + smp.Width / 2, n - smp.Height / 2 + y] = 255;
//                }
//            }
//            return a;
//        }
//        public int[,] Tem2(Bitmap bmp) //정규 제곱화 매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            double max = 1;
//            double k = 0;
//            int[,] c = new int[bmp.Width, bmp.Height];

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    double sum = 0;
//                    double sum2 = 0;
//                    double sum3 = 0;
//                    double sum4 = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum += Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2] - b[j, i], 2);
//                            sum2 += Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2], 2);
//                            sum3 += Math.Pow(b[j, i], 2);
//                            sum4 = sum / Math.Pow(sum2 * sum3, 1 / 2);
//                        }
//                    }
//                    k = sum4 / (smp.Width * smp.Height);

//                    if (max < k)
//                    {
//                        max = k;
//                    }
//                    c[x, y] = (int)(k * 255 / max);
//                }
//            }
//            return c;
//        }
//        public byte[,] Temple3(Bitmap bmp) //상관관계 매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            int max = 0;
//            int k = 0;

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    int sum = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum += (int)a[x + j - smp.Width / 2, y + i - smp.Height / 2] * b[j, i];
//                        }
//                    }
//                    k = sum / (smp.Width * smp.Height);

//                    if (max < k)
//                    {
//                        max = k;

//                    }
//                }
//            }
//            return a;
//        }

//        public int[,] Tem3(Bitmap bmp) //상관관계 매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            int max = 0;
//            int k = 0;
//            int[,] c = new int[bmp.Width, bmp.Height];

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    int sum = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum += (int)a[x + j - smp.Width / 2, y + i - smp.Height / 2] * b[j, i];
//                        }
//                    }
//                    k = sum / (smp.Width * smp.Height);
//                    if (max < k)
//                    {
//                        max = k;
//                    }
//                    c[x, y] = k * 255 / max;
//                }
//            }
//            return c;
//        }

//        public byte[,] Temple4(Bitmap bmp) //상관계수 매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            int max = 1;
//            int k = 0;
//            int m = 0;
//            int n = 0;

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    int sum = 0;
//                    int sum2 = 0;
//                    int sum3 = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum2 += a[x + j - smp.Width / 2, y + i - smp.Height / 2];
//                            sum3 += b[j, i];
//                            sum += (int)((a[x + j - smp.Width / 2, y + i - smp.Height / 2] - sum2 / (bmp.Width * bmp.Height)) * (b[j, i] - sum3 / (smp.Width * smp.Height)));
//                        }
//                    }
//                    k = sum / (smp.Width * smp.Height);

//                    if (max < k)
//                    {
//                        max = k;
//                        m = x;
//                        n = y;
//                    }
//                }
//            }
//            for (int x = 0; x < smp.Width; x++)
//            {
//                for (int y = 0; y < smp.Height; y++)
//                {
//                    a[m - smp.Width / 2 + x, n - smp.Height / 2] = 255;
//                    a[m - smp.Width / 2, n - smp.Height / 2 + y] = 255;
//                    a[m - smp.Width / 2 + x, n + smp.Height / 2] = 255;
//                    a[m + smp.Width / 2, n - smp.Height / 2 + y] = 255;
//                }
//            }
//            return a;
//        }
//        public int[,] Tem4(Bitmap bmp) //상관계수 매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            int max = 1;
//            int k = 0;
//            int[,] c = new int[bmp.Width, bmp.Height];

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    int sum = 0;
//                    int sum2 = 0;
//                    int sum3 = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum2 += a[x + j - smp.Width / 2, y + i - smp.Height / 2];
//                            sum3 += b[j, i];
//                            sum += (int)((a[x + j - smp.Width / 2, y + i - smp.Height / 2] - sum2 / (smp.Width * smp.Height)) * (b[j, i] - sum3 / (smp.Width * smp.Height)));
//                        }
//                    }
//                    k = sum / (smp.Width * smp.Height);

//                    if (max < k)
//                    {
//                        max = k;
//                    }
//                }
//            }
//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    int sum = 0;
//                    int sum2 = 0;
//                    int sum3 = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum2 += a[x + j - smp.Width / 2, y + i - smp.Height / 2];
//                            sum3 += b[j, i];
//                            sum += (int)((a[x + j - smp.Width / 2, y + i - smp.Height / 2] - sum2 / (smp.Width * smp.Height)) * (b[j, i] - sum3 / (smp.Width * smp.Height)));
//                        }
//                    }
//                    k = sum / (smp.Width * smp.Height);

//                    if (k < 0)
//                    {
//                        c[x, y] = -k * 255 / max;
//                    }
//                    else
//                    {
//                        c[x, y] = k * 255 / max;
//                    }
//                }
//            }
//            return c;
//        }
//        public byte[,] Temple5(Bitmap bmp) //정규 상관관계 매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            double max = 0;
//            double k = 0;
//            int m = 0;
//            int n = 0;

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    double sum = 0;
//                    double sum2 = 0;
//                    double sum3 = 0;
//                    double sum4 = 0;
//                    double sum5 = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum += (int)a[x + j - smp.Width / 2, y + i - smp.Height / 2] * b[j, i];
//                            sum2 += (int)Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2], 2);
//                            sum3 += (int)Math.Pow(b[j, i], 2);
//                            sum5 = Math.Sqrt(sum2 * sum3);
//                            sum4 = sum / sum5;
//                        }
//                    }
//                    k = sum4 / (smp.Width * smp.Height);

//                    if (max < k)
//                    {
//                        max = k;
//                        m = x;
//                        n = y;
//                    }
//                }
//            }
//            for (int x = 0; x < smp.Width; x++)
//            {
//                for (int y = 0; y < smp.Height; y++)
//                {
//                    a[m - smp.Width / 2 + x, n - smp.Height / 2] = 255;
//                    a[m - smp.Width / 2, n - smp.Height / 2 + y] = 255;
//                    a[m - smp.Width / 2 + x, n + smp.Height / 2] = 255;
//                    a[m + smp.Width / 2, n - smp.Height / 2 + y] = 255;

//                }
//            }
//            return a;
//        }

//        public int[,] Tem5(Bitmap bmp) //정규 상관관계 매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            double max = 0;
//            double k = 0;
//            int[,] c = new int[bmp.Width, bmp.Height];

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    double sum = 0;
//                    double sum2 = 0;
//                    double sum3 = 0;
//                    double sum4 = 0;
//                    double sum5 = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum += (int)a[x + j - smp.Width / 2, y + i - smp.Height / 2] * b[j, i];
//                            sum2 += (int)Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2], 2);
//                            sum3 += (int)Math.Pow(b[j, i], 2);
//                            sum5 = Math.Sqrt(sum2 * sum3);
//                            sum4 = sum / sum5;
//                        }
//                    }
//                    k = sum4 / (smp.Width * smp.Height);

//                    if (max < k)
//                    {
//                        max = k;
//                    }
//                    c[x, y] = (int)(k * 255 / max);
//                }
//            }
//            return c;
//        }

//        public byte[,] Temple6(Bitmap bmp) //정규 상관계수 매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);
//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            double max = 0;
//            double k = 0;
//            int m = 0;
//            int n = 0;

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    double sum = 0;
//                    double sum2 = 0;
//                    double sum3 = 0;
//                    double sum4 = 0;
//                    double sum5 = 0;
//                    double sum6 = 0;
//                    double sum7 = 0;
//                    double sum8 = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum2 += a[x + j - smp.Width / 2, y + i - smp.Height / 2];
//                            sum3 += b[j, i];
//                            sum5 = sum2 / (smp.Width * smp.Height);
//                            sum6 = sum3 / (smp.Width * smp.Height);
//                            sum += ((a[x + j - smp.Width / 2, y + i - smp.Height / 2] - sum5) * (b[j, i] - sum6));
//                            sum7 += Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2] - sum5, 2);
//                            sum8 += Math.Pow(b[j, i] - sum6, 2);

//                        }
//                    }
//                    sum4 = sum / Math.Sqrt((sum7 * sum8));
//                    k = sum4 / (smp.Width * smp.Height);

//                    if (max < k)
//                    {
//                        max = k;
//                        m = x;
//                        n = y;
//                    }
//                }
//            }
//            for (int x = 0; x < smp.Width; x++)
//            {
//                for (int y = 0; y < smp.Height; y++)
//                {
//                    a[m - smp.Width / 2 + x, n - smp.Height / 2] = 255;
//                    a[m - smp.Width / 2, n - smp.Height / 2 + y] = 255;
//                    a[m - smp.Width / 2 + x, n + smp.Height / 2] = 255;
//                    a[m + smp.Width / 2, n - smp.Height / 2 + y] = 255;
//                }
//            }
//            return a;
//        }
//        public int[,] Tem6(Bitmap bmp) //정규 상관계수 매칭
//        {
//            Bitmap smp = (Bitmap)(pictureBox2.Image);

//            byte[,] a = Grayscale1(bmp);
//            byte[,] b = Grayscale1(smp);
//            double max = 0;
//            double k = 0;
//            int[,] c = new int[bmp.Width, bmp.Height];

//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    double sum = 0;
//                    double sum2 = 0;
//                    double sum3 = 0;
//                    double sum4 = 0;
//                    double sum5 = 0;
//                    double sum6 = 0;
//                    double sum7 = 0;
//                    double sum8 = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum2 += a[x + j - smp.Width / 2, y + i - smp.Height / 2];
//                            sum3 += b[j, i];
//                            sum5 = sum2 / (smp.Width * smp.Height);
//                            sum6 = sum3 / (smp.Width * smp.Height);
//                            sum += ((a[x + j - smp.Width / 2, y + i - smp.Height / 2] - sum5) * (b[j, i] - sum6));
//                            sum7 += Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2] - sum5, 2);
//                            sum8 += Math.Pow(b[j, i] - sum6, 2);
//                        }
//                    }
//                    sum4 = sum / Math.Sqrt((sum7 * sum8));
//                    k = sum4 / (smp.Width * smp.Height);

//                    if (max < k)
//                    {
//                        max = k;
//                    }
//                }
//            }
//            for (int y = smp.Height / 2; y < bmp.Height - smp.Height / 2; y++)
//            {
//                for (int x = smp.Width / 2; x < bmp.Width - smp.Width / 2; x++)
//                {
//                    double sum = 0;
//                    double sum2 = 0;
//                    double sum3 = 0;
//                    double sum4 = 0;
//                    double sum5 = 0;
//                    double sum6 = 0;
//                    double sum7 = 0;
//                    double sum8 = 0;
//                    for (int i = 0; i < smp.Height; i++)
//                    {
//                        for (int j = 0; j < smp.Width; j++)
//                        {
//                            sum2 += a[x + j - smp.Width / 2, y + i - smp.Height / 2];
//                            sum3 += b[j, i];
//                            sum5 = sum2 / (smp.Width * smp.Height);
//                            sum6 = sum3 / (smp.Width * smp.Height);
//                            sum += ((a[x + j - smp.Width / 2, y + i - smp.Height / 2] - sum5) * (b[j, i] - sum6));
//                            sum7 += Math.Pow(a[x + j - smp.Width / 2, y + i - smp.Height / 2] - sum5, 2);
//                            sum8 += Math.Pow(b[j, i] - sum6, 2);
//                        }
//                    }
//                    sum4 = sum / Math.Sqrt((sum7 * sum8));
//                    k = sum4 / (smp.Width * smp.Height);
//                    if (k < 0)
//                    {
//                        c[x, y] = (int)(-k * 255 / max);
//                    }
//                    else
//                    {
//                        c[x, y] = (int)(k * 255 / max);
//                    }
//                }
//            }
//            return c;
//        }
//        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
//        {

//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            string image_file = string.Empty;

//            OpenFileDialog dialog = new OpenFileDialog();
//            dialog.InitialDirectory = @"C:\Users\hyunho39403940\Downloads\과제 이미지\과제 이미지";

//            if (dialog.ShowDialog() == DialogResult.OK)
//            {
//                image_file = dialog.FileName;
//                ImageData = dialog.FileName;
//                pictureBox1.Image = Bitmap.FromFile(image_file);
//            }
//        }
//        private void button1_MouseDown(object sender, MouseEventArgs e)
//        {
//            if (e.Button == MouseButtons.Right)
//            {
//                string save;

//                SaveFileDialog dialog1 = new SaveFileDialog();

//                dialog1.InitialDirectory = @"D:\";
//                dialog1.Title = "경로를 설정하세요.";
//                dialog1.OverwritePrompt = true;
//                dialog1.Filter = "JPEG File(*.JPG)|*.jpg|Bitmap File(*.bmp)|*.bmp|PNG File(*.png)|*.png";
//                if (dialog1.ShowDialog() == DialogResult.OK)
//                {
//                    save = dialog1.FileName;
//                    pictureBox1.Image.Save(save);
//                }
//            }
//        }
//        private void button2_Click(object sender, EventArgs e)
//        {
//            {
//                string image_file = string.Empty;

//                OpenFileDialog dialog = new OpenFileDialog();
//                dialog.InitialDirectory = @"C:\Users\hyunho39403940\Downloads\과제 이미지\과제 이미지";

//                if (dialog.ShowDialog() == DialogResult.OK)
//                {
//                    image_file = dialog.FileName;
//                    pictureBox2.Image = Bitmap.FromFile(image_file);
//                }
//            }
//        }
//        private void button2_MouseDown(object sender, MouseEventArgs e)
//        {
//            if (e.Button == MouseButtons.Right)
//            {
//                string save;

//                SaveFileDialog dialog2 = new SaveFileDialog();
//                dialog2.InitialDirectory = @"D:\";
//                dialog2.Title = "저장 경로를 지정하세요.";
//                dialog2.OverwritePrompt = true;
//                dialog2.Filter = "JPEG File(*.JPG)|*.jpg|Bitmap File(*.bmp)|*,bmp|PNG File(*.png)|*.png";
//                if (dialog2.ShowDialog() == DialogResult.OK)
//                {
//                    save = dialog2.FileName;
//                    pictureBox2.Image.Save(save);
//                }
//            }
//        }
//        private unsafe void button3_Click(object sender, EventArgs e)
//        {
//            sw.Start();
//            try
//            {
//                if (pictureBox1.Image == null)
//                {
//                    MessageBox.Show("사진을 추가해주세요");
//                }
//                else
//                {
//                    Bitmap bmp = new Bitmap(pictureBox1.Image);
//                    Bitmap smp = Dilation(bmp);
//                    pictureBox2.Image = smp;
//                }
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message, "Unimpliable Mask");
//            }
//            sw.Stop();
//            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
//            sw.Reset();
//        }
//        private void button4_Click(object sender, EventArgs e)
//        {
//            sw.Start();
//            try
//            {
//                if (pictureBox1.Image == null)
//                {
//                    MessageBox.Show("사진을 추가하세요");
//                }
//                else
//                {
//                    Bitmap bmp = (Bitmap)pictureBox1.Image;
//                    Bitmap smp = Erosion(bmp);
//                    pictureBox2.Image = smp;
//                }
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message, "Unimpliable Mask");
//            }
//            sw.Stop();
//            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
//            sw.Reset();
//        }
//        private unsafe void button5_Click(object sender, EventArgs e)
//        {
//            sw.Start();
//            try
//            {
//                Bitmap bmp = (Bitmap)(pictureBox1.Image);
//                int[,] smp = Equalize(bmp);
//                pictureBox2.Image = WriteImage(smp);
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message, "Unimpliable Mask");
//            }
//            sw.Stop();
//            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
//            sw.Reset();
//        }
//        private unsafe void button6_Click(object sender, EventArgs e)
//        {
//            sw.Start();
//            try
//            {
//                Bitmap bmp = (Bitmap)(pictureBox1.Image);
//                byte[,] b_mp = Grayscale1(bmp);
//                Bitmap b__mp = WriteImage1(b_mp);
//                int[] his = Histogram(b__mp);
//                int width = bmp.Width;
//                int height = bmp.Height;
//                int M = T(his);
//                byte[,] b___mp = Binarization1(b_mp, width, height, M);
//                pictureBox2.Image = WriteImage1(b___mp);
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message, "Unimpliable Mask");
//            }
//            sw.Stop();
//            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
//            sw.Reset();
//        }
//        private unsafe void button7_Click(object sender, EventArgs e)
//        {
//            sw.Start();
//            try
//            {
//                Bitmap bmp = (Bitmap)(pictureBox1.Image);
//                int[,] s__mp = ReadImage(bmp);
//                int[,] r__mp = (int[,])s__mp.Clone();
//                int[,] f = Gaussian(bmp);
//                pictureBox2.Image = WriteImage(f);
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message, "Unimpliable Mask");
//            }
//            sw.Stop();
//            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
//            sw.Reset();
//        }
//        private void button8_Click(object sender, EventArgs e)
//        {
//            sw.Start();
//            try
//            {
//                Bitmap bmp = (Bitmap)(pictureBox1.Image);
//                byte[,] smp = Laplacian(bmp);
//                pictureBox2.Image = WriteImage1(smp);
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message, "Unimpliable Mask");
//            }
//            sw.Stop();
//            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
//            sw.Reset();
//        }
//        private void button9_Click(object sender, EventArgs e)
//        {
//            sw.Start();
//            try
//            {
//                Bitmap bmp = (Bitmap)(pictureBox1.Image);
//                Bitmap smp = resize(bmp);
//                int W = smp.Width;
//                int H = smp.Height;
//                Complex[,] b_mp = fft2(smp, W, H);
//                Complex[,] b__mp = center(b_mp);
//                Complex[,] s_mp = remove(b__mp);
//                Complex[,] s__mp = center(s_mp);
//                int[,] k_mp = fftr(s__mp, W, H);
//                pictureBox2.Image = WriteImage(k_mp);
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message, "Unimpliable Mask");
//            }
//            sw.Stop();
//            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
//            sw.Reset();
//        }
//        private void button10_Click(object sender, EventArgs e)
//        {
//            sw.Start();
//            try
//            {
//                Bitmap bmp = (Bitmap)(pictureBox1.Image);
//                byte[,] c = Temple(bmp);
//                pictureBox1.Image = WriteImage1(c);
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message, "Unimpliable Mask");
//            }
//            sw.Stop();
//            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
//            sw.Reset();
//        }

//        private unsafe void button11_Click(object sender, EventArgs e)
//        {
//            sw.Start();
//            try
//            {
//                Bitmap bmp = (Bitmap)(pictureBox1.Image);
//                int[,] c = Tem(bmp);
//                pictureBox1.Image = WriteImage(c);
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message, "Unimpliable Mask");
//            }
//            sw.Stop();
//            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
//            sw.Reset();
//        }

//        private void button12_Click(object sender, EventArgs e)
//        {
//            angle -= 45;
//            Bitmap bmp = (Bitmap)Bitmap.FromFile(ImageData);
//            Bitmap b_mp = rotate2(bmp, angle);
//            pictureBox1.Image = b_mp;
//            pictureBox1.Invalidate();
//        }

//        private void button13_Click(object sender, EventArgs e)
//        {
//            angle += 45;
//            Bitmap bmp = (Bitmap)Bitmap.FromFile(ImageData);
//            Bitmap b_mp = rotate2(bmp, angle);
//            pictureBox1.Image = b_mp;
//            pictureBox1.Invalidate();
//        }
//    }
//}