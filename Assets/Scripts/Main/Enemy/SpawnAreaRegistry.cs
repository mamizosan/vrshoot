using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnAreaRegistry : MonoBehaviour {
	[ SerializeField ] private GameObject[ ] _enemy_spawn_area = new GameObject[ 1 ];
	[ SerializeField ] private GameObject _boss_spawn_area = null;
	
	public enum ENEMY_SPAWN_AREA { 
		AREA_1,
		AREA_2,
		AREA_3,
		AREA_4,
		AREA_5,
		AREA_6,
		AREA_7,
		AREA_8,
		AREA_9,
		AREA_10,
		AREA_11,
		AREA_12,
		AREA_13,
		AREA_14,
		AREA_15,
		AREA_16,
		AREA_17,
		AREA_18,
	};


	public GameObject getEnemySpawnArea( ENEMY_SPAWN_AREA enemy_spawn_area ) {
		if ( ( int )enemy_spawn_area >= _enemy_spawn_area.Length ) { 
			Assert.IsNotNull( null, "[SpawnAreaRegistry]取得しようとしているAreaが配列の最大値を超えています。" );
			return null; 
		}
		return _enemy_spawn_area[ ( int )enemy_spawn_area ];
	}

	public GameObject getBossSpawnArea( ) { 
		return _boss_spawn_area;	
	}

}
