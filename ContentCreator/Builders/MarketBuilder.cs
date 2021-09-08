using ContentCreator.Configuration;
using ContentCreator.Persistor;
using Finsoft.EVenue.Odi.ODict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCreator.Builders
{
    public static class MarketBuilder
    {
        public static MarketDelta CreateMarket(EventDelta evnt,string providerMarketId)
        {

            var marketConfiguration = ConfigurationLoader.GetMarketConfiguration(providerMarketId);

            MarketDelta market = new MarketDelta("FS" /* DataSourceId */, $"Market Test {evnt.SportId}"/* CdsId */);
            market.Name = marketConfiguration.Name;
            market.MarketTypeId = marketConfiguration.EvMarketTypeId; 
            market.EventRef = evnt;
            market.MarketLifeStateId = "NE";

            return market;
        }
    }
}
