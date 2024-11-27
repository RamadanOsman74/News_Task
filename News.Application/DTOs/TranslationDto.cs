using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Application.DTOs
{
    public class TranslationDTO
    {
        public string Language { get; set; } // Arabic, English, French
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
