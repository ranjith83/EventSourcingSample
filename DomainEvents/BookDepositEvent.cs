using EventSourcingExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingExample.DomainEvents
{
    public class BookDepositEvent : LibraryEvent
    {
        public BookDepositEvent(DateTime eventTime, Book book, Person person, TrackingType trackingType) : base(eventTime, book, person, trackingType)
        {
        }

        internal override void Process()
        {
           Person.HandleDeposit(this);
        }
    }
}
