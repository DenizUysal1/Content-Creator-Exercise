using ContentCreator.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static ContentCreator.Configuration.MarketElement;

namespace ContentCreator.Configuration
{
    public static class ConfigurationLoader
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<string, SportConfiguration> _sportConfiguration;
        private static Dictionary<string, Dictionary<string, MarketElement>> _marketConfiguration;

        static ConfigurationLoader()
        {
            string configurationPath =
                string.Concat(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), @"\Configuration\");

            LoadSportConfiguration(string.Concat(configurationPath, "Sports.xml"));
            LoadMarketsConfiguration(string.Concat(configurationPath,"Markets.xml"));
        }

        private static void LoadSportConfiguration(string configurationPath)
        {
            _sportConfiguration = new Dictionary<string, SportConfiguration>();

            try
            {
                log.InfoFormat("Loading sports configuration from {0}", configurationPath);

                XmlDocument document = new XmlDocument();
                document.Load(configurationPath);

                XmlNodeList nodeList = document.SelectNodes("/Sports/Sport");
                foreach (XmlNode node in nodeList)
                {
                    SportConfiguration sportConf = new SportConfiguration();
                    sportConf.IdEvSport = node.Attributes["IDEVSport"].Value;
                    sportConf.BetradarSportID = node.Attributes["BetradarSportID"].Value;
                    sportConf.EventStructureTypeId = node.Attributes["EventStructureTypeId"].Value;
                    sportConf.EventPatternName = (node.Attributes["EventPatternName"] != null) ? node.Attributes["EventPatternName"].Value : null;
                    sportConf.PeriodPattern = (node.Attributes["PeriodPattern"] != null) ? node.Attributes["PeriodPattern"].Value : null;
                    sportConf.VenueType = (node.Attributes["VenueType"] != null) ? node.Attributes["VenueType"].Value : null;
                    sportConf.CompetitionOrgMethod = (node.Attributes["CompetitionOrgMethod"] != null) ? node.Attributes["CompetitionOrgMethod"].Value : null;
                    sportConf.StoreEventResults = (node.Attributes["StoreEventResults"] != null) ? bool.Parse(node.Attributes["StoreEventResults"].Value) : false;

                    _sportConfiguration[sportConf.BetradarSportID] = sportConf;

                    log.DebugFormat("Loaded sport BetradarSportID:'{0}' IDEVSport:'{1}' EventStructureTypeId:'{2}' EventPatternName:'{3}'", sportConf.BetradarSportID, sportConf.IdEvSport, sportConf.EventStructureTypeId, sportConf.EventPatternName);
                }

                log.InfoFormat("Loaded {0} sport configurations.", _sportConfiguration.Count);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Could not load sports configuration from location {0}\n{1}", configurationPath, ex);
            }
        }

        private static void LoadMarketsConfiguration(string configurationPath )
        {
            _marketConfiguration = new Dictionary<string, Dictionary<string, MarketElement>>();

            XmlDocument document = new XmlDocument();
            document.Load(configurationPath);

            XmlNodeList marketList = document.SelectNodes("/Config/Markets/Market");
            LoadMarkets(marketList);
        }

        private static void LoadMarkets(XmlNodeList marketList)
        {
            foreach (XmlNode marketNode in marketList)
            {
                string providerMarketId = "";
                try
                {
                    MarketElement marketElement = LoadMarketConf(marketNode);
                    providerMarketId = marketElement.ProviderMarketId;

                    XmlNodeList attributesNodeList = marketNode.SelectNodes("./MarketAttribute");
                    if (attributesNodeList.Count > 0)
                    {
                        foreach (XmlNode attribute in attributesNodeList)
                        {
                            switch (attribute.Attributes["name"].Value)
                            {
                                case "NUMSELECTIONS":
                                    int numSel = 0;
                                    if (int.TryParse(attribute.Attributes["value"].Value, out numSel))
                                        marketElement.NumSelections = numSel;
                                    break;
                                case "CATEGORIES":
                                    if (!string.IsNullOrEmpty(attribute.Attributes["value"].Value))
                                    {
                                        marketElement.Categories = new HashSet<string>(attribute.Attributes["value"].Value.Split(','));
                                    }

                                    break;
                                case "LINE":
                                    marketElement.Line = attribute.Attributes["value"].Value;
                                    marketElement.Attributes.Add(Constants.MarketAttributes.Line, attribute.Attributes["value"].Value);
                                    break;
                                case "LINE_TYPE":
                                    marketElement.LineType = attribute.Attributes["value"].Value;
                                    marketElement.Attributes.Add(Constants.MarketAttributes.LineType, attribute.Attributes["value"].Value);
                                    break;
                                case "PERIOD":
                                    marketElement.Period = attribute.Attributes["value"].Value;
                                    marketElement.Attributes.Add(Constants.MarketAttributes.Period, attribute.Attributes["value"].Value);
                                    break;
                                case "SUBPERIOD":
                                    marketElement.SubPeriod = attribute.Attributes["value"].Value;
                                    marketElement.Attributes.Add(Constants.MarketAttributes.Subperiod, attribute.Attributes["value"].Value);
                                    break;
                                case "PARTICIPANT_NUMBER":
                                    marketElement.ParticipantNumber = attribute.Attributes["value"].Value;
                                    marketElement.Attributes.Add(Constants.MarketAttributes.ParticipantNumber, attribute.Attributes["value"].Value);
                                    break;
                                case "PARTICIPANT_ID":
                                    marketElement.ParticipantId = attribute.Attributes["value"].Value;
                                    marketElement.Attributes.Add(Constants.MarketAttributes.ParticipantId, attribute.Attributes["value"].Value);
                                    break;
                                case "PARTICIPANT_MEMBER":
                                    marketElement.ParticipantMember = attribute.Attributes["value"].Value;
                                    marketElement.Attributes.Add(Constants.MarketAttributes.ParticipantMember, attribute.Attributes["value"].Value);
                                    break;
                                case "INDEX":
                                    marketElement.Index = attribute.Attributes["value"].Value;
                                    marketElement.Attributes.Add(Constants.MarketAttributes.Index, attribute.Attributes["value"].Value);
                                    break;
                                case "RANGE":
                                    marketElement.Range = attribute.Attributes["value"].Value;
                                    marketElement.Attributes.Add(Constants.MarketAttributes.Range, attribute.Attributes["value"].Value);
                                    break;
                                case "RANGE_TYPE":
                                    marketElement.Rangetype = attribute.Attributes["value"].Value;
                                    marketElement.Attributes.Add(Constants.MarketAttributes.RangeType, attribute.Attributes["value"].Value);
                                    break;
                            }
                        }
                    }

                    // Market sport configuration
                    XmlNodeList sportNodeList = marketNode.SelectNodes("./Sport");
                    if (sportNodeList.Count > 0)
                    {
                        foreach (XmlNode sport in sportNodeList)
                        {
                            MarketSportElement tmpMSportEl = new MarketSportElement();
                            tmpMSportEl.EvSportId = sport.Attributes["evSportId"].Value;
                            if (null != sport.Attributes["prematchOrder"])
                            {
                                tmpMSportEl.PrematchOrder = int.Parse(sport.Attributes["prematchOrder"].Value);
                            }
                            else
                            {
                                tmpMSportEl.PrematchOrder = 1000;//Utils.MARKET_ORDER_DEFAULT;
                                //log.WarnFormat("prematchOrder from market.xml doesn´t exist for evSportId [{0}] and providerMarketId [{1}]", tmpMSportEl.EvSportId, providerMarketId);
                            }

                            if (null != sport.Attributes["liveOddsOrder"])
                            {
                                tmpMSportEl.LiveOddsOrder = int.Parse(sport.Attributes["liveOddsOrder"].Value);
                            }
                            else
                            {
                                tmpMSportEl.LiveOddsOrder = 1000;//Utils.MARKET_ORDER_DEFAULT;
                                //log.WarnFormat("liveOddsOrder from market.xml doesn´t exist for evSportId [{0}] and providerMarketId [{1}]", tmpMSportEl.EvSportId, providerMarketId);
                            }

                            tmpMSportEl.CustomMarketName = sport.Attributes["customMarketName"] != null ? sport.Attributes["customMarketName"].Value : "";

                            if (sport.Attributes["customMakeUpValueFuction"] != null)
                            {
                                Constants.MakeUpValueFunctions tmpEnum;
                                if (Enum.TryParse(sport.Attributes["customMakeUpValueFuction"].Value, out tmpEnum))
                                {
                                    tmpMSportEl.CustomMakeUpValueFuction = tmpEnum;
                                }
                                else
                                {
                                    tmpMSportEl.CustomMakeUpValueFuction = Constants.MakeUpValueFunctions.Default;
                                }
                            }
                            else
                            {
                                tmpMSportEl.CustomMakeUpValueFuction = Constants.MakeUpValueFunctions.Default;
                            }

                            marketElement.SportsConfiguration.Add(tmpMSportEl.EvSportId, tmpMSportEl);
                        }
                    }


                    string[] marketConfId = providerMarketId.Split('|');
                    Dictionary<string, MarketElement> marketElementDict;


                    if (marketConfId.Length > 1 && _marketConfiguration.TryGetValue(marketConfId[0], out marketElementDict))
                    {
                        string key = marketElement.ProviderMarketId;
                        marketElementDict.Add(key, marketElement);
                    }
                    else
                    {
                        marketElementDict = new Dictionary<string, MarketElement>();
                        marketElementDict.Add(marketElement.ProviderMarketId, marketElement);
                        _marketConfiguration.Add(marketConfId[0], marketElementDict);
                    }
                }
                catch (Exception exc)
                {
                    log.ErrorFormat("Error load market configuration for {0}: \n{1}", providerMarketId, exc);
                    //throw;
                }
            }
        }

        private static MarketElement LoadMarketConf(XmlNode node)
        {
            MarketElement marketConfiguration = new MarketElement();

            marketConfiguration.ProviderMarketId = node.Attributes["providerMarketId"].Value;
            marketConfiguration.Name = node.Attributes["name"].Value;
            marketConfiguration.Groups = node.Attributes["groups"].Value;
            marketConfiguration.EvMarketTypeId = node.Attributes["evMarketTypeId"].Value;

            if (node.Attributes["variantAttributes"] != null)
            {
                marketConfiguration.VariantAttributes = node.Attributes["variantAttributes"].Value;
            }

            if (node.Attributes["variantValue"] != null)
            {
                marketConfiguration.VariantValue = node.Attributes["variantValue"].Value;
            }

            if (node.Attributes["selectionPatternId"] != null)
            {
                marketConfiguration.SelectionPatternId = node.Attributes["selectionPatternId"].Value;
            }

            if (node.Attributes["useMarketNameFromFeed"] != null)
            {
                bool tmpBool;
                if (bool.TryParse(node.Attributes["useMarketNameFromFeed"].Value, out tmpBool))
                {
                    marketConfiguration.UseMarketNameFromFeed = tmpBool;
                }

            }

            if (node.Attributes["suspendOnDeactivated"] != null)
            {
                bool tmpBool;
                if (bool.TryParse(node.Attributes["suspendOnDeactivated"].Value, out tmpBool))
                {
                    marketConfiguration.SuspendOnDeactivated = tmpBool;
                }
            }

            return marketConfiguration;
        }

        internal static SportConfiguration GetSportConfiguration(string betradarSportID)
        {
            SportConfiguration sportConfiguration = null;
            if (!_sportConfiguration.TryGetValue(betradarSportID, out sportConfiguration))
            {
                log.WarnFormat(string.Format("There is no sport configuration for BetradarSportID={0}", betradarSportID));
            }

            return sportConfiguration;
        }

        internal static MarketElement GetMarketConfiguration(string providerMarketId)
        {
            Dictionary<string, MarketElement> tmpMarketElements;
            MarketElement returnElement = null;

            if (_marketConfiguration.TryGetValue(providerMarketId, out tmpMarketElements))
            {

                returnElement = tmpMarketElements.ElementAt(0).Value;
            }
            else
            {
                log.DebugFormat("There is no market configuration with providerMarketId={0}", providerMarketId);
            }

            return returnElement;
        }

        public static int GetTotalNumberOfSportsLoaded()
        {
            return _sportConfiguration.Count();
        }
    }
}
