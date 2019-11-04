using UnityEngine;
using UnityEngine.Assertions;

//ノーマルエネミーの制御クラス
public class NormalEnemy : Enemy {
	private AudioSource _audio_source = null;
	private EnemyObserver _enemy_observer = null;
	private EnemyMove _enemy_move = null;
	private ENEMY_TYPE _enemy_type = ENEMY_TYPE.TYPE_A;
	private float _delete_time = 0;

	private void Awake( ) {
		_audio_source = GetComponent< AudioSource >( );
	}

	private void FixedUpdate( ) {
		if ( _enemy_move == null ) return;	//初期化されるまで更新しない

		Move( );
		DeleteTimeCount( );
	}

	private void OnTriggerEnter( Collider other ) {
		//弾に当たったら死ぬ
		if ( other.gameObject.tag == StringConstantRegistry.getTag( StringConstantRegistry.TAG.BULLET ) ) {
			Destroy( other.gameObject );
			Death( );
		}
	}

	//移動処理
	protected override void Move( ) {
		_enemy_move.Move( );
	}

	//死亡処理
	protected override void Death( ) {
		_audio_source.PlayOneShot( PathDataRegistry.getSE( PathDataRegistry.SE.EXPLOSION ) );	//爆発SEを鳴らす
		Instantiate( PathDataRegistry.getEffect( PathDataRegistry.EFFECT.NORMAL_ENEMY_EXPLOSION ), transform.position, Quaternion.identity );	//爆発エフェクト生成
		_enemy_observer.NormalEnmeyDestroy( _enemy_type, transform.position );	//通知
		Destroy( this.gameObject );
	}

	private void DeleteTimeCount( ) { 
		_delete_time -= Time.deltaTime;

		if ( _delete_time < 0 ) {
			Destroy( this.gameObject );
		}
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _audio_source, "[Enemy]AudioSourceがアタッチされていません" );
	}

	//初期化
	public void Initialize( ENEMY_TYPE enmey_type, EnemyMove enemy_move, EnemyObserver enemy_observer, float delete_time ) {  
		_enemy_type = enmey_type;
		_enemy_move = enemy_move;
		_enemy_observer = enemy_observer;
		_delete_time = delete_time;

		_enemy_observer.NormalEnemySpawn( this.gameObject );
	}
}
