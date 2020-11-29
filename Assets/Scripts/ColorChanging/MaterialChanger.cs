using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.ColorChanging.BlendModeChanger;

namespace Assets.Scripts.ColorChanging
{
    public abstract class MaterialChanger : MonoBehaviour {
        List<ColorInformation> originalMaterials = new List<ColorInformation>();

        private void Start() {
            SaveOriginalColors();
        }

        /// <summary>
        /// Saving original colours in map.
        /// </summary>
        public void SaveOriginalColors() {
            Renderer renderer = GetComponent<MeshRenderer>();
            if (renderer != null) {
                foreach (Material material in renderer.materials) {
                    originalMaterials.Add(new ColorInformation(material, material.color, this.name));
                }
            } else {
                for (int j = 0; j < transform.childCount; j++) { //when renderers are only inside children
                    Renderer render = transform.GetChild(j).GetComponent<MeshRenderer>();

                    if (render != null) {
                        foreach (Material material in render.materials) {
                            originalMaterials.Add(new ColorInformation(material, material.color, transform.GetChild(j).name));
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Changes materials blend mode, transparency and color.
        /// </summary>
        /// <param name="blendMode">Changes blend mode to the given one.</param>
        /// <param name="transparency">If given, changes transparency of material. Should be used with transparent blend mode.</param>
        /// <param name="changeColor">Color of material. If not given, changes to original color. Used for highlighting. </param>
        public void ChangeMaterial(BlendMode blendMode, float? transparency = null, Color? changeColor = null) {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null) {
                ChangeMaterialInRenderer(renderer, blendMode, transparency, changeColor);
            } else {
                for (int j = 0; j < transform.childCount; j++) { //when renderers are only inside children
                    ChangeMaterialInRenderer(transform.GetChild(j).GetComponent<Renderer>(), blendMode, transparency, changeColor);
                }
            }
        }

        public void ChangeMaterial(Material material) {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null) {
                ChangeMaterialInRenderer(renderer, material);
            } else {
                for (int j = 0; j < transform.childCount; j++) { //when renderers are only inside children
                    ChangeMaterialInRenderer(transform.GetChild(j).GetComponent<Renderer>(), material);
                }
            }
        }

        void ChangeMaterialInRenderer(Renderer renderer, BlendMode blendMode, float? transparency = null, Color? changeColor = null) {
            if (renderer == null)
                return;

            for (int i = 0; i < renderer.materials.Length; i++) {
                BlendModeChanger.ChangeRenderMode(renderer.materials[i], blendMode);

                Color color;
                if (changeColor == null)
                    color = originalMaterials.Where(x => x.name.Equals(renderer.name)).First().color;
                else
                    color = (Color)changeColor;

                if (transparency != null) {
                    color.a = (float)transparency;
                }

                renderer.materials[i].SetColor("_Color", color);

            }
        }

        void ChangeMaterialInRenderer(Renderer renderer, Material material) {
            if (renderer == null)
                return;

            renderer.material = material;
        }

        public void ReturnFromHighlight() {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null) {
                for (int i = 0; i < renderer.materials.Length; i++) {
                    renderer.material = originalMaterials.Where(x => x.name.Equals(renderer.name)).First().material;
                }
            } else {
                for (int j = 0; j < transform.childCount; j++) { //when renderers are only inside children
                    renderer = transform.GetChild(j).GetComponent<Renderer>();
                    for (int i = 0; i < renderer.materials.Length; i++) {
                        renderer.material = originalMaterials.Where(x => x.name.Equals(renderer.name)).First().material;
                    }
                }
            }

        }
    }
}
