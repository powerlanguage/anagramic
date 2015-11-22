using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileRackScript : MonoBehaviour {

	public int numSlots;
	public List<GameObject> tiles;
	public GameObject[] rack; 
	public float spacer = 1;
	public float tileWidth = 2;

	// Use this for initialization
	void Start () {
		tiles = new List<GameObject>();
		rack = new GameObject[6]; //needs to be passed at creation time?
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Add Tile
	public void AddTile(GameObject tile){
		tiles.Add(tile);
		tile.transform.SetParent(this.transform, false);
		Vector3 tilePosition = new Vector3 (
			tile.transform.position.x + tiles.Count * tileWidth, 
			tile.transform.position.y, 
			tile.transform.position.z
		);
		tile.transform.position = tilePosition;
		//if not first tile, add spacer to position
		if (tiles.Count != 0) {
			//tile.transform.position.x += spacer;
		}
	}
	
	//Remove Tile
	public void RemoveTile(){
		
	}

	//Return Rack contents as String
	public string getRackString(){
		string rackContents = "";
		foreach (GameObject tile in tiles) {
			rackContents += tile.GetComponent<TileScript>().letterString;
		}
		return rackContents;
	}
}
