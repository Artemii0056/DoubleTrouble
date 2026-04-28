namespace Game.Core.IdServices
{
    public class GlobalServiceId : IGlobalServiceId
    {
        private int _id;

        public int Next() => 
            _id++;
    }
}