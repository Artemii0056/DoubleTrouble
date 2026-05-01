using System;

namespace Game.Stats
{
    [Serializable]
    public class StatSetup
    {
        public StatType Type { get; private set; }
        public float Value { get; private set; }
    }
}