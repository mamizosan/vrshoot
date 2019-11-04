using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

//ミニマップの制御クラス
public class MiniMap : MonoBehaviour {
	private readonly float UI_DISTANCE  = 1f;
	private readonly float UI_WIDTH     = 3f;
	private readonly float UI_HEIGHT    = 1f;
	private readonly float UI_LIMIT_POS = 49f;

	[ SerializeField ] private Player _player = null;

	private List< GameObject > _spawn_enemy = new List< GameObject >( );
	private List< Image > _enemy_minimap_ui = new List< Image >( );

	private void Start( ) {
		CheckReference( );
	}

	private void Update( ) {
		MiniMapUpdate( );
	}

	//ミニマップの座標や消去などの更新
	private void MiniMapUpdate( ) {
		for ( int i = 0; i < _spawn_enemy.Count; i++ ) {
			if ( _spawn_enemy[ i ] == null ) {
				DeleteUI( i );
				return;
			}

			UIPosUpdate( i );
		}
	}

	//不要になったエネミーUIを削除	
	private void DeleteUI( int delete_num ) { 
		Destroy( _enemy_minimap_ui[ delete_num ].gameObject );
		_spawn_enemy.Remove( _spawn_enemy[ delete_num ] );
		_enemy_minimap_ui.Remove( _enemy_minimap_ui[ delete_num ] );
	}

	//UIの座標の更新
	private void UIPosUpdate( int index ) {
		if ( index < 0 ) return;

		Vector3 length = _spawn_enemy[ index ].transform.position - _player.transform.position;	//座標のエネミーとプレイヤーの間の長さ
		if ( length.y < 0 ) { 
			length.y = 0;	
		}

		Vector2 ui_pos = Vec3ToVec2( length * UI_DISTANCE );
		LimitPos( ref ui_pos );
		
		_enemy_minimap_ui[ index ].rectTransform.localPosition = ui_pos;
		_enemy_minimap_ui[ index ].rectTransform.sizeDelta = new Vector2( UI_WIDTH, length.y * UI_HEIGHT );
	}

	//z座標をy座標にしてVector3からVector2に変換
	private Vector2 Vec3ToVec2( Vector3 vec3 ) {
		if ( vec3.GetType( ) == typeof( Vector2 ) ) return vec3;

		Vector2 vec2 = Vector2.zero;
		vec2.x = vec3.x;
		vec2.y = vec3.z;

		return 	vec2;
	}

	//UIの移動制限
	private void LimitPos( ref Vector2 ui_pos ) { 
		if ( ui_pos.x < -UI_LIMIT_POS ) ui_pos.x = -UI_LIMIT_POS;
		if ( ui_pos.x > UI_LIMIT_POS  ) ui_pos.x = UI_LIMIT_POS;
		if ( ui_pos.y < -UI_LIMIT_POS ) ui_pos.y = -UI_LIMIT_POS;
		if ( ui_pos.y > UI_LIMIT_POS  ) ui_pos.y = UI_LIMIT_POS;
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _player, "[MiniMpa]Playerの参照がありません" );
	}

	//新しいエネミーUIの生成
	public void AddEnemyUI( GameObject spawn_enemy ) { 
		_spawn_enemy.Add( spawn_enemy );

		Image image_data = PathDataRegistry.getImage( PathDataRegistry.IMAGE.ENEMY_MINIMAP_UI );
		Image create_ui = Instantiate( image_data, transform.position, Quaternion.identity );
		create_ui.transform.SetParent( this.transform );
		_enemy_minimap_ui.Add( create_ui );
		UIPosUpdate( _enemy_minimap_ui.Count - 1 );
	}
}
