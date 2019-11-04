
//リザルトシーン進行の制御クラス
public class ResultManager : ScenesManager {
    Controller controller = null;

	private void Awake( ) {
		controller = Controller.getInstance( );
	}

	private void Update( ) {
        if ( controller.getCotrollerInput( Controller.INPUT_TYPE.TRIGGER_DOWN ) == true ) {
            NextScene( );
        }
	}

    protected override void NextScene( ) {
        _scene_changer.SceneChange( StringConstantRegistry.SCENE_NAME.START );
    }


}
