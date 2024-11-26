using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Domain.Entities
{
    public class Language
    {
        [Key]
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public ICollection<NewTranslation> NewTranslations { get; set; }
    }
}
