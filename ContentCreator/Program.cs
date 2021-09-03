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
            IOdi odi = OdiFactory.GetOdi("Default");

            IOdiSession session = odi.CreateSession("INS", "betradaruof", "betradaruof");

            IODictService oDictService = session.GetODictService();

            //var dataSourceId = "FS";
            //var cdsId = "FS#1";

            //Event evnt = new Event(dataSourceId, cdsId)
            //{
            //    Name = "PSG v RMFC",
            //    VenueRef = Venue.CreateReference(dataSourceId, cdsId),
            //    CompetitionRef = Competition.CreateReference(dataSourceId, cdsId),
            //    EventCategoryId = "B",
            //    EventStructureTypeId = "MF",
            //    CompetitorReferenceId = "TFS",
            //    EventCompetitorStructureId = "TP",
            //    SportId = "BKB",
            //    NamedStartTime = "2021-9-3 12:00:00",
            //    DeclaredStartTime = new DateTimeTz(2021, 9, 3, 12, 0, 0, "+1:00"),
            //    ParentEventRef = Event.CreateReference(dataSourceId, cdsId)
            //};

            //EventCompetitor competitor = new EventCompetitor(dataSourceId, cdsId, "CompetitorTest1")
            //{
            //    Name = "PSG TEST",
            //    CompetitorNumber = "1"
            //};

            //evnt.Competitors.Add(competitor);
            //oDictService.CreateEvent(evnt);

            //VenueDelta venue = new VenueDelta("FS" /* DataSourceId */, "Venue1" /* CdsId */);
            //venue.Name = "Old Trafford Test";
            //venue.CityOrTown = "London";
            //venue.CountryId = "GB";
            //venue.VenueTypeId = "STADIUM";
            //oDictService.ApplyVenueDelta(venue); // persist to database
            //// Update Venue CDS – change Name
            //VenueDelta venueDelta = new VenueDelta("FS", "Venue1");
            //venueDelta.Name = "Old Trafford - London tEST";
            //oDictService.ApplyVenueDelta(venueDelta);
            //// Create Event CDS
            EventDelta evnt = new EventDelta("FS" /* DataSourceId */, "Event1" /* CdsId */);
            evnt.Name = "Man United v Chelsea";
            evnt.VenueRef = Venue.CreateReference("FS", "Venue1");
            evnt.CompetitionRef = Competition.CreateReference("FS", "PREMIERSHIP");
            // assuming Premiership competition has been created before
            evnt.EventCategoryId = "B";
            evnt.EventStructureTypeId = "MF";
            evnt.CompetitorReferenceId = "THA";
            evnt.EventCompetitorStructureId = "TP";
            evnt.SportId = "FBL";
            evnt.NamedStartTime = "2007-09-23 15:00:00";
            evnt.DeclaredStartTime = new DateTimeTz(2007, 09, 23, 15, 0, 0,"+0:00");
            evnt.WeatherForecast = "Mostly sunny...";

            EventOfficialDelta official = new EventOfficialDelta("FS" /* DataSourceId */, "Event1" /* CdsId */, "1" /* CdsDetailId, unique ONLY within this CDS instance (within this Event) */);
            official.CountryId = "GB";
            official.Dob = new DateTime(1968, 6, 2);
            official.TypeId = "R";
            official.Name = "Mike Dean";
            official.FirstName = "Mike";
            official.LastName = "Dean";
            evnt.Officials.Add(official);
            EventCompetitorDelta competitor1 = new EventCompetitorDelta("FS",
            "Event1", "1");
            competitor1.Name = "Man United";
            competitor1.CompetitorNumber = "1";
            competitor1.HaValueId = "H";
            competitor1.ManagerOrCoach = "Alex Ferguson";
            evnt.Competitors.Add(competitor1);
            EventCompetitorDelta competitor2 = new EventCompetitorDelta("FS",
            "Event1", "2");
            competitor2.Name = "Chelsea";
            competitor2.CompetitorNumber = "2";
            competitor2.HaValueId = "A";
            competitor2.ManagerOrCoach = "Avram Grant";
            evnt.Competitors.Add(competitor2);
            EventCompetitorMemberDelta competitorMember1 = new EventCompetitorMemberDelta("FS", "Event1", "1" /* EventCompetitor detail id */, "1" /* EventCompetitorMember detail id. It should be unique inside single Event only (it is not sufficient to be unique inside single EventCompetitor) */);
            competitorMember1.Number = "1";
            competitorMember1.Name = "Edwin van der Sar";
            competitorMember1.FirstName = "Edwin";
            competitorMember1.LastName = "Van der Sar";
            competitorMember1.Dob = new DateTime(1970, 10, 29);
            competitor1.Members.Add(competitorMember1);
            EventCompetitorMemberDelta competitorMember2 = new EventCompetitorMemberDelta("FS", "Event1", "1", "2");
            competitorMember2.Number = "6";
            competitorMember2.Name = "Wes Brown";
            competitorMember2.FirstName = "Wes";
            competitorMember2.LastName = "Brown";
            competitorMember2.Dob = new DateTime(1979, 10, 13);
            competitor1.Members.Add(competitorMember2);
            // … other Man Utd members
            EventCompetitorMemberDelta competitorMember31 = new EventCompetitorMemberDelta("FS", "Event1", "2", "31");
            competitorMember31.Number = "1";
            competitorMember31.Name = "Petr Cech";
            competitorMember31.FirstName = "Petr";
            competitorMember31.LastName = "Cech";
            competitorMember31.Dob = new DateTime(1982, 5, 20);
            competitor2.Members.Add(competitorMember31);
            EventCompetitorMemberDelta competitorMember32 = new EventCompetitorMemberDelta("FS", "Event1", "2", "32");
            competitorMember32.Number = "20";
            competitorMember32.Name = "Paulo Ferreira";
            competitorMember32.FirstName = "Paulo";
            competitorMember32.LastName = "Ferreira";
            competitorMember32.Dob = new DateTime(1979, 1, 18);
            competitor2.Members.Add(competitorMember32);
            // … other Chelsea members
            oDictService.ApplyEventDelta(evnt);

        }
    }
}
