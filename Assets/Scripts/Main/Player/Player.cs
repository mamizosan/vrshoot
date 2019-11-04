using UnityEngine;
using UnityEngine.Assertions;

//プレイヤーの制御クラス
public class Player : MonoBehaviour {
	[ SerializeField ] private GazeController _gaze_controller = null;
	[ SerializeField ] private Gun _gun = null;

	private Controller _controller = null;

	private void Awake( ) {
		_controller = Controller.getInstance( );		
	}

	private void Start( ) {
		CheckReference( );
	}

	private void Update( ) {
		Shoot( );	
	}

	

	//射撃処理
	private void Shoot( ) {
		if ( !_controller.getCotrollerInput( Controller.INPUT_TYPE.TRIGGER_DOWN ) ) return;	//ボタンを入力してなかったらreturn

		//敵をロックオンしていたら撃つ
		if ( _gaze_controller.getLockOnObject( ) != null ) {
			_gun.Shoot( _gaze_controller.getLockOnObject( ) );
		}

		//if ( レーザーフラグが立っていたら ) {
		//	if ( !_controller.getCotrollerInput( Controller.INPUT_TYPE.TRIGGER ) ) return;	//ボタンを入力してなかったらreturn
		//	_gun.laserShoot();
		//		
		//}

	}
	
	private void CheckReference( ) {
		Assert.IsNotNull( _gaze_controller, "[Player]GazeControllerの参照がありません" );
		Assert.IsNotNull( _gun, "[Player]Gunの参照がありません" );
	}
}