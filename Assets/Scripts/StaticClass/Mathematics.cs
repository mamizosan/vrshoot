using UnityEngine;

//汎用的な計算系の関数を纏めるクラス
public static class Mathematics {

    //Vecter3型を渡して2乗にして返す関数
    private static Vector3 VectorPow( Vector3 vec, float pow ) {
        Vector3 vec_pow = Vector3.zero;
        vec_pow.x = Mathf.Pow( vec.x, pow );
        vec_pow.y = Mathf.Pow( vec.y, pow );
        vec_pow.z = Mathf.Pow( vec.z, pow );

        return vec_pow;
    }

    //Vecter3型を渡して平方根にして返す関数
    private static Vector3 VectorSqrt( Vector3 vec ) {
        Vector3 vec_sqrt = Vector3.zero;
        vec_sqrt.x = Mathf.Sqrt( vec.x );
        vec_sqrt.y = Mathf.Sqrt( vec.y );
        vec_sqrt.z = Mathf.Sqrt( vec.z );

        return vec_sqrt;
    }

    private static Vector3 VectorAbs( Vector3 vec ) {
        Vector3 vec_abs = Vector3.zero;
        vec_abs.x = Mathf.Abs( vec.x );
        vec_abs.y = Mathf.Abs( vec.y );
        vec_abs.z = Mathf.Abs( vec.z );

        return vec_abs;
    }

	//指定したベクトルの距離を返す
	public static Vector3 VectorDistance( Vector3 vec1, Vector3 vec2 ) { 
		Vector3 vec_dis = Vector3.zero;
		vec_dis.x = vec1.x - vec2.x;
		vec_dis.y = vec1.y - vec2.y;
		vec_dis.z = vec1.z - vec2.z;

		vec_dis = VectorPow( vec_dis, 2 );
		vec_dis = VectorSqrt( vec_dis );
		vec_dis = VectorAbs( vec_dis );

		return vec_dis;
	}

	//指定したベクトルのorigin_vecからのtarget_posへの方向を返す
	public static Vector3 VectorDirection( Vector3 target_vec, Vector3 origin_vec ) {
		return ( target_vec - origin_vec ).normalized;
	}


}
