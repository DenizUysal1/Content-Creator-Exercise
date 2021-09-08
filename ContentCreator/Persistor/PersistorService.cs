using Finsoft.EVenue.Odi;
using Finsoft.EVenue.Odi.ODict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCreator.Persistor
{
    public static class PersistorService
    {
        private static IODictService _oDictService { get; set; }

        static PersistorService()
        {
            IOdi odi = OdiFactory.GetOdi("Default");

            IOdiSession session = odi.CreateSession("INS", "betradaruof", "betradaruof");

            _oDictService = session.GetODictService();
        }

        public static void ApplyToDb(CdsDelta item)
        {
            _oDictService.ApplyCdsDelta(item);
        }
    }
}
