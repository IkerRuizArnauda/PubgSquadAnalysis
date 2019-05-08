using System;
using System.Collections.Generic;

namespace PG
{
    public class PlayerAttributes
    {
        public string patchVersion { get; set; }
        public string name { get; set; }
        public object stats { get; set; }
        public string titleId { get; set; }
        public string shardId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class Matches
    {
        public List<Datum> data { get; set; }
    }

    public class PlayerRelationships
    {
        public Assets assets { get; set; }
        public Matches matches { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
        public string schema { get; set; }
    }

    public class Player
    {
        public string type { get; set; }
        public string id { get; set; }
        public PlayerAttributes attributes { get; set; }
        public PlayerRelationships relationships { get; set; }
        public Links links { get; set; }
    }

    public class Links2
    {
        public string self { get; set; }
    }

    public class Meta
    {
    }

    public class PlayerRequest
    {
        public List<Player> data { get; set; }
        public Links2 links { get; set; }
        public Meta meta { get; set; }
    }
}
