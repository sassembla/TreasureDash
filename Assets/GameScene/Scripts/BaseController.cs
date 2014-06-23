﻿using UnityEngine;
using System.Collections;

/**
	地面のコントローラ。
	出現時に種類とかアイテムを設定している。
*/
public class BaseController : MonoBehaviour {
	public int id;

	public enum BASE_KIND {
		KIND_ENEMY,
		KIND_COIN,
		KIND_TREASURE
	};

	public BASE_KIND baseKind;

	public GameObject enemyPrefab;
	public bool fall = false;
	int hp;

	int coin;

	GameObject enemy;

	// Use this for initialization
	void Start () {
		"2014/06/24 3:58:31".TimeAssert(100, "種類の設定をもうちょっとちゃんとしたい。でももっと優先すべきは「ゲームになるかどうか」の部分か。");
		if (baseKind == BASE_KIND.KIND_ENEMY) {
			enemy = Instantiate(enemyPrefab, transform.position + new Vector3(0, 1, 0), transform.rotation) as GameObject;
			hp = (int)Random.Range(1, 2);
		} else {
			"2014/06/23 10:18:29".TimeAssert(10000, "コインの設定、あと宝箱の設定とか。とりあえずランダムで出す現在の形で良いと思う。");
			// coinValue = baseKind;
		}
	}
	

	// Update is called once per frame
	void Update () {
		if (fall) {
			rigidbody.useGravity = true;
			rigidbody.isKinematic = false;
		}
	}

	public int Arrived () {
		if (0 < hp) {
			hp--;
			if (hp == 0) {
				Destroy(enemy);
			}
			return -1;
		}

		return coin;
	}


}
