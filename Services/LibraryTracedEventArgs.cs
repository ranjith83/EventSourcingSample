using EventSourcingExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingExample.Services
{
    public class LibraryTracedEventArgs : EventArgs
    {
        #region Public Properties

        public Guid TrackingServiceId { get; set; }
        public DateTime Recorded { get; set; }
        public TrackingType TrackingType { get; set; }
        public Book TrackedBook { get; set; }
        public Person OldPerson { get; set; }
        public Person NewPerson { get; set; }

        #endregion Public Properties
    }
}
