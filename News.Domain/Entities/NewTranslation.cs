using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Domain.Entities
{
    public class NewTranslation
    {
        [Key]
        public int TranslationId { get; set; }
        [ForeignKey("New")]
        public int NewId { get; set; }
        //[ForeignKey("Language")]
        //public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public New New { get; set; }
        public Languages Language { get; set; }

    }
}
