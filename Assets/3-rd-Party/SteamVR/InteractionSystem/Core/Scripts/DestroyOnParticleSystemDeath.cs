﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Destroys this object when its particle system dies
//
//=============================================================================

using UnityEngine;

namespace Assets.SteamVR.InteractionSystem.Core.Scripts
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( ParticleSystem ) )]
	public class DestroyOnParticleSystemDeath : MonoBehaviour
	{
		private ParticleSystem particles;

		//-------------------------------------------------
		void Awake()
		{
			particles = GetComponent<ParticleSystem>();

			InvokeRepeating( "CheckParticleSystem", 0.1f, 0.1f );
		}


		//-------------------------------------------------
		private void CheckParticleSystem()
		{
			if ( !particles.IsAlive() )
			{
				Destroy( this.gameObject );
			}
		}
	}
}
