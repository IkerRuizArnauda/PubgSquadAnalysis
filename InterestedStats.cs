using System.Collections.Generic;

namespace PG
{
    public class InterestedStats
    {
        public double DBNOs; //Number of enemy players knocked
        public double walkDistance; //Total distance traveled on foot measured in meters
        public double revives; //Number of times this player revived teammates
        public double kills; //Number of enemy players killed
        public double damageDealt; //Total damage dealt.Note: Self inflicted damage is subtracted
        public double assists; //Number of enemy players this player damaged that were killed by teammates
        public double weaponsAcquired; //Number of weapons picked up
        public double timeSurvived; //Amount of time survived measured in seconds
        public double headshotKills; //Number of enemy players killed with headshots
        public double killPlace; //This player's rank in the match based on kills
        public double teamKills; //Number of times this player killed a teammate
        public double winPlace; //This player's placement in the match
        public List<TelemetryPackage> Telemetry = new List<TelemetryPackage>();
    }
}
