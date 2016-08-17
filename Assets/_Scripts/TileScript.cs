using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TileScript : MonoBehaviour {


	public bool merged = false;

	public int rowIndex;
	public int colIndex;

	[SerializeField]
	private Text tileNumber;

	[SerializeField]
	private Image tileColor;

	public int Number{
		get{
			return number;
		}
		set{
			number = value;
			if (number == 0) {
				TurnOffTile ();
			} else {
				PickNumber (number);
				TurnOnTile ();
			}
		}
	}
	private int number;


	void PickFromTileController (int index) {
	
		tileNumber.text = TileColorController.instance.tileColors [index].number.ToString();
		tileNumber.color = TileColorController.instance.tileColors [index].numberColor;
		tileColor.color = TileColorController.instance.tileColors [index].tileColor;
	}

	void PickNumber(int num){

		switch (num) {

		case 2:
			PickFromTileController (0);
			break;
		case 4:
			PickFromTileController (1);
			break;
		case 8:
			PickFromTileController (2);
			break;
		case 16:
			PickFromTileController (3);
			break;
		case 32:
			PickFromTileController (4);
			break;
		case 64:
			PickFromTileController (5);
			break;
		case 128:
			PickFromTileController (6);
			break;
		case 256:
			PickFromTileController (7);
			break;
		case 512:
			PickFromTileController (8);
			break;
		case 1024:
			PickFromTileController (9);
			break;
		case 2048:
			PickFromTileController (10);
			break;
		case 4096:
			PickFromTileController (11);
			break;
		//default:
			//	If numbers are not the same as numbers in TileColorController
			//Debug.Log("Check colors");
		}
	}

	private void TurnOnTile(){
		tileColor.enabled = true;
		tileNumber.enabled = true;
	}

	private void TurnOffTile(){
		tileColor.enabled = false;
		tileNumber.enabled = false;
	}
}
