using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G16_2013.DAL;
using G16_2013.Models.TradeModel;
using G16_2013.Models.MemberModel;
using System.Data.Entity;
using System.Xml;
using G16_2013.BLL.TradeReport;


namespace TradeConsole
{
    class Program
    {
        static void Main(string[] args)
        {


            DropCreateDatabase();
            using (TradeContext context = new TradeContext())
            {
                var symbols = context.SymbolTypes.ToList();
                foreach (var item in symbols)
                {
                    Console.WriteLine("");
                }
            }


        }
        static void InsertTxtTradeReport()
        {
            //string filePath = @"c:\tw20130411.txt";
            //DateTime reportDate = new DateTime(2013, 4, 11);
            //ConcordsTradeReportResolver resolver =
            //    new ConcordsTradeReportResolver(BusinessType.Futures, reportDate, filePath);
            //List<TextFuturesTradeRecord> listRecords = resolver.ResolveTradeReport();
            //using (var context = new G16MemberContext())
            //{
            //    foreach (var record in listRecords)
            //    {
            //        context.TextFuturesTradeRecords.Add(record);
            //    }
            //    context.SaveChanges();

            //}

           

        }

        private static void DropCreateDatabase()
        {

            Database.SetInitializer(new DropCreateTradeDatabaseWithSeedData());
            InsertStockAccount();
            InsertFuturesAccount();
            InsertMonthCode();
            InsertParentSymbolType();
            InsertSymbolType();
            InsertCountries();
            InsertParentExchanges();
            InsertSubExchanges();
            InsertParentSymbols();
            InsertFuturesSymbols();
            InsertOptionsSymbols();
            InsertContractSpecs();
            InsertClearDayRules();
            InsertHolidays();
            InsertFuturesMargin();
            InsertOptionsMargin();

            InsertOrderConditions();
            InsertOrderKinds();

            InsertChargeFeeGroup();
            InsertChargeGroupsSymbols();

            InsertFuturesTradeSessions();

        }
        static void InsertOrderConditions()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/OrderConditions.xml");

            XmlNode root = doc.SelectSingleNode("//OrderConditions");
            XmlNodeList nodeList = root.SelectNodes("Condition");

            using (TradeContext context = new TradeContext())
            {
                foreach (XmlNode node in nodeList)
                {
                    string code = node.SelectSingleNode("Code").InnerText;
                    string name = node.SelectSingleNode("Name").InnerText;
                    string description = node.SelectSingleNode("Description").InnerText;

                    OrderCondition orderCondition = new OrderCondition
                    {
                        Code = code,
                        Name = name,
                        Description = description
                    };
                    context.OrderConditions.Add(orderCondition);
                }

                context.SaveChanges();
            }
        }
        static void InsertOrderKinds()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/OrderKind.xml");

            XmlNode root = doc.SelectSingleNode("//OrderKinds");
            XmlNodeList nodeList = root.SelectNodes("OrderKind");

            using (TradeContext context = new TradeContext())
            {
                foreach (XmlNode node in nodeList)
                {
                    string code = node.SelectSingleNode("Code").InnerText;
                    string name = node.SelectSingleNode("Name").InnerText;
                    string exchange = node.SelectSingleNode("Exchange").InnerText;
                    
                    var exchangeObject = context.Exchanges.Where(e => e.Code == exchange).FirstOrDefault();
                    var orderKind = new OrderKind()
                    {
                        // Exchange = exchangeObject,
                        Code = code,
                        Name = name,
                    };
                    var conditionNode = node.SelectSingleNode("Conditions");
                    if(conditionNode!=null)
                    {
                        string stringConditions = node.SelectSingleNode("Conditions").InnerText;
                        string[] conditions = stringConditions.Split(',');
                        foreach (string condition in conditions)
                        {
                            var orderCondition = context.OrderConditions.Where(c => c.Code == condition).FirstOrDefault();
                            orderKind.OrderConditions.Add(orderCondition);
                        }
                    }
                    
                    exchangeObject.OrderKinds.Add(orderKind);
                }

                context.SaveChanges();
            }
        }

        static void InsertFuturesTradeSessions()
        {
            XmlDocument doc = new XmlDocument();
            string dataFilePath = @"XMLFiles/TradeSessions.xml";
            doc.Load(dataFilePath);
            XmlNode root = doc.SelectSingleNode("//TradeSessions");
            XmlNodeList futuresSessions = root.SelectNodes("FuturesTradeSession");
            using (TradeContext context = new TradeContext())
            {
                foreach (XmlNode item in futuresSessions)
                {
                    string type = item.SelectSingleNode("SessionType").InnerText;
                    string name = item.SelectSingleNode("Name").InnerText;
                    string exchangeCode = item.SelectSingleNode("ExchangeCode").InnerText;
                    var exchange = context.Exchanges.Where(e => e.Code == exchangeCode).FirstOrDefault();
                    if (exchange == null) return;
                    string contractsFor = item.SelectSingleNode("ContractsFor").InnerText;
                    string orderNumber = item.SelectSingleNode("OrderNumber").InnerText;
                    string displayOrder = item.SelectSingleNode("DisplayOrder").InnerText;
                    string description = item.SelectSingleNode("Description").InnerText;

                    string startTime = item.SelectSingleNode("StartTime").InnerText;
                    string endTime = item.SelectSingleNode("EndTime").InnerText;

                    string matchStartTime = item.SelectSingleNode("MatchStartTime").InnerText;
                    string matchEndTime = item.SelectSingleNode("MatchEndTime").InnerText;

                    string timeZoneName = item.SelectSingleNode("TimeZoneName").InnerText;
                    string symbols = item.SelectSingleNode("Symbols").InnerText;
                    string allowOrderKinds = item.SelectSingleNode("AllowOrderKinds").InnerText;

                    var tradeSession = new MatchingSession()
                    {
                        Name = name,
                        OrderNumber = Convert.ToInt32(orderNumber),
                        DisplayOrder = Convert.ToInt32(displayOrder),
                        TimeZoneName = timeZoneName,
                        ExchangeCode = exchangeCode,
                        StartTime = GetTimeSpan(startTime, false),
                        EndTime = GetTimeSpan(endTime, false),
                        MatchStartTime = GetTimeSpan(matchStartTime, false),
                        MatchEndTime = GetTimeSpan(matchEndTime, false),
                        Description = description
                    };
                    if (type == "AfterHours") tradeSession.SessionType = SessionType.AfterHours;                    
                    else tradeSession.SessionType = SessionType.Regular;

                    if (contractsFor == "All") tradeSession.ContractsFor = ContractsFor.All;
                    else tradeSession.ContractsFor = ContractsFor.Expiring;
                    
                    tradeSession.AllowOrderKinds = GetOrderKinds(allowOrderKinds,exchange);
                    tradeSession.TradingSymbols = GetTradingSymbols(symbols);

                    var preOpenNode = item.SelectSingleNode("PreOpening");
                    if (preOpenNode != null)
                    {
                        string preOpeningStart = preOpenNode.SelectSingleNode("StartTime").InnerText;
                        string preOpeningEnd = preOpenNode.SelectSingleNode("EndTime").InnerText;
                        string openPriceMatchTime = preOpenNode.SelectSingleNode("OpenPriceMatchTime").InnerText;
                        PreOpening preOpening = new PreOpening
                        {
                            StartTime = GetTimeSpan(preOpeningStart, false),
                            EndTime = GetTimeSpan(preOpeningEnd, false),
                            OpenPriceMatchTime = GetTimeSpan(openPriceMatchTime, false),
                        };
                        var preOpenSessionNode = preOpenNode.SelectSingleNode("PreOpenSessions");
                        var preOpenSessions = preOpenSessionNode.SelectNodes("Session");
                        preOpening.PreOpeningSessions = new List<PreOpeningSession>(); 
                        foreach (XmlNode sessionNode in preOpenSessions)
                        {
                            var preOpeningSession = new PreOpeningSession()
                            {
                                Name = sessionNode.SelectSingleNode("Name").InnerText,
                                OrderNumber = Convert.ToInt32(sessionNode.SelectSingleNode("OrderNumber").InnerText),
                                DisplayOrder = Convert.ToInt32(sessionNode.SelectSingleNode("DisplayOrder").InnerText),
                                StartTime = GetTimeSpan(sessionNode.SelectSingleNode("StartTime").InnerText, false),
                                EndTime = GetTimeSpan(sessionNode.SelectSingleNode("EndTime").InnerText, false),
                                AllowNewOrder = Convert.ToBoolean(sessionNode.SelectSingleNode("AllowNewOrder").InnerText),
                                AllowCancel = Convert.ToBoolean(sessionNode.SelectSingleNode("AllowCancel").InnerText),
                                Description = sessionNode.SelectSingleNode("Description").InnerText,
                            };
                           
                            if (preOpeningSession.AllowNewOrder)
                            {
                                //string[] values = sessionNode.SelectSingleNode("AllowOrderKinds").InnerText.Split(',');
                                //foreach (string kind in values)
                                //{
                                //    var orderKind = exchange.OrderKinds.Where(o => o.Name == kind).FirstOrDefault();
                                //    preOpeningSession.AllowOrderKinds.Add(orderKind);
                                //}
                                preOpeningSession.AllowOrderKinds = 
                                    GetOrderKinds(sessionNode.SelectSingleNode("AllowOrderKinds").InnerText, exchange);
                            }
                            preOpening.PreOpeningSessions.Add(preOpeningSession);
                        }
                        tradeSession.PreOpening = preOpening;

                    }
                    var preCloseNode = item.SelectSingleNode("PreClosing");
                    if (preCloseNode != null)
                    {
                        string preClosingStart = preCloseNode.SelectSingleNode("StartTime").InnerText;
                        string preClosingEnd = preCloseNode.SelectSingleNode("EndTime").InnerText;                      
                        string closePriceMatchTime = preCloseNode.SelectSingleNode("ClosePriceMatchTime").InnerText;

                        tradeSession.PreClosing = new PreClosing
                        {
                            StartTime = GetTimeSpan(preClosingStart, false),
                            EndTime = GetTimeSpan(preClosingEnd, false),      
                            ClosePriceMatchTime = GetTimeSpan(closePriceMatchTime, false),
                        };
                        var preClosingNode = preCloseNode.SelectSingleNode("PreClosingSessions");                       
                        var preClosingSessions = preClosingNode.SelectNodes("Session");
                        tradeSession.PreClosing.PreClosingSessions = new List<PreClosingSession>();
                        foreach (XmlNode sessionNode in preClosingSessions)
                        {
                            var preClosingSession = new PreClosingSession()
                            {
                                Name = sessionNode.SelectSingleNode("Name").InnerText,
                                OrderNumber = Convert.ToInt32(sessionNode.SelectSingleNode("OrderNumber").InnerText),
                                DisplayOrder = Convert.ToInt32(sessionNode.SelectSingleNode("DisplayOrder").InnerText),
                                StartTime = GetTimeSpan(sessionNode.SelectSingleNode("StartTime").InnerText, false),
                                EndTime = GetTimeSpan(sessionNode.SelectSingleNode("EndTime").InnerText, false),
                                AllowNewOrder = Convert.ToBoolean(sessionNode.SelectSingleNode("AllowNewOrder").InnerText),
                                AllowCancel = Convert.ToBoolean(sessionNode.SelectSingleNode("AllowCancel").InnerText),
                                Description = sessionNode.SelectSingleNode("Description").InnerText,
                            };

                            if (preClosingSession.AllowNewOrder)
                            {                                
                                preClosingSession.AllowOrderKinds =
                                    GetOrderKinds(sessionNode.SelectSingleNode("AllowOrderKinds").InnerText, exchange);
                            }
                            tradeSession.PreClosing.PreClosingSessions.Add(preClosingSession);
                        }

                    }
                   


                    context.TradeSessions.Add(tradeSession);

                }
                context.SaveChanges();
            }


        }

        static List<OrderKind> GetOrderKinds(string input,Exchange exchange)
        {
            string[] values = input.Split(',');
            List<OrderKind> orderKinds = new List<OrderKind>();
            using (TradeContext context = new TradeContext())
            {
                foreach (string kind in values)
                {
                    var orderKind = exchange.OrderKinds.Where(o => o.Name == kind).FirstOrDefault();

                    orderKinds.Add(orderKind);
                }
             
            }
            return orderKinds;
        }

        static List<TradingSymbol> GetTradingSymbols(string input)
        {
            List<TradingSymbol> tradingSymbols = new List<TradingSymbol>();
            string[] values = input.Split(',');
            using (TradeContext context = new TradeContext())
            {
                foreach (string symbolName in values)
                {
                    var symbolObject = context.Symbols.Where(s => s.Name == symbolName).FirstOrDefault();

                    tradingSymbols.Add(symbolObject as TradingSymbol);
                }
               
            }
            return tradingSymbols;


        }

        static TimeSpan GetTimeSpan(string input, bool isEnd)
        {
            string[] values = input.Split(':');
            int day = 0;
            int hour = Convert.ToInt32(values[0]);
            int minute = Convert.ToInt32(values[1]);
            int second = Convert.ToInt32(values[2]);
            int mileSecond = 0;
            TimeSpan returnValue = new TimeSpan(day, hour, minute, second, mileSecond);
            if (isEnd)
            {
                TimeSpan diff = new TimeSpan(0, 0, 0, 0, -1);
                returnValue = returnValue.Add(diff);
            }


            return returnValue;
        }

        static void InsertChargeGroupsSymbols()
        {
            XmlDocument doc = new XmlDocument();
            string dataFilePath = @"XMLFiles/ChargeGroupsSymbols.xml";
            doc.Load(dataFilePath);
            using (TradeContext context = new TradeContext())
            {
                XmlNode root = doc.SelectSingleNode("//ChargeFeeGroups");
                foreach (XmlNode node in root.ChildNodes)
                {
                    XmlElement element = (XmlElement)node;
                    XmlAttribute companyAttribute = element.GetAttributeNode("Company");

                    var company = context.TradeCompanies.Where(c => c.Name == companyAttribute.Value).FirstOrDefault();
                    XmlAttribute attributeName = element.GetAttributeNode("Name");
                    string name = attributeName.Value;
                    //var group = (from c in context.ChargeFeeGroups
                    //             where c.CompanyId == company.TradeCompanyId && c.Name == name
                    //             select c).FirstOrDefault();
                    var group = context.ChargeFeeGroups.Where(c => c.Name == name && c.Company.TradeCompanyId == company.TradeCompanyId).FirstOrDefault();
                    foreach (XmlNode item in node.ChildNodes)
                    {
                        XmlElement itemElement = (XmlElement)item;
                        XmlAttribute nameAttribute = itemElement.GetAttributeNode("Name");
                        string symbolName = nameAttribute.Value;

                        var symbol = context.Symbols.OfType<TradingSymbol>().Where(s => s.Name == symbolName).FirstOrDefault();
                        group.Symbols.Add(symbol);

                    }

                    context.SaveChanges();
                }
            }


        }
        static void InsertChargeFeeGroup()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/ChargeFeeGroup.xml");


            XmlNode root = doc.SelectSingleNode("//ChargeFeeGroups");
            XmlNodeList nodeList = root.SelectNodes("Group");

            using (TradeContext context = new TradeContext())
            {
                foreach (XmlNode node in nodeList)
                {
                    string company = node.SelectSingleNode("Company").InnerText;
                    string currency = node.SelectSingleNode("Currency").InnerText;
                    string name = node.SelectSingleNode("Name").InnerText;
                    string code = node.SelectSingleNode("Code").InnerText;
                    string cost = node.SelectSingleNode("Cost").InnerText;
                    string redPointLimit = node.SelectSingleNode("RedPointLimit").InnerText;
                    string redPointGroup = node.SelectSingleNode("RedPointGroup").InnerText;

                    var chargeGroup = new FuturesChargeFeeGroup
                    {
                        Name = name,
                        Code = code,
                        Company = context.TradeCompanies.Where(c => c.Name == company).FirstOrDefault(),
                        Cost = Convert.ToDouble(cost),
                        Currency = context.Currencies.Where(c => c.Name == currency).FirstOrDefault(),
                        RedPointLimit = Convert.ToDouble(redPointLimit),
                        RedPointGroup = context.RedPointGroups.Where(r => r.Name == redPointGroup).FirstOrDefault()
                    };
                    context.ChargeFeeGroups.Add(chargeGroup);

                }

                context.SaveChanges();

            }
        }
        private static void InsertStockAccount()
        {
            var stockAccount = new StockTradeAccount
            {
                CompanyId = 2,
                AccountNumber = "60033495"
            };
            using (TradeContext context = new TradeContext())
            {
                context.TradeAccounts.Add(stockAccount);
                context.SaveChanges();
            }
        }
        private static void InsertFuturesAccount()
        {
            var futuresAccount = new FuturesTradeAccount
            {
                CompanyId = 1,
                AccountNumber = "8565565"
            };
            using (TradeContext context = new TradeContext())
            {
                context.TradeAccounts.Add(futuresAccount);
                context.SaveChanges();
            }
        }

        private static void InsertMonthCode()
        {
            using (TradeContext context = new TradeContext())
            {
                var code = new string[] { "F", "G", "H", "J", "K", "M", "N", "Q", "U", "V", "X", "Z" };
                for (int m = 1; m <= 12; m++)
                {
                    var month = new MonthCode
                    {
                        Code = code[m - 1],
                        Month = m
                    };
                    context.MonthCodes.Add(month);
                }
                context.SaveChanges();
            }
        }

        private static void InsertParentSymbolType()
        {

            using (TradeContext context = new TradeContext())
            {
                string[] types = new string[] { "股市", "能源", "外匯", "金屬", "農產品", "利率" };
                for (int i = 1; i <= types.Length; i++)
                {
                    SymbolType type = new SymbolType()
                    {
                        Name = types[i - 1],
                        ParentSymbolTypeId = 0,
                        DisplayOrder = i,

                    };
                    context.SymbolTypes.Add(type);
                }
                context.SaveChanges();
            }
        }
        private static void InsertSymbolType()
        {
            string parentTypeName = "股市";
            int parentTypeId = GetSymbolTypeIdByName(parentTypeName);
            if (parentTypeId == 0) return;
            int displayOrder = 0;
            using (TradeContext context = new TradeContext())
            {
                SymbolType index = new SymbolType()
                {
                    Name = "股價指數",
                    ParentSymbolTypeId = parentTypeId,
                    DisplayOrder = displayOrder,

                };
                context.SymbolTypes.Add(index);
                displayOrder++;

                SymbolType stock = new SymbolType()
                {
                    Name = "台灣股票",
                    ParentSymbolTypeId = parentTypeId,
                    DisplayOrder = displayOrder,

                };
                context.SymbolTypes.Add(stock);
                displayOrder++;

                parentTypeName = "農產品";
                parentTypeId = GetSymbolTypeIdByName(parentTypeName);
                if (parentTypeId == 0) return;
                SymbolType grain = new SymbolType()
                {
                    Name = "穀物",
                    ParentSymbolTypeId = parentTypeId,
                    DisplayOrder = displayOrder,

                };
                context.SymbolTypes.Add(grain);
                displayOrder++;
                SymbolType softs = new SymbolType()
                {
                    Name = "軟性商品",
                    ParentSymbolTypeId = parentTypeId,
                    DisplayOrder = displayOrder,

                };
                context.SymbolTypes.Add(softs);
                displayOrder++;

                SymbolType livestock = new SymbolType()
                {
                    Name = "牲畜",
                    ParentSymbolTypeId = parentTypeId,
                    DisplayOrder = displayOrder,

                };
                context.SymbolTypes.Add(livestock);
                displayOrder++;

                parentTypeName = "金屬";
                parentTypeId = GetSymbolTypeIdByName(parentTypeName);
                if (parentTypeId == 0) return;
                SymbolType precious = new SymbolType()
                {
                    Name = "貴金屬",
                    ParentSymbolTypeId = parentTypeId,
                    DisplayOrder = displayOrder,

                };
                context.SymbolTypes.Add(precious);
                displayOrder++;
                SymbolType baseMetal = new SymbolType()
                {
                    Name = "基本金屬",
                    ParentSymbolTypeId = parentTypeId,
                    DisplayOrder = displayOrder,

                };
                context.SymbolTypes.Add(baseMetal);
                displayOrder++;

                context.SaveChanges();
            }


        }
        private static int GetSymbolTypeIdByName(string parentTypeName)
        {
            using (TradeContext context = new TradeContext())
            {
                var parentType = context.SymbolTypes.Where(st => st.Name == parentTypeName).FirstOrDefault();
                if (parentType == null) return 0;
                return parentType.SymbolTypeId;
            }
        }

        private static void InsertCountries()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/Countries.xml");

            XmlNode root = doc.SelectSingleNode("//Countries");
            XmlNodeList nodeList = root.SelectNodes("Country");

            using (TradeContext context = new TradeContext())
            {
                foreach (XmlNode node in nodeList)
                {
                    string continent = node.SelectSingleNode("Continent").InnerText;

                    var country = new Country()
                    {
                        Name = node.SelectSingleNode("Name").InnerText,
                        Continent = context.Continents.Where(c => c.Name == continent).FirstOrDefault(),

                    };
                    context.Countries.Add(country);
                }
                context.SaveChanges();
            }

        }

        private static void InsertParentExchanges()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/Exchanges.xml");


            XmlNode root = doc.SelectSingleNode("//Exchanges");
            XmlNodeList nodeList = root.SelectNodes("Exchange");
            List<Exchange> parentExchanges = new List<Exchange>();


            using (TradeContext context = new TradeContext())
            {
                int displayOrder = 0;
                foreach (XmlNode node in nodeList)
                {
                    string parent = node.SelectSingleNode("ParentExchange").InnerText;
                    string name = node.SelectSingleNode("Name").InnerText;
                    string code = node.SelectSingleNode("Code").InnerText;
                    string country = node.SelectSingleNode("Country").InnerText;
                    var exchange = new Exchange();
                    exchange.Name = name;
                    exchange.Code = code;
                    exchange.DisplayOrder = displayOrder;
                    exchange.Country = context.Countries.Where(c => c.Name == country).FirstOrDefault();
                    if (parent == "0")
                    {
                        //exchange.ParentExchangeId = 0;
                        parentExchanges.Add(exchange);
                        displayOrder++;
                    }


                }

                foreach (var exchange in parentExchanges)
                {
                    context.Exchanges.Add(exchange);
                }
                context.SaveChanges();

            }

        }

        private static void InsertSubExchanges()
        {
            List<Exchange> subExchanges = new List<Exchange>();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/Exchanges.xml");

            XmlNode root = doc.SelectSingleNode("//Exchanges");
            XmlNodeList nodeList = root.SelectNodes("Exchange");
            using (TradeContext context = new TradeContext())
            {
                int displayOrder = 0;
                foreach (XmlNode node in nodeList)
                {
                    string parent = node.SelectSingleNode("ParentExchange").InnerText;
                    string name = node.SelectSingleNode("Name").InnerText;
                    string code = node.SelectSingleNode("Code").InnerText;
                    string country = node.SelectSingleNode("Country").InnerText;
                    var exchange = new Exchange();
                    exchange.Name = name;
                    exchange.Code = code;
                    exchange.DisplayOrder = displayOrder;
                    exchange.Country = context.Countries.Where(c => c.Name == country).FirstOrDefault();
                    if (parent != "0")
                    {
                        int parentExchangeId = (from e in context.Exchanges
                                                where e.Code == parent
                                                select e.ExchangeId).FirstOrDefault();
                        exchange.ParentExchangeId = parentExchangeId;
                        //exchange.ParentExchange=
                        subExchanges.Add(exchange);
                        displayOrder++;
                    }


                }
                foreach (var exchange in subExchanges)
                {
                    context.Exchanges.Add(exchange);
                }
                context.SaveChanges();
            }

        }
        private static void InsertParentSymbols()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/Symbols.xml");


            XmlNode root = doc.SelectSingleNode("//Symbols");
            XmlNodeList nodeList = root.SelectNodes("Symbol");

            using (TradeContext context = new TradeContext())
            {
                int displayOrder = 0;
                foreach (XmlNode node in nodeList)
                {
                    string name = node.SelectSingleNode("Name").InnerText;
                    string code = node.SelectSingleNode("Code").InnerText;
                    string country = node.SelectSingleNode("Country").InnerText;
                    string symbolType = node.SelectSingleNode("SymbolType").InnerText;
                    var symbol = new Symbol
                    {
                        Name = name,
                        Code = code,
                        DisplayOrder = displayOrder,
                        Country = context.Countries.Where(c => c.Name == country).FirstOrDefault(),
                        SymbolType = context.SymbolTypes.Where(st => st.Name == symbolType).FirstOrDefault(),

                    };
                    context.Symbols.Add(symbol);
                    displayOrder++;
                }

                context.SaveChanges();
            }
        }

        private static void InsertFuturesSymbols()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/FuturesSymbols.xml");


            XmlNode root = doc.SelectSingleNode("//FuturesSymbols");
            XmlNodeList nodeList = root.SelectNodes("FuturesSymbol");
            string testString = "";
            using (TradeContext context = new TradeContext())
            {
                int displayOrder = 0;
                int parentSymbolId = 0;
                foreach (XmlNode node in nodeList)
                {
                    string name = node.SelectSingleNode("Name").InnerText;
                    string code = node.SelectSingleNode("Code").InnerText;
                    string country = node.SelectSingleNode("Country").InnerText;
                    string symbolType = node.SelectSingleNode("SymbolType").InnerText;
                    string parentSymbol = node.SelectSingleNode("ParentSymbol").InnerText;
                    var parentSymbolObject = context.Symbols.Where(s => s.Name == parentSymbol).FirstOrDefault();
                    if (parentSymbolObject == null)
                    {
                        testString += name + ",";
                    }
                    else
                    {
                        parentSymbolId = parentSymbolObject.SymbolId;
                    }
                    string exchange = node.SelectSingleNode("Exchange").InnerText;
                    string currency = node.SelectSingleNode("Currency").InnerText;
                    string quoteLeftText = node.SelectSingleNode("QuoteLeftText").InnerText;
                    string quotePointSymbol;
                    var pointSymbolNode = node.SelectSingleNode("QuotePointSymbol");
                    if (pointSymbolNode != null)
                    {
                        quotePointSymbol = pointSymbolNode.InnerText;
                    }
                    else
                    {
                        quotePointSymbol = ".";
                    }

                    string quoteIntDigits = node.SelectSingleNode("QuoteIntDigits").InnerText;
                    string quoteFloatDigits = node.SelectSingleNode("QuoteFloatDigits").InnerText;
                    string quoteRightText = node.SelectSingleNode("QuoteRightText").InnerText;
                    string clearWay = node.SelectSingleNode("ClearWay").InnerText;
                    ClearWay wayToClear = ClearWay.Cash;
                    if (clearWay == "Physical")
                    {
                        wayToClear = ClearWay.Physical;
                    }


                    var futuresSymbol = new Futures
                    {
                        Name = name,
                        Code = code,
                        DisplayOrder = displayOrder,
                        Country = context.Countries.Where(c => c.Name == country).FirstOrDefault(),
                        SymbolType = context.SymbolTypes.Where(st => st.Name == symbolType).FirstOrDefault(),

                        ParentSymbolId = parentSymbolId,
                        Exchange = context.Exchanges.Where(e => e.Code == exchange).FirstOrDefault(),
                        Currency = context.Currencies.Where(c => c.Name == currency).FirstOrDefault(),
                        ClearWay = wayToClear,
                        QuoteWay = new QuoteWay
                        {
                            LeftText = quoteLeftText,
                            IntDigits = Convert.ToInt32(quoteIntDigits),
                            FloatDigits = Convert.ToInt32(quoteFloatDigits),
                            RightText = quoteRightText,
                            PointSymbol = quotePointSymbol
                        }
                    };
                    context.Symbols.Add(futuresSymbol);
                    displayOrder++;
                }

                context.SaveChanges();
            }



        }
        private static void InsertOptionsSymbols()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/OptionSymbols.xml");


            XmlNode root = doc.SelectSingleNode("//OptionSymbols");
            XmlNodeList nodeList = root.SelectNodes("OptionSymbol");
            string testString = "";
            using (TradeContext context = new TradeContext())
            {
                int displayOrder = 0;
                int parentSymbolId = 0;
                foreach (XmlNode node in nodeList)
                {
                    string name = node.SelectSingleNode("Name").InnerText;
                    string code = node.SelectSingleNode("Code").InnerText;
                    string country = node.SelectSingleNode("Country").InnerText;
                    string symbolType = node.SelectSingleNode("SymbolType").InnerText;
                    string parentSymbol = node.SelectSingleNode("ParentSymbol").InnerText;
                    var parentSymbolObject = context.Symbols.Where(s => s.Name == parentSymbol).FirstOrDefault();
                    if (parentSymbolObject == null)
                    {
                        testString += name + ",";
                    }
                    else
                    {
                        parentSymbolId = parentSymbolObject.SymbolId;
                    }
                    string exchange = node.SelectSingleNode("Exchange").InnerText;
                    string currency = node.SelectSingleNode("Currency").InnerText;
                    string quoteLeftText = node.SelectSingleNode("QuoteLeftText").InnerText;
                    string quoteIntDigits = node.SelectSingleNode("QuoteIntDigits").InnerText;
                    string quoteFloatDigits = node.SelectSingleNode("QuoteFloatDigits").InnerText;
                    string quoteRightText = node.SelectSingleNode("QuoteRightText").InnerText;
                    string clearWay = node.SelectSingleNode("ClearWay").InnerText;

                    ClearWay wayToClear = ClearWay.Cash;
                    if (clearWay == "Physical")
                    {
                        wayToClear = ClearWay.Physical;
                    }


                    var optionSymbol = new Options
                    {
                        Name = name,
                        Code = code,
                        DisplayOrder = displayOrder,
                        Country = context.Countries.Where(c => c.Name == country).FirstOrDefault(),
                        SymbolType = context.SymbolTypes.Where(st => st.Name == symbolType).FirstOrDefault(),

                        ParentSymbolId = parentSymbolId,
                        Exchange = context.Exchanges.Where(e => e.Code == exchange).FirstOrDefault(),
                        Currency = context.Currencies.Where(c => c.Name == currency).FirstOrDefault(),
                        ClearWay = wayToClear,
                        QuoteWay = new QuoteWay
                        {
                            LeftText = quoteLeftText,
                            IntDigits = Convert.ToInt32(quoteIntDigits),
                            FloatDigits = Convert.ToInt32(quoteFloatDigits),
                            RightText = quoteRightText,
                            PointSymbol = "."
                        },
                        OptionType = OptionsType.Europe
                    };
                    context.Symbols.Add(optionSymbol);
                    displayOrder++;
                }

                context.SaveChanges();
            }
        }

        private static void InsertContractSpecs()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/ContractSpec.xml");


            XmlNode root = doc.SelectSingleNode("//ContractSpecs");
            XmlNodeList nodeList = root.SelectNodes("ContractSpec");

            using (TradeContext context = new TradeContext())
            {
                foreach (XmlNode node in nodeList)
                {
                    string symbol = node.SelectSingleNode("Symbol").InnerText;
                    string quantity = node.SelectSingleNode("Quantity").InnerText;
                    string unit = node.SelectSingleNode("Unit").InnerText;
                    string onePointValue = node.SelectSingleNode("OnePointValue").InnerText;
                    string month = node.SelectSingleNode("Month").InnerText;
                    string regularMonth = node.SelectSingleNode("RegularMonth").InnerText;
                    string quarterlyMonth = node.SelectSingleNode("QuarterlyMonth").InnerText;
                    string minimumTick = node.SelectSingleNode("MinimumTick").InnerText;
                    var symbolObject = context.Symbols.Where(s => s.Name == symbol).FirstOrDefault();
                    int symbolId = symbolObject.SymbolId;
                    var derivative = symbolObject as Derivative;
                    derivative.ContractSpec = new DerivativeContractSpec
                    {
                        SymbolId = symbolId,
                        ContractSize = new ContractSize
                        {
                            Quantity = Convert.ToDecimal(quantity),
                            Unit = unit
                        },
                        OnePointValue = Convert.ToDouble(onePointValue),
                        MonthRule = new MonthRule
                        {
                            TradeMonth = month,
                            RegularMonth = Convert.ToInt32(regularMonth),
                            QuarterlyMonth = Convert.ToInt32(quarterlyMonth)
                        },
                        MinimumTick = minimumTick
                    };

                }

                context.SaveChanges();

            }
        }

        private static void InsertClearDayRules()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/ContractDayRule.xml");


            XmlNode root = doc.SelectSingleNode("//ContractDayRules");
            XmlNodeList nodeList = root.SelectNodes("ContractDayRule");

            using (TradeContext context = new TradeContext())
            {
                foreach (XmlNode node in nodeList)
                {
                    string symbol = node.SelectSingleNode("Symbol").InnerText;
                    string holidayCountry = node.SelectSingleNode("HolidayCountry").InnerText;
                    string contractDay = node.SelectSingleNode("ContractDay").InnerText;
                    string monthDiff = node.SelectSingleNode("MonthDiff").InnerText;
                    string countWay = node.SelectSingleNode("CountWay").InnerText;
                    string countOrder = node.SelectSingleNode("CountOrder").InnerText;
                    string dayKind = node.SelectSingleNode("DayKind").InnerText;
                    string weekDay = node.SelectSingleNode("WeekDay").InnerText;

                    var symbolObject = context.Symbols.Where(s => s.Name == symbol).FirstOrDefault();
                    int symbolId = symbolObject.SymbolId;
                    var derivative = symbolObject as Derivative;

                    var contractDayRule = new ContractDayRule()
                    {
                        SymbolId = symbolId,
                        HolidayCountry = holidayCountry,
                        ContractDay = GetContractDay(contractDay),
                        MonthDiff = Convert.ToInt32(monthDiff),
                        CountWay = GetCountWay(countWay),
                        CountOrder = Convert.ToInt32(countOrder),
                        DayKind = GetDayKind(dayKind),
                        WeekDay = GetWeekDay(weekDay)
                    };
                    context.Entry(derivative).Reference(d => d.ContractSpec).Load();
                    context.Entry(derivative.ContractSpec).Collection(c => c.ContractDayRules).Load();

                    derivative.ContractSpec.ContractDayRules.Add(contractDayRule);
                }
                context.SaveChanges();
            }
        }

        private static void InsertHolidays()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/Holidays.xml");
            XmlNode root = doc.SelectSingleNode("//Holidays");
            XmlNodeList nodeList = root.SelectNodes("Holiday");

            using (TradeContext context = new TradeContext())
            {
                foreach (XmlNode node in nodeList)
                {
                    string country = node.SelectSingleNode("Country").InnerText;
                    int year = Convert.ToInt32(node.SelectSingleNode("Year").InnerText);
                    int month = Convert.ToInt32(node.SelectSingleNode("Month").InnerText);
                    int day = Convert.ToInt32(node.SelectSingleNode("Day").InnerText);
                    string name = node.SelectSingleNode("Name").InnerText;
                    var countryObject = context.Countries.Where(c => c.Name == country).FirstOrDefault();
                    context.Entry(countryObject).Collection(c => c.Holidays).Load();
                    var holiday = new Holiday
                    {
                        Year = year,
                        DateOfHoliday = new DateTime(year, month, day),
                        HolidayName = name
                    };
                    countryObject.Holidays.Add(holiday);
                }
                context.SaveChanges();
            }
        }

        private static void InsertFuturesMargin()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/Margins.xml");
            XmlNode root = doc.SelectSingleNode("//Margins");
            XmlNodeList nodeList = root.SelectNodes("FuturesMargin");
            using (TradeContext context = new TradeContext())
            {
                foreach (XmlNode node in nodeList)
                {
                    string symbol = node.SelectSingleNode("Symbol").InnerText;
                    int iniMargin = Convert.ToInt32(node.SelectSingleNode("IniMargin").InnerText);
                    int maintanceMargin = Convert.ToInt32(node.SelectSingleNode("MaintanceMargin").InnerText);
                    var symbolObject = context.Symbols.Where(s => s.Name == symbol).FirstOrDefault();
                    var derivative = symbolObject as Derivative;
                    derivative.Margin = new DerivativeMargin
                    {
                        InitialMargin = iniMargin,
                        MaintenanceMargin = maintanceMargin,
                        LastUpdate = DateTime.Now,

                    };
                }
                context.SaveChanges();
            }
        }
        private static void InsertOptionsMargin()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XMLFiles/Margins.xml");
            XmlNode root = doc.SelectSingleNode("//Margins");
            XmlNodeList nodeList = root.SelectNodes("OptionMargin");
            using (TradeContext context = new TradeContext())
            {
                foreach (XmlNode node in nodeList)
                {
                    string symbol = node.SelectSingleNode("Symbol").InnerText;
                    int iniMargin = Convert.ToInt32(node.SelectSingleNode("IniMargin").InnerText);
                    int maintanceMargin = Convert.ToInt32(node.SelectSingleNode("MaintanceMargin").InnerText);
                    string marginType = node.SelectSingleNode("MarginType").InnerText;
                    var symbolObject = context.Symbols.Where(s => s.Name == symbol).FirstOrDefault();

                    var derivative = symbolObject as Derivative;
                    derivative.Margin = new OptionsMargin
                    {
                        InitialMargin = iniMargin,
                        MaintenanceMargin = maintanceMargin,
                        LastUpdate = DateTime.Now,
                        MarginType = marginType
                    };
                }
                context.SaveChanges();
            }
        }

        private static void InsertContract()
        {

        }



        private static ContractDay GetContractDay(string input)
        {
            if (input == "Clear") return ContractDay.Clear;
            if (input == "LastTrade") return ContractDay.LastTrade;
            if (input == "FirstNotice") return ContractDay.FirstNotice;
            return ContractDay.Clear;
        }
        private static CountWay GetCountWay(string input)
        {
            if (input == "Forward") return CountWay.Forward;
            if (input == "Backward") return CountWay.Backward;

            return CountWay.Forward;
        }
        private static DayKind GetDayKind(string input)
        {
            if (input == "BusinessDay") return DayKind.BusinessDay;
            if (input == "WeekDay") return DayKind.WeekDay;

            return DayKind.BusinessDay;
        }
        private static WeekDay GetWeekDay(string input)
        {
            if (input == "Sun") return WeekDay.Sun;
            if (input == "Mon") return WeekDay.Mon;
            if (input == "Tue") return WeekDay.Tue;
            if (input == "Wed") return WeekDay.Wed;
            if (input == "Thu") return WeekDay.Thu;
            if (input == "Fri") return WeekDay.Fri;
            if (input == "Sat") return WeekDay.Sat;

            return WeekDay.Sun;
        }

    }
}
