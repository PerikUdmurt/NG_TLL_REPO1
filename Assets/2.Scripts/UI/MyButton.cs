using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

namespace MyPetProject.UI
{
    [AddComponentMenu(("MyPetProject.UI/MyButton"))]
    public class MyButton : Selectable, IPointerClickHandler
    {
        private bool isReloading;

        [Serializable]
        public class ButtonClickedEvent<T> : UnityEvent { }

        protected MyButton() { }

        [FormerlySerializedAs("onClick")]
        [SerializeField]
        private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();
        public ButtonClickedEvent onClick
        {
            get { return m_OnClick; }
            set { m_OnClick = value; }
        }

        [SerializeField]
        private ButtonClickedEvent<string> m_OnClickError = new ButtonClickedEvent<string>();
        private void Press()
        {
            if (isReloading)
            {
                m_OnClickError.Invoke();
            }
            
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("Button.onClick", this);
            m_OnClick.Invoke();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();
        }

        public void ReloadButton(float time)
        {
            if (isReloading) return;
            StartCoroutine(Reload(5));
        }

        private IEnumerator Reload(float time)
        {
            isReloading = true;
            yield return new WaitForSeconds(time);
            isReloading = false;
        }
    }
}
