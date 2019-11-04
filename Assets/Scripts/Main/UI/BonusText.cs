using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

//ボーナステキストUIの制御クラス
public class BonusText : MonoBehaviour {
	[ SerializeField ] private ScoreManager _score_manager = null;
	[ SerializeField ] private Image _score_bonus_text = null;

	private Sprite _bonus_text = null;

	private void Awake( ) {
		_bonus_text = PathDataRegistry.getSprite( PathDataRegistry.SPRITE.BONUS_TIME );
	}

	private void Start( ) {
		_score_bonus_text.sprite = _bonus_text;
		_score_bonus_text.gameObject.SetActive( false );
	}

	private void Update( ) {
		if ( !_score_manager.getIsBonusTime( ) && _score_bonus_text.gameObject.activeInHierarchy ) { 
			_score_bonus_text.gameObject.SetActive( false );
		}

		if ( _score_manager.getIsBonusTime( ) && !_score_bonus_text.gameObject.activeInHierarchy ) { 
			_score_bonus_text.gameObject.SetActive( true );
		}
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _score_manager, "[BonusText]ScoreManagerが参照されていません" );
		Assert.IsNotNull( _score_bonus_text, "[BonusText]ScoreBonusTextが参照されていません" );
	}

}