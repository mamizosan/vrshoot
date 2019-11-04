using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
	public enum ENEMY_TYPE { 
		TYPE_A,
		TYPE_B,
		TYPE_C,
		TYPE_A_BONUS_A,
	};

	public enum ENEMY_DIR { 
		LEFT,
		RIGHT,
		FORWARD,
		BACK,
		PLAYER,
	}

	protected virtual void Move( ) { }
	protected abstract void Death( );

}