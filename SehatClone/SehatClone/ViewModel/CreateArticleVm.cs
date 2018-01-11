using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SehatClone.Models;

namespace SehatClone.ViewModel
{
    public class CreateArticleVm
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Type { get; set; }

        public List<ArticleCategory> Categories { get; set; }
    }
}