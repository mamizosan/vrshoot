using UnityEngine;
using UnityEngine.Assertions;

//メインシーン進行の制御クラス
public class MainManager : ScenesManager {
	private enum STATUS { 
		NORMAL,
		BOSS,
		GAME_END,
	}

	private enum TIME_OVER_OR_CLEAR { 
		TIME_OVER,
		GAME_CLEAR
	}

	
	[ SerializeField ] private SpawnManager _spawn_manager = null;
	[ SerializeField ] private EnemyObserver _enemy_observer = null;
	[ SerializeField ] private CountDown _count_donw_ui = null;
	[ SerializeField ] private TimeLimitCount _time_limit_count_ui = null;
	[ SerializeField ] private GameEndText _game_end_text = null;
	[ SerializeField ] private float _next_scene_count = 0;

	private IProgressStatus _progress_status = null;
	private STATUS _status = STATUS.NORMAL;
	private bool _is_count_start = false;

	private void Start( ) {
		CheckReference( );
		ScoreManager.ResetScore( );		//スコアリセット
		_count_donw_ui.CountStart( );	//スタートまでのカウントダウン開始
	}

	private void FixedUpdate( ) {
		//シーン遷移までのカウント
		if ( _next_scene_count <= 0 ) return;

		if ( _is_count_start ) {
			_next_scene_count -= Time.deltaTime;
		}
	}

	private void Update( ) {
		//カウントダウンが終了していて進行度ステータスが初期化されていなかったら
		if ( _count_donw_ui.getIsFinishCount( ) && _progress_status == null ) {
			StatusInitialize( );
		}

		//進行度ステータスを初期化していなかったら処理しない
		if ( _progress_status == null ) return;


		//残り時間がなくてゲーム終了ステータスでなかったら
		if ( _time_limit_count_ui.getTimeLimit( ) <= 0 && _status != STATUS.GAME_END ) { 
			GameEnd( TIME_OVER_OR_CLEAR.TIME_OVER );
		}

		//ゲーム終了ステータスでなかったら
		if ( _status != STATUS.GAME_END ) { 
			ProgressStatusChange( );
		}

		//ゲーム終了ステータスで次のへのシーンのカウントが0になっていたら
		if ( _status == STATUS.GAME_END && _next_scene_count <= 0 ) {
			NextScene( );
		}
	}

	//次のステータスへ行く条件を満たしていたらステータスを変える
	private void ProgressStatusChange( ) {
		if ( !_progress_status.IsNextStatus( ) ) return;

		switch ( _status ) {
			case STATUS.NORMAL:
				_progress_status = new BossStatus( _spawn_manager, _enemy_observer, _time_limit_count_ui );
				_status = STATUS.BOSS;
				break;

			case STATUS.BOSS:
				GameEnd( TIME_OVER_OR_CLEAR.GAME_CLEAR );
				break;

			default:
				Assert.IsNotNull( null, "[MainManager]予期しないStatusになっています" );
				break;
		}
	}

	//ゲームがスタートした時に行う初期化
	private void StatusInitialize( ) {
		_progress_status = new NormalStatus( _spawn_manager, _time_limit_count_ui );
	}

	//ゲーム終了時処理
	private void GameEnd( TIME_OVER_OR_CLEAR game_over_or_clear ) {
		switch ( game_over_or_clear ) { 
			case TIME_OVER_OR_CLEAR.TIME_OVER:
				_game_end_text.ShowTimeOverText( );
				break;

			case TIME_OVER_OR_CLEAR.GAME_CLEAR:
				_game_end_text.ShowGameClearText( );
				break;

			default:
				Assert.IsNotNull( null, "[MainManager]予期しない終了状態になっています" );
				break;
		}

		_spawn_manager.SpawnStop( );	//エネミー生成終了
		_is_count_start = true;
		_status = STATUS.GAME_END;
	}

	private void CheckReference( ) {
		Assert.IsNotNull( _spawn_manager, "[MainManager]SpaenManagerの参照がありません" );
		Assert.IsNotNull( _enemy_observer, "[MainManager]EnemyObserverの参照がありません" );
		Assert.IsNotNull( _count_donw_ui, "[MainManager]CountDownの参照がありません" );
		Assert.IsNotNull( _time_limit_count_ui, "[MainManager]TimeLimitCountの参照がありません" );
		Assert.IsNotNull( _game_end_text, "[MainManager]GameOverTextの参照がありません" );
	}

	protected override void NextScene( ) { 
		_scene_changer.SceneChange( StringConstantRegistry.SCENE_NAME.RESULT );
	} 

}

//全てのスクリプトで全体的に名前が良くないかも。
//Debug・Releaseで使う処理と使わない処理を分けるようにする。

//SEの鳴らし方を考えないといけない。敵が遠すぎて爆発SEが聞こえない(設定で何とかなる？)(もしかして元の音が小さくて聞こえない？)
//もしかしたらGazeControllerのRayの飛ばし方を工夫すれば2DのロックオンUIを作れるかも
//GazeControllerから角度をもらえばレーザーを実現できるかな？

//staticクラスとシングルトンパターンクラスの違い
//staticクラス　→　様々な所で使う可能性があり、インスタンスが複数あるべきじゃないもの
//シングルトンパターンクラス　→　ある程度使うところが決まっていてインスタンスが複数無くていいもの
