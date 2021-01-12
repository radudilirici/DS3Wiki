using DS3Wiki.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DS3Wiki.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [CommentValidation(ErrorMessage = "The comment has to be longer than this")]
        public string Text { get; set; }
    }
}