using System;
using System.Net;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using Newtonsoft.Json;
using System.Diagnostics;

namespace PG
{
    class Program
    {
        static Dictionary<string, InterestedStats> Statistics = new Dictionary<string, InterestedStats>();
        static readonly HashSet<string> InteresedPlayers = new HashSet<string> { "killthelastone", "NnnnnnJx", "qingiho-", "#unknown" }; //Accounts are case sensitive
        static string key = ""; //Your API Key
        static void Main(string[] args)
        {
            Console.WriteLine("[PLAYER] Requesting initial player data for users: {0}", string.Join(",", InteresedPlayers));
            PlayerRequest playerRequest = GetPlayerRequest(InteresedPlayers);

            if (playerRequest != null)
            {
                Console.WriteLine("[PLAYER] PlayerRequest replied with data for users: " + string.Join(",", playerRequest.data.Select(p => p.attributes.name)));
                foreach (var player in playerRequest.data)
                {
                    Console.WriteLine("[PLAYER] Processing data for player {0}", player.attributes.name);
                    if (!Statistics.ContainsKey(player.attributes.name))
                        Statistics.Add(player.attributes.name, new InterestedStats());

                    var limit = 0;
                    foreach (var match in player.relationships.matches.data)
                    {
                        InterestedStats playerStats = null;
                        MatchRequest req = GetMatch(match.id);
                        if (req.data.attributes.gameMode == "squad")
                        {
                            foreach (var included in req.included)
                            {
                                if (included.attributes.stats == null || included.attributes.stats.name == null)
                                    continue;

                                playerStats = Statistics[player.attributes.name];

                                if (included.attributes.stats.name.Equals(player.attributes.name))
                                {
                                    Console.WriteLine("[PROCESS] Parsing match {0} information for player {1}", match.id, player.attributes.name);

                                    playerStats.teamKills += Convert.ToDouble(included.attributes.stats.teamKills);
                                    playerStats.damageDealt += Convert.ToDouble(included.attributes.stats.damageDealt);
                                    playerStats.DBNOs += Convert.ToDouble(included.attributes.stats.kills);
                                    playerStats.kills += Convert.ToDouble(included.attributes.stats.kills);
                                    playerStats.headshotKills += Convert.ToDouble(included.attributes.stats.headshotKills);
                                    playerStats.killPlace += Convert.ToDouble(included.attributes.stats.killPlace);
                                    playerStats.weaponsAcquired += Convert.ToDouble(included.attributes.stats.weaponsAcquired);
                                    playerStats.assists += Convert.ToDouble(included.attributes.stats.assists);
                                    playerStats.revives += Convert.ToDouble(included.attributes.stats.revives);
                                    playerStats.walkDistance += Convert.ToDouble(included.attributes.stats.walkDistance);
                                    playerStats.timeSurvived += Convert.ToDouble(included.attributes.stats.timeSurvived);
                                    playerStats.winPlace += Convert.ToDouble(included.attributes.stats.winPlace);
                                    break;
                                }
                            }

                            if (playerStats != null)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine("[TELEMETRY] Requesting telemery metadatada for ID {0}", req.data.id);
                                Console.ResetColor();

                                TelemetryRequest tReq = GetTelemetry(req.data.id);
                                if (tReq != null && tReq.data.relationships.assets.data.Count > 0)
                                {
                                    var telemetryId = tReq.data.relationships.assets.data.First().id;
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("[TELEMETRY] Found data package ID {0} for Telemetry ID {1}", telemetryId, req.data.id);
                                    Console.ResetColor();
                                    var telemData = tReq.included.Find(v => v.id == telemetryId);
                                    if (telemData != null)
                                    {
                                        var url = telemData.attributes.URL;
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("[TELEMETRY] Grabbing data from URL: {0}", url);
                                        Console.ResetColor();

                                        var telemPkg = GetTelemetryPackage(url, out double ByteLength);
                                        if (telemPkg != null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.WriteLine("[TELEMETRY] Downloaded telemetry package with {0} bytes length.", ByteLength);
                                            Console.ResetColor();

                                            var playerDict = new HashSet<string>(telemPkg.Select(v => v.Character?.Name));
                                            foreach (var username in InteresedPlayers)
                                                if (playerDict.Contains(username))
                                                    continue;

                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("[TELEMETRY] All interested players found on telemetry package, Aggregating to player {0}", player.attributes.name);
                                            Console.ResetColor();
                                            TelemetryPackage[] userTelemetry = telemPkg.Where(t => t.Character != null && t.Character.Name.ToString().Equals(player.attributes.name, StringComparison.InvariantCultureIgnoreCase)).ToArray();
                                            playerStats.Telemetry.AddRange(userTelemetry);
                                        }
                                    }
                                }
                            }

                            Thread.Sleep(100);
                        }

                        if (limit++ == 10)
                            break;
                    }
                }

                HashSet<string> TelemtryType = new HashSet<string>();
                HashSet<string> DamageType = new HashSet<string>();
                HashSet<string> AttackType = new HashSet<string>();
                HashSet<string> ItemCategories = new HashSet<string>();
                Random rnd = new Random();
                Console.WriteLine("\nResults:");
                foreach (var kvp in Statistics)
                {
                    var str = string.Format("P {0} Walk {1} Revives {2} Assists {3} Damage {4} Kills {5} SurvivedTime {6} weaponsAcquired {7} HeadshotKills {8} KillPlace {9} TeamKills {10} WinPlace {11} DBNOs {12}",
                         kvp.Key,
                         kvp.Value.walkDistance,
                         kvp.Value.revives,
                         kvp.Value.assists,
                         kvp.Value.damageDealt,
                         kvp.Value.kills,
                         TimeSpan.FromMinutes(kvp.Value.timeSurvived).TotalMinutes,
                         kvp.Value.weaponsAcquired,
                         kvp.Value.headshotKills,
                         kvp.Value.killPlace,
                         kvp.Value.teamKills,
                         kvp.Value.winPlace,
                         kvp.Value.DBNOs
                         );

                    Console.WriteLine(str);

                    foreach (var tel in kvp.Value.Telemetry)
                    {
                        if(tel.ItemPackage != null)
                            foreach (var item in tel.ItemPackage.Items)
                                if (!string.IsNullOrEmpty(item.Category))
                                    ItemCategories.Add(item.Category);

                        if(tel.Item != null)
                            if(!string.IsNullOrEmpty(tel.Item.Category))
                                ItemCategories.Add(tel.Item.Category);

                        if (!string.IsNullOrEmpty(tel.T))
                        {
                            Debug.WriteLine("TelemetryType: " + tel.T);
                            TelemtryType.Add(tel.T);
                        }

                        if (!string.IsNullOrEmpty(tel.DamageTypeCategory))
                        {
                            Debug.WriteLine("DamageTypeCategory: " + tel.DamageTypeCategory);
                            DamageType.Add(tel.DamageTypeCategory);
                        }

                        if (!string.IsNullOrEmpty(tel.AttackType))
                        {
                            Debug.WriteLine("AttackType: " + tel.AttackType);
                            AttackType.Add(tel.AttackType);
                        }                    
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nLoot Analysis:");
                var greedyPlayer = Statistics.Aggregate((x, y) => x.Value.Telemetry.Count(v => v.T.Equals("LogItemPickup", StringComparison.InvariantCultureIgnoreCase)) > y.Value.Telemetry.Count(v => v.T.Equals("LogItemPickup", StringComparison.InvariantCultureIgnoreCase)) ? x : y).Key;

                Console.WriteLine("Most greedy player: {0}", greedyPlayer);
                foreach (var itemCat in ItemCategories)
                    Console.WriteLine($"{itemCat}: {Statistics[greedyPlayer].Telemetry.Count(v => v.T.Contains("LogItemPickup") && v.Item.Category.Contains($"{itemCat}"))}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nSquad Results:");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nMAX Results:");
                Console.WriteLine("TeamKills - Number of times this player killed a teammate: {0}", Statistics.Aggregate((x, y) => x.Value.teamKills > y.Value.teamKills ? x : y).Key);
                Console.WriteLine("DamageDealt - Total damage dealt.Note: Self inflicted damage is subtracted: {0}", Statistics.Aggregate((x, y) => x.Value.damageDealt > y.Value.damageDealt ? x : y).Key);
                Console.WriteLine("DBNOs - Number of enemy players knocked: {0}", Statistics.Aggregate((x, y) => x.Value.DBNOs > y.Value.DBNOs ? x : y).Key);
                Console.WriteLine("Kills - Number of enemy players killed: {0}", Statistics.Aggregate((x, y) => x.Value.kills > y.Value.kills ? x : y).Key);
                Console.WriteLine("HeadshotKills - Number of enemy players killed with headshots: {0}", Statistics.Aggregate((x, y) => x.Value.headshotKills > y.Value.headshotKills ? x : y).Key);
                Console.WriteLine("KillPlace - This player's rank in the match based on kills: {0}", Statistics.Aggregate((x, y) => x.Value.killPlace > y.Value.killPlace ? x : y).Key);
                Console.WriteLine("WeaponsAcquired - Number of weapons picked up: {0}", Statistics.Aggregate((x, y) => x.Value.weaponsAcquired > y.Value.weaponsAcquired ? x : y).Key);
                Console.WriteLine("Assists - Number of enemy players this player damaged that were killed by teammates: {0}", Statistics.Aggregate((x, y) => x.Value.assists > y.Value.assists ? x : y).Key);
                Console.WriteLine("Revives - Number of times this player revived teammates: {0}", Statistics.Aggregate((x, y) => x.Value.revives > y.Value.revives ? x : y).Key);
                Console.WriteLine("WalkDistance - Total distance traveled on foot measured in meters: {0}", Statistics.Aggregate((x, y) => x.Value.walkDistance > y.Value.walkDistance ? x : y).Key);
                Console.WriteLine("TimeSurvived - Amount of time survived measured in seconds: {0}", Statistics.Aggregate((x, y) => x.Value.timeSurvived > y.Value.timeSurvived ? x : y).Key);
                Console.WriteLine("WinPlace - This player's placement in the match: {0}", Statistics.Aggregate((x, y) => x.Value.winPlace > y.Value.winPlace ? x : y).Key);
                Console.WriteLine();
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nMIN Results:");

                Console.WriteLine("\nLoot Results:");
                Console.WriteLine("Least greedy player: " + Statistics.Aggregate((x, y) => x.Value.Telemetry.Count(v => v.T.Contains("LogItemPickup")) < y.Value.Telemetry.Count(v => v.T.Contains("LogItemPickup")) ? x : y).Key);

                Console.WriteLine("TeamKills - Number of times this player killed a teammate: {0}", Statistics.Aggregate((x, y) => x.Value.teamKills < y.Value.teamKills ? x : y).Key);
                Console.WriteLine("DamageDealt - Total damage dealt.Note: Self inflicted damage is subtracted: {0}", Statistics.Aggregate((x, y) => x.Value.damageDealt < y.Value.damageDealt ? x : y).Key);
                Console.WriteLine("DBNOs - Number of enemy players knocked: {0}", Statistics.Aggregate((x, y) => x.Value.DBNOs < y.Value.DBNOs ? x : y).Key);
                Console.WriteLine("Kills - Number of enemy players killed: {0}", Statistics.Aggregate((x, y) => x.Value.kills < y.Value.kills ? x : y).Key);
                Console.WriteLine("HeadshotKills - Number of enemy players killed with headshots: {0}", Statistics.Aggregate((x, y) => x.Value.headshotKills < y.Value.headshotKills ? x : y).Key);
                Console.WriteLine("KillPlace - This player's rank in the match based on kills: {0}", Statistics.Aggregate((x, y) => x.Value.killPlace < y.Value.killPlace ? x : y).Key);
                Console.WriteLine("WeaponsAcquired - Number of weapons picked up: {0}", Statistics.Aggregate((x, y) => x.Value.weaponsAcquired < y.Value.weaponsAcquired ? x : y).Key);
                Console.WriteLine("Assists - Number of enemy players this player damaged that were killed by teammates: {0}", Statistics.Aggregate((x, y) => x.Value.assists < y.Value.assists ? x : y).Key);
                Console.WriteLine("Revives - Number of times this player revived teammates: {0}", Statistics.Aggregate((x, y) => x.Value.revives < y.Value.revives ? x : y).Key);
                Console.WriteLine("WalkDistance - Total distance traveled on foot measured in meters: {0}", Statistics.Aggregate((x, y) => x.Value.walkDistance < y.Value.walkDistance ? x : y).Key);
                Console.WriteLine("TimeSurvived - Amount of time survived measured in seconds: {0}", Statistics.Aggregate((x, y) => x.Value.timeSurvived < y.Value.timeSurvived ? x : y).Key);
                Console.WriteLine("WinPlace - This player's placement in the match: {0}", Statistics.Aggregate((x, y) => x.Value.winPlace < y.Value.winPlace ? x : y).Key);
                Console.WriteLine();

                Console.ResetColor();
            }
            else
                Console.WriteLine("Failed player request.");

            Console.ReadLine();
        }

        private static Dictionary<string, string> TelemetryDataCache = new Dictionary<string, string>();
        private static TelemetryPackage[] GetTelemetryPackage(string url, out double length)
        {
            length = 0;
            try
            {
                if (TelemetryDataCache.ContainsKey(url))
                {
                    Console.WriteLine("Using Telemetry TelemetryDataCache");
                    length = TelemetryDataCache[url].Length;
                    return TelemetryPackage.FromJson(TelemetryDataCache[url]);
                }

                using (GZipWebClient wc = new GZipWebClient())
                {
                    wc.Headers.Add("Accept", "application/vnd.api+json");
                    wc.Headers.Add("Accept-Encoding", "gzip");

                    var str = wc.DownloadString(url);

                    length = str.Length;
                    TelemetryDataCache.Add(url, str);
                    var t = TelemetryPackage.FromJson(str);
                    return t;
                }
            }
            catch
            {
                return null;
            }
        }

        private static Dictionary<string, string> MatchCache = new Dictionary<string, string>();
        private static MatchRequest GetMatch(string id)
        {
            try
            {
                string url = string.Format(@"https://api.pubg.com/shards/pc-na/matches/{0}", id);

                if (MatchCache.ContainsKey(url))
                {
                    Console.WriteLine("Using Match Cache.");
                    return JsonConvert.DeserializeObject<MatchRequest>(MatchCache[url]);
                }

                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("Accept", "application/vnd.api+json");
                    wc.Headers.Add("Authorization", string.Format("Bearer {0}", key));

                    var str = wc.DownloadString(url);

                    MatchCache.Add(url, str);
                    return JsonConvert.DeserializeObject<MatchRequest>(str);
                }
            }
            catch
            {
                return null;
            }
        }

        private static PlayerRequest GetPlayerRequest(HashSet<string> username)
        {
            try
            {
                string url = string.Format(@"https://api.pubg.com/shards/pc-na/players?filter[playerNames]={0}", string.Join(",", username));
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("Accept", "application/vnd.api+json");
                    wc.Headers.Add("Authorization", string.Format("Bearer {0}", key));

                    var str = wc.DownloadString(url);
                    return JsonConvert.DeserializeObject<PlayerRequest>(str);
                }
            }
            catch
            {
                return null;
            }
        }

        private static Dictionary<string, string> TelemetryCache = new Dictionary<string, string>();
        private static TelemetryRequest GetTelemetry(string matchId)
        {
            try
            {
                string url = string.Format(@"https://api.pubg.com/shards/pc-na/matches/{0}", matchId);

                if (TelemetryCache.ContainsKey(url))
                {
                    Console.WriteLine("Using Telemetry Light Cache.");
                    return JsonConvert.DeserializeObject<TelemetryRequest>(TelemetryCache[url]);
                }

                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("Accept", "application/vnd.api+json");

                    var str = wc.DownloadString(url);

                    TelemetryCache.Add(url, str);
                    return JsonConvert.DeserializeObject<TelemetryRequest>(str);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
