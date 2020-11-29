//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using Assets.SteamVR.InteractionSystem.Core.Scripts;
using UnityEngine;

namespace Assets.SteamVR.InteractionSystem.Samples.Scripts
{
    public class RenderModelChangerUI : UIElement
    {
        public GameObject leftPrefab;
        public GameObject rightPrefab;

        protected SkeletonUIOptions ui;

        protected override void Awake()
        {
            base.Awake();

            ui = this.GetComponentInParent<SkeletonUIOptions>();
        }

        protected override void OnButtonClick()
        {
            base.OnButtonClick();

            if (ui != null)
            {
                ui.SetRenderModel(this);
            }
        }
    }
}