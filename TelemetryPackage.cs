using System;
using System.Globalization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PG
{
    public partial class TelemetryPackage
    {
        [JsonProperty("MatchId", NullValueHandling = NullValueHandling.Ignore)]
        public string MatchId { get; set; }

        [JsonProperty("PingQuality", NullValueHandling = NullValueHandling.Ignore)]
        public string PingQuality { get; set; }

        [JsonProperty("SeasonState", NullValueHandling = NullValueHandling.Ignore)]
        public string SeasonState { get; set; }

        [JsonProperty("_D")]
        public DateTimeOffset D { get; set; }

        [JsonProperty("_T")]
        public string T { get; set; }

        [JsonProperty("accountId", NullValueHandling = NullValueHandling.Ignore)]
        public string AccountId { get; set; }

        [JsonProperty("common", NullValueHandling = NullValueHandling.Ignore)]
        public Common Common { get; set; }

        [JsonProperty("character", NullValueHandling = NullValueHandling.Ignore)]
        public Character Character { get; set; }

        [JsonProperty("item", NullValueHandling = NullValueHandling.Ignore)]
        public ChildItem Item { get; set; }

        [JsonProperty("vehicle")]
        public Vehicle Vehicle { get; set; }

        [JsonProperty("elapsedTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? ElapsedTime { get; set; }

        [JsonProperty("numAlivePlayers", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumAlivePlayers { get; set; }

        [JsonProperty("attackId", NullValueHandling = NullValueHandling.Ignore)]
        public long? AttackId { get; set; }

        [JsonProperty("fireWeaponStackCount", NullValueHandling = NullValueHandling.Ignore)]
        public long? FireWeaponStackCount { get; set; }

        [JsonProperty("attacker")]
        public Character Attacker { get; set; }

        [JsonProperty("attackType", NullValueHandling = NullValueHandling.Ignore)]
        public string AttackType { get; set; }

        [JsonProperty("weapon", NullValueHandling = NullValueHandling.Ignore)]
        public ChildItem Weapon { get; set; }

        [JsonProperty("swimDistance", NullValueHandling = NullValueHandling.Ignore)]
        public double? SwimDistance { get; set; }

        [JsonProperty("seatIndex", NullValueHandling = NullValueHandling.Ignore)]
        public long? SeatIndex { get; set; }

        [JsonProperty("mapName", NullValueHandling = NullValueHandling.Ignore)]
        public string MapName { get; set; }

        [JsonProperty("weatherId", NullValueHandling = NullValueHandling.Ignore)]
        public string WeatherId { get; set; }

        [JsonProperty("characters", NullValueHandling = NullValueHandling.Ignore)]
        public Character[] Characters { get; set; }

        [JsonProperty("cameraViewBehaviour", NullValueHandling = NullValueHandling.Ignore)]
        public string CameraViewBehaviour { get; set; }

        [JsonProperty("teamSize", NullValueHandling = NullValueHandling.Ignore)]
        public long? TeamSize { get; set; }

        [JsonProperty("isCustomGame", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsCustomGame { get; set; }

        [JsonProperty("isEventMode", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEventMode { get; set; }

        [JsonProperty("blueZoneCustomOptions", NullValueHandling = NullValueHandling.Ignore)]
        public string BlueZoneCustomOptions { get; set; }

        [JsonProperty("gameState", NullValueHandling = NullValueHandling.Ignore)]
        public GameState GameState { get; set; }

        [JsonProperty("rideDistance", NullValueHandling = NullValueHandling.Ignore)]
        public double? RideDistance { get; set; }

        [JsonProperty("damageTypeCategory", NullValueHandling = NullValueHandling.Ignore)]
        public string DamageTypeCategory { get; set; }

        [JsonProperty("damageCauserName", NullValueHandling = NullValueHandling.Ignore)]
        public string DamageCauserName { get; set; }

        [JsonProperty("distance", NullValueHandling = NullValueHandling.Ignore)]
        public double? Distance { get; set; }

        [JsonProperty("parentItem", NullValueHandling = NullValueHandling.Ignore)]
        public ChildItem ParentItem { get; set; }

        [JsonProperty("childItem", NullValueHandling = NullValueHandling.Ignore)]
        public ChildItem ChildItem { get; set; }

        [JsonProperty("victim", NullValueHandling = NullValueHandling.Ignore)]
        public Character Victim { get; set; }

        [JsonProperty("damageReason", NullValueHandling = NullValueHandling.Ignore)]
        public string DamageReason { get; set; }

        [JsonProperty("damage", NullValueHandling = NullValueHandling.Ignore)]
        public double? Damage { get; set; }

        [JsonProperty("isAttackerInVehicle", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsAttackerInVehicle { get; set; }

        [JsonProperty("dBNOId", NullValueHandling = NullValueHandling.Ignore)]
        public long? DBnoId { get; set; }

        [JsonProperty("killer", NullValueHandling = NullValueHandling.Ignore)]
        public Character Killer { get; set; }

        [JsonProperty("reviver", NullValueHandling = NullValueHandling.Ignore)]
        public Character Reviver { get; set; }

        [JsonProperty("itemPackage", NullValueHandling = NullValueHandling.Ignore)]
        public ItemPackage ItemPackage { get; set; }
    }

    public partial class Character
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("teamId")]
        public long TeamId { get; set; }

        [JsonProperty("health")]
        public double Health { get; set; }

        [JsonProperty("location")]
        public Tion Location { get; set; }

        [JsonProperty("ranking")]
        public long Ranking { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }
    }

    public partial class Tion
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }
    }

    public partial class ChildItem
    {
        [JsonProperty("itemId")]
        public string ItemId { get; set; }

        [JsonProperty("stackCount")]
        public long StackCount { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("subCategory")]
        public string SubCategory { get; set; }

        [JsonProperty("attachedItems")]
        public string[] AttachedItems { get; set; }
    }

    public partial class Common
    {
        [JsonProperty("isGame")]
        public double IsGame { get; set; }
    }

    public partial class GameState
    {
        [JsonProperty("elapsedTime")]
        public long ElapsedTime { get; set; }

        [JsonProperty("numAliveTeams")]
        public long NumAliveTeams { get; set; }

        [JsonProperty("numJoinPlayers")]
        public long NumJoinPlayers { get; set; }

        [JsonProperty("numStartPlayers")]
        public long NumStartPlayers { get; set; }

        [JsonProperty("numAlivePlayers")]
        public long NumAlivePlayers { get; set; }

        [JsonProperty("safetyZonePosition")]
        public Tion SafetyZonePosition { get; set; }

        [JsonProperty("safetyZoneRadius")]
        public double SafetyZoneRadius { get; set; }

        [JsonProperty("poisonGasWarningPosition")]
        public Tion PoisonGasWarningPosition { get; set; }

        [JsonProperty("poisonGasWarningRadius")]
        public double PoisonGasWarningRadius { get; set; }

        [JsonProperty("redZonePosition")]
        public Tion RedZonePosition { get; set; }

        [JsonProperty("redZoneRadius")]
        public double RedZoneRadius { get; set; }
    }

    public partial class ItemPackage
    {
        [JsonProperty("itemPackageId")]
        public string ItemPackageId { get; set; }

        [JsonProperty("location")]
        public Tion Location { get; set; }

        [JsonProperty("items")]
        public ChildItem[] Items { get; set; }
    }

    public partial class Vehicle
    {
        [JsonProperty("vehicleType")]
        public string VehicleType { get; set; }

        [JsonProperty("vehicleId")]
        public string VehicleId { get; set; }

        [JsonProperty("healthPercent")]
        public double HealthPercent { get; set; }

        [JsonProperty("feulPercent")]
        public double FeulPercent { get; set; }
    }

    public partial class TelemetryPackage
    {
        public static TelemetryPackage[] FromJson(string json) => JsonConvert.DeserializeObject<TelemetryPackage[]>(json);
    }
}
