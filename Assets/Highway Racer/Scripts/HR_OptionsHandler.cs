//----------------------------------------------
//           	   Highway Racer
//
// Copyright © 2014 - 2020 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class HR_OptionsHandler : MonoBehaviour {

	public GameObject pausedMenu;
	public GameObject pausedButtons;
	public GameObject optionsMenu;
	public GameObject optionsMenu_PP;

	void OnEnable(){

		HR_GamePlayHandler.OnPaused += OnPaused;
		HR_GamePlayHandler.OnResumed += OnResumed;

	}

	public void ResumeGame () {
		
		HR_GamePlayHandler.Instance.Paused();
		
	}

	public void RestartGame () {

		HR_GamePlayHandler.Instance.RestartGame();
	
	}

	public void MainMenu () {
		
		HR_GamePlayHandler.Instance.MainMenu();
		
	}

	public void OptionsMenu (bool open) {

		optionsMenu.SetActive (open);

		if (open)
			pausedButtons.SetActive (false);
		else
			pausedButtons.SetActive (true);

	}

	void OnPaused () {

		pausedMenu.SetActive(true);
		pausedButtons.SetActive(true);

		AudioListener.pause = true;
		Time.timeScale = 0;

		
	}

	public void OnResumed () {

		pausedMenu.SetActive(false);
		pausedButtons.SetActive(false);

		AudioListener.pause = false;
		Time.timeScale = 1;

	}

	public void ChangeCamera(){

		if (GameObject.FindObjectOfType<HR_CarCamera> ())
			GameObject.FindObjectOfType<HR_CarCamera> ().ChangeCamera ();

	}

	void OnDisable(){

		HR_GamePlayHandler.OnPaused -= OnPaused;
		HR_GamePlayHandler.OnResumed -= OnResumed;

	}

}
