using UnityEngine;

namespace Game.Combat
{
    public class Health
    {
        private float _value;

        public Health()
        {
            _value = 100f;
        }
        
        public bool IsAlive => _value > 0;

        public void IncreaseValue(float value) =>
            _value += value;

        public void DecreaseValue(float value)
        {
            Debug.Log(value);
            _value -= value;
        }
    }
}