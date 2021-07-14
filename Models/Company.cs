using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iAgenda.Models
{
    public class Company
    {

        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
}
