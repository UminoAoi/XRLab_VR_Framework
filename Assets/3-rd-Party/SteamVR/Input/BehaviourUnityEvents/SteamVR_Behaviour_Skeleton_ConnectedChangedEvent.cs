﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System;
using UnityEngine.Events;

namespace Assets.SteamVR.Input.BehaviourUnityEvents
{
    [Serializable]
    public class SteamVR_Behaviour_Skeleton_ConnectedChangedEvent : UnityEvent<SteamVR_Behaviour_Skeleton, SteamVR_Input_Sources, bool> { }
}