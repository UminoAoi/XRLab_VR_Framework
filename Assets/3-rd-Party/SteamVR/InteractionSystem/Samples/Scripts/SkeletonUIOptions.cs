//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using Assets.SteamVR.Input;
using Assets.SteamVR.InteractionSystem.Core.Scripts;
using Assets.SteamVR.Plugins;
using UnityEngine;

namespace Assets.SteamVR.InteractionSystem.Samples.Scripts
{
    public class SkeletonUIOptions : MonoBehaviour
    {

        public void AnimateHandWithController()
        {
            for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
            {
                Hand hand = Player.instance.hands[handIndex];
                if (hand != null)
                {
                    hand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithController);
                }
            }
        }

        public void AnimateHandWithoutController()
        {
            for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
            {
                Hand hand = Player.instance.hands[handIndex];
                if (hand != null)
                {
                    hand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithoutController);
                }
            }
        }

        public void ShowController()
        {
            for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
            {
                Hand hand = Player.instance.hands[handIndex];
                if (hand != null)
                {
                    hand.ShowController(true);
                }
            }
        }

        public void SetRenderModel(RenderModelChangerUI prefabs)
        {
            for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
            {
                Hand hand = Player.instance.hands[handIndex];
                if (hand != null)
                {
                    if (hand.handType == SteamVR_Input_Sources.RightHand)
                        hand.SetRenderModel(prefabs.rightPrefab);
                    if (hand.handType == SteamVR_Input_Sources.LeftHand)
                        hand.SetRenderModel(prefabs.leftPrefab);
                }
            }
        }

        public void HideController()
        {
            for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
            {
                Hand hand = Player.instance.hands[handIndex];
                if (hand != null)
                {
                    hand.HideController(true);
                }
            }
        }
    }
}