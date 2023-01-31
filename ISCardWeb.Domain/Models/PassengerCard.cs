

namespace ISCardsWeb.Domain.Models
{
    public class PassengerCard : BaseCard
    {
        public bool IsSent { get; set; }
        public bool WorkStopped { get; set; }
        public string NameOfOrganization { get; set; } = "";
        public string NumberOfAuto { get; set; } = "";
        public string TypeOfAuto { get; set; } = "";
        public bool EmergencyKit { get; set; }
        public bool MonitoringSystem { get; set; }
        public bool ForeignObjects { get; set; }
        public bool RoutePassport { get; set; }
        public bool BusPassport { get; set; }
        public bool SeatBeltsFastened { get; set; }
        public bool CargoFixed { get; set; }
        public bool SafeLaneChange { get; set; }
        public bool KeepingDistance { get; set; }
        public bool SpeedLimit { get; set; }
        public bool SafeBehavior { get; set; }
        public bool NoCell { get; set; }
        public bool ControlOfAuto { get; set; }
        public bool NotEat { get; set; }
        public bool UnderstandsRoadConditions { get; set; }
        public bool RoadSignRequirements { get; set; }
        public bool TimelyTurnOffTheLights { get; set; }
        public bool AttentionToPedestrians { get; set; }
        public bool GiveWay { get; set; }
        public bool AutoSafelyInReverse { get; set; }
        public bool HandbrakeUsing { get; set; }
        public bool RestRegime { get; set; }
        public bool Clear { get; set; }
        public bool Snow { get; set; }
        public bool Cloud { get; set; }
        public bool RainHail { get; set; }
        public bool Sun { get; set; }
        public bool Night { get; set; }
        public bool Rain { get; set; }
        public bool Dirt { get; set; }
        public bool Asphalt { get; set; }
        public bool Ice { get; set; }
        public bool Gravel { get; set; }
        public bool OffRoad { get; set; }
        public bool Ground { get; set; }
        public string PassengerComment { get; set; } = "";
        public string Actions { get; set; } = "";
    }
}
