using System;
using System.Xml.Linq;
using CommandLine;
using Microsoft.Web.XmlTransform;

namespace XmlTester
{
    public class Program
    {
        const string xmlTransformExtension = ".xmgr";

        public static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(RunOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void RunOptions(Options options)
        {
            if (!string.IsNullOrEmpty(options.UniqueFilename))
            {
                var targetFilename = Path.GetFileNameWithoutExtension(options.UniqueFilename);
                var targetFileExtension = Path.GetExtension(options.UniqueFilename);

                var transformationFile = options.UniqueFilename + xmlTransformExtension;
                var outputFile = targetFilename + "-transformado" + targetFileExtension;

                MergeXml(options.UniqueFilename, transformationFile, outputFile);
            }
            else if (!string.IsNullOrEmpty(options.InputFilename))
            {
                MergeXml(
                    options.InputFilename,
                    options.TransformationFilename,
                    options.OutputFilename);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private static void MergeXml(string targetFile, string sourceFile, string outputFile)
        {
            if (!File.Exists(targetFile))
            {
                throw new InvalidOperationException($"el archivo de entrada \"{targetFile}\" no existe.");
            }

            if (!File.Exists(sourceFile))
            {
                throw new InvalidOperationException($"el archivo de transformaciones \"{sourceFile}\" no existe.");
            }

            using var outputXml = new XmlTransformableDocument
            {
                PreserveWhitespace = true
            };

            outputXml.Load(targetFile);

            using var transform = new XmlTransformation(sourceFile);

            if (!transform.Apply(outputXml))
            {
                throw new XmlTransformationException(string.Format(Message.Error.MsgCannotTransformXml, sourceFile));
            }

            outputXml.Save(outputFile);
        }
    }
}