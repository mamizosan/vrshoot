using UnityEngine;
using UnityEngine.Assertions;

//ポイントテキストUIの制御クラス
public class PointText : MonoBehaviour {
	private readonly float BIG_TEXT_MAGNIFICATION = 10f;

	SpriteRenderer _sprite_renderer = null;
	Anim _anim = null;

	private void Awake( ) {
		_sprite_renderer = GetComponent< SpriteRenderer >( );
		_anim = GetComponent< Anim >( );
	}

	private void Start( ) {
		CheckReference( );
	}

	private void Update( ) {
		if ( _anim.IsAnimationEnd( StringConstantRegistry.ANIMATION_NAME.POINT_TEXT ) ) { 
			Destroy( this.gameObject );	
		}
	}


	private void CheckReference( ) { 
		Assert.IsNotNull( _anim, "[PointText]Animの参照がありません" );
	}

	public void Initialize( Sprite sprite, Vector3 player_pos, bool is_big ) {
		_sprite_renderer.sprite = sprite;
		transform.forward = -Mathematics.VectorDirection( player_pos, transform.position );	//なぜか反対こうしないと反対になっている

		if ( is_big ) { 
			transform.localScale = transform.localScale * BIG_TEXT_MAGNIFICATION;
		}
	}
}
