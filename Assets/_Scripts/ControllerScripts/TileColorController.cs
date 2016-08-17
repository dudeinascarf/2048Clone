using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileColors{

	//	Tile number
	public int number;
	// Number color
	public Color32 numberColor;
	//	Tile color
	public Color32 tileColor;

}

public class TileColorController : MonoBehaviour {


	public static TileColorController instance;

	public TileColors[] tileColors;


	void Awake () {
		MakeInstance ();
	}

	private void MakeInstance(){
		if (instance == null) {
			instance = this;
		}
	}
	

}
