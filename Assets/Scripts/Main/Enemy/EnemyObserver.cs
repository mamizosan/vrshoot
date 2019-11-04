using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyObserver : MonoBehaviour {
	//[ SerializeField ] private SpawnManager _spawn_manager = null;
	[ SerializeField ] private ScoreManager _score_manager = null;
	[ SerializeField ] private MiniMap _minimap = null;
	
	private bool _is_boss_enmey_destory = false;

	//生成されたことを通知
	public void NormalEnemySpawn( GameObject enemy ) { 
		_minimap.AddEnemyUI( enemy );
	}
	
	//通知を受け取り処理する
	public void NormalEnmeyDestroy( Enemy.ENEMY_TYPE enemy_type, Vector3 point_text_pos ) {
		switch ( enemy_type ) { 
			case Enemy.ENEMY_TYPE.TYPE_A:
			case Enemy.ENEMY_TYPE.TYPE_B:
			case Enemy.ENEMY_TYPE.TYPE_C:
				_score_manager.Score( ScoreManager.SCORE_TYPE.NORMAL_ENMEY, point_text_pos );
				break;

			case Enemy.ENEMY_TYPE.TYPE_A_BONUS_A:
				_score_manager.Score( ScoreManager.SCORE_TYPE.BONUS_NORMAL_ENEMY, point_text_pos );
				break;

			default:
				Assert.IsNotNull( null, "[EnemyObserver]予期しないエネミータイプが入っています" );
				break;
		}
	}
	
	public void BossEnemyDestroy( Vector3 point_text_pos ) {
		_is_boss_enmey_destory = true;
		_score_manager.Score( ScoreManager.SCORE_TYPE.BOSS, point_text_pos );
	}
	
	////ノーマルエネミーが全滅してるかどうか
	//public bool IsAllNormalEnemyDestroy( ) {
	//	return ( _spawn_manager.getNormalEnemySpawnMaxNum( ) == _normal_enmey_destory_count );	
	//}
	
	public bool IsBossEnemyDestroy( ) {
		return _is_boss_enmey_destory;	
	}
	
}
