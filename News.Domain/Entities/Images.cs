using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace News.Domain.Entities
{
    public class Images
    {
        [Key]
        public int ImageId { get; set; } 
        [ForeignKey("New")]
        public int NewsId { get; set; } 
        public string ImagePath { get; set; }
        [JsonIgnore]
        public New New { get; set; }
    }
}
