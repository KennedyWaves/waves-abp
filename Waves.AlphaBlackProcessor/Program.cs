using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CommandLine;
using Waves.AlphaBlackProcessor.Arguments;

namespace Waves.AlphaBlackProcessor
{
    class Program
    {
        public static void PlaceHeader()
        {
            Console.WriteLine("WAVES PRO Tools for Windows [Version 1.0.1.1]");
            Console.WriteLine("(c) 2022 WAVES systems. All rights reserved.");
            Console.WriteLine();
            Console.WriteLine("Alpha-BW Processor 1.0.0.1");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            AlphaProcessor alphaProcessor = new AlphaProcessor();
            Parser.Default.ParseArguments<ArgOptions>(args)
                  .WithParsed<ArgOptions>(o =>
                  {
                      alphaProcessor.SourcePath = o.SourcePath;
                      if (!alphaProcessor.TryOutputPath(o.OutputPath))
                      {
                          if (o.OutputPath == "")
                          {
                              alphaProcessor.GetOutputPathFromSource(alphaProcessor.SourcePath);
                          }
                          else
                          {
                              return;
                          }
                      }
                      else
                      {
                          alphaProcessor.OutputPath = o.OutputPath;
                      }
                      if (o.RemoveColor.ToLower() == "w" || o.RemoveColor.ToLower() == "white")
                      {
                          alphaProcessor.ColorToRemove = TargetColor.White;
                      }
                      if (o.ForegroundColor.ToLower() == "b" || o.ForegroundColor.ToLower() == "black")
                      {
                          alphaProcessor.ColorToWriteOut = TargetColor.Black;
                      }
                      if (alphaProcessor.CheckFileExists(alphaProcessor.SourcePath))
                      {
                          PlaceHeader();
                          Console.WriteLine();
                          Console.WriteLine("Source: "+alphaProcessor.SourcePath);
                          Console.WriteLine("Destination: "+alphaProcessor.OutputPath);
                          Console.WriteLine();
                          alphaProcessor.ProcessImage();
                          Console.WriteLine();
                          Console.WriteLine($"{alphaProcessor.Info.Total} pixels processed.");
                          Console.WriteLine();
                          alphaProcessor.SaveFile();
                      }
                  });
        }
    }
}
