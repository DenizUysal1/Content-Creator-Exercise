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

            SelectionDelta selection1 = new SelectionDelta("FS", market.CdsId, "H");
            selection1.HadValueId = "H";
            selection1.Name = "Nadal";
            selection1.SelectionTypeId = "P"; // i.e. “participant”
            selection1.SelectionStatusId = "T";
            //selection1.CompetitorRef = competitor1; // or: = EventCompetitor.CreateReference("FS", "Event1", "1");
            market.Selections.Add(selection1);

            PriceSetDelta priceSet = new PriceSetDelta("FS" /* DataSourceId */,"PriceSet1Test" /* CdsId */);
            priceSet.MarketRef = market;
            priceSet.PriceTypeId = "F"; // “fixed”
            priceSet.PriceStyleId = "F"; // “fraction”

            SelectionPriceDelta price1 = new SelectionPriceDelta("FS", "PriceSet1Test", "1");
            price1.PriceMultiplicator = 11;
            price1.PriceQuotient = 4;
            price1.PriceStatusId = "C"; // “current”
            price1.SelectionRef = selection1; // or: =  Selection.CreateReference("FS", "Market1", "H");
            priceSet.SelectionPrices.Add(price1);


            PersistorService.SaveEvent(evntDelta);
            PersistorService.SaveMarket(market);
            PersistorService.SavePriceSet(priceSet);
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

            SelectionDelta selection1 = new SelectionDelta("FS", market.CdsId, "H");
            selection1.HadValueId = "H";
            selection1.Name = "Francia";
            selection1.SelectionTypeId = "P";
            selection1.SelectionStatusId = "T";
            market.Selections.Add(selection1);

            PriceSetDelta priceSet = new PriceSetDelta("FS" /* DataSourceId */, "PriceSet1TestVolleyball" /* CdsId */);
            priceSet.MarketRef = market;
            priceSet.PriceTypeId = "F"; // “fixed”
            priceSet.PriceStyleId = "D"; // “fraction”

            SelectionPriceDelta price1 = new SelectionPriceDelta("FS", "PriceSet1TestVolleyball", "1");
            price1.PriceMultiplicator = 5;
            price1.PriceQuotient = 10;
            price1.PriceStatusId = "C"; 
            price1.SelectionRef = selection1;
            priceSet.SelectionPrices.Add(price1);

            PersistorService.SaveEvent(evntDelta);
            PersistorService.SaveMarket(market);
            PersistorService.SavePriceSet(priceSet);
        }

        private static void CreateRandomEvent()
        {
            var random = new Random();
            var numberOfSports = ConfigurationLoader.GetTotalNumberOfSportsLoaded();
            var randomSportId = random.Next(1, numberOfSports).ToString();

            var sportConfiguration = ConfigurationLoader.GetSportConfiguration(randomSportId);
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

            SelectionDelta selection1 = new SelectionDelta("FS", market.CdsId, "H");
            selection1.HadValueId = "H";
            selection1.Name = "Random Team";
            selection1.SelectionTypeId = "P";
            selection1.SelectionStatusId = "T";
            market.Selections.Add(selection1);

            PriceSetDelta priceSet = new PriceSetDelta("FS", $"PriceSet1Test{sportConfiguration.IdEvSport}");
            priceSet.MarketRef = market;
            priceSet.PriceTypeId = "F"; // “fixed”
            priceSet.PriceStyleId = "D"; // “fraction”

            SelectionPriceDelta price1 = new SelectionPriceDelta("FS", $"PriceSet1Test{sportConfiguration.IdEvSport}", "1");
            price1.PriceMultiplicator = random.Next(1,50);
            price1.PriceQuotient = random.Next(1,10);
            price1.PriceStatusId = "C";
            price1.SelectionRef = selection1;
            priceSet.SelectionPrices.Add(price1);

            PersistorService.SaveEvent(evntDelta);
            PersistorService.SaveMarket(market);
            PersistorService.SavePriceSet(priceSet);
        }

        
    }
}
