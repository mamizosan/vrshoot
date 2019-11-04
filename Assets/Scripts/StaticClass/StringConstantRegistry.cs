
//String型の定数を纏めておくクラス
public static class StringConstantRegistry {
	public enum TAG { 
		BULLET,
		BOSS,
		ENEMY_OBSERVER,
		SCORE_MANAGER,
		WEAK_POINT,
	}

	public enum LAYER { 
		JUDGMENT_AGAINST_GAZE,	
	}

	public enum SCENE_NAME { 
		START,
		MAIN,
		RESULT
	}

	public enum ANIMATION_NAME { 
		BOSS_APPEAR,
		BOSS_DIES,
		POINT_TEXT,
	}

	public enum ANIMATION_FLAG { 
		BOSS_DEATH,	
	}

	public static string getTag( TAG tag ) { 
		switch ( tag ) {
			case TAG.BULLET:
				return "Bullet";

			case TAG.BOSS:
				return "Boss";

			case TAG.ENEMY_OBSERVER:
				return "EnemyObserver";

			case TAG.SCORE_MANAGER:
				return "ScoreManager";

			case TAG.WEAK_POINT:
				return "WeakPoint";

			default:
				return null;
		}
	}

	public static string getLayer( LAYER layer ) { 
		switch ( layer ) { 
			case LAYER.JUDGMENT_AGAINST_GAZE:
				return "JudgmentAgainstGaze";

			default:
				return null;
		}	
	}

	public static string getSceneName( SCENE_NAME scene_name ) { 
		switch ( scene_name ) { 
			case SCENE_NAME.START:
				return "Start";

			case SCENE_NAME.MAIN:
				return "Main";

			case SCENE_NAME.RESULT:
				return "Result";

			default:
				return null;
		}
	}

	public static string getAnimationName( ANIMATION_NAME animation_name ) { 
		switch ( animation_name ) { 
			case ANIMATION_NAME.BOSS_APPEAR:
				return "BossAppear";
	
			case ANIMATION_NAME.BOSS_DIES:
				return "BossDies";

			case ANIMATION_NAME.POINT_TEXT:
				return "PointText";
	
			default:
				return null;
		}	
	}

	public static string getAnimationFlag( ANIMATION_FLAG animation_flag ) { 
		switch ( animation_flag ) { 
			case ANIMATION_FLAG.BOSS_DEATH:
				return "BossDeath";

			default:
				return null;
		}
	}
}
