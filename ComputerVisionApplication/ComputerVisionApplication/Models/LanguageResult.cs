namespace ComputerVisionApplication.Models.Language
{

    public class LanguageResult
    {
        public Document[] Documents { get; set; }
        public object[] Errors { get; set; }
    }

    public class Document
    {
        public string Id { get; set; }
        public Detectedlanguage[] DetectedLanguages { get; set; }
    }

    public class Detectedlanguage
    {
        public string Name { get; set; }
        public string Iso6391Name { get; set; }
        public float Score { get; set; }
    }

}