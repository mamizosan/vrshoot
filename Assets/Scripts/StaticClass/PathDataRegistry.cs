using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

//パスから取得するデータを取得・保持しておくクラス
public static class PathDataRegistry {
	public enum SE { 
		BULLET,
		ENGINE,
		EXPLOSION,
		LOCK_ON,
	}

	public enum EFFECT { 
		NORMAL_ENEMY_EXPLOSION,
		BOSS_ENMEY_EXPLOSION,
		BOSS_ENMEY_WEAK_POINT_EXPLOSION,
	}

	public enum SPRITE {
		COUNT_DONW_START,
		COUNT_DOWN_NUMBER_1,
		COUNT_DOWN_NUMBER_2,
		COUNT_DOWN_NUMBER_3,
		COUNT_DOWN_NUMBER_4,
		COUNT_DOWN_NUMBER_5,
		GAME_OVER,
		GAME_CLEAR,
		BONUS_TIME,
		POINT_20,
		POINT_40,
		POINT_100,
		POINT_200,
		LOCK_ON_DOING,
		LOCK_ON_DONE,
	}

	public enum ENEMY {
		NORMAL_ENEMY,
		BONUS_NORMAL_ENEMY,
		BOSS_ENEMY,
	}

	public enum BULLET { 
		BULLET,	
	}

	public enum IMAGE { 
		ENEMY_MINIMAP_UI,	
	}

	private static Dictionary< SE, AudioClip > _se = new Dictionary< SE, AudioClip >( );
	private static Dictionary< EFFECT, GameObject > _effect = new Dictionary< EFFECT, GameObject >( );
	private static Dictionary< SPRITE, Sprite > _sprite = new Dictionary< SPRITE, Sprite >( );
	private static Dictionary< ENEMY, Enemy > _enemy = new Dictionary< ENEMY, Enemy >( );
	private static Dictionary< BULLET, Bullet > _bullet = new Dictionary< BULLET, Bullet >( );
	private static Dictionary< IMAGE, Image > _image = new Dictionary< IMAGE, Image >( );

	private static PointText _point_text = null;


	//読み込んでいないSEを読み込む
	private static void LoadSE( SE se ) {
		AudioClip se_data = null;
		string path = "";

		switch ( se ) { 
			case SE.BULLET:
				path = "SE/Bullet";
				break;

			case SE.ENGINE:
				path = "SE/Engine";
				break;

			case SE.EXPLOSION:
				path = "SE/Explosion";
				break;

			case SE.LOCK_ON:
				path = "SE/LockOn";
				break;

			default:
				Assert.IsNotNull( null, "[PathDataRegistry]読み込みたいSEが存在しません" );
				break;
		}

		se_data = Resources.Load< AudioClip >( path );
		_se.Add( se, se_data );
	}

	//読み込んでいないエフェクトを読み込む
	private static void LoadEffect( EFFECT effect ) {
		GameObject effect_data = null;
		string path = "";

		switch ( effect ) { 
			case EFFECT.NORMAL_ENEMY_EXPLOSION:
				path = "Prefabs/ExplosionEffect/NormalEnemyExplosion";
				break;

			case EFFECT.BOSS_ENMEY_EXPLOSION:
				path = "Prefabs/ExplosionEffect/BossEnemyExplosion";
				break;

			case EFFECT.BOSS_ENMEY_WEAK_POINT_EXPLOSION:
				path = "Prefabs/ExplosionEffect/BossEnemyWeakPointExplosion";
				break;

			default:
				Assert.IsNotNull( null, "[PathDataRegistry]読み込みたいEffectが存在しません" );
				break;
		}
		 
		effect_data = Resources.Load< GameObject >( path );
		_effect.Add( effect, effect_data );
	}

	//読み込んでいないスプライト化した画像を読み込む
	private static void LoadSprite( SPRITE sprite ) {
		Sprite sprite_data = null;
		string path = "";

		switch ( sprite ) { 
			case SPRITE.COUNT_DONW_START:
				path = "Text/MainScenes/CountDown/CountDown_Start";
				break;

			case SPRITE.COUNT_DOWN_NUMBER_1:
				path = "Text/MainScenes/CountDown/CountDown_1";
				break;

			case SPRITE.COUNT_DOWN_NUMBER_2:
				path = "Text/MainScenes/CountDown/CountDown_2";
				break;

			case SPRITE.COUNT_DOWN_NUMBER_3:
				path = "Text/MainScenes/CountDown/CountDown_3";
				break;

			case SPRITE.COUNT_DOWN_NUMBER_4:
				path = "Text/MainScenes/CountDown/CountDown_4";
				break;

			case SPRITE.COUNT_DOWN_NUMBER_5:
				path = "Text/MainScenes/CountDown/CountDown_5";
				break;

			case SPRITE.GAME_OVER:
				path = "Text/MainScenes/GameOverText";
				break;

			case SPRITE.GAME_CLEAR:
				path = "Text/MainScenes/GameClearText2";
				break;

			case SPRITE.BONUS_TIME:
				path = "Text/MainScenes/BonusTimeText";
				break;

			case SPRITE.POINT_20:
				path = "Text/MainScenes/Point/20PointText";
				break;

			case SPRITE.POINT_40:
				path = "Text/MainScenes/Point/40PointText";
				break;

			case SPRITE.POINT_100:
				path = "Text/MainScenes/Point/100PointText";
				break;

			case SPRITE.POINT_200:
				path = "Text/MainScenes/Point/200PointText";
				break;

			default:
				Assert.IsNotNull( null, "[PathDataRegistry]読み込みたいSpriteが存在しません" );
				break;
		}
		 
		sprite_data = Resources.Load< Sprite >( path );
		_sprite.Add( sprite, sprite_data );
	}
	
	//読み込んでいないエネミーを読み込む
	private static void LoadEnemy( ENEMY enemy ) {
		Enemy enemy_data = null;
		string path = "";

		switch ( enemy ) { 
			case ENEMY.NORMAL_ENEMY:
				path = "Prefabs/Enemy/NormalEnemy";
				break;

			case ENEMY.BONUS_NORMAL_ENEMY:
				path = "Prefabs/Enemy/BonusNormalEnemy";
				break;

			case ENEMY.BOSS_ENEMY:
				path = "Prefabs/Enemy/BossEnemy2";
				break;

			default:
				Assert.IsNotNull( null, "[PathDataRegistry]読み込みたいEnemyが存在しません" );
				break;
		}
		 
		enemy_data = Resources.Load< Enemy >( path );
		_enemy.Add( enemy, enemy_data );
	}


	private static void LoadBullet( BULLET bullet ) { 
		Bullet bullet_data = null;
		string path = "";

		switch ( bullet ) { 
			case BULLET.BULLET:
				path = "Prefabs/Bullet";
				break;

			default:
				Assert.IsNotNull( null, "[PathDataRegistry]読み込みたいBulletが存在しません" );
				break;
		}
		 
		bullet_data = Resources.Load< Bullet >( path );
		_bullet.Add( bullet, bullet_data );
	}

	private static void LoadImage( IMAGE image ) { 
		Image image_data = null;
		string path = "";

		switch ( image ) { 
			case IMAGE.ENEMY_MINIMAP_UI:
				path = "Prefabs/UI/MiniMpaHeightLineUI";
				break;

			default:
				Assert.IsNotNull( null, "[PathDataRegistry]読み込みたいImageが存在しません" );
				break;
		}
		 
		image_data = Resources.Load< Image >( path );
		_image.Add( image, image_data );
	}

	private static void LoadPointText( ) { 
		string path = "Prefabs/UI/PointText";
		_point_text = Resources.Load< PointText >( path );
	}
	

	public static AudioClip getSE( SE se ) { 
		if ( !_se.ContainsKey( se ) ) { 
			LoadSE( se );
		}

		return _se[ se ];
	}

	public static GameObject getEffect( EFFECT effect ) { 
		if ( !_effect.ContainsKey( effect ) ) { 
			LoadEffect( effect );
		}

		return _effect[ effect ];
	}

	public static Sprite getSprite( SPRITE sprite ) { 
		if ( !_sprite.ContainsKey( sprite ) ) { 
			LoadSprite( sprite );
		}

		return _sprite[ sprite ];
	}

	public static Enemy getEnemy( ENEMY enemy ) { 
		if ( !_enemy.ContainsKey( enemy ) ) { 
			LoadEnemy( enemy );
		}

		return _enemy[ enemy ];
	}

	public static Bullet getBullet( BULLET bullet ) { 
		if ( !_bullet.ContainsKey( bullet ) ) { 
			LoadBullet( bullet );	
		}	

		return _bullet[ bullet ];
	}

	public static Image getImage( IMAGE image ) { 
		if ( !_image.ContainsKey( image ) ) { 
			LoadImage( image );
		}

		return _image[ image ];
	}

	public static PointText getPointText( ) { 
		if ( _point_text == null ) { 
			LoadPointText( );	
		}	

		return _point_text;
	}
}

//Flyweightパターン(のつもり)
//このクラスによって、Resources.Loadが一回で済む。一回ロードしたものの参照を返している。
//オブジェクト指向的にこのやり方が合ってるか分からない
//エネミーやカウントダウンに関しては[SerializeField]とあまり変わらないけど統一するために

//このやり方だと、使い終わったデータのメモリが解放されずにいつでも残り続ける。逆に言うと一回しかロードしないのでそれ以上は増えることは無いし2週目以降も使いまわせる。どっちがいいのか
//↑メモリ解放機能をつける？でも解放するタイミングとかどこでやるとかがわからん