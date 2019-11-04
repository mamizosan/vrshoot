using UnityEngine;

//スコア表示の制御クラス
public class ShowScore : MonoBehaviour {
	private readonly string TITLE_TEXT = "スコア:";
	private TextMesh _score_text = null;

	private void Awake( ) {
		_score_text = GetComponent< TextMesh >( );
	}

	private void Update( ) {
		_score_text.text = TITLE_TEXT + ScoreManager.getScore( ).ToString( );
	}
}
