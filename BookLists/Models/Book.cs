using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookLists.Models
{
    public class Book
    {
        public Book()
        {
        }

        public Book(string name)
        {
            this.Name = name;
            this.ID = Guid.NewGuid().ToString();
        }

        public string ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
