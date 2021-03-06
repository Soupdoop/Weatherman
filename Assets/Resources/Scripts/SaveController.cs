﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class SaveController : MonoBehaviour {
	//on enable on disable for autosave

	public static SaveController s_instance;
	WeatherController WC;
	public Slider control;

	public bool first;

	// Use this for initialization
	void Awake()
	{
		if (s_instance == null)
		{
			DontDestroyOnLoad(gameObject); // save object on scene mvm
			s_instance = this;
		}
		else if (s_instance != this)
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		WC = this.gameObject.GetComponent<WeatherController> () as WeatherController;
		first = true;
	}

	void Update(){
		if (first) {
			first = false;
			Load ();
			this.gameObject.GetComponent<WeatherController> ().setInstruments (WC.days [WC.currentDay]);
			GameObject.Find ("WindowPane").GetComponent<WindowOpener> ().setExterior (WC.days [WC.currentDay]);
		}
	}

	void OnGUI()
	{

	}

	public void SaveAndQuit() {
		Save ();
		Application.Quit ();
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/saveInfo" + ".dat");
		SaveData data = WriteToData();
		bf.Serialize(file, data);
		file.Close();
		PlayerPrefs.SetInt ("saved", 1);
	}

	public void Load()
	{
		int saved = PlayerPrefs.GetInt ("saved", 0);
		if (saved != 0) {
			if (File.Exists (Application.persistentDataPath + "/saveInfo" + ".dat")) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/saveInfo" + ".dat",
					                  FileMode.Open);
				SaveData data = (SaveData)bf.Deserialize (file);
				file.Close ();

				WriteFromData (data);
			}
		} else {
			//new game
		}
	}

	private void WriteFromData(SaveData data) //loading
	{
		//Health.GetComponent<Slider>().value = data.health;
		WC.daysPerSeason = data.daysPerSeason;
		WC.numberOfSeasons = data.numberOfSeasons;
		WC.currentDay = data.currentDay;
		//WC.devices = data.devices;
		WC.days = data.days;
		control.value = data.sliderPos;
		WC.predictions = data.predictions;
	}

	private SaveData WriteToData () //saving 
	{
		SaveData data = new SaveData();
		data.daysPerSeason = WC.daysPerSeason;
		data.numberOfSeasons = WC.numberOfSeasons;
		data.currentDay = WC.currentDay;
		//data.devices = WC.devices;
		data.days = WC.days;
		data.predictions = WC.predictions;
		data.sliderPos = control.value;
		return data;
	}
}

[Serializable]
class SaveData 
{
	public int daysPerSeason;
	public int numberOfSeasons;
	public int currentDay;
	//public Device[] devices;
	public Day[] days;
	public bool[][] predictions;
	public float sliderPos;
}
