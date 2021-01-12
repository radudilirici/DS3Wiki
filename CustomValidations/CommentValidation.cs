using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DS3Wiki.CustomValidations
{
    public class CommentValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string com = value.ToString();

            return com.Length >= 10;
        }
    }
}