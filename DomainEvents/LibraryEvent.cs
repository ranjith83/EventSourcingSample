using EventSourcingExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingExample.DomainEvents
{
    public abstract class LibraryEvent : DomainEvent
    {

        private Book _book;
        public Book Book
        {
            get { return _book; }
            set { _book = value; }
        }
        private Person _person;

        public Person Person
        {
            get { return _person; }
            set { _person = value; }
        }


        internal TrackingType trackingType { get; private set; }

        internal LibraryEvent(DateTime eventTime, Book book, Person person, TrackingType trackingType) : base(eventTime)
        {
            this.Book = book;
            this.Person = person;
            this.trackingType = trackingType;
        }
        internal override void Process()
        {

        }
    }
}
