using System;

namespace EventSourcingExample.DomainEvents
{
    public abstract class DomainEvent
    {
        private DateTime occured;

        internal DomainEvent(DateTime eventTime)
        {
            this.occured = eventTime;
        }

        abstract internal void Process();
    }
}