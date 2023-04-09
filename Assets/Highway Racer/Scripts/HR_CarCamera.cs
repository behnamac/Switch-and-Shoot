//----------------------------------------------
//           	   Highway Racer
//
// Copyright © 2014 - 2020 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class HR_CarCamera : MonoBehaviour{

	public CameraMode cameraMode;
	public enum CameraMode{Top, TPS, FPS}

	private GameObject audioListener;

	internal int cameraSwitchCount = 0;

	private RCC_HoodCamera hoodCam;
	private RCC_WheelCamera tpsCam;

	private float targetFieldOfView = 50f;
	public float topFOV = 48f;
	public float tpsFOV = 55f;
	public float fpsFOV = 65f;

	// The target we are following
	public Transform playerCar;
	private Rigidbody playerRigid;
	public bool gameover = false;

	private Camera cam;
	private Vector3 targetPosition = new Vector3(0, 0, 50);
	private Vector3 pastFollowerPosition, pastTargetPosition;

	// The distance in the x-z plane to the target
	public float distance = 8f;
	
	// the height we want the camera to be above the target
	public float height = 8.5f;

	private float rotation = 30f;

	private float currentT;
	private float oldT;

	private float speed = 0f;
	private float maxShakeAmount = 0.00025f;

	public GameObject mirrors;
	
	void Start(){

		cam = GetComponent<Camera>();

		transform.position = new Vector3(2f, 1f, 55f);
		transform.rotation = Quaternion.Euler(new Vector3(0f, -40f, 0f));

		if(GetComponent<AudioListener>())
			Destroy(GetComponent<AudioListener>());

		audioListener = new GameObject("Audio Listener");
		audioListener.transform.SetParent(transform, false);
		audioListener.AddComponent<AudioListener>();
		
	}

	void OnEnable(){

		HR_PlayerHandler.OnPlayerSpawned += OnPlayerSpawned;
		HR_PlayerHandler.OnPlayerDied += OnPlayerCrashed;

	}

	void OnPlayerSpawned (HR_PlayerHandler player){

		playerCar = player.transform;
		playerRigid = player.GetComponent<Rigidbody>();
		hoodCam = player.GetComponentInChildren<RCC_HoodCamera>();
		tpsCam = player.GetComponentInChildren<RCC_WheelCamera>();

		if(GameObject.Find ("Mirrors"))
			mirrors = GameObject.Find ("Mirrors").gameObject;

	}

	void OnPlayerCrashed (HR_PlayerHandler player){

		gameover = true;

	}
	
	void LateUpdate(){

		if (!playerCar){
			playerCar = GameObject.FindObjectOfType<RCC_CarControllerV3>().transform;
			playerRigid = playerCar.GetComponent<Rigidbody>();
			return;
		}
		
		if(playerRigid != playerCar.GetComponent<Rigidbody>())
			playerRigid = playerCar.GetComponent<Rigidbody>();

		if(!cam)
			cam = GetComponent<Camera>();

		if (!playerCar || !playerRigid || Time.timeSinceLevelLoad < 1.5f) {
			transform.position += Quaternion.identity * Vector3.forward * (Time.deltaTime * 3f);
		} else if (playerCar && playerRigid) {

			//targetPosition = playerCar.position;
			cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, targetFieldOfView, Time.deltaTime * 3f);

			if (!gameover) {

				switch (cameraMode) {

				case CameraMode.Top:
					TOP ();
					if(mirrors)
						mirrors.SetActive (false);
					break;

				case CameraMode.TPS:
					if (tpsCam) {
						TPS ();
					} else {
						cameraSwitchCount++;
						ChangeCamera ();
					}
					break;

				case CameraMode.FPS:
					if (hoodCam) {
						FPS ();
						if(mirrors)
							mirrors.SetActive (true);
					} else {
						cameraSwitchCount++;
						ChangeCamera ();
					}
					break;

				}

				if(HR_HighwayRacerProperties.Instance._shakeCamera)
					targetPosition += (Random.insideUnitSphere * speed * maxShakeAmount);

			} else {

				if (Time.timeScale >= 1)
					transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + Mathf.Clamp (currentT, 0f, Mathf.Infinity));

			}

			switch (cameraSwitchCount) {

			case 0:
				cameraMode = CameraMode.Top;
				break;
			case 1:
				cameraMode = CameraMode.TPS;
				break;
			case 2:
				cameraMode = CameraMode.FPS;
				break;

			}

		}

		audioListener.transform.position = new Vector3(playerCar.position.x, transform.position.y, transform.position.z);

		pastFollowerPosition = transform.position;
		pastTargetPosition = targetPosition;

		currentT = (transform.position.z - oldT);
		oldT = transform.position.z;

	}
		
	void Update(){

		if (Input.GetKeyDown (RCC_Settings.Instance.changeCameraKB))
			ChangeCamera ();

	}

	public void ChangeCamera(){

		cameraSwitchCount ++;

		if(cameraSwitchCount >= 3)
			cameraSwitchCount = 0;

	}

	void TOP(){

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation, 0f, 0f), Time.deltaTime * 2f);
//		Vector3 qwe = new Vector3 ();
		targetPosition = new Vector3(0f, playerCar.position.y, playerCar.position.z);
		targetPosition -= transform.rotation * Vector3.forward * distance;
//		targetPosition = Vector3.Lerp(targetPosition, new Vector3(targetPosition.x, height, targetPosition.z), 1 - Mathf.Exp( -200f * Time.deltaTime));
		targetPosition = new Vector3(targetPosition.x, height, targetPosition.z);
//		transform.position = SmoothApproach( pastFollowerPosition, pastTargetPosition, targetPosition, (speed / 3f) * Mathf.Clamp(Time.timeSinceLevelLoad - 1.5f, 0f, .85f) );

		if(Time.timeSinceLevelLoad < 3f)
			transform.position = SmoothApproach( pastFollowerPosition, pastTargetPosition, targetPosition, (speed / 2f) * Mathf.Clamp(Time.timeSinceLevelLoad - 1.5f , 0f, 10f) );
		else
			transform.position = targetPosition;

//		transform.position =  Vector3.Lerp(transform.position, targetPosition, 1 - Mathf.Exp( -20 * Time.deltaTime) );
		targetFieldOfView = topFOV;

	}

	void TPS(){

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation / 4f, 0f, 0f), Time.deltaTime * 2f);
		targetPosition = new Vector3(playerCar.position.x, tpsCam.transform.position.y, tpsCam.transform.position.z);
//		transform.position = SmoothApproach( pastFollowerPosition, pastTargetPosition, targetPosition, (speed / 3f) * Mathf.Clamp(Time.timeSinceLevelLoad - 1.5f, 0f, .85f) );
		transform.position = targetPosition;
		targetFieldOfView = tpsFOV;

	}

	void FPS(){

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 2f);

		if(HR_HighwayRacerProperties.Instance._tiltCamera)
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.InverseTransformDirection(playerRigid.velocity).x / 2f, -transform.InverseTransformDirection(playerRigid.velocity).x / 2f);
		
		targetPosition = hoodCam.transform.position;
		transform.position = targetPosition;
		targetFieldOfView = fpsFOV;

	}

	// Used for smooth position lerping.
	private Vector3 SmoothApproach(Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float delta){

		if(Time.timeScale == 0 || float.IsNaN(delta) || float.IsInfinity(delta) || delta == 0 || pastPosition == Vector3.zero || pastTargetPosition == Vector3.zero || targetPosition == Vector3.zero)
			return transform.position;

		float t = (Time.deltaTime * delta) + .00001f;
		Vector3 v = ( targetPosition - pastTargetPosition ) / t;
		Vector3 f = pastPosition - pastTargetPosition + v;
		Vector3 l = targetPosition - v + f * Mathf.Exp( -t );

		#if UNITY_2017_1_OR_NEWER
		if (l != Vector3.negativeInfinity && l != Vector3.positiveInfinity && l != Vector3.zero)
			return l;
		else
			return transform.position;
		#else
		return l;
		#endif

	}

	void FixedUpdate(){

		if(!playerRigid)
			return;

		speed = Mathf.Lerp(speed, (playerRigid.velocity.magnitude * 3.6f), Time.deltaTime * 1.5f);

	}

	void OnDisable(){

		HR_PlayerHandler.OnPlayerSpawned -= OnPlayerSpawned;
		HR_PlayerHandler.OnPlayerDied -= OnPlayerCrashed;

	}

}