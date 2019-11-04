using UnityEngine;
using UnityEngine.UI;

//ゲーム終了時テキストUIの制御クラス
public class GameEndText : MonoBehaviour {
	private Image _image = null;

	private void Awake( ) {
		_image = GetComponent< Image >( );
		Debug.Log( _image );
	}

	private void Start( ) {
		this.gameObject.SetActive( false );
	}
	
	public void ShowTimeOverText( ) {
		_image.sprite = PathDataRegistry.getSprite( PathDataRegistry.SPRITE.GAME_OVER );
		this.gameObject.SetActive( true );
	}

	public void ShowGameClearText( ) {
		_image.sprite = PathDataRegistry.getSprite( PathDataRegistry.SPRITE.GAME_CLEAR );
		this.gameObject.SetActive( true );
	}
}
