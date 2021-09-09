using Finsoft.EVenue.Odi;
using Finsoft.EVenue.Odi.ODict;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCreator.Persistor
{
    public static class PersistorService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IODictService _oDictService { get; set; }

        static PersistorService()
        {
            IOdi odi = OdiFactory.GetOdi("Default");

            IOdiSession session = odi.CreateSession("INS", "betradaruof", "betradaruof");

            _oDictService = session.GetODictService();
        }

        public static void SaveEvent(EventDelta evnt)
        {
            PersistorService.ApplyToDb(evnt);
            log.InfoFormat($"Event {evnt.Name} saved to DDBB ");
        }

        public  static void SaveMarket(MarketDelta market)
        {
            PersistorService.ApplyToDb(market);
            log.InfoFormat($"Market {market.Name} for Event {market.EventRef.CdsId} saved to DDBB ");
        }

        public static void SavePriceSet(PriceSetDelta priceSet)
        {
            PersistorService.ApplyToDb(priceSet);
            log.InfoFormat($"Priceset {priceSet.CdsId}  saved to DDBB ");
        }

        private static void ApplyToDb(CdsDelta item)
        {
            _oDictService.ApplyCdsDelta(item);
        }
    }
}
