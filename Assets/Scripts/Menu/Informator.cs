using System.Collections.Generic;
using Assets.Scripts.ColorChanging;
using Assets.Scripts.VR;
using Assets.Scripts.VR.Interactable;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class Informator : Interactable {
        public List<GameObject> objectsToDisappear;
        public List<GameObject> objectsToShow;

        [TextArea(10,20)]
        public List<string> textForEachObject;

        bool isActive = false;

        HighlightChanger highlightChanger;

        private void Start() {
            highlightChanger = GetComponent<HighlightChanger>();
            HideObjects();
        }

        public override void OnInteraction(HandManager handManager, PointerEventArgs args) {
            highlightChanger.MakeDefaultState();
            MakeAction();
        }

        public override void OnPointerEnter(HandManager handManager, PointerEventArgs args) {
            highlightChanger.DoHighlight();
        }

        public override void OnPointerExit(HandManager handManager, PointerEventArgs args) {
            highlightChanger.MakeDefaultState();
        }

        void HideObjects() {
            objectsToDisappear.ForEach(x => x.SetActive(true));
            objectsToShow.ForEach(x => x.SetActive(false));
            isActive = false;
        }

        void ShowObjects() {
            objectsToDisappear.ForEach(x => x.SetActive(false));
            objectsToShow.ForEach(x => x.SetActive(true));

            for(int i = 0; i < objectsToShow.Count; i++) {
                TextMeshPro textMesh = objectsToShow[i].GetComponentInChildren<TextMeshPro>();

                if (textMesh == null || textForEachObject.Count <= i)
                    continue;

                textMesh.text = textForEachObject[i];
            }

            isActive = true;
        }

        void MakeAction() {
            if (isActive)
                HideObjects();
            else
                ShowObjects();
        }
    }
}
