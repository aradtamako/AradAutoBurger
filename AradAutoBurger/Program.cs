using Core;
using Core.Material;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace AradAutoBurger
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        static Bitmap TakeScreenShot(int x, int y, int width, int height)
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(x, y, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            }
            return bmp;
        }

        static void PressKey(InputSimulator simulator, VirtualKeyCode keyCode)
        {
            simulator.Keyboard.KeyDown(keyCode);
            Thread.Sleep(100);
            simulator.Keyboard.KeyUp(keyCode);
        }

        static void Main(string[] args)
        {
            var pid = Process.GetProcessesByName("ARAD").First().Id;
            var hWnd = Process.GetProcessesByName("ARAD").First().MainWindowHandle;
            if (hWnd != IntPtr.Zero)
            {
                GetClientRect(hWnd, out var rect);

                if (rect.Width != 1600 || rect.Height != 900)
                {
                    Console.WriteLine("アラド戦記の解像度を1600x900に設定して下さい。");
                    return;
                }

                MoveWindow(hWnd, 0, 0, rect.Width, rect.Height, true);
            }

            var autoBurger = new AutoBurger();
            var simulator = new InputSimulator();

            Microsoft.VisualBasic.Interaction.AppActivate(pid);

            while (true)
            {
                var img = TakeScreenShot(547, 188, 160, 220);
                var results = autoBurger.GetMaterials(img);

                foreach (var result in results)
                {
                    switch (result)
                    {
                        case TomatoMaterial x:
                            PressKey(simulator, VirtualKeyCode.LEFT);
                            break;
                        case PattyMaterial x:
                            PressKey(simulator, VirtualKeyCode.UP);
                            break;
                        case LettuceMaterial x:
                            PressKey(simulator, VirtualKeyCode.DOWN);
                            break;
                        case CheeseMaterial x:
                            PressKey(simulator, VirtualKeyCode.RIGHT);
                            break;
                    }
                    Thread.Sleep(100);
                }

                if (results.Any())
                {
                    PressKey(simulator, VirtualKeyCode.SPACE);
                }

                Thread.Sleep(1000);
            }
        }
    }
}
