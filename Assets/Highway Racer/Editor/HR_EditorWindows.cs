//----------------------------------------------
//           	   Highway Racer
//
// Copyright © 2014 - 2020 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class HR_EditorWindows : Editor {

	[MenuItem("Highway Racer/General Settings", false, -100)]
	public static void OpenGeneralSettings(){
		Selection.activeObject =HR_HighwayRacerProperties.Instance;
	}

	[MenuItem("Highway Racer/Quick Switch To Desktop", false, -50)]
	public static void QuickSwitchToDesktop(){

		Selection.activeObject = RCC_Settings.Instance;
		RCC_Settings.Instance.toolbarSelectedIndex = 0;
		RCC_Settings.Instance.controllerType = RCC_Settings.ControllerType.Keyboard;
		EditorUtility.SetDirty (RCC_Settings.Instance);

		if(EditorUtility.DisplayDialog("Switched Build", "RCC Controller has been switched to Keyboard mode.", "Ok"))
			Selection.activeObject = HR_HighwayRacerProperties.Instance;

		EditorUtility.SetDirty (HR_HighwayRacerProperties.Instance);

	}

	[MenuItem("Highway Racer/Quick Switch To Mobile", false, -50)]
	public static void QuickSwitchToMobile(){

		Selection.activeObject = RCC_Settings.Instance;
		RCC_Settings.Instance.toolbarSelectedIndex = 1;
		RCC_Settings.Instance.controllerType = RCC_Settings.ControllerType.Mobile;
		EditorUtility.SetDirty (RCC_Settings.Instance);

		if (EditorUtility.DisplayDialog ("Switched Build", "RCC Controller has been switched to Mobile mode.", "Ok"))
			Selection.activeObject = HR_HighwayRacerProperties.Instance;

		EditorUtility.SetDirty (HR_HighwayRacerProperties.Instance);

	}

	[MenuItem("Highway Racer/Configure Player Cars", false, 1)]
	public static void OpenCarSettings(){
		Selection.activeObject =HR_PlayerCars.Instance;
	}

	[MenuItem("Highway Racer/Configure Upgradable Wheels", false, 1)]
	public static void OpenWheelsSettings(){
		Selection.activeObject =HR_Wheels.Instance;
	}

	[MenuItem("Highway Racer/PDF Documentation", false, 2)]
	public static void OpenDocs(){
		string url = "https://dl.dropboxusercontent.com/u/248930654/_Documentations/Highway%20Racer%20Complete%20Project.pdf";
		Application.OpenURL(url);
	}

	[MenuItem("Highway Racer/Highlight Traffic Cars Folder", false, 102)]
	public static void OpenTrafficCarsFolder(){
		Selection.activeObject = AssetDatabase.LoadMainAssetAtPath ("Assets/Highway Racer/Resources/TrafficCars");
	}

	[MenuItem("Highway Racer/Help", false, 1000)]
	static void Help(){

		EditorUtility.DisplayDialog("Contact", "Please include your invoice number while sending a contact form.", "Ok");

		string url = "http://www.bonecrackergames.com/contact/";
		Application.OpenURL (url);

	}

}
