using UnityEngine;

//エネミーの円運動しながら進む動きのクラス
public class CircleMove : EnemyMove {
    public float _speed = 0.5f;
    public float _radius = 0.5f; //半径の大きさ
    GameObject _enemy = null;

    public CircleMove( float speed, GameObject enemy, Vector3 dir, float radius ) {
        _speed = speed;
        _radius = radius;
        _enemy = enemy;

        _enemy.transform.forward = dir;
    }

    public void Move( ) {
       float _sin = _radius * Mathf.Sin( Time.time * _speed );
       float _cos = _radius * Mathf.Cos( Time.time * _speed );

        _enemy.transform.forward = new Vector3( _sin, 0.5f, _cos　- _speed );

        _enemy.transform.position += _enemy.transform.forward * _speed * Time.deltaTime;
    }
}
