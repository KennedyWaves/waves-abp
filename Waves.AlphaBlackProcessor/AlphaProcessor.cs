using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waves.AlphaBlackProcessor
{
    public class AlphaProcessor
    {
        public TargetColor ColorToRemove { get; set; } = TargetColor.White;
        public TargetColor ColorToWriteOut { get; set; } = TargetColor.Black;
        public string OutputPath { get; set; }
        public string SourcePath { get; set; }
        public bool IsImageProcessed { get; private set; }
        public Bitmap Data { get; private set; }
        public PixelInProcess Info { get; private set; }
        public bool CheckFileExists(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid File.");
                Console.WriteLine();
                return false;
            }
            return true;
        }
        public bool TryOutputPath(string path)
        {
            string teste = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            try
            {
                System.IO.File.WriteAllText(path, teste);
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Ivalid destination.");
                Console.WriteLine();
                return false;
            }
            System.IO.File.Delete(path);
            return true;
        }
        private TargetColor ParseStringToTargetColor(string input)
        {
            return (TargetColor)int.Parse(input);
        }
        private bool TrySelectTargetColorEnum(string input)
        {
            try
            {
                TargetColor color = ParseStringToTargetColor(input);
                return true;
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Invalid selection.");
                Console.WriteLine();
                return false;
            }
        }
        public string GetOutputPathFromSource(string source)
        {
            string directory = Path.GetDirectoryName(source);
            string filename = Path.GetFileNameWithoutExtension(source);
            string extension = Path.GetExtension(source);
            return OutputPath = directory + filename + "_processed" + extension;
        }
        public void SaveFile()
        {
            if (IsImageProcessed)
            {
                Data.Save(OutputPath);
                Console.WriteLine("The image has been saved succesfuly!");
                Console.WriteLine();
                Console.WriteLine(OutputPath);
            }
            else
            {
                Console.WriteLine("The image is still not precessed!");
            }
        }
        private PixelInProcess GetCurrentStageFromMatrix(int x, int y, int width, int height)
        {
            PixelInProcess pixelInProcess = new PixelInProcess()
            {
                Current = ((x + 1) + ((y) * width)),
                Total = width * height

            };
            pixelInProcess.Value = (float)pixelInProcess.Current / pixelInProcess.Total;
            return pixelInProcess;
        }
        public Bitmap ProcessImage()
        {
            Data = new Bitmap(SourcePath);
            Console.Write("Processing image... ");
            ProgressBar progressBar = new ProgressBar();
            for (int y = 0; y < Data.Height; y++)
            {
                for (int x = 0; x < Data.Width; x++)
                {
                    Color pixel = Data.GetPixel(x, y);
                    Color newPixel;

                    byte target = pixel.R;
                    byte output = 0;
                    if (ColorToWriteOut == TargetColor.White)
                    {
                        output = 255;
                    }

                    if (ColorToRemove == TargetColor.White)
                    {
                        target = (byte)(255 - (int)pixel.R);
                    }

                    if (pixel.A == 255)
                    {
                        newPixel = Color.FromArgb(target, output, output, output);
                    }
                    else
                    {
                        newPixel = pixel;
                    }
                    Data.SetPixel(x, y, newPixel);
                    Info = GetCurrentStageFromMatrix(x, y, Data.Width, Data.Height);
                    progressBar.Report(Info.Value);
                }
            }
            progressBar.Dispose();
            IsImageProcessed = true;

            return Data;
        }
    }
}
