namespace ComputerVisionApplication.Models.Sentiment
{
    
    public class SentimentResult
    {
        public Document[] Documents { get; set; }
        public Error[] Errors { get; set; }
    }

    public class Document
    {
        public float Score { get; set; }
        public string Id { get; set; }
    }

    public class Error
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }

}
