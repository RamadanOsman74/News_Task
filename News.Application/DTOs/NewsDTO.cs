using Microsoft.AspNetCore.Http;
using News.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Application.DTOs
{
    public class NewsDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<IFormFile> Image { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string TranslationsJson { get; set; }
    }
}
