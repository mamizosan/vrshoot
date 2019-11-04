using UnityEngine;
using UnityEngine.Assertions;

//銃を撃つための制御クラス
public class Gun : MonoBehaviour {

	[ SerializeField ] private float _set_next_shoot_time = 0;	//次に撃てるまでの時間
	[ SerializeField ] private float _bullet_pos_y = 0;

	private AudioSource _audio_source = null;
	private Bullet _bullet = null;
	private float _next_shoot_time = 0;
	private bool _is_shoot = true;

	private void Awake( ) {
		_audio_source = GetComponent< AudioSource >( );
		_bullet = PathDataRegistry.getBullet( PathDataRegistry.BULLET.BULLET );
	}

	private void Start( ) {
		CheckReference( );

		_next_shoot_time = _set_next_shoot_time;
	}

	private void FixedUpdate( ) {
		NextShootCount( );
	}


	//撃てない状態のときは次に撃てるまでの時間をカウントする
	private void NextShootCount( ) {
		if ( !_is_shoot ) {
			_next_shoot_time -= Time.deltaTime;
			if ( _next_shoot_time <= 0 ) {	//時間が来たら撃てる状態にする
				_next_shoot_time = 0;
				_is_shoot = true;
			}
		}
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _audio_source, "[Gun]AudioSourceがアタッチされていません" );
	}

	//射撃処理
	public void Shoot( GameObject lock_on_obj ) {
		if ( !_is_shoot ) return;	//撃てる状態でなければ撃たない

		Vector3 bullet_pos = transform.position;
		bullet_pos.y += _bullet_pos_y;
		Vector3 bullet_dir = Mathematics.VectorDirection( lock_on_obj.transform.position, transform.position );
		Bullet bullet_obj = Instantiate( _bullet, bullet_pos, Quaternion.LookRotation( bullet_dir ) );

		_audio_source.PlayOneShot( PathDataRegistry.getSE( PathDataRegistry.SE.BULLET ) );

		bullet_obj.setTarget( lock_on_obj );	//弾に追尾する対象を入れる

		_is_shoot = false;							//一度撃ったら撃てない状態にする
		_next_shoot_time = _set_next_shoot_time;	//次撃てるまでの時間を入れなおす
	}

	//public void LaserShoot( ) { 
	//	if ( !_is_shoot ) return;	//撃てる状態でなければ撃たない
	//
	//レイを視線の方向に飛ばす
	//そのレイに当たり判定をつける。レイに一直線に飛んでいくエフェクトつける。そのエフェクトに判定をつけるか空のゲームオブジェクトで判定をつける
	//	Vector3 bullet_pos = transform.position;
	//	bullet_pos.y += _bullet_pos_y;
	//	Bullet bullet_obj = Instantiate( _bullet, bullet_pos, Quaternion.LookRotation( transform.forward ) );
	//
	//	_audio_source.PlayOneShot( PathDataRegistry.getSE( PathDataRegistry.SE.BULLET ) );
	//
	//	//bullet_obj.setTarget( lock_on_obj );	//弾に追尾する対象を入れる
	//
	//	_is_shoot = false;							//一度撃ったら撃てない状態にする
	//	_next_shoot_time = _set_next_shoot_time;	//次撃てるまでの時間を入れなおす
	//}
}

//TheLabの通りに弾を撃つなら一発一発に判定を持たせるのではなく、
//ロックオンした状態で1回撃ったら1セット分の弾が出て、そのアニメーションが終わったらその時を削除する的な感じだと思われる