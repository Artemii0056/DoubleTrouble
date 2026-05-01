namespace Game.Stats
{
    public class StatSetup
    {
        public StatSetup(StatType type, float value)
        {
            Type = type;
            Value = value;
        }

        public StatType Type { get; private set; }
        public float Value { get; private set; }

        public void Increase(float value)
        {
            if ( value <= 0)
                return;
            
            Value += value;
        }

        public void Decrease(float value)
        {
            
        }
    }
}