using UnityEngine;

//各シーンのマネージャーに継承させるクラス
public abstract class ScenesManager : MonoBehaviour {
	protected SceneChanger _scene_changer = new SceneChanger( );
	protected abstract void NextScene( );
	
}
