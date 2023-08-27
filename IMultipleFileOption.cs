using CommandLine;

namespace XmlTester
{
    public interface IMultipleFileOption
    {
        [Option('i', "input", SetName = "diff-name", Required = true, HelpText = "Especifica el archivo a transformar")]
        public string InputFilename { get; set; }

        [Option('t', "transformation", SetName = "diff-name", Required = true, HelpText = "Especifica el archivo fuente de transformaciones")]
        public string TransformationFilename { get; set; }

        [Option('o', "output", SetName = "diff-name", Required = true, HelpText = "Especifica el nombre del archivo de salida")]
        public string OutputFilename { get; set; }
    }
}