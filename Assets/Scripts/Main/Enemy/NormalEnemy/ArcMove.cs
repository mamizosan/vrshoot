using UnityEngine;

//エネミーの弧を描きながら動きのクラス
public class ArcMove : EnemyMove {
    public float radius = 1.0f;
    public float speed = 1.0f;
    private float pos_x = 1.0f;
    private float pos_z = 1.0f;


    public void Move( ) {
        Arc( );
    }

    public void Arc( ) {
		pos_x = radius * Mathf.Sin( Time.time * speed );
		pos_z = radius * Mathf.Cos( Time.time * speed );
    }
}
