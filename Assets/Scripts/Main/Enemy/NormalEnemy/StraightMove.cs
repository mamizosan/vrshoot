using UnityEngine;

//エネミーの真っすぐ進む動きのクラス
public class StraightMove : EnemyMove {
    float _speed = 0;
    GameObject _enemy = null;

    public StraightMove( float speed, GameObject enemy, Vector3 dir ) {
        _speed = speed;
        _enemy = enemy;
		_enemy.transform.forward = dir;
    }

    public void Move( ) {
		_enemy.transform.position += _enemy.transform.forward * _speed * Time.deltaTime;
	}
}
