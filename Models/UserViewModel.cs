﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS3Wiki.Models
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }

        public string RoleName { get; set; }
    }
}