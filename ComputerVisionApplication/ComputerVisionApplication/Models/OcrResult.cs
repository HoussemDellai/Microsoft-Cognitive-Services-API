using System.Collections.Generic;

namespace CognitiveServices.Models.Ocr
{
    
    public class OcrResult
    {
        public string Language { get; set; }
        public float TextAngle { get; set; }
        public string Orientation { get; set; }
        public Region[] Regions { get; set; }
    }

    public class Region
    {
        public string BoundingBox { get; set; }
        public List<Line> Lines { get; set; }
    }

    public class Line
    {
        public string BoundingBox { get; set; }
        public Word[] Words { get; set; }
    }

    public class Word
    {
        public string BoundingBox { get; set; }
        public string Text { get; set; }
    }

}
