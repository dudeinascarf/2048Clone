using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {


	public static ScoreController instance;

	private int score;

	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private Text highScoreText;

	public int Score{
		get{
			return score;
		}set{
			score = value;
			scoreText.text = score.ToString ();

			//	Tracking high score
			if (PlayerPrefs.GetInt ("HighScore") < score) {
				PlayerPrefs.SetInt ("HighScore", score);
				highScoreText.text = score.ToString ();
			}
		}
	}


	void Awake(){
		MakeInstance ();

		//	Checking for player high score
		if (!PlayerPrefs.HasKey ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", 0);
		}

		scoreText.text = "0";
		highScoreText.text = "0";
		highScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString();
	}

	void MakeInstance(){
		if (instance == null) {
			instance = this;
		}
	}

}
