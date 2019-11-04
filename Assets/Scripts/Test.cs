using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	[ SerializeField ] private GazeController _gaze_controller = null;
	[ SerializeField ] private TextMesh[ ] _debug_button_texts = new TextMesh[ 1 ];
	[ SerializeField ] private TextMesh _debug_pos_text = null;
	[ SerializeField ] private TextMesh _debug_player_pos_text = null;
	[ SerializeField ] private TextMesh _debug_player_dir_text = null;
	[ SerializeField ] private TextMesh _debug_gaze_dir_text = null;
	[ SerializeField ] private TextMesh _debug_ray_hit_text = null;
	[ SerializeField ] private TextMesh _debug_hit_object_text = null;
	[ SerializeField ] private GameObject _player = null;

	private Controller _controller = null;
	private int[ ] _button_input_count = null;
	private string[ ] _button_name = null;
	private bool _is_show_debug = true;

	private void Awake( ) {
		_controller = Controller.getInstance( );
		_button_input_count = new int[ _debug_button_texts.Length ];
		_button_name = new string [ _debug_button_texts.Length ];
	}

	private void Start( ) {
		for ( int i = 0; i < _button_input_count.Length; i++ ) { 
			_button_input_count[ i ] = 0;	
		}

		_button_name[ ( int )Controller.INPUT_TYPE.BACK_BUTTON            ] = "バックボタン:";
		_button_name[ ( int )Controller.INPUT_TYPE.HOME_BUTTON_LONG_PRESS ] = "ホームボタン長押し:";
		_button_name[ ( int )Controller.INPUT_TYPE.TRIGGER                ] = "トリガー長押し:";
		_button_name[ ( int )Controller.INPUT_TYPE.TRIGGER_DOWN           ] = "トリガー押し込み:";
		_button_name[ ( int )Controller.INPUT_TYPE.TRIGGER_UP             ] = "トリガー離した:";
		_button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_CLICK        ] = "タッチパッドクリック:";
		_button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_UP    ] = "タッチパッドスクロール上:";
		_button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_DOWN  ] = "タッチパッドスクロール下:";
		_button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_LEFT  ] = "タッチパッドスクロール左:";
		_button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_RIGHT ] = "タッチパッドスクロール右:";


		_debug_button_texts[ ( int )Controller.INPUT_TYPE.BACK_BUTTON            ].text = _button_name[ ( int )Controller.INPUT_TYPE.BACK_BUTTON            ] + _button_input_count[ ( int )Controller.INPUT_TYPE.BACK_BUTTON            ];
		_debug_button_texts[ ( int )Controller.INPUT_TYPE.HOME_BUTTON_LONG_PRESS ].text = _button_name[ ( int )Controller.INPUT_TYPE.HOME_BUTTON_LONG_PRESS ] + _button_input_count[ ( int )Controller.INPUT_TYPE.HOME_BUTTON_LONG_PRESS ];
		_debug_button_texts[ ( int )Controller.INPUT_TYPE.TRIGGER                ].text = _button_name[ ( int )Controller.INPUT_TYPE.TRIGGER                ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TRIGGER                ];
		_debug_button_texts[ ( int )Controller.INPUT_TYPE.TRIGGER_DOWN           ].text = _button_name[ ( int )Controller.INPUT_TYPE.TRIGGER_DOWN           ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TRIGGER_DOWN           ];
		_debug_button_texts[ ( int )Controller.INPUT_TYPE.TRIGGER_UP             ].text = _button_name[ ( int )Controller.INPUT_TYPE.TRIGGER_UP             ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TRIGGER_UP             ];
		_debug_button_texts[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_CLICK        ].text = _button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_CLICK        ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_CLICK        ];
		_debug_button_texts[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_UP    ].text = _button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_UP    ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_UP    ];
		_debug_button_texts[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_DOWN  ].text = _button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_DOWN  ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_DOWN  ];
		_debug_button_texts[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_LEFT  ].text = _button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_LEFT  ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_LEFT  ];
		_debug_button_texts[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_RIGHT ].text = _button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_RIGHT ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_RIGHT ];

		_debug_pos_text.text = "Xpos:" + _controller.getControllerTouchpadPos( ).x + 
			                   "\nYpos:" + _controller.getControllerTouchpadPos( ).y;

		_debug_player_pos_text.text = "PlayerXpos:" + _player.transform.position.x + 
			                          "\nPlayerYpos:" + _player.transform.position.y + 
									  "\nPlayerZpos:" + _player.transform.position.z;

		_debug_player_dir_text.text = "PlayerXdir:" + _player.transform.eulerAngles.x + 
			                          "\nPlayerYdir:" + _player.transform.eulerAngles.y + 
									  "\nPlayerZdir:" + _player.transform.eulerAngles.z;

		_debug_gaze_dir_text.text = "GazeXdir:" + _gaze_controller.GetDirection( ).x + 
			                          "\nGazeYdir:" + _gaze_controller.GetDirection( ).y + 
									  "\nGazeZdir:" + _gaze_controller.GetDirection( ).z;
		
		_debug_ray_hit_text.text = _gaze_controller.getIsHit( ).ToString( );

		if ( _gaze_controller.getLockOnObject( ) != null ) { 
			_debug_hit_object_text.text = _gaze_controller.getLockOnObject( ).ToString( );
		} else { 
			_debug_hit_object_text.text = "NULL";
		}
	}


	// Update is called once per frame
	private void Update( ) {
		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.BACK_BUTTON ) ) { 
			_is_show_debug = !_is_show_debug;
			ShowDebug( );
		}

		DebugData( );
	}

	private void DebugData( ) { 
		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.BACK_BUTTON ) ) {
			_button_input_count[ ( int )Controller.INPUT_TYPE.BACK_BUTTON ]++;
			_debug_button_texts[ ( int )Controller.INPUT_TYPE.BACK_BUTTON ].text = _button_name[ ( int )Controller.INPUT_TYPE.BACK_BUTTON ] + _button_input_count[ ( int )Controller.INPUT_TYPE.BACK_BUTTON ];
		}

		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.HOME_BUTTON_LONG_PRESS ) ) {
			_button_input_count[ ( int )Controller.INPUT_TYPE.HOME_BUTTON_LONG_PRESS ]++;
			_debug_button_texts[ ( int )Controller.INPUT_TYPE.HOME_BUTTON_LONG_PRESS ].text = _button_name[ ( int )Controller.INPUT_TYPE.HOME_BUTTON_LONG_PRESS ] + _button_input_count[ ( int )Controller.INPUT_TYPE.HOME_BUTTON_LONG_PRESS ];
		}

		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.TRIGGER ) ) {
			_button_input_count[ ( int )Controller.INPUT_TYPE.TRIGGER ]++;
			_debug_button_texts[ ( int )Controller.INPUT_TYPE.TRIGGER ].text = _button_name[ ( int )Controller.INPUT_TYPE.TRIGGER ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TRIGGER ];
		}
	
		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.TRIGGER_DOWN ) ) {
			_button_input_count[ ( int )Controller.INPUT_TYPE.TRIGGER_DOWN ]++;
			_debug_button_texts[ ( int )Controller.INPUT_TYPE.TRIGGER_DOWN ].text = _button_name[ ( int )Controller.INPUT_TYPE.TRIGGER_DOWN ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TRIGGER_DOWN ];
		}

		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.TRIGGER_UP ) ) {
			_button_input_count[ ( int )Controller.INPUT_TYPE.TRIGGER_UP ]++;
			_debug_button_texts[ ( int )Controller.INPUT_TYPE.TRIGGER_UP ].text = _button_name[ ( int )Controller.INPUT_TYPE.TRIGGER_UP ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TRIGGER_UP ];
		}

		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.TOUCH_PAD_CLICK ) ) {
			_button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_CLICK ]++;
			_debug_button_texts[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_CLICK ].text = _button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_CLICK ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_CLICK ];
		}

		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_UP ) ) { 
			_button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_UP ]++;
			_debug_button_texts[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_UP ].text = _button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_UP ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_UP ];
		}

		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_DOWN ) ) {
			_button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_DOWN ]++;
			_debug_button_texts[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_DOWN ].text = _button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_DOWN ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_DOWN ];
		}

		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_LEFT ) ) {
			_button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_LEFT ]++;
			_debug_button_texts[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_LEFT ].text = _button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_LEFT ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_LEFT ];
		}

		if ( _controller.getCotrollerInput( Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_RIGHT ) ) {
			_button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_RIGHT ]++;
			_debug_button_texts[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_RIGHT ].text = _button_name[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_RIGHT ] + _button_input_count[ ( int )Controller.INPUT_TYPE.TOUCH_PAD_SCREEL_RIGHT ];
		}

		_debug_pos_text.text = "Xpos:" + _controller.getControllerTouchpadPos( ).x + 
			                   "\nYpos:" + _controller.getControllerTouchpadPos( ).y;

		_debug_player_pos_text.text = "PlayerXpos:" + _player.transform.position.x + 
			                          "\nPlayerYpos:" + _player.transform.position.y + 
									  "\nPlayerZpos:" + _player.transform.position.z;

		_debug_player_dir_text.text = "PlayerXdir:" + _player.transform.eulerAngles.x + 
			                          "\nPlayerYdir:" + _player.transform.eulerAngles.y + 
									  "\nPlayerZdir:" + _player.transform.eulerAngles.z;

		_debug_gaze_dir_text.text = "GazeXdir:" + _gaze_controller.GetDirection( ).x + 
			                          "\nGazeYdir:" + _gaze_controller.GetDirection( ).y + 
									  "\nGazeZdir:" + _gaze_controller.GetDirection( ).z;

		_debug_ray_hit_text.text = _gaze_controller.getIsHit( ).ToString( );

		if ( _gaze_controller.getLockOnObject( ) != null ) { 
			_debug_hit_object_text.text = _gaze_controller.getLockOnObject( ).ToString( );
		} else { 
			_debug_hit_object_text.text = "NULL";
		}
	}

	private void ShowDebug( ) {
		for( int i = 0; i < _debug_button_texts.Length; i++ ) { 
			_debug_button_texts[ i ].gameObject.SetActive( _is_show_debug );
		}

		_debug_pos_text.gameObject.SetActive( _is_show_debug );
		_debug_player_pos_text.gameObject.SetActive( _is_show_debug );
		_debug_player_dir_text.gameObject.SetActive( _is_show_debug );
		_debug_gaze_dir_text.gameObject.SetActive( _is_show_debug );
		_debug_ray_hit_text.gameObject.SetActive( _is_show_debug );
		_debug_hit_object_text.gameObject.SetActive( _is_show_debug );
	}
}



//NULLだったら処理しないという処理を書く