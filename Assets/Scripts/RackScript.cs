using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class RackScript : MonoBehaviour {

	public int numSlots;
	public GameObject[] rack; 
	public float spacer = 1;
	public float slotWidth = 2;

	public void Setup(int slots){
		rack = new GameObject[slots];
	}

	//Add Tile
	public void AddTile(GameObject tile){

		//check if tile is already in this array?

		for (int i = 0; i < rack.Length; i++) {
			//loop until we find a null spot in array, then add tile there
			if (rack[i] == null) {
				rack[i] = tile;
				UpdateTilePosition(tile, i);
				//We're done adding the tile, so stop the loop
				break;
			}
		}
	}
	
	//Remove Tile
	public void RemoveTile(GameObject tile){
		//check if tile is already in this array?
		int i = ArrayUtility.IndexOf (rack, tile);
		rack [i] = null;
	}

	public void ClearRack(){
		foreach (GameObject tile in rack) {
			Destroy(tile);
		}
		ArrayUtility.Clear(ref rack);
		//ArrayUtility.Clear<GameObject> (rack);
	}

	//Position the tile in relation to this rack, in the correct slot
	public void UpdateTilePosition(GameObject tile, int slot){

		//Remove parent rack before performing transforms
		tile.transform.SetParent(null);

		//Tiles are positioned relative to their parent rack
		Vector3 tilePosition = new Vector3 (slot * slotWidth, 0, 0 );

		tile.transform.position = tilePosition;
		//Don't keep world position when setting parent rack
		tile.transform.SetParent (this.transform, false);
		
		Debug.Log (tile.name + " added at slot " + slot + " local x:" + tile.transform.localPosition.x);
	}

	public bool containsTile(GameObject tile){
		return ArrayUtility.Contains (rack, tile);
	}

	//Return Rack contents as String
	public string GetRackString(){
		string rackContents = "";
		foreach (GameObject tile in rack) {
			rackContents += tile.GetComponent<TileScript>().letterString;
		}
		return rackContents;
	}
}
