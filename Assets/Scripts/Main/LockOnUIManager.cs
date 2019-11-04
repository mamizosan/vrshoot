using UnityEngine;
using UnityEngine.Assertions;

//ロックオンUIの制御クラス
public class LockOnUIManager : MonoBehaviour {
	[ SerializeField ] private GazeController _gaze_controller = null;
	[ SerializeField ] private GameObject _lock_on_doing_ui = null;
	[ SerializeField ] private GameObject _lock_on_done_ui = null;
	[ SerializeField ] private GameObject _lock_on_effect = null;
	[ SerializeField ] private GameObject _vr_camera = null;
	[ SerializeField ] private Vector3 _initial_scale = new Vector3( 0, 0, 0 );
	[ SerializeField ] private Vector3 _lock_on_ui_change_scale = new Vector3( 0, 0, 0 );

	private GameObject _pre_lock_on_doing_obj = null;
	private GameObject _pre_lock_on_done_obj = null;
	private AudioSource _audio_source = null;


	private void Awake( ) {
		_audio_source = GetComponent< AudioSource >( );
	}

	private void Start( ) {
		CheckReference( );

		_lock_on_doing_ui.SetActive( false );
		_lock_on_done_ui.SetActive( false );
		_lock_on_effect.SetActive( false );
	}

	private void Update( ) {
		LockOnUIDisplayChange( );
		LockOnUIPosUpdate( );
	}

	private void LockOnUIDisplayChange( ) {
		//ロックオン中UIの表示切替---------------------------------------------------------------------------
		//ロックオン中UIが表示されていてロックオン中のオブジェクトがNULLだったら非表示にする
		if ( _lock_on_doing_ui.activeSelf && _gaze_controller.getLockOnDoingObject( ) == null ) {
			_lock_on_doing_ui.SetActive( false );
		}

		//前のフレームの時とロックオン中のオブジェクトが違っていてオブジェクトがNULLじゃなかったら表示更新
		if ( _pre_lock_on_doing_obj != _gaze_controller.getLockOnDoingObject( ) && 
			 _gaze_controller.getLockOnDoingObject( ) != null ) {
			LockOnUIScaleChange( _lock_on_doing_ui, _gaze_controller.getLockOnDoingObject( ) );
			_lock_on_doing_ui.SetActive( true );
		}
		//--------------------------------------------------------------------------------------------------

		//ロックオンUIの表示切替----------------------------------------------------------------------------
		//ロックオンUIが表示されていてロックオンしたオブジェクトがNULLだったら非表示にする
		if ( _lock_on_done_ui.activeSelf && _gaze_controller.getLockOnObject( ) == null ) { 
			_lock_on_done_ui.SetActive( false );
		}

		//前のフレームの時とロックオンしたオブジェクトが違っていてオブジェクトがNULLじゃなかったら表示更新
		if ( _pre_lock_on_done_obj != _gaze_controller.getLockOnObject( ) && 
			 _gaze_controller.getLockOnObject( ) != null ) {
			LockOnUIScaleChange( _lock_on_done_ui, _gaze_controller.getLockOnObject( ) );
			_lock_on_done_ui.SetActive( true );
			_audio_source.PlayOneShot( PathDataRegistry.getSE( PathDataRegistry.SE.LOCK_ON ) );	//ロックオンUIが表示されたときにSEを鳴らす
		}
		//---------------------------------------------------------------------------------------------------

		//ロックオンエフェクトの表示切替----------------------------------------------
		if ( _lock_on_doing_ui.activeSelf || _lock_on_done_ui.activeSelf ) {
			_lock_on_effect.SetActive( true );
		} else { 
			_lock_on_effect.SetActive( false );	
		}
		//----------------------------------------------------------------------------

		//このフレームのオブジェクトに更新
		_pre_lock_on_doing_obj = _gaze_controller.getLockOnDoingObject( );
		_pre_lock_on_done_obj = _gaze_controller.getLockOnObject( );

	}
	
	//ロックオンUIの大きさを変える
	private void LockOnUIScaleChange( GameObject lock_on_ui, GameObject lock_on_obj ) { 
		if ( lock_on_obj.tag == StringConstantRegistry.getTag( StringConstantRegistry.TAG.WEAK_POINT ) ) { 
			lock_on_ui.transform.localScale = _lock_on_ui_change_scale;
		} else {
			lock_on_ui.transform.localScale = _initial_scale;	
		}
	}

	//ロックオンUIが表示されていたら場所を更新
	private void LockOnUIPosUpdate( ) {
	
		if ( _lock_on_doing_ui.activeSelf && _gaze_controller.getLockOnDoingObject( ) != null ) {
			_lock_on_doing_ui.transform.position = _gaze_controller.getLockOnDoingObject( ).transform.position;
			Vector3 dir = Mathematics.VectorDirection( _vr_camera.transform.position, _lock_on_doing_ui.transform.position );
			_lock_on_doing_ui.transform.forward = dir; 

			_lock_on_effect.transform.position = _gaze_controller.getLockOnDoingObject( ).transform.position;	//エフェクト座標更新
		}
		
		if ( _lock_on_done_ui.activeSelf && _gaze_controller.getLockOnObject( ) != null ) {
			_lock_on_done_ui.transform.position = _gaze_controller.getLockOnObject( ).transform.position;
			Vector3 dir = Mathematics.VectorDirection( _vr_camera.transform.position, _lock_on_done_ui.transform.position );
			_lock_on_done_ui.transform.forward = dir;

			_lock_on_effect.transform.position = _gaze_controller.getLockOnObject( ).transform.position;		//エフェクト座標更新
		}
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _gaze_controller, "[LockOnUIManager]GazeControllerの参照がありません" );
		Assert.IsNotNull( _lock_on_doing_ui, "[LockOnUIManager]GameObjectのLockOnDoingUIの参照がありません" );
		Assert.IsNotNull( _lock_on_done_ui, "[LockOnUIManager]GameObjectのLockOndoneUIの参照がありません" );
		Assert.IsNotNull( _lock_on_effect, "[LockOnUIManager]LockOnEffectがアタッチされていません" );
		Assert.IsNotNull( _audio_source, "[LockOnUIManager]AudioSourceがアタッチされていません" );
		Assert.IsNotNull( _vr_camera, "[LockOnUIManager]VRCameraがアタッチされていません" );
	}

}