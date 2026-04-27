namespace Game.Combat.Statuses
{
    public readonly struct StatusData
    {
        public StatusData(StatusType type, float power, float duration)
        {
            Type = type;
            Power = power;
            Duration = duration;
        }
    
        public readonly StatusType Type;
        public readonly float Power;
        public readonly float Duration;
    }
}