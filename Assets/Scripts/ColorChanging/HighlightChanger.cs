using UnityEngine;

namespace Assets.Scripts.ColorChanging
{
    public class HighlightChanger : MaterialChanger
    {

        private Material grayMaterial;
        private Material highlightMaterial;

        public HighlightMode highlightMode;

        private void Start() {
            grayMaterial = Resources.Load<Material>("Materials/GrayMaterial");
            highlightMaterial = Resources.Load<Material>("Materials/HighlightMaterial");

            SaveOriginalColors();
            MakeDefaultState();
        }


        public void DoHighlight() {

            if (highlightMode == HighlightMode.HighlightColors) {
                ChangeMaterial(highlightMaterial);
            } else if (highlightMode == HighlightMode.OriginalColors)
                MakeDefaultState();
        }

        public void MakeDefaultState() {
            ReturnFromHighlight();
        }

        public enum HighlightMode {
            OriginalColors,
            HighlightColors
        }

    }
}
