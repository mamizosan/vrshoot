using UnityEngine;
using UnityEngine.Assertions;

//ボスエネミーの制御クラス
public class BossEnemy : Enemy {
	[ SerializeField ] private WeakPoint[ ] _weak_points = new WeakPoint[ 1 ];

	private EnemyObserver _enmey_observer = null;
	private Anim _anim = null;

	private bool _death = false;

	private void Awake( ) {
		_anim = GetComponent< Anim >( );
	}

	private void Update( ) {
		if ( IsDeath( ) && !_death ) {	//死亡条件を満たしていて、死亡処理をしていなかったら
			Death( );
		}

		if ( _anim.IsAnimationEnd( StringConstantRegistry.ANIMATION_NAME.BOSS_DIES ) && _death ) {	//アニメーションをしていなくて、死亡処理をしていたら
			BossEnemyDestory( );
		}
	}

	//死ぬ条件を満たしているかどうか
	private bool IsDeath( ) {
		for ( int i = 0; i < _weak_points.Length; i++ ) {
			if ( _weak_points[ i ] != null ) { 
				return false;	
			}
		}

		return true;
	}

	//ボス消去
	private void BossEnemyDestory( ) {
		Destroy( this.gameObject );
	}

	
	private void CheckReference( ) {
		Assert.IsNotNull( _enmey_observer, "[BossEnmey]EnemyObserverの参照がありません" );
		for ( int i = 0; i < _weak_points.Length; i++ ) { 
			Assert.IsNotNull( _weak_points[ i ], "[BossEnemy]WeakPointの参照がありません" );
		}
		Assert.IsNotNull( _anim, "[BossEnmey]Animの参照がありません" );
	}

	//死亡時処理
	protected override void Death( ) {
		_anim.AnimationStart( StringConstantRegistry.ANIMATION_FLAG.BOSS_DEATH );			//死亡アニメーションスタート
		Instantiate( PathDataRegistry.getEffect( PathDataRegistry.EFFECT.BOSS_ENMEY_EXPLOSION ), transform.position, Quaternion.identity );	//爆発生成
		_enmey_observer.BossEnemyDestroy( transform.position );	//通知する
		_death = true;	//死亡処理フラグを立てる
	}

	//初期化
	public void Initialize( EnemyObserver enemy_observer ) {
		//if ( _weak_points.Length != weak_point_hp.Length ) {
		//	Assert.IsNotNull( null, "[Boss]Bossに存在する弱点の数と設定される弱点の数が違います" );
		//}

		//for ( int i = 0; i < _weak_points.Length; i++ ) {
		//	_weak_points[ i ].setHP( weak_point_hp[ i ] );
		//}

		_enmey_observer = enemy_observer;
	}
}

//あらかじめボスにある弱点の数とSpawnManagerで設定する弱点の数を合わせなければならないのがごみ
