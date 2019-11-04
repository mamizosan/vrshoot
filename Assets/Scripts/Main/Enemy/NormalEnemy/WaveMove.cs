using UnityEngine;

//エネミーの蛇行しながら進む動きのクラス
public class WaveMove : EnemyMove {

    public float _wave_time = 0;	 //一往復するのにかかる時間
    public float _width = 0;		 //幅の大きさ
    public float _speed = 0;
    GameObject _enemy = null;

	public WaveMove( float speed, GameObject enemy, Vector3 dir, float width, float wave_time ) {
		_speed = speed;
		_width = width;
		_wave_time = wave_time;
		_enemy = enemy;

		_enemy.transform.forward = dir;
	}

    public void Move( ) {
        float f = 1.0f / _wave_time;	//周波数(ふり幅)
        float sin = Mathf.Sin( 2 * Mathf.PI * f * Time.time );
       
        _enemy.transform.forward = new Vector3( sin * _width, 0, 1.0f - _speed );
		_enemy.transform.position += _enemy.transform.forward * _speed * Time.deltaTime;
    } 
}
