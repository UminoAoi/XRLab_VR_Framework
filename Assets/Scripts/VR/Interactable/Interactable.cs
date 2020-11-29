using UnityEngine;

namespace Assets.Scripts.VR.Interactable
{
    public abstract class Interactable : MonoBehaviour {
        public abstract void OnPointerEnter(HandManager handManager, PointerEventArgs args);
        public abstract void OnPointerExit(HandManager handManager, PointerEventArgs args);
        public abstract void OnInteraction(HandManager handManager, PointerEventArgs args);
    }
}
