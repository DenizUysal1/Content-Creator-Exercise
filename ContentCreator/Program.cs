using ContentCreator.Builders;
using ContentCreator.Configuration;
using ContentCreator.Persistor;
using Finsoft.EVenue.Odi;
using Finsoft.EVenue.Odi.ODict;
using Finsoft.Utilities;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: XmlConfigurator(Watch = true)]
namespace ContentCreator
{

    class Program
    {

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            
            CreateTennisEvent();
            log.InfoFormat("Created tennis event");

            CreateVolleyballEvent();
            log.InfoFormat("Created Volleyball event");

            CreateRandomEvent();
            log.InfoFormat("Created random event");
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

            PersistorService.SaveEvent(evntDelta);
            PersistorService.SaveMarket(market);
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
            var market = MarketBuilder.CreateMarket(evntDelta, "201");

            PersistorService.SaveEvent(evntDelta);
            PersistorService.SaveMarket(market);
        }

        private static void CreateRandomEvent()
        {
            var numberOfSports = ConfigurationLoader.GetTotalNumberOfSportsLoaded();
            var randomSport = new Random().Next(1, numberOfSports).ToString();

            var sportConfiguration = ConfigurationLoader.GetSportConfiguration(randomSport);
            var cdsId = $"EventTest {sportConfiguration.IdEvSport}";

            Event evnt = new Event("FS" /* DataSourceId */, cdsId /* CdsId */);
            evnt.Name = $"{sportConfiguration.IdEvSport} Test";
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

            PersistorService.SaveEvent(evntDelta);
            PersistorService.SaveMarket(market);
        }

        
    }
}
