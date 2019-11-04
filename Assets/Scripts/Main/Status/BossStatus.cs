using UnityEngine;

//ゲームの進行度がボス状態時の制御クラス
public class BossStatus : IProgressStatus {
	private SpawnManager _spawn_manager = null;
	private EnemyObserver _enemy_observer = null;
	private TimeLimitCount _time_limit_count = null;
	private GameObject _boss = null;

	public BossStatus( SpawnManager spawn_manager, EnemyObserver enemy_observer, TimeLimitCount time_limit_count ) {
		_spawn_manager = spawn_manager;
		_enemy_observer = enemy_observer;
		_time_limit_count = time_limit_count;

		_spawn_manager.SpawnBoss( );
		
		_boss = GameObject.FindGameObjectWithTag( StringConstantRegistry.getTag( StringConstantRegistry.TAG.BOSS ) );
	}

	public bool IsNextStatus( ) {
		if ( _enemy_observer.IsBossEnemyDestroy( ) ) { //条件を満たしていたら
			_time_limit_count.TimeLimitCountEnd( );
		}

		return ( _enemy_observer.IsBossEnemyDestroy( ) && _boss == null );	//スコア表示とゲームクリア表示が同時になってしまうので、エネミーが完全に消えたかを見る
	}


}
