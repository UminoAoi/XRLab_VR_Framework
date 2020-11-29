//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Provides a haptic bump when colliding with balloons
//
//=============================================================================

using Assets.SteamVR.InteractionSystem.Core.Scripts;
using UnityEngine;

namespace Assets.SteamVR.InteractionSystem.Longbow.Scripts
{
	//-------------------------------------------------------------------------
	public class BalloonHapticBump : MonoBehaviour
	{
		public GameObject physParent;

		//-------------------------------------------------
		void OnCollisionEnter( Collision other )
		{
			Balloon contactBalloon = other.collider.GetComponentInParent<Balloon>();
			if ( contactBalloon != null )
			{
				Hand hand = physParent.GetComponentInParent<Hand>();
				if ( hand != null )
				{
					hand.TriggerHapticPulse( 500 );
				}
			}
		}
	}
}
