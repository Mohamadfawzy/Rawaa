﻿
using System;

namespace Rawaa_Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string Phone { get; set; } 
        public string FullName { get; set; } 
        public DateTime? CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }

    }
}
