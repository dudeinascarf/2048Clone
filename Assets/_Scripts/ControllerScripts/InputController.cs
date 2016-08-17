using UnityEngine;
using System.Collections;


public enum Movements{
	Right, Left, Up, Down
}

public class InputController : MonoBehaviour {


	private GameController gameController;

	void Start(){

		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	void Update () {
	
		//	Movement
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			gameController.MoveTile (Movements.Right);
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			gameController.MoveTile (Movements.Left);
		}
		else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			gameController.MoveTile (Movements.Up);
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			gameController.MoveTile (Movements.Down);
		}
	}
}
