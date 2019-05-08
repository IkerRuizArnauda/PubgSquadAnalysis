using System.Collections.Generic;

namespace PG
{
    public class TelemRosters
    {
        public List<Datum> data { get; set; }
    }

    public class Assets
    {
        public List<Datum> data { get; set; }
    }

    public class TelemRelationships
    {
        public TelemRosters rosters { get; set; }
        public Assets assets { get; set; }
    }

    public class TelemData
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes2 attributes { get; set; }
        public TelemRelationships relationships { get; set; }
        public Links links { get; set; }
    }

    public class TelemetryRequest
    {
        public TelemData data { get; set; }
        public List<Included> included { get; set; }
        public Links2 links { get; set; }
        public Meta meta { get; set; }
    }
}
