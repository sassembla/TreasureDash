using UnityEngine;
using System.Collections;


public class CharaController : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		"2014/06/23 3:05:34".TimeAssert(10000, "落下したら死ぬ、を作る。");
		(0 < transform.position.y).Assert("character dropped!! :" + transform.position.y);
	}

	/**
		複数のキャラクター(自分 + 他人 + FOEとか) をセットする。FOEイイナーーー
	*/
	public void InitializeWithId (string identity) {
		"2014/06/23 3:49:40".TimeAssert(1000000, "キャラクターの初期化、なんらかキャッシュとか考えないとなーと。");
		Debug.Log("character InitializeWithId " + identity);
	}

}

