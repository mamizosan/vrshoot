using UnityEngine;

//ボスの弱点の制御クラス
public class WeakPoint : Enemy {

	private void OnTriggerEnter( Collider other ) {
		if ( other.gameObject.tag == StringConstantRegistry.getTag( StringConstantRegistry.TAG.BULLET ) ) { 
			Destroy( other.gameObject );
			Death( );
		}
	}

	//死亡時処理
	protected override void Death( ) {
		Instantiate( PathDataRegistry.getEffect( PathDataRegistry.EFFECT.BOSS_ENMEY_WEAK_POINT_EXPLOSION ), transform.position, Quaternion.identity );	//爆発生成
		Destroy( this.gameObject );	
	}
}
