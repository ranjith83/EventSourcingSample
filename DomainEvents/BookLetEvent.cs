using EventSourcingExample.Models;
using System;

namespace EventSourcingExample.DomainEvents
{
    public class BookLetEvent : LibraryEvent
    {
        
       public BookLetEvent(DateTime eventTime, Book book, Person person, TrackingType trackingType) : base(eventTime, book,person,trackingType)
        {
        }

        internal override void Process()
        {
            Person.HandleLet(this);
        }
    }
}
