using UnityEngine;

//制限時間テキストUIの制御クラス
public class TimeLimitCount : MonoBehaviour {
	private readonly string TITLE_TEXT = "残り時間:";

	[ SerializeField ] private float _time_limit = 0;

	private float _initial_time_limit = 0;
	private bool _is_time_limit_count = false;
	private TextMesh _time_limit_ui = null;


	private void Awake( ) {
		_time_limit_ui = GetComponent< TextMesh >( );
	}

	private void Start( ) {
		ShowTimeLimit( );

		_initial_time_limit = _time_limit;
	}

	private void FixedUpdate( ) {
		if ( !_is_time_limit_count ) return;
		if ( _time_limit <= 0 ) { 
			_time_limit = 0;
			return;
		}

		_time_limit -= Time.deltaTime;	//時間をカウントする
	}

	private void Update( ) {
		if ( !_is_time_limit_count ) return;
		ShowTimeLimit( );
	}

	private void ShowTimeLimit( ) { 
		//時間を表示
		int show_time_count = ( int )_time_limit;
		_time_limit_ui.text = TITLE_TEXT + show_time_count.ToString( );
	}

	//カウントをスタートする
	public void TimeLimitCountStart( ) {
		if ( _is_time_limit_count ) { 
			Debug.Log( "タイムカウントはすでにスタートしています" );
			return;
		}

		_is_time_limit_count = true;
	}

	//カウントを終了する
	public void TimeLimitCountEnd( ) { 
		if ( !_is_time_limit_count ) { 
			Debug.Log( "タイムカウントはすでに終了しています" );
			return;
		}

		_is_time_limit_count = false;
	}

	//現在の時間取得
	public int getTimeLimit( ) { 
		return ( int )_time_limit;	
	}

	//初期の時間を取得
	public int getInitialTimeLimit( ) { 
		return ( int )_initial_time_limit;	
	}
}