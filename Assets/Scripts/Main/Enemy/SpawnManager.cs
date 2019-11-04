using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif
//エネミーの生成を制御するクラス
public class SpawnManager : MonoBehaviour {
	private readonly int SPAWN_ORDER_IDX = 0;
	private readonly Vector3 Y_AXIS = new Vector3( 0, 1, 0 );
	private readonly float REVERSE_DIR = 180f;

	[ System.Serializable ]
	private struct EnemySpawn {
		public Enemy.ENEMY_TYPE type;
		public SpawnAreaRegistry.ENEMY_SPAWN_AREA area;
		public Enemy.ENEMY_DIR dir;
		public float spawn_time;
		public float move_speed;
		public float delete_time;
        public float wave_width;
        public float wave_time;
		public float radius;
	};


	private List< EnemySpawn > _enemy_spawn = new List< EnemySpawn >( );
	private SpawnAreaRegistry _spawn_area_registry = null;
	private Player _player = null;
	private EnemyObserver _enemy_observer = null; 
	private float _spawn_count = 0;
	private int _normal_enemy_spawn_max_num = 0; 
	private bool _is_spawn_start = false;


	//Editor拡張用------------------------------------------------------------------------------------------
	//[ SerializeField ]が無いとEditor拡張して表示している値がUnityを再生したときにリセットされてしまう
	[ SerializeField ] private Object _enemy = null;
	[ SerializeField ] private SpawnAreaRegistry _editor_spawn_area_registry = null;
	[ SerializeField ] private Player _editor_player = null;
	[ SerializeField ] private EnemyObserver _editor_enemy_obserber = null;

	[ SerializeField ] private int _editor_max_index = 0;
	[ SerializeField ] private List< Enemy.ENEMY_TYPE > _editor_type					= new List< Enemy.ENEMY_TYPE >( );
	[ SerializeField ] private List< SpawnAreaRegistry.ENEMY_SPAWN_AREA > _editor_area  = new List< SpawnAreaRegistry.ENEMY_SPAWN_AREA >( );
	[ SerializeField ] private List< Enemy.ENEMY_DIR > _editor_dir						= new List< Enemy.ENEMY_DIR >( );
	[ SerializeField ] private List< float > _editor_spawn_time							= new List< float >( );
	[ SerializeField ] private List< float > _editor_move_speed							= new List< float >( );
	[ SerializeField ] private List< float > _editor_delete_time						= new List< float >( );
	[ SerializeField ] private List< float > _editor_wave_width							= new List< float >( );
	[ SerializeField ] private List< float > _editor_wave_time							= new List< float >( );
	[ SerializeField ] private List< float > _editor_radius								= new List< float >( );
	[ SerializeField ] private List< bool > _editor_foldout								= new List< bool >( );
	//------------------------------------------------------------------------------------------------------

	private void Awake( ) {
		//Editor拡張で使用した値を入れる--------------------------
		for ( int i = 0; i < _editor_max_index; i++ ) { 
			EnemySpawn spawn = new EnemySpawn { 
				type		= _editor_type[ i ],
				area		= _editor_area[ i ],
				dir			= _editor_dir[ i ],
				spawn_time	= _editor_spawn_time[ i ],
				move_speed	= _editor_move_speed[ i ],
				delete_time	= _editor_delete_time[ i ],
				wave_width	= _editor_wave_width[ i ],
				wave_time	= _editor_wave_time[ i ],
				radius		= _editor_radius[ i ],
		
			};
			_enemy_spawn.Add( spawn );
		}

		_spawn_area_registry = _editor_spawn_area_registry;
		_player = _editor_player;
		_enemy_observer = _editor_enemy_obserber;
		//--------------------------------------------------------
	}

	private void Start( ) {
		CheckReference( );

		_normal_enemy_spawn_max_num = _enemy_spawn.Count;
	}

	private void FixedUpdate( ) {
		SpawnCount( );
	}

	private void Update( ) {
		SpawnEnemy( );
	}

	//敵を指定した順番に生成していく
	//常に先頭にある要素で生成する
	private void SpawnEnemy( ) {
		if ( !IsSpawnNext( ) ) return;
		if ( !_is_spawn_start ) return;

		//時間になったら生成
		if ( IsSpawn( ) ) {
			GameObject spawn_area = _spawn_area_registry.getEnemySpawnArea( _enemy_spawn[ SPAWN_ORDER_IDX ].area );
			NormalEnemy enemy_body = getNormalEnemyBody( _enemy_spawn[ SPAWN_ORDER_IDX ].type );
			NormalEnemy enemy_obj = Instantiate( enemy_body, spawn_area.transform.position, Quaternion.identity );
			NormalEnemyInitialize( enemy_obj );
			_enemy_spawn.Remove( _enemy_spawn[ SPAWN_ORDER_IDX ] );

			//次があったら次の時間を入れる
			if ( IsSpawnNext( ) ) {
				_spawn_count = _enemy_spawn[ SPAWN_ORDER_IDX ].spawn_time;
			} else {
				Debug.Log( "[SpawnManager]全て生成終了" );

			}
		}
	}

	//生成するまでのカウント
	private void SpawnCount( ) {
		if ( !_is_spawn_start && !_is_spawn_start ) return;

		_spawn_count -= Time.deltaTime;	
	}

	//カウントが終わってるかどうか
	private bool IsSpawn( ) { 
		return _spawn_count <= 0;
	}

	//タイプによって生成するエネミーオブジェクトを返す
	private NormalEnemy getNormalEnemyBody( Enemy.ENEMY_TYPE enemy_type ) { 
		switch ( enemy_type ) { 
			case Enemy.ENEMY_TYPE.TYPE_A:
			case Enemy.ENEMY_TYPE.TYPE_B:
			case Enemy.ENEMY_TYPE.TYPE_C:
				return ( NormalEnemy )PathDataRegistry.getEnemy( PathDataRegistry.ENEMY.NORMAL_ENEMY );

			case Enemy.ENEMY_TYPE.TYPE_A_BONUS_A:
				return ( NormalEnemy )PathDataRegistry.getEnemy( PathDataRegistry.ENEMY.BONUS_NORMAL_ENEMY );
			
			default:
				Assert.IsNotNull( null, "[SpawnManager]予期しないエネミータイプです" );
				return null;
		}
	}

	//生成したエネミーの初期化処理
	private void NormalEnemyInitialize( NormalEnemy enemy ) {
		EnemyMove enemy_move = null;
		switch ( _enemy_spawn[ SPAWN_ORDER_IDX ].type ) {
			case Enemy.ENEMY_TYPE.TYPE_A:
			case Enemy.ENEMY_TYPE.TYPE_A_BONUS_A:
				enemy_move = new StraightMove( _enemy_spawn[ SPAWN_ORDER_IDX ].move_speed, enemy.gameObject, getNormalEnemyDir( ) );
				break;
		
			case Enemy.ENEMY_TYPE.TYPE_B:
				enemy_move = new WaveMove( _enemy_spawn[ SPAWN_ORDER_IDX ].move_speed, enemy.gameObject, getNormalEnemyDir( ), 
										   _enemy_spawn[ SPAWN_ORDER_IDX ].wave_width, _enemy_spawn[ SPAWN_ORDER_IDX ].wave_time );
				break;

			case Enemy.ENEMY_TYPE.TYPE_C:
				enemy_move = new CircleMove( _enemy_spawn[ SPAWN_ORDER_IDX ].move_speed, enemy.gameObject, getNormalEnemyDir( ), _enemy_spawn[ SPAWN_ORDER_IDX ].radius );
				break;
		
			default:
				Assert.IsNotNull( null, "[Enemy]予期しない敵の種類が入っています" );
				break;
		}
		enemy.Initialize( _enemy_spawn[ SPAWN_ORDER_IDX ].type, enemy_move, _enemy_observer, _enemy_spawn[ SPAWN_ORDER_IDX ].delete_time );
	}

	private Vector3 getNormalEnemyDir( ) { 
		switch ( _enemy_spawn[ SPAWN_ORDER_IDX ].dir ) { 
			case Enemy.ENEMY_DIR.LEFT:
				return Vector3.left;

			case Enemy.ENEMY_DIR.RIGHT:
				return Vector3.right;

			case Enemy.ENEMY_DIR.FORWARD:
				return Vector3.forward;

			case Enemy.ENEMY_DIR.BACK:
				return Vector3.back;

			case Enemy.ENEMY_DIR.PLAYER:
				Vector3 origin_pos = _spawn_area_registry.getEnemySpawnArea( _enemy_spawn[ SPAWN_ORDER_IDX ].area ).transform.position;
				return Mathematics.VectorDirection( _player.transform.position, origin_pos );

			default:
				Assert.IsNotNull( null, "[SpawnManager]予期しない敵の方向が入っています" );
				return Vector3.zero;
		}
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _spawn_area_registry, "[SpawnManger]SpawnAreaRegistryの参照がありません" );
		if ( _enemy_spawn.Count == 0 ) { 
			Assert.IsNotNull( null, "[SpawnManger]NormalEnemyの要素がありません" );
		}
		Assert.IsNotNull( _player, "[SpawnManger]Playerの参照がありません" );
		Assert.IsNotNull( _enemy_observer, "[SpawnManager]EnemyObserberの参照がありません" );
	}

	//次に生成するものがあるかどうか
	public bool IsSpawnNext( ) { 
		return	_enemy_spawn.Count != 0;
	}

	//ボスエネミー生成
	public void SpawnBoss( ) {
		BossEnemy boss_enmey = ( BossEnemy )PathDataRegistry.getEnemy( PathDataRegistry.ENEMY.BOSS_ENEMY );
		GameObject spawn_area = _spawn_area_registry.getBossSpawnArea( );
		BossEnemy boss_obj = Instantiate( boss_enmey, spawn_area.transform.position, Quaternion.AngleAxis( REVERSE_DIR, Y_AXIS ) );
		boss_obj.Initialize( _enemy_observer );
	}

	public int getNormalEnemySpawnMaxNum( ) {
		return _normal_enemy_spawn_max_num;
	}

	//指定してあるエネミー生成を開始する
	public void SpawnStart( ) { 
		if ( _is_spawn_start ) { 
			Debug.Log( "エネミー生成はすでにスタートしています" );
			return;
		}

		_is_spawn_start = true;	
		_spawn_count = _enemy_spawn[ SPAWN_ORDER_IDX ].spawn_time;
	}

	//エネミー生成を停止する
	public void SpawnStop( ) { 
		_is_spawn_start = false;
	}


	#if UNITY_EDITOR
	//Editor拡張クラス-----------------------------------------------------------------------------------------------------------------------------------------------------
	[ CustomEditor( typeof( SpawnManager ) ) ]
	public class EditorExpansion : Editor {
		private SpawnManager _target;
		
	
		private void Awake( ) {
			_target = target as SpawnManager;
			//ここで変数をいれて初期化するとインスペクターで選択時に毎回呼ばれる
		}
	
		public override void OnInspectorGUI( ) {
			ButtonLayout( );
			SpawnStatusLayout( );
			ReferenceLayout( );
		}
	
		private void ButtonLayout( ) { 
			if ( GUILayout.Button( "追加" ) ) { 
				_target._editor_max_index++;
				AddList( );
			}
	
			if ( GUILayout.Button( "削除" ) ) { 
				_target._editor_max_index--;
				if ( _target._editor_max_index < 0 ) { 
					_target._editor_max_index = 0;
				}
				RemoveList( );
			}
		}
	
		private void SpawnStatusLayout( ) { 
			for ( int i = 0; i < _target._editor_max_index; i++ ) {
				EditorGUILayout.LabelField( "---------------------------------------------------------" );
				if ( _target._editor_foldout[ i ]	  = EditorGUILayout.Foldout( _target._editor_foldout[ i ], "敵" + i.ToString( ) ) ) {
					_target._editor_type[ i ]		  = ( Enemy.ENEMY_TYPE )EditorGUILayout.EnumPopup( "敵のタイプ" + i.ToString( ), _target._editor_type[ i ] );
					_target._editor_area[ i ]		  = ( SpawnAreaRegistry.ENEMY_SPAWN_AREA )EditorGUILayout.EnumPopup( "出現エリア" + i.ToString( ), _target._editor_area[ i ] );
					_target._editor_dir[ i ]		  = ( Enemy.ENEMY_DIR )EditorGUILayout.EnumPopup( "敵の初期方向" + i.ToString( ), _target._editor_dir[ i ] );
					_target._editor_spawn_time[ i ]	  = EditorGUILayout.FloatField( "出現時間" + i.ToString( ), _target._editor_spawn_time[ i ] );
					_target._editor_move_speed[ i ]	  = EditorGUILayout.FloatField( "移動スピード" + i.ToString( ), _target._editor_move_speed[ i ] );
					_target._editor_delete_time[ i ]  = EditorGUILayout.FloatField( "自動削除時間" + i.ToString( ), _target._editor_delete_time[ i ] );

					if ( _target._editor_type[ i ] == Enemy.ENEMY_TYPE.TYPE_B ) {
						_target._editor_wave_width[ i ]   = EditorGUILayout.FloatField( "ウェーブの横幅" + i.ToString( ), _target._editor_wave_width[ i ] );
						_target._editor_wave_time[ i ]    = EditorGUILayout.FloatField( "１往復にかかる時間" + i.ToString( ), _target._editor_wave_time[ i ] );
					}

					if ( _target._editor_type[ i ] == Enemy.ENEMY_TYPE.TYPE_C ) { 
						_target._editor_radius[ i ] = EditorGUILayout.FloatField( "円の半径" + i.ToString( ), _target._editor_radius[ i ] );
					}
				}
				EditorGUILayout.LabelField( "---------------------------------------------------------" );
				EditorGUILayout.Space( );
				EditorGUILayout.Space( );
			}
		}

		private void ReferenceLayout( ) { 
			EditorGUILayout.Space( );
			_target._editor_spawn_area_registry = ( SpawnAreaRegistry )EditorGUILayout.ObjectField( "シーン上のSpawnAreaRegistry", _target._editor_spawn_area_registry, typeof( SpawnAreaRegistry ), true );
			_target._editor_player				= ( Player )EditorGUILayout.ObjectField( "シーン上のPlayer", _target._editor_player, typeof( Player ), true );
			_target._editor_enemy_obserber		= ( EnemyObserver )EditorGUILayout.ObjectField( "シーン上のEnemyObserver", _target._editor_enemy_obserber, typeof( EnemyObserver ), true );
		}
	
		private void AddList( ) {
			_target._editor_type.Add( Enemy.ENEMY_TYPE.TYPE_A );
			_target._editor_area.Add( SpawnAreaRegistry.ENEMY_SPAWN_AREA.AREA_1 );
			_target._editor_dir.Add( Enemy.ENEMY_DIR.LEFT );
			_target._editor_spawn_time.Add( 0 );
			_target._editor_move_speed.Add( 0 );
			_target._editor_delete_time.Add( 0 );
			_target._editor_wave_width.Add( 1 );
			_target._editor_wave_time.Add( 1 );
			_target._editor_radius.Add( 1 );
			_target._editor_foldout.Add( false );
		}
	
		private void RemoveList( ) { 
			if ( _target._editor_max_index <= 0 ) return;
			int remove_index = _target._editor_max_index - 1;
	
			_target._editor_type.Remove			( _target._editor_type[ remove_index ] );
			_target._editor_area.Remove			( _target._editor_area[ remove_index ] );
			_target._editor_dir.Remove			( _target._editor_dir[ remove_index ] );
			_target._editor_spawn_time.Remove	( _target._editor_spawn_time[ remove_index ] );
			_target._editor_move_speed.Remove	( _target._editor_move_speed[ remove_index ] );
			_target._editor_delete_time.Remove  ( _target._editor_delete_time [ remove_index ] );
			_target._editor_wave_width.Remove	( _target._editor_wave_width[ remove_index ] );
			_target._editor_wave_time.Remove	( _target._editor_wave_time[ remove_index ] );
			_target._editor_radius.Remove		( _target._editor_radius[ remove_index ] );
			_target._editor_foldout.Remove		( _target._editor_foldout[ remove_index ] );
		}
	
	}
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
	#endif
}

//構造体をEditorで日本語表示したい場合は、メンバを抽出してそれの配列を表示する
//もし[ SerializeField ]をで前にやっていた場合は一度消して値をリセットしないと保持されてしまい、Editor拡張時に不具合がでるかも
