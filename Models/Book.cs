using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingExample.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string BookName { get; set; }

        public override string ToString()
        {
            return BookName;
        }

      
    }
}
