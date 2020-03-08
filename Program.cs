using EventSourcingExample.DomainEvents;
using EventSourcingExample.Models;
using EventSourcingExample.Services;
using System;

namespace EventSourcingExample
{
    class Program
    {
        private static LibraryTrackingServices _trackingService;
        private static EventProcessor<LibraryEvent> _eventProcessor;
        private static Random _randomBook;
        private static int _selectedBookId;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _trackingService = new LibraryTrackingServices();
            _randomBook = new Random();
            _eventProcessor = new EventProcessor<LibraryEvent>();
            _trackingService.BookTracked += _trackingService_BookTracked;
            SetData();
        }

        private static void _trackingService_BookTracked(object sender, LibraryTracedEventArgs e)
        {

            Console.WriteLine(
            $"TrackingId: {e.TrackingServiceId}\r\n" +
            $"RecordedAt: {e.Recorded.ToLongTimeString()}\r\n" +
            $"TrackingType: {e.TrackingType}\r\n" +
            $"Book: {e.TrackedBook.BookName} Id: {e.TrackedBook.BookID}\r\n" +
            $"Current Person : {e.OldPerson.Name}\r\n" +
            $"New Person : {e.NewPerson.Name}" +
            "\r\n\r\n");
            Console.ReadLine();
        }

        private static void SetData()
        {
            if (_trackingService != null)
            {
                // we simulate tracking events by selecting a RANDOM ship
                // next tracking type is set to Arrival or Departure depending on the 
                // current location of the selected ship
                // if port of selected ship == "AT SEA" then we set the tracking event type
                // as an ARRIVAL and will set ship port to the next port (!= 0) in the Port list
                // if port of selected ship != "AT SEA" then we set the tracking event type
                // as a DEPARTURE and set port to "AT SEA"

                int maxPerson = 4;

                // select a random ship in the list
                _selectedBookId = _randomBook.Next(1, maxPerson);

                // set tracked ship to the current selected id
                _trackingService.TrackedBook = _trackingService.Books[_selectedBookId];

                // set the tracking event type (Arrival or Departure) depening on the current location (PortId 0 is AT-SEA)
                _trackingService.TrackingType = _trackingService.TrackedBook.BookID == 0 ? TrackingType.Let : TrackingType.Deposit;

                // set the time of tracking recording
                _trackingService.Recorded = DateTime.Now;

                // create a unique id for the tracking
                _trackingService.TrackingServiceId = Guid.NewGuid();

                // set the new port of the tracking, this is a random port in
                // the list in case of arrival 
                // or port 0 = AT SEA in case of departure
                if (_trackingService.TrackingType == TrackingType.Let)
                {
                    int maxPort = _trackingService.Books.Count;
                    _selectedBookId = _randomBook.Next(1, maxPort);
                }
                else
                {
                    _selectedBookId = 0;
                }

                _trackingService.SetPerson = _trackingService.Persons[_selectedBookId];

                // handle the tracking by the tracking service
                // the tracking service will now send an Arrival event or Departure event to the Ship
                _trackingService.RecordTracking(_eventProcessor);
            }
        }
    }
}
