using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SSILab4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string x = "Obrazek.JPG";
            string x2 = "Obrazek2.JPG";
            string x3 = "Obrazek3.JPG";

            double[][] kernel = new double[3][];
            kernel[0] = new double[] { 1, 2, 1 };
            kernel[1] = new double[] { 2, 4, 2 };
            kernel[2] = new double[] { 1, 2, 1 };

            double[][] kernel2 = new double[3][];
            kernel2[0] = new double[] { -1, -1, -1 };
            kernel2[1] = new double[] { -1, 8, -1 };
            kernel2[2] = new double[] { -1, -1, -1 };


            Filtr(x, x2, kernel);
            Filtr(x2, x3, kernel2);
            Odejmowanie();
            PunktyKluczowe();

            Console.ReadKey();
        }

        public static void Filtr(string n1, string n2, double[][] kernel)
        {
            Bitmap btm = new Bitmap(@"C:\Users\LENOVO\Desktop\" + n1);

            Bitmap btmF = new Bitmap(btm.Width, btm.Height);


            for (int i = 0; i < btm.Width - kernel.Length; i++)
                for (int j = 0; j < btm.Height - kernel[0].Length; j++)
                {
                    double x = 0, y = 0, z = 0;
                    double K = 0;

                    for (int k = kernel.Length - 1; k > -1; k--)
                        for (int l = kernel[0].Length - 1; l > -1; l--)
                        {
                            Color pxl = btm.GetPixel(i + kernel.Length - 1 - k, j + kernel[0].Length - 1 - l);
                            x += (kernel[k][l] * pxl.R);
                            y += (kernel[k][l] * pxl.G);
                            z += (kernel[k][l] * pxl.B);
                            K += kernel[k][l];
                        }

                    if (K == 0)
                        K = 1;

                    x = x / K;
                    y = y / K;
                    z = z / K;

                    y = y < 0 ? 0 : y;
                    x = x < 0 ? 0 : x;
                    z = z < 0 ? 0 : z;


                    x = x > 255 ? 255 : x;
                    y = y > 255 ? 255 : y;
                    z = z > 255 ? 255 : z;

                    btmF.SetPixel(i, j, Color.FromArgb((int)(x), (int)(y), (int)(z)));
                }


            btmF.Save(@"C:\Users\LENOVO\Desktop\" + n2);
        }

        public static void Odejmowanie()
        {
            Bitmap btm1 = new Bitmap(@"C:\Users\LENOVO\Desktop\Obrazek2.JPG"); //filtr Gaussa
            Bitmap btm2 = new Bitmap(@"C:\Users\LENOVO\Desktop\Obrazek3.JPG"); //Edge detection

            Bitmap btmF = new Bitmap(btm1.Width, btm1.Height);

            for (int i = 0; i < btm1.Width; i++)
            {
                for (int j = 0; j < btm1.Height; j++)
                {
                    Color pxl1 = btm1.GetPixel(i, j);
                    Color pxl2 = btm2.GetPixel(i, j);
                    int R = pxl1.R - pxl2.R;
                    int G = pxl1.G - pxl2.G;
                    int B = pxl1.B - pxl2.B;

                    R = R < 0 ? 0 : R;
                    G = G < 0 ? 0 : G;
                    B = B < 0 ? 0 : B;

                    btmF.SetPixel(i, j, Color.FromArgb(R, G, B));
                }
            }
            btmF.Save(@"C:\Users\LENOVO\Desktop\Obrazek4.JPG");
        }

        public static void PunktyKluczowe()
        {
            Bitmap btm0 = new Bitmap(@"C:\Users\LENOVO\Desktop\Obrazek.JPG");
            Bitmap btm3 = new Bitmap(@"C:\Users\LENOVO\Desktop\Obrazek4.JPG");

            // Bitmap btmF2 = new Bitmap(btm3.Width, btm3.Height);

            for (int i = 0; i < btm3.Width; i++)
            {
                for (int j = 0; j < btm3.Height; j++)
                {
                    Color pxl3 = btm3.GetPixel(i, j);
                    Color pxl5 = btm0.GetPixel(i, j);
                    if (pxl3.GetBrightness() > 0.8)
                        btm0.SetPixel(i, j, Color.Red);

                }
            }
            btm0.Save(@"C:\Users\LENOVO\Desktop\Obrazek5.JPG");

        }
    }
}
