using UnityEngine;
using System.Collections;
using System.Linq;

public class GameScript : MonoBehaviour {

	public GameObject TileRackPrefab;
	public GameObject TilePrefab;
    public GameObject handTileRack;
	public GameObject playTileRack;
	public string answerWord;

	// Use this for initialization
	void Start () {
		string shuffledWord = ShuffleWord (answerWord);
		//create tiles & add to hand
		foreach (char c in shuffledWord) {
			GameObject newTile = (GameObject)Instantiate(TilePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			newTile.GetComponent<TileScript>().setLetter(c.ToString());
			handTileRack.GetComponent<TileRackScript>().AddTile(newTile);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//http://answers.unity3d.com/questions/16531/randomizing-arrays.html
	string ShuffleWord(string answerWord){
		char[] chars = answerWord.ToCharArray();

		for (var i = chars.Length - 1; i > 0; i--) {
			int r = Random.Range(0,i);
			char tmp = chars[i];
			chars[i] = chars[r];
			chars[r] = tmp;
		}

		return new string (chars);
	}
}
