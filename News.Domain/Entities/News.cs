﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Domain.Entities
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }
        public string Image {  get; set; }
        public DateTime CreatedDate => DateTime.Now;
        public bool IsFeatured { get; set; }
        public ICollection<NewsTranslation> Translations { get; set; }

    }
}