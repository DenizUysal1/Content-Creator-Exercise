using ContentCreator.Builders;
using ContentCreator.Configuration;
using ContentCreator.Persistor;
using Finsoft.EVenue.Odi;
using Finsoft.EVenue.Odi.ODict;
using Finsoft.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateTennisEvent();
            CreateVolleyballEvent();
        }

        private static  void CreateTennisEvent()
        {

            var sportConfiguration = ConfigurationLoader.GetSportConfiguration("5");

            Event evnt = new Event("FS" /* DataSourceId */, "Event1TennisTest" /* CdsId */);
            evnt.Name = "Nadal v Federer Test";
            evnt.VenueRef = Venue.CreateReference("FS", "Venue1Test");
            evnt.CompetitionRef = Competition.CreateReference("FS", "Competition1234");
            evnt.EventCategoryId = "B";
            evnt.EventStructureTypeId = sportConfiguration.EventStructureTypeId;
            evnt.CompetitorReferenceId = "THA";
            evnt.EventCompetitorStructureId = "PP";
            evnt.SportId = sportConfiguration.IdEvSport;
            evnt.NamedStartTime = "2021-09-07 15:00:00";
            evnt.DeclaredStartTime = new DateTimeTz(2021, 09, 09, 15, 0, 0, "+0:00");
            evnt.WeatherForecast = "Mostly sunny...";

            var evntDelta = EventBuilder.CreateEvent(evnt);
            var market = MarketBuilder.CreateMarket(evntDelta, "1");

            PersistorService.ApplyToDb(evntDelta);
            PersistorService.ApplyToDb(market);
        }

        private static void CreateVolleyballEvent()
        {

            var sportConfiguration = ConfigurationLoader.GetSportConfiguration("23");

            Event evnt = new Event("FS" /* DataSourceId */, "Event1VolleyballTest" /* CdsId */);
            evnt.Name = "Francia vs Canada Test";
            evnt.VenueRef = Venue.CreateReference("FS", "Venue1Test");
            evnt.CompetitionRef = Competition.CreateReference("FS", "Competition1234");
            evnt.EventCategoryId = "B";
            evnt.EventStructureTypeId = sportConfiguration.EventStructureTypeId;
            evnt.CompetitorReferenceId = "THA";
            evnt.EventCompetitorStructureId = "TP";
            evnt.SportId = sportConfiguration.IdEvSport;
            evnt.NamedStartTime = "2021-09-07 15:00:00";
            evnt.DeclaredStartTime = new DateTimeTz(2021, 09, 09, 15, 0, 0, "+0:00");
            evnt.WeatherForecast = "Mostly sunny...";

            var evntDelta = EventBuilder.CreateEvent(evnt);
            var market = MarketBuilder.CreateMarket(evntDelta, "1");

            PersistorService.ApplyToDb(evntDelta);
            PersistorService.ApplyToDb(market);
        }
    }
}
