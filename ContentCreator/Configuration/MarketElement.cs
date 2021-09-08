using ContentCreator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCreator.Configuration
{
    public class MarketElement
    {
        public string ProviderMarketId { get; set; }

        public string Name { get; set; }

        public string Groups { get; set; }

        public string Order { get; set; }

        public string EvMarketTypeId { get; set; }

        public string SelectionPatternId { get; set; }

        public int NumSelections { get; set; }

        public string Line { get; set; }

        public string LineType { get; set; }

        public string Period { get; set; }

        public string SubPeriod { get; set; }

        public string ParticipantNumber { get; set; }

        public string ParticipantId { get; set; }

        public string ParticipantMember { get; set; }

        public string Index { get; set; }

        public string Range { get; set; }

        public string Rangetype { get; set; }

        public HashSet<string> Categories { get; set; }

        public string VariantAttributes { get; set; }

        public string VariantValue { get; set; }

        public bool UseMarketNameFromFeed { get; set; }

        public bool SuspendOnDeactivated { get; set; }

        public IDictionary<string, MarketSportElement> SportsConfiguration { get; set; } = new Dictionary<string, MarketSportElement>();

        public IDictionary<Constants.MarketAttributes, string> Attributes { get; set; } = new Dictionary<Constants.MarketAttributes, string>();

        //internal SelectionPatternElement SelectionConfigurationElement { get; set; }

        //internal SelectionElement GetSelectionConfiguration(string providerId)
        //{
        //    SelectionConfigurationElement.Selections.TryGetValue(providerId, out SelectionElement selectionConfiguration);
        //    return selectionConfiguration;
        //}

        public class MarketSportElement
        {
            public string EvSportId { get; set; }

            public int PrematchOrder { get; set; }

            public int LiveOddsOrder { get; set; }

            public string CustomMarketName { get; set; }

            public Constants.MakeUpValueFunctions CustomMakeUpValueFuction { get; set; }
        }
    }
}
