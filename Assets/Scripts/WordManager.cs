using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Weird global object that references itself to be allowed to be a global?

public class WordManager : MonoBehaviour {

	public static WordManager wordManager;

	private Hashtable unsolvedWords;
	private Hashtable solvedWords;

	void Awake(){

		//Load saved word arrays
		Load ();

		//If there is no unsolved word list, then this is the first time
		//the script is being run, add some
		if (unsolvedWords == null) {
			unsolvedWords = new Hashtable ();
			unsolvedWords.Add ("CRUNK", "CRUNK");
			unsolvedWords.Add ("FLAT", "FLAT");
		}

		solvedWords = new Hashtable ();

		if (wordManager == null) {
			wordManager = this;
		}
	}


	public string GetUnsolvedWord(){

		ICollection keys = unsolvedWords.Keys;

		string [] keyArray = new string[keys.Count];
		keys.CopyTo(keyArray, 0);
		string randomKey = keyArray[UnityEngine.Random.Range(0, keys.Count)];

        return (string)unsolvedWords[randomKey];
	}

	public void MarkWordAsSolved(string word){
		unsolvedWords.Remove(word);
		solvedWords.Add (word, word);
	}

	//https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/persistence-data-saving-loading

	//Saves to local storage
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/wordInfo.dat");

		WordData data = new WordData ();
		data.unsolvedWords = unsolvedWords;
		data.solvedWords = solvedWords;

		bf.Serialize (file, data);
		file.Close ();
	}

	//Loads local storage
	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/wordInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/wordInfo.dat", FileMode.Open);
			WordData data = (WordData)bf.Deserialize(file);

			unsolvedWords = data.unsolvedWords;
			solvedWords = data.solvedWords;
		}
	}

	//Deletes local storage
	public void Clear(){
		if (File.Exists (Application.persistentDataPath + "/wordInfo.dat")) {
			File.Delete (Application.persistentDataPath + "/wordInfo.dat");
		}
	}
}

[Serializable]
class WordData{
	public Hashtable unsolvedWords;
	public Hashtable solvedWords;
}
