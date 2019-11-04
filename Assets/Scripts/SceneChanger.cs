using UnityEngine.SceneManagement;

//シーンチェンジクラス
public class SceneChanger {
	public void SceneChange( StringConstantRegistry.SCENE_NAME scene_name ) { 
		SceneManager.LoadScene( StringConstantRegistry.getSceneName( scene_name ) );	
	}
}
