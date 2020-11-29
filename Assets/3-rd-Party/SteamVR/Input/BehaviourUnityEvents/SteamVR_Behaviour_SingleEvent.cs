//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System;
using UnityEngine.Events;

namespace Assets.SteamVR.Input.BehaviourUnityEvents
{
    [Serializable]
    public class SteamVR_Behaviour_SingleEvent : UnityEvent<SteamVR_Behaviour_Single, SteamVR_Input_Sources, float, float> { }
}