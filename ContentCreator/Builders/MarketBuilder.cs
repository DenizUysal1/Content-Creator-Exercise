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

            MarketDelta market = new MarketDelta("FS" /* DataSourceId */, "Market1Test"/* CdsId */);
            market.Name = marketConfiguration.Name;
            market.MarketTypeId = marketConfiguration.EvMarketTypeId; // “football outright”
            market.EventRef = evnt; // or: market1.EventRef =  Event.CreateReference("FS", "Event1");
            market.MarketLifeStateId = "NE"; // “new” – i.e. not open for betting yet
            //market1.MaxWinners = 1;


            return market;
            //PersistorService.ApplyToDb(market1);
        }
    }
}
