//----------------------------------------------
//           	   Highway Racer
//
// Copyright © 2014 - 2020 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// It must be attached to external camera. This external camera will be used as posture status.
/// </summary>
//[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Mirror")]
public class HR_PostureStatus : MonoBehaviour {

	private Camera cam;
	private HR_PlayerHandler player;

	public Vector3 transformOffset = new Vector3(0f, 1.5f, 20f);
	public Vector3 rotationOffset = new Vector3(3f, 180f, 0f);

	void OnEnable(){

		HR_PlayerHandler.OnPlayerSpawned += OnPlayerSpawned;

	}

	void InvertCamera () {

		cam = GetComponentInChildren<Camera>();
		cam.ResetWorldToCameraMatrix ();
		cam.ResetProjectionMatrix ();

	}

	void OnPlayerSpawned(HR_PlayerHandler _player){

		player = _player;

	}

	void OnPreRender () {
		GL.invertCulling = true;
	}

	void OnPostRender () {
		GL.invertCulling = false;
	}

	void Update(){

		if(!cam){
			InvertCamera();
			return;
		}

		cam.transform.position = player.transform.position + transformOffset;
		cam.transform.rotation = Quaternion.Euler (rotationOffset);

//		cam.enabled = carController.canControl;

	}

	void OnDisable(){

		HR_PlayerHandler.OnPlayerSpawned -= OnPlayerSpawned;

	}

}
