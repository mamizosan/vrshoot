using UnityEngine;
using UnityEngine.Assertions;

//スコア機能の制御クラス
public class ScoreManager : MonoBehaviour {
	private readonly int NORMAL_ENEMY_POINT = 20;
	private readonly int BONUS_NORMAL_ENEMY_POINT = 20;
	private readonly int BOSS_POINT = 100;

	public enum SCORE_TYPE { 
		NORMAL_ENMEY,
		BONUS_NORMAL_ENEMY,
		BOSS,
	}

	public static int _score = 0;	//関数からのみこの変数をいじること。直接いじらない

	[ SerializeField ] private Player _player = null;
	[ SerializeField ] private float _bonus_time = 0;
	[ SerializeField ] private int _bonus_multiple = 2;

	PointText _point_text_data = null;
	private float _initial_bonus_time = 0;
	private bool _is_bonus_time = false;


	private void Awake( ) {
		_point_text_data = PathDataRegistry.getPointText( );
	}

	private void Start( ) {
		_initial_bonus_time = _bonus_time;
	}

	private void FixedUpdate( ) {
		//ボーナスタイム処理
		if ( !_is_bonus_time ) return;

		_bonus_time -= Time.deltaTime;

		if ( _bonus_time <= 0 ) { 
			_is_bonus_time = false;
		}
	}

	//ボーナスタイム開始処理
	private void BonusTimeStart( ) { 
		_is_bonus_time = true;
		_bonus_time = _initial_bonus_time;
	}

	//スコア加算
	private void AddScore( int add_score ) { 
		if ( _is_bonus_time ) { 
			_score += add_score * _bonus_multiple;
		} else {
			_score += add_score;
		}
	}

	//ポイントテキスト生成
	private void CreatePointText( SCORE_TYPE score_type, Vector3 pos ) { 
		PointText point_text = null;
		point_text = Instantiate( _point_text_data, pos, Quaternion.identity );
		
		point_text.Initialize( getPointTextSprite( score_type ), _player.transform.position, ( score_type == SCORE_TYPE.BOSS ) );
	}

	//ポイントのスプライトの取得
	private Sprite getPointTextSprite( SCORE_TYPE score_type ) {
		switch ( score_type ) {
			case SCORE_TYPE.NORMAL_ENMEY:
			case SCORE_TYPE.BONUS_NORMAL_ENEMY:
				if ( getIsBonusTime( ) ) {
					return PathDataRegistry.getSprite( PathDataRegistry.SPRITE.POINT_40 );
				} else { 
					return PathDataRegistry.getSprite( PathDataRegistry.SPRITE.POINT_20 );
				}

			case SCORE_TYPE.BOSS:
				if ( getIsBonusTime( ) ) {
					return PathDataRegistry.getSprite( PathDataRegistry.SPRITE.POINT_200 );
				} else { 
					return PathDataRegistry.getSprite( PathDataRegistry.SPRITE.POINT_100 );
				}

			default:
				return null;
		}
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _player, "[ScoreManager]Playerの参照がありません" );
	}

	//スコア処理
	public void Score( SCORE_TYPE score_type, Vector3 pos ) {
		switch ( score_type ) {
			case SCORE_TYPE.NORMAL_ENMEY:
				AddScore( NORMAL_ENEMY_POINT );
				CreatePointText( score_type, pos );
				break;

			case SCORE_TYPE.BONUS_NORMAL_ENEMY:
				AddScore( BONUS_NORMAL_ENEMY_POINT );
				CreatePointText( score_type, pos );
				BonusTimeStart( );
				break;

			case SCORE_TYPE.BOSS:
				AddScore( BOSS_POINT );
				CreatePointText( score_type, pos );
				break;

			default:
				Assert.IsNotNull( null, "[ScoreManager]追加したいスコアタイプが存在しません" );
				break;
		}
	}

	public static void ResetScore( ) { 
		_score = 0;	
	}

	public static int getScore( ) { 
		return _score;	
	}

	public bool getIsBonusTime( ) { 
		return _is_bonus_time;
	}
	
}

//もし得点を同じにする場合は、readonlyを使わないといけないかも