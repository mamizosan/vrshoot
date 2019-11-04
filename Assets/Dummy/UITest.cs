using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour {
	RectTransform _rect = null;
	// Use this for initialization
	void Start () {
		_rect = GetComponent< RectTransform >( );
	}
	
	// Update is called once per frame
	void Update () {
		_rect.transform.position += new Vector3( 1, 0, 0 );
	}
}
