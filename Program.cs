using System;
using System.Xml.Linq;
using CommandLine;
using Microsoft.Web.XmlTransform;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

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

                var before = File.ReadAllLines(options.UniqueFilename).Aggregate((a, b) => a + Environment.NewLine + b);
                var after = File.ReadAllLines(outputFile).Aggregate((a, b) => a + Environment.NewLine + b);
                ShowDiff(before, after);
            }
            else if (!string.IsNullOrEmpty(options.InputFilename))
            {
                MergeXml(
                    options.InputFilename,
                    options.TransformationFilename,
                    options.OutputFilename);

                var before = File.ReadAllLines(options.InputFilename).Aggregate((a, b) => a + Environment.NewLine + b);
                var after = File.ReadAllLines(options.OutputFilename).Aggregate((a, b) => a + Environment.NewLine + b);
                ShowDiff(before, after);
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

        public static void ShowDiff(string before, string after)
        {
            var diff = InlineDiffBuilder.Diff(before, after);

            var savedColor = Console.ForegroundColor;
            foreach (var line in diff.Lines)
            {
                switch (line.Type)
                {
                    case ChangeType.Inserted:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{line.Position} + ");
                        Console.WriteLine(line.Text);
                        break;
                    case ChangeType.Deleted:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{line.Position} - ");
                        Console.WriteLine(line.Text);
                        break;
                    default:
                        break;
                }
            }
            Console.ForegroundColor = savedColor;
        }
    }
}