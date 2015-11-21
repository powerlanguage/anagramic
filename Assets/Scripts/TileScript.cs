using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	public GameObject letterMesh;
	public string letterString;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setLetter(string letter){
		letterMesh.GetComponent<TextMesh>().text = letter;
		letterString = letter;
	}
}
