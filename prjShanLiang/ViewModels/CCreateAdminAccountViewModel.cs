﻿using prjShanLiang.Models;
using System.ComponentModel.DataAnnotations;

namespace prjShanLiang.ViewModels
{
    public class CCreateAdminAccountViewModel
    {
        public string? AdminName { get; set; }
 
        public string? Password { get; set; }

        public string? Password2 { get; set; }

        public int? IdentificationId { get; set; }
    }
}
