//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using Assets.SteamVR.InteractionSystem.Core.Scripts;
using UnityEngine;

namespace Assets.SteamVR.InteractionSystem.Samples.Scripts
{
    public class ButtonEffect : MonoBehaviour
    {
        public void OnButtonDown(Hand fromHand)
        {
            ColorSelf(Color.cyan);
            fromHand.TriggerHapticPulse(1000);
        }

        public void OnButtonUp(Hand fromHand)
        {
            ColorSelf(Color.white);
        }

        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }
    }
}