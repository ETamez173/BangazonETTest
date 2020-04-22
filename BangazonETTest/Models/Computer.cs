﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonETTest.Models
{
    public class Computer
    {
     
            [Required]
            public int Id { get; set; }

            [Required]
            public string Make { get; set; }

            [Required]
            public string Model { get; set; }

            [Required]
            public DateTime PurchaseDate { get; set; }

            public DateTime? DecomissionDate { get; set; }

            public Employee Employee { get; set; }
        }
    }
