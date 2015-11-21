using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileRackScript : MonoBehaviour {

	public int numSlots;
	public List<GameObject> rack;

	// Use this for initialization
	void Start () {
		rack = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Add Tile
	public void AddTile(GameObject tile){
		rack.Add(tile);
		tile.transform.SetParent(this.transform); //does this work?
	}
	
	//Remove Tile
	public void RemoveTile(){
		
	}

	//Return Rack contents as String
	public string getRackString(){
		string rackContents = "";
		foreach (GameObject tile in rack) {
			rackContents += tile.GetComponent<TileScript>().letterString;
		}
		return rackContents;
	}
}
