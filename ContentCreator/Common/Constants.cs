using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCreator.Common
{
    public class Constants
    {
        private Constants() { }

        public enum SelectionAttributes { Competitor, HADValue, SelectionOrderNumber, RangeMin, RangeMax, Line, ScoreLine1, ScoreLine2, ScoreLine3, ScoreLine4, CompetitorMember }
        public enum MarketAttributes { NumSelections, Line, LineType, Period, Subperiod, ParticipantNumber, ParticipantId, ParticipantMember, Index, Range, RangeType }

        public enum MakeUpValueFunctions { PrematchFullTimeScores, Default }
    }
}
