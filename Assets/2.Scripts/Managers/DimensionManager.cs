using UnityEngine.Events;

namespace MyPetProject
{
    public sealed class DimensionManager
    {
        public enum Dimension
        {
            PeopleDimension, DemonDimension
        }
        private Dimension _currentDimension = Dimension.PeopleDimension;
        public Dimension CurrentDimension
        { 
            get { return _currentDimension; }
            set 
            {
                _currentDimension = value;
                DimensionSwetched?.Invoke(_currentDimension);
            }
        }
        public UnityEvent<Dimension> DimensionSwetched = new UnityEvent<Dimension>();
    }
}
