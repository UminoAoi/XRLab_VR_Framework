﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Custom Unity Events that take in additional parameters
//
//=============================================================================

using UnityEngine.Events;

namespace Assets.SteamVR.InteractionSystem.Core.Scripts
{
	//-------------------------------------------------------------------------
	public static class CustomEvents
	{
		//-------------------------------------------------
		[System.Serializable]
		public class UnityEventSingleFloat : UnityEvent<float>
		{
		}


		//-------------------------------------------------
		[System.Serializable]
		public class UnityEventHand : UnityEvent<Hand>
		{
		}
	}
}
