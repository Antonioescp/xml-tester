using CommandLine;

namespace XmlTester
{
    public interface ISingleFileOption
    {

        [Option('s', "same-name", SetName = "same-name", Required = true, HelpText = "Especifica que el archivo original y el archivo de cambios tienen el mismo nombre, el archivo transformado tendrá el mismo nombre mas \"-transformado\"")]
        public string UniqueFilename { get; set; }
    }
}