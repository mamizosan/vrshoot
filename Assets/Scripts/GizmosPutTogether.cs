using UnityEngine;
using UnityEngine.Assertions;

//Gizumo表示クラス
public class GizmosPutTogether : MonoBehaviour {
	[ SerializeField ] private GameObject[ ] _spawn_areas = new GameObject[ 1 ];
	[ SerializeField ] private GameObject[ ] _enemy_destory_areas = new GameObject[ 1 ];

	private void OnDrawGizmos( ) {
		//CheckReference( );

		SpawnAreaGizmos( );
		EnemyDestoryAreaGizmos( );
	}

	private void SpawnAreaGizmos( ) {
		Color wire_cube_color = Color.black;
		Color cube_color = new Color( 1, 0, 0, 0.5f );	//半透明の赤

		for( int i = 0; i < _spawn_areas.Length; i++ ) {
			Gizmos.color = wire_cube_color;
			Gizmos.DrawWireCube( _spawn_areas[ i ].transform.position, _spawn_areas[ i ].transform.localScale );

			Gizmos.color = cube_color;
			Gizmos.DrawCube( _spawn_areas[ i ].transform.position, _spawn_areas[ i ].transform.localScale );
		}
	}

	private void EnemyDestoryAreaGizmos( ) {
		Color wire_cube_color = Color.black;
		Color cube_color = new Color( 0, 0, 1, 0.5f );	//半透明の青

		for( int i = 0; i < _enemy_destory_areas.Length; i++ ) {
			Gizmos.color = wire_cube_color;
			Gizmos.DrawWireCube( _enemy_destory_areas[ i ].transform.position, _enemy_destory_areas[ i ].transform.localScale );

			Gizmos.color = cube_color;
			Gizmos.DrawCube( _enemy_destory_areas[ i ].transform.position, _enemy_destory_areas[ i ].transform.localScale );
		}
	}

	private void CheckReference( ) {
		for ( int i = 0; i < _spawn_areas.Length; i ++ ) {
			Assert.IsNotNull( _spawn_areas[ i ], "[GizmosPutTogether]SpawnArea" + i + "の参照がありません" );
		}

		for ( int i = 0; i < _enemy_destory_areas.Length; i ++ ) {
			Assert.IsNotNull( _enemy_destory_areas[ i ], "[GizmosPutTogether]EnemyDestoryArea" + i + "の参照がありません" );
		}
	}

}


//NULLだったら処理しないという処理を書く
//EnemyDestoryAreaのGizumoは角度まで対応しないからどうしたものか