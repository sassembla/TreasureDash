using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageController : MonoBehaviour {
	public GameObject basePrefab;

	Vector3 [] positions;
	List<GameObject> baseObjList;
	List<BaseController> baseList;
	
	public GameObject charaPrefab;
	GameObject chara;
	CharaController charaCont;

	XaisController xaisController;
	
	public enum GAME_STATE {
		STATE_INITIALIZING,
		STATE_RUNNING,
		STATE_XAIING
	};

	public GAME_STATE state;

	int currentSycro;

	int stepCount;

	int gameCount;

	int dropIndex;

	int index = 0;
	int coin = 0;

	Vector3 before;

	// Use this for initialization
	void Start () {
		chara = Instantiate(charaPrefab) as GameObject;
		charaCont = chara.GetComponent<CharaController>();

		"2014/06/23 3:32:01".TimeAssert(1000000, "複数のプレイヤーを落とせないだろうか？");
		charaCont.InitializeWithId("myname");

		before = chara.transform.position + transform.position;

		// generate xai s Controller
		xaisController = new XaisController();
		state = GAME_STATE.STATE_INITIALIZING;



		stepCount = 0;
		gameCount = 0;

		dropIndex = -1 ;

		positions = new Vector3[30];
		baseObjList = new List<GameObject>();
		baseList = new List<BaseController>();

		for (int i = 0; i < positions.Length; i++) {
			
			var theBase = Instantiate(basePrefab) as GameObject;
			baseObjList.Add(theBase);

			var baseCont = theBase.GetComponent<BaseController>();
			baseList.Add(baseCont);
			
			baseCont.id = i;
			var kindRandom = (int)Random.Range(1, 10);

			// mapper
			"2014/06/23 10:18:29".TimeAssert(10000, "マッパーを作る必要あると思う。ランダムに作成した位置マップを持つ。");
			{
				if (8 < kindRandom) {
					baseCont.baseKind = BaseController.BASE_KIND.KIND_ENEMY;
				}
			}

			// set position
			"2014/06/24 3:29:48".TimeAssert(10000, "theBaseの位置を動かしてるけどきっとこれ中身空だ。xに特定の値移動してる。");
			if (i >= 1) {
				theBase.transform.position = positions[i-1] + new Vector3(1.3f, 0, 0);
			}
			positions[i] = theBase.transform.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (xaisController.IsTappable) {
				"2014/06/23 3:32:01".TimeAssert(1000, "tapが単純なフラグなので、変形させたい。->クラス作成をして、タッチ自体を委譲したい感じ。");
				"2014/06/23 3:32:01".TimeAssert(10000, "タップしたらサイコロ回す処理。このへんを優先して作るか。");
				currentSycro = (int)Random.Range(1, 6);
				xaisController.IsTappable = false;

				// 出た目の数だけ、キャラクターの位置インデックスを動かす
				stepCount = currentSycro * 10;
			}
		}

		"2014/06/24 3:32:01".TimeAssert(100, "サイコロに合わせて進むところ、stepCountを分解する。");
		if (stepCount <= 0) {
			if (!xaisController.IsTappable) {
				
				stepCount = 0;
				xaisController.IsTappable = true;

				var result = baseList[index].Arrived();
				if (0 < result) {
					coin += result;
				} else {
					// 敵にぶつかったので、index が1戻る
					index --;
					chara.transform.position = baseObjList[index].transform.position + new Vector3(0, 1, 0);
				}
			}
		} else {
			if (stepCount % 10 == 0) {
				"2014/06/23 3:58:31".TimeAssert(10000, "キャラクターの移動、キャラクターごとに「次どこに進むか」を持たせられればいい感じする。");
				chara.transform.position = baseObjList[index++].transform.position + new Vector3(0, 1, 0);
			}

			stepCount --;
		}

		"2014/06/23 3:58:31".TimeAssert(1000000, "カメラの移動、キャラクターから引き離したい。");
		transform.position = chara.transform.position + before;
		transform.LookAt(chara.transform);
	}

	/**
		ゲームフレームを回す処理
	*/
	void FixedUpdate () {
		if (gameCount % 120 == 0) {
			"2014/06/24 3:32:01".TimeAssert(1000, "フレームに合わせて地面を落とす処理、同時多発的に複数箇所を落とす、とかがやりたい。");
			if (0 <= dropIndex) baseList[dropIndex].fall = true;
			dropIndex ++;
		}
		gameCount++;
	}


	void OnGUI () {
		GUI.Label(new Rect(10,10,120,20), "time:"+gameCount);	

		GUI.Label(new Rect(10,30,120,20), "[ ]:"+(int)Random.Range(1, 6));

		GUI.Label(new Rect(10,50,120,20), "now:"+currentSycro);

		if (0 < stepCount) {
			GUI.Label(new Rect(10,80,120,20), "rest:"+stepCount/100);
		}

		if (0 < coin) {
			GUI.Label(new Rect(10,100,120,20), "coin:"+stepCount/100);	
		}
	}
}
