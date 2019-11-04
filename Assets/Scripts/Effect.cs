using UnityEngine;

//生成してから数秒後に消えてほしいエフェクトにつけるクラス
public class Effect : MonoBehaviour {
	[ SerializeField ] private float _destroy_time = 0;

	private void Start( ) {
		Destroy( this.gameObject, _destroy_time );
	}
}
