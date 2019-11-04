using UnityEngine;
using UnityEngine.Assertions;

//アニメーションの制御クラス
public class Anim : MonoBehaviour {
	private Animator _animator = null;

	private void Awake( ) {
		_animator = GetComponent< Animator >( );
	}

	private void Start( ) {
		CheckReference( );
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _animator, "[BossEnemyAnimaton]Animatorの参照がありません" );
	}

	//指定したアニメーション開始
	public void AnimationStart( StringConstantRegistry.ANIMATION_FLAG anim_flag ) {
		_animator.SetTrigger( StringConstantRegistry.getAnimationFlag( anim_flag ) );
	}

	//指定したアニメーションが終了しているかどうか
	public bool IsAnimationEnd( StringConstantRegistry.ANIMATION_NAME anim_name ) {
		AnimatorStateInfo anim_info = _animator.GetCurrentAnimatorStateInfo( 0 );

		return ( ( anim_info.normalizedTime >= 1.0f ) && 
				 ( anim_info.IsName( StringConstantRegistry.getAnimationName( anim_name ) ) ) );
	}
	
}
