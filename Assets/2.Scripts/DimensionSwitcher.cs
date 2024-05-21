using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace MyPetProject
{
    public class DimensionSwitcher : MonoBehaviour
    {
        public UnityEvent DSAttemptError;

        public FrameInput frameInput;
        public float cooldown;

        private DimensionManager _dimensionManager;
        private bool _canSwitchDim = true;
        [Inject]
        private void Construct(DimensionManager dimensionManager)
        {
            _dimensionManager = dimensionManager;
        }
        private void Update()
        {
            if (frameInput.DimensionSwitchPressed)
            {

            }
            if (frameInput.DimensionSwitchUpped && _canSwitchDim)
            {
                StartCoroutine(CooldownTimer(cooldown));
                SwitchDimension();
            }
            else if (frameInput.DimensionSwitchUpped && !_canSwitchDim)    DSAttemptError?.Invoke();
        }

        private void SwitchDimension()
        {
            if (_dimensionManager.CurrentDimension == DimensionManager.Dimension.PeopleDimension)
            {
                _dimensionManager.CurrentDimension = DimensionManager.Dimension.DemonDimension;
            }
            else _dimensionManager.CurrentDimension = DimensionManager.Dimension.PeopleDimension;
        }

        private void ShowAreaOfAnotherDim()
        {
            Debug.Log("ShowArea");
        }

        private void DimensionInfo(DimensionManager.Dimension currentDimension)
        {
            Debug.Log("Текущее измерение:" + currentDimension);
        }

        private IEnumerator CooldownTimer(float time)
        {
            _canSwitchDim = false;
            yield return new WaitForSeconds(time);
            _canSwitchDim = true;
        }
    }
}
