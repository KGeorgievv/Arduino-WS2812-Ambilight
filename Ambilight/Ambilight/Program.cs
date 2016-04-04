using System;
using System.IO.Ports;
using System.Threading;

namespace AbilightTest
{
    class Program
    {
        private static SerialPort port;

        static void Main()
        {
            port = new SerialPort("COM5", 115200);
            port.Open();

            Console.WriteLine("Ambilight by Kiril");

            using (port)
            {
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = 80;

                Bitmap screen = new Bitmap(width, height, PixelFormat.Format16bppRgb555);
                Graphics screenGraphics = Graphics.FromImage(screen);
                Color[] pixels = new Color[23];

                while (true)
                {
                    screenGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0,
                    screen.Size, CopyPixelOperation.SourceCopy);

                    for (int i = 0; i < 23; i++)
                    {
                        pixels[i] = screen.GetPixel(i * 83, 75);
                        SendLedData(i, pixels[i]);
                        Thread.Sleep(2);
                    }
                }
            }
        }

        public static void SendLedData(int led, Color color)
        {
            string dataToSend = $"{ConvertToData(led)} {color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2")}" + "\n";

            port.Write(dataToSend);
        }

        private static string ConvertToData(int data)
        {
            if (data < 9)
            {
                return "0" + data;
            }

            return data.ToString();
        }
    }
}
