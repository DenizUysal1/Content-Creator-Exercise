using ContentCreator.Configuration;
using ContentCreator.Persistor;
using Finsoft.EVenue.Odi.ODict;
using Finsoft.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCreator.Builders
{
    public static class EventBuilder
    {
        public static EventDelta CreateEvent(Event evnt)
        {
            // Create Event CDS
            EventDelta evntDelta = new EventDelta(evnt.DataSourceId /* DataSourceId */, evnt.CdsId /* CdsId */);
            evntDelta.Name = evnt.Name;
            evntDelta.VenueRef = evnt.VenueRef;
            evntDelta.CompetitionRef = evnt.CompetitionRef;
            evntDelta.EventCategoryId = evnt.EventCategoryId;
            evntDelta.EventStructureTypeId = evnt.EventStructureTypeId;
            evntDelta.CompetitorReferenceId = evnt.CompetitorReferenceId ;
            evntDelta.EventCompetitorStructureId = evnt.EventCompetitorStructureId;
            evntDelta.SportId = evnt.SportId;
            evntDelta.NamedStartTime = evnt.NamedStartTime;
            evntDelta.DeclaredStartTime = evnt.DeclaredStartTime;
            evntDelta.WeatherForecast = evnt.WeatherForecast;

            return evntDelta;
        }
    }
}
