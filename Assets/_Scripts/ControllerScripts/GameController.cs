using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public static GameController instance;

	private TileScript[,] AllTArray = new TileScript[4,4];
	private List<TileScript[]> cols = new List<TileScript[]> ();
	private List<TileScript[]> rows = new List<TileScript[]> ();
	private List<TileScript> NullTiles = new List<TileScript>();

	[SerializeField]
	private Text gameOverScoreText;

	[SerializeField]
	private Text gameOverWonText;

	[SerializeField]
	private GameObject gameOverPanel;

	void Awake(){
		MakeInstance ();
	}

	void Start(){
		TileScript[] AllTilesArray = GameObject.FindObjectsOfType<TileScript> ();
		foreach (TileScript tile in AllTilesArray) {
			tile.Number = 0;
			AllTArray [tile.rowIndex, tile.colIndex] = tile;

			//	Add all tiles array to nullTiles
			NullTiles.Add (tile);
		}

		//	Columns
		cols.Add (new TileScript[]{AllTArray[0, 0], AllTArray[1, 0], AllTArray[2, 0], AllTArray[3, 0]});
		cols.Add (new TileScript[]{AllTArray[0, 1], AllTArray[1, 1], AllTArray[2, 1], AllTArray[3, 1]});
		cols.Add (new TileScript[]{AllTArray[0, 2], AllTArray[1, 2], AllTArray[2, 2], AllTArray[3, 2]});
		cols.Add (new TileScript[]{AllTArray[0, 3], AllTArray[1, 3], AllTArray[2, 3], AllTArray[3, 3]});

		//	Rows
		rows.Add (new TileScript[]{AllTArray[0, 0], AllTArray[0, 1], AllTArray[0, 2], AllTArray[0, 3]});
		rows.Add (new TileScript[]{AllTArray[1, 0], AllTArray[1, 1], AllTArray[1, 2], AllTArray[1, 3]});
		rows.Add (new TileScript[]{AllTArray[2, 0], AllTArray[2, 1], AllTArray[2, 2], AllTArray[2, 3]});
		rows.Add (new TileScript[]{AllTArray[3, 0], AllTArray[3, 1], AllTArray[3, 2], AllTArray[3, 3]});

		//	Creating two tiles on start
		Creating ();
		Creating ();

	}

	private void MakeInstance(){
		if (instance == null) {
			instance = this;
		}
	}

	private void GameWin(){
		gameOverWonText.text = "YOU WON!";
		gameOverPanel.SetActive (true);
	}

	private void GameOver(){

		gameOverWonText.text = "GAME OVER";
		gameOverScoreText.text = "SCORE: " + ScoreController.instance.Score;
		gameOverPanel.SetActive (true);
	}

	bool TilesMove(){
		if (NullTiles.Count > 0) {
			return true;
		} else {

			//	Checking cols
			for (int i = 0; i < cols.Count; i++) {
				for (int j = 0; j < rows.Count - 1; j++) {
					if (AllTArray [j, i].Number == AllTArray [j + 1, i].Number) {
						return true;
					}
				}
			}
			//	Checking rows
			for (int i = 0; i < rows.Count; i++) {
				for (int j = 0; j < cols.Count - 1; j++) {
					if (AllTArray [i, j].Number == AllTArray [i, j + 1].Number) {
						return true;
					}
				}
			}
		}
		return false;
	}

	public void LoadNewGame(){

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	bool MoveDown(TileScript[] TLine){
		
		for (int i = 0; i < TLine.Length - 1; i++) {

			//	Moving
			if (TLine [i].Number == 0 && TLine [i + 1].Number != 0) {
				TLine [i].Number = TLine [i + 1].Number;
				TLine [i + 1].Number = 0;

				return true;
			}
			//	Merging
			if (TLine [i].Number != 0 && TLine [i].Number == TLine [i + 1].Number && TLine [i].merged == false && TLine [i + 1].merged == false) {
				TLine [i].Number *= 2;
				TLine [i + 1].Number = 0;
				TLine [i].merged = true;

				//	Add same ammount of points that player combine
				ScoreController.instance.Score += TLine [i].Number;

				if (TLine [i].Number == 2048) {
					GameWin ();
				}

				return true;
			}
		}
		return false;
	}

	bool MoveUp(TileScript[] TLine){
		
		for (int i = TLine.Length - 1; i > 0; i--) {

			//	Moving
			if (TLine [i].Number == 0 && TLine [i - 1].Number != 0) {
				TLine [i].Number = TLine [i - 1].Number;
				TLine [i - 1].Number = 0;
				return true;
			}
			//	Merging
			if (TLine [i].Number != 0 && TLine [i].Number == TLine [i - 1].Number && TLine [i].merged == false && TLine [i - 1].merged == false) {
				TLine [i].Number *= 2;
				TLine [i - 1].Number = 0;
				TLine [i].merged = true;

				//	Add same ammount of points that player combine
				ScoreController.instance.Score += TLine [i].Number;

				if (TLine [i].Number == 2048) {
					GameWin ();
				}

				return true;
			}
		}
		return false;
	}

	//	Creating new tiles
	void Creating(){
		if (NullTiles.Count > 0) {

			//	Picking the index for random number in nullTiles
			int randomNumIndex = Random.Range (0, NullTiles.Count);
			//	Pick the index for new random number
			int moreRandomNum = Random.Range(0, 10);
			if (moreRandomNum == 0) {
				//	Set this number to 4
				NullTiles [randomNumIndex].Number = 4;
			} else {
				//	Set this number to 2
				NullTiles [randomNumIndex].Number = 2;
			}
			//	Remove tile at specific index, because it's not available
			NullTiles.RemoveAt (randomNumIndex);
		}
	}

	private void ResetMerging(){
		foreach (TileScript tile in AllTArray) {
			tile.merged = false;
		}
	}

	//	Updating information about tiles
	private void TileNullOrFullUpdate(){

		NullTiles.Clear ();
		foreach (TileScript tile in AllTArray) {

			if (tile.Number == 0) {
				NullTiles.Add (tile);
			}
		}
	}

	//	Movement controll
	public void MoveTile(Movements movements){

		bool moved = false;
		ResetMerging ();

		for (int i = 0; i < rows.Count; i++) {

			switch (movements) {

			case Movements.Down:
				while (MoveUp (cols [i])) {
					moved = true;
				}
				break;
			case Movements.Left:
				while (MoveDown (rows [i])) {
					moved = true;
				}
				break;
			case Movements.Right:
				while (MoveUp (rows [i])) {
					moved = true;
				}
				break;
			case Movements.Up:
				while (MoveDown (cols [i])) {
					moved = true;
				}
				break;
			}
		}

		if (moved) {
			TileNullOrFullUpdate ();

			//	After each move create new tile
			Creating ();

			if (!TilesMove ()) {
				GameOver ();
			}
		}
	
	}
}
