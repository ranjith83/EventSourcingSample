using EventSourcingExample.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingExample
{
   public class EventProcessor<T> where T : LibraryEvent
    {
        private IList<T> eventLogger = new List<T>();

        public void ProcessEvent(T e)
        {
            e.Process();
            eventLogger.Add(e);
        }
    }
}
