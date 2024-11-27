using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
 public enum Languages
{
    Arabic , English , French 
}
namespace News.Domain.Entities
{
    public class New
    {
        [Key]
        public int NewId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public ICollection<Images> Image {  get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsFeatured { get; set; }
        public ICollection<NewTranslation> Translations { get; set; }
        public Languages language {  get; set; }  
    }
}
