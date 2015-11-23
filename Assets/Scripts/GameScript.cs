using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;

public class GameScript : MonoBehaviour {

	public GameObject TileRackPrefab;
	public GameObject TilePrefab;
    public GameObject handTileRack;
	public GameObject playTileRack;
	public string[] answerWords;
	public string answerWord;
	public RackScript handScript;
	public RackScript playScript;

	// Use this for initialization
	void Start () {
		SetupGame ();
	}

	void SetupGame(){

		//Pick random word as answer
		//answerWord = answerWords[Random.Range (0, answerWords.Length)];

		//setup as a global thing
		answerWord = WordManager.wordManager.GetUnsolvedWord ();

		//Add slots to hand and play
		handScript.Setup (answerWord.Length);
		playScript.Setup (answerWord.Length);
		
		string shuffledWord = ShuffleWord (answerWord);
		//create tiles & add to hand
		foreach (char c in shuffledWord) {
			GameObject newTile = (GameObject)Instantiate(TilePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			newTile.GetComponent<TileScript>().setLetter(c.ToString());
			newTile.tag = "tile";
			newTile.name = c.ToString();
			handScript.AddTile(newTile);
		}
	}

	void Update(){
		//Check to see if object has been clicked on
		if (Input.GetMouseButtonDown (0)) {
			ScreenMouseRay();
		}

		if (Input.GetKey("space")) {
			WordManager.wordManager.Clear();
		}

	}

	//Simple Gui to help resetting.  Doesn't actually seem to work on mobile
	void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 100, 30), "Reset")) {
			WordManager.wordManager.Clear();
		}
	}

	//Cast ray from screen to mouse point
	void ScreenMouseRay(){
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if(hit.collider != null) {
			Debug.Log ("Ray hit " + hit.collider.gameObject.name);
			ObjectClicked(hit.collider.gameObject);
		}
    }

	void ObjectClicked(GameObject target){
		if (target.tag == "tile") {
			//tile is in hand
			if(handScript.containsTile(target)){
				handScript.RemoveTile(target);
				playScript.AddTile(target);

				//because we've added a tile to the play rack, we should check if the game is over
				EvaluateGame();

			} else {
				//tile is in play area
				playScript.RemoveTile(target);
				handScript.AddTile(target);
			}
		}
	}

	//Checks to see if the game has been won
	void EvaluateGame(){
		//Check if play rack contains any empty spots
		if(ArrayUtility.Contains(playScript.rack, null) == false ){
			//If not, check if the tiles equal the answer
			if (playScript.GetRackString() == answerWord) {
				GameWon();
			}
		}
	}

	//Runs when the game is won
	void GameWon(){
		WordManager.wordManager.MarkWordAsSolved (answerWord);
		WordManager.wordManager.Save ();
		handScript.ClearRack ();
		playScript.ClearRack ();
		SetupGame ();
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
