﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButterAPI.Models
{
    public class UserUpdate
    {
        public int UserId { get; set; }
        [Required]
        public string NickName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        //remarque ajouter contrainte date +de 18 ans
        public DateTime BirthDate { get; set; }
        [Required]
        public string Town { get; set; }

        public string Genre { get; set; }
    }
}
