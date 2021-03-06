using UnityEngine;
using System.Collections;

using SublimeSocketAsset;

public class CharaController : MonoBehaviour {

	XaisController xaisController;

	// Use this for initialization
	void Start () {
		// generate xai s Controller
		xaisController = new XaisController();
	}
	
	// Update is called once per frame
	void Update () {
		
		(0 < transform.position.y).Assert("character dropped!! :" + transform.position.y);

		if (Input.anyKey) {
			Debug.Log("L:first!");
			if (xaisController.IsTappable) xaisController.Tapped();
		}

		// 以降の処理はStepでカウントされる。
	}



	/**
		複数のキャラクター(自分 + 他人 + FOEとか) をセットする。FOEイイナーーー
	*/
	public void InitializeWithId (string identity) {
		"2014/07/15 3:49:40".TimeAssert("キャラクターの初期化、なんらかキャッシュとか考えないとなー。");
		Debug.Log("character InitializeWithId " + identity);
	}


	/**
		キャラクターごとにstepを更新していって、
		サイコロもキャラクター単位で持たせる。

		進行を管理するメソッドなので、
		これが呼ばれたらキャラが進む
		という事象を叶えたい。
	*/
	int characterCount = 0;
	public void Step () {
		Debug.Log("characterCount "+characterCount);
		if (characterCount < 60) {
			characterCount ++;
			return;
		}

		characterCount = 0;

		var hasNext = xaisController.IsConsumed();

		// chara.transform.position = baseObjList[index++].transform.position + new Vector3(0, 1, 0);

		if (hasNext) {
			
		} else {
			/*
				最後の一歩だったので、サイコロが振れるようになる、、予定。
			*/
		}
		// 		var result = baseList[index].Arrived();
		// 		if (0 < result) {
		// 			coin += result;
		// 		} else {
		// 			// 敵にぶつかったので、index が1戻る
		// 			index --;
		// 			chara.transform.position = baseObjList[index].transform.position + new Vector3(0, 1, 0);
		// 		}
		// 	}
		// } else {
		// 	if (stepCount % 10 == 0) {
		// 		"2014/07/15 3:58:31".TimeAssert(10000, "キャラクターの移動、キャラクターごとに「次どこに進むか」を持たせられればいい感じする。");
		// 		
		// 	}

		// 	stepCount --;
		// }
	}

	void OnGUI () {
		GUI.Label(new Rect(10,50,120,20), "now:"+xaisController.currentCount);
	}
}

