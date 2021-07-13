﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iAgenda.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string InternalPhone { get; set; }
        
        public string Mobile1 { get; set; }
        
        public string Mobile2 { get; set; }

        public string FourDigitsCode { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        //FK
        public int DepartmentId { get; set; }
        public  Department  Departments { get; set; }



    }
}