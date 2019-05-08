using System;
using System.Collections.Generic;

namespace PG
{
    public class MatchAttributes
    {
        public int duration { get; set; }
        public object tags { get; set; }
        public bool isCustomMatch { get; set; }
        public string shardId { get; set; }
        public string mapName { get; set; }
        public string seasonState { get; set; }
        public DateTime createdAt { get; set; }
        public object stats { get; set; }
        public string gameMode { get; set; }  
        public string titleId { get; set; }
    }

    public class Rosters
    {
        public List<Player> data { get; set; }
    }

    public class Datum
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Matchrelationships
    {
        public Rosters rosters { get; set; }
        public Assets assets { get; set; }
    }


    public class Data
    {
        public string type { get; set; }
        public string id { get; set; }
        public MatchAttributes attributes { get; set; }
        public Matchrelationships relationships { get; set; }
        public Links links { get; set; }
    }

    public class Stats
    {
        public int? DBNOs { get; set; }
        public int? assists { get; set; }
        public int? boosts { get; set; }
        public double? damageDealt { get; set; }
        public string deathType { get; set; }
        public int? headshotKills { get; set; }
        public int? heals { get; set; }
        public int? killPlace { get; set; }
        public int? killPoints { get; set; }
        public int? killPointsDelta { get; set; }
        public int? killStreaks { get; set; }
        public int? kills { get; set; }
        public int? lastKillPoints { get; set; }
        public int? lastWinPoints { get; set; }
        public double? longestKill { get; set; }
        public int? mostDamage { get; set; }
        public string name { get; set; }
        public string playerId { get; set; }
        public int? rankPoints { get; set; }
        public int? revives { get; set; }
        public double? rideDistance { get; set; }
        public int? roadKills { get; set; }
        public double? swimDistance { get; set; }
        public int? teamKills { get; set; }
        public double? timeSurvived { get; set; }
        public int? vehicleDestroys { get; set; }
        public double? walkDistance { get; set; }
        public int? weaponsAcquired { get; set; }
        public int? winPlace { get; set; }
        public int? winPoints { get; set; }
        public int? winPointsDelta { get; set; }
        public int? rank { get; set; }
        public int? teamId { get; set; }
    }

    public class Attributes2
    {
        public Stats stats { get; set; }
        public string actor { get; set; }
        public string shardId { get; set; }
        public string won { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime? createdAt { get; set; }
        public string URL { get; set; }
    }

    public class Team
    {
        public object data { get; set; }
    }

    public class Datum3
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Participants
    {
        public List<Datum3> data { get; set; }
    }

    public class Relationships2
    {
        public Team team { get; set; }
        public Participants participants { get; set; }
    }

    public class Included
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes2 attributes { get; set; }
        public Relationships2 relationships { get; set; }
    }

    public class MatchRequest
    {
        public Data data { get; set; }
        public List<Included> included { get; set; }
        public Links2 links { get; set; }
        public Meta meta { get; set; }
    }
}
