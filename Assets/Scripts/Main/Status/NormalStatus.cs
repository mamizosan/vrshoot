
//ゲームの進行度が通常状態時の制御クラス
public class NormalStatus : IProgressStatus {
	private SpawnManager _spawn_manager = null;
	private TimeLimitCount _time_limit_count = null;

	public NormalStatus( SpawnManager spawn_manager, TimeLimitCount time_limit_count ) {
		_spawn_manager = spawn_manager;
		_time_limit_count = time_limit_count;

		_spawn_manager.SpawnStart( );
		_time_limit_count.TimeLimitCountStart( );
	}

	public bool IsNextStatus( ) {
		return ( _time_limit_count.getTimeLimit( ) <= _time_limit_count.getInitialTimeLimit( ) / 2 );	//残り時間が半分以下になったかどうか
	}

}
