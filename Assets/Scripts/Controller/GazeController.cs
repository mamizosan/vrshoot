using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

//視線コントローラーの制御や情報取得クラス
public class GazeController : MonoBehaviour {
	[ SerializeField ] private Canvas _gaze_canvas = null;	//視線UIのあるキャンバス
	[ SerializeField ] private float _set_lock_on_timer = 0;
	[ SerializeField ] private Image _indicator_gaauge = null;

	private RectTransform _rec = null;
	private Ray _gaze;								//視線
	private GameObject _hit_object = null;			//視線に当たったオブジェクト
	private bool _is_hit = false;					//視線に当たったかどうか
	private bool _is_lock_on = false;				//ロックオンするかどうか
	private float _lock_on_timer = 0;

	private void Awake( ) {
		_rec = GetComponent< RectTransform >( );
	}

	private void Start( ) {
		CheckReference( );
		_indicator_gaauge.fillAmount = 0;
	}

	private void FixedUpdate( ) {
		GazeFly( );					//この関数が直接FixedUpdateが必要な処理は無いが、LockOnCount( )との処理のラグを防ぐため
		LockOnCount( );
		IndicatorUpdate( );
	}

	//視線を飛ばす
	private void GazeFly( ) {
		Vector2 origin = RectTransformUtility.WorldToScreenPoint( _gaze_canvas.worldCamera,_rec.position );	//UIの座標をスクリーン座標に変換
		_gaze = Camera.main.ScreenPointToRay( origin );

		int layer_mask = LayerMask.GetMask( StringConstantRegistry.getLayer( StringConstantRegistry.LAYER.JUDGMENT_AGAINST_GAZE ) );
		RaycastHit hit;
		_is_hit = Physics.Raycast( _gaze, out hit, layer_mask );
		LockOn( hit );
	}

	//ロックオン処理
	private void LockOn( RaycastHit hit ) {
		if ( hit.collider == null ) {
			_hit_object = null;
			_is_lock_on = false;
			return;
		}

		//前のフレームで取得したオブジェクトと一緒だったらロックオンを始める(同じオブジェクトじゃなくてもカウントがそのまま進むバグ防止)
		if ( _hit_object != hit.collider.gameObject ) { 
			_hit_object = hit.collider.gameObject;
			_is_lock_on = false;
		} else {
			_is_lock_on = true;
		}
	}

	//見ていたらカウントする
	private void LockOnCount( ) {
		if ( _is_lock_on ) {
			_lock_on_timer -= Time.deltaTime;	
		} else {
			_lock_on_timer = _set_lock_on_timer;
		}

		if ( _lock_on_timer < 0 ) {
			_lock_on_timer = 0;	
		}
	}

	//インジゲーター処理
	private void IndicatorUpdate( ) { 
		if ( getLockOnDoingObject( ) != null ) {		//ロックオン中オブジェクトがあったら
			_indicator_gaauge.color = Color.white;
			if ( _indicator_gaauge.fillAmount >= 1f ) {	//ゲージが溜まりきっていたら
				_indicator_gaauge.fillAmount = 1f;
			} else { 
				_indicator_gaauge.fillAmount += 1.0f / _set_lock_on_timer * Time.deltaTime;
			}
			
		}

		if ( getLockOnObject( ) != null ) {				//ロックオンしたオブジェクトがあったら
			_indicator_gaauge.fillAmount = 1f;
			_indicator_gaauge.color = Color.blue;
		}

		if ( !_is_lock_on ) {							//ロックオン中またはロックオンしているものがなかったら
			_indicator_gaauge.fillAmount = 0;
		}
	}

	private void CheckReference( ) {
		Assert.IsNotNull( _gaze_canvas, "[GazeController]GazeCanvasの参照がありません" );
	}


	public GameObject getLockOnObject( ) {
		//視線がオブジェクトにヒットしていて、一定以上見ていたら(ロックオンが完了していたら)そのオブジェクト返す
		if ( _hit_object == null || _lock_on_timer > 0 ) {
			return null;
		} else {
			return _hit_object;
		}
	}

	public GameObject getLockOnDoingObject( ) {
		//視線がオブジェクトにヒットしていて、一定以上見ていなかったら(ロックオン中だったら)そのオブジェクトを返す
		if ( _hit_object != null && _lock_on_timer > 0 ) {
			return _hit_object;
		} else {
			return null;
		}
		
	}

	//テスト用---------------------------------------------------------------
	public Vector3 GetDirection( ) {
		return 	_gaze.direction;	//恐らく、2Dでの計算の角度を取得する
	}

	public bool getIsHit( ) {
		return _is_hit;
	}
	//-------------------------------------------------------------------------
}