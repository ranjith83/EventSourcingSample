using EventSourcingExample.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingExample.Models
{
    public class Person
    {

        public int ID { get; set; }
        public string Name { get; set; }
        
        public Book book { get; set; }

        public void HandleLet(BookLetEvent ev)
        {
            book = ev.Book;
        }

        public void HandleDeposit(BookDepositEvent ev)
        {
            book = ev.Book;
        }

    }
}
