using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waves.AlphaBlackProcessor.Arguments
{
    public class ArgOptions
    {
        [Option('s',"source",Required =true,HelpText ="Specifies the path of source image.")]
        public string SourcePath { get; set; }
        [Option('o', "output", Required = false, HelpText = "Specifies the path of the precessed image. If no path specified, the source path with \"_processed\" prefix will be used.")]
        public string OutputPath { get; set; }
        [Option('r',"remove", Required = false, Default ="white", HelpText = "Specifies the color that will be removed [ w, white | b, black].")]
        public string RemoveColor { get; set; }
        [Option('f', "foreground", Required = false, Default = "black", HelpText = "Specifies the foreground color [ w, white | b, black].")]
        public string ForegroundColor { get; set; }
    }
}
