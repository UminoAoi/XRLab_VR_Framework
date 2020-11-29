//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System;
using Assets.SteamVR.Plugins;
using UnityEngine.Events;

namespace Assets.SteamVR.Input.BehaviourUnityEvents
{
    [Serializable]
    public class SteamVR_Behaviour_Pose_TrackingChangedEvent : UnityEvent<SteamVR_Behaviour_Pose, SteamVR_Input_Sources, ETrackingResult> { }
}