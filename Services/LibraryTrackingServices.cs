using EventSourcingExample.DomainEvents;
using EventSourcingExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingExample.Services
{
    public class LibraryTrackingServices
    {
        private List<Book> _books;
        private List<Person> _person;
        private TrackingType _trackingType;
        private Person _setPerson;
        private Book _trackedBook;
        private Guid _trackingServiceId;
        private DateTime _recorded;
        public delegate void LibraryTracedEventHandler (object sender, LibraryTracedEventArgs e);
        public event LibraryTracedEventHandler BookTracked;

        public TrackingType TrackingType
        {
            get { return _trackingType; }
            set { _trackingType = value; }
        }

        public Guid TrackingServiceId
        {
            get { return _trackingServiceId; }
            set { _trackingServiceId = value; }
        }

        public Person SetPerson
        {
            get { return _setPerson; }
            set { _setPerson = value; }
        }

        public Book TrackedBook
        {
            get { return _trackedBook; }
            set { _trackedBook = value; }
        }

        public List<Book> Books
        {
            get { return _books; }
            set { _books = value; }
        }

        public List<Person> Persons
        {
            get { return _person; }
            set { _person = value; }
        }
       

        public DateTime Recorded
        {
            get { return _recorded; }
            set { _recorded = value; }
        }


        public LibraryTrackingServices()
        {

            _books = new List<Book>()
            {

                new Book()  { BookID = 1, BookName = "Hunger Games"     },
                new Book()  { BookID =2, BookName = "Harry Potter"  },
                new Book()  { BookID = 3, BookName = "Black Beauty"   },
                new Book()  { BookID = 4, BookName= "The Kite Runner"  },

            };

            _person = new List<Person>()
            {
                new Person() { ID = 1, Name = "Thomson Vite", book = _books[0]},
                  new Person() { ID = 2, Name = "MOncika", book = _books[0]},
                  new Person() { ID = 3, Name = "Jameson", book = _books[0]},
                  new Person() { ID = 4, Name = "Jessica Pierc", book = _books[0]},
            };



        }

        public void RecordTracking(EventProcessor<LibraryEvent> eProc)
        {
            // Create event depending on TrackingType
            Person OldPerson = SetPerson;
            LibraryEvent ev;
            if (TrackingType == TrackingType.Let)
            {
                ev = new BookLetEvent(DateTime.Now, TrackedBook, SetPerson, TrackingType);
            }
            else
            {
                ev = new BookDepositEvent(DateTime.Now, TrackedBook, SetPerson, TrackingType);
            }

            // send the event to the event handler (ship) which will update it's status on the provided event data
            eProc.ProcessEvent(ev);

            // notify the UI Tracking List so it can update itself
            LibraryTracedEventArgs args = new LibraryTracedEventArgs()
            {
                TrackingServiceId = TrackingServiceId,
                Recorded = Recorded,
                TrackingType = TrackingType,
                TrackedBook = TrackedBook,
                OldPerson = OldPerson,
                NewPerson = SetPerson
            };

            OnBookTracked(args);

        }

        protected virtual void OnBookTracked(LibraryTracedEventArgs args)
        {

            if (BookTracked != null)
                BookTracked(this, args);

        }
    }
}
