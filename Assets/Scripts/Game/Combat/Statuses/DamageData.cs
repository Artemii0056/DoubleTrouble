namespace Game.Combat.Statuses
{
    public readonly struct DamageData
    {
        public readonly int SourceId;
        public readonly float Amount;
        public readonly DamageType Type;

        public DamageData(int sourceId, float amount, DamageType type)
        {
            SourceId = sourceId;
            Amount = amount;
            Type = type;
        }
    }
}