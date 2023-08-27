using CommandLine;

namespace XmlTester
{
    public class Options : IMultipleFileOption, ISingleFileOption
    {
        public string InputFilename { get; set; } = string.Empty;
        public string TransformationFilename { get; set; } = string.Empty;
        public string OutputFilename { get; set; } = string.Empty;

        public string UniqueFilename { get; set; } = string.Empty;

    }
}