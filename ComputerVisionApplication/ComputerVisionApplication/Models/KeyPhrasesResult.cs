using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVisionApplication.Models.KeyPhrases
{

    public class KeyPhrasesResult
    {
        public Document[] Documents { get; set; }
        public object[] Errors { get; set; }
    }

    public class Document
    {
        public List<string> KeyPhrases { get; set; }
        public string Id { get; set; }
    }
}
