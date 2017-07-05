using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour {

	public GameObject character;
	private SaveData data;

	void Start() {
		data = new SaveData ();

		Load ();
	}

	void Update() {
		Save ();
	}

	public void Save() {
		// check gamedata have savedata .if not, make it 
		if (!Directory.Exists (Application.dataPath + "/saves"))
			Directory.CreateDirectory (Application.dataPath + "/saves");

		BinaryFormatter bf = new BinaryFormatter ();

		FileStream file = File.Create(Application.dataPath + "/saves/SaveData.dat");

		DataSaving ();

		bf.Serialize (file, data);
		file.Close ();

	}

	public void DataSaving() {
		data.characterAtk = character.GetComponent<CharacterController> ().GetPAtk ();
		data.characterHealth = character.GetComponent<CharacterController> ().GetMaxHealth ();
		data.characterEXP = character.GetComponent<CharacterController> ().GetEXP ();
		data.characterLevel = character.GetComponent<CharacterController> ().GetLevel ();
	}

	public void Load() {
		// can Load if have savedata only
		if (File.Exists(Application.dataPath + "/saves/SaveData.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.dataPath + "/saves/SaveData.dat", FileMode.Open);
			data = (SaveData) bf.Deserialize(file);

			DataLoading();
			file.Close();
		}
	}

	public void DataLoading() {
		character.GetComponent<CharacterController> ().SetPAtk (data.characterAtk);
		character.GetComponent<CharacterController> ().SetMaxHealth (data.characterHealth);
		character.GetComponent<CharacterController> ().SetEXP (data.characterEXP);
		character.GetComponent<CharacterController> ().SetLevel (data.characterLevel);
	}
}

[System.Serializable]
public class SaveData {
	public float characterAtk, characterHealth, characterEXP;
	public int characterLevel;
}