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

	public enum STAGE_STATE {
		STATE_INITIALIZING,
		STATE_RUNNING,
		STATE_XAIING
	};

	public STAGE_STATE stageState;

	int dropIndex;

	int index = 0;
	int coin = 0;

	Vector3 before;

	// Use this for initialization
	void Start () {
		chara = Instantiate(charaPrefab) as GameObject;
		charaCont = chara.GetComponent<CharaController>();

		"2014/06/30 3:32:01".TimeAssert(1000000, "複数のプレイヤーを落とせないだろうか？ ");
		charaCont.InitializeWithId("myname");

		before = chara.transform.position + transform.position;

		stageState = STAGE_STATE.STATE_INITIALIZING;

		stageCounter = 0;

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
			"2014/06/28 10:18:29".TimeAssert(10000, "マッパーを作る必要あると思う。ランダムに作成した位置マップを持つ。");
			{
				if (8 < kindRandom) {
					baseCont.baseKind = BaseController.BASE_KIND.KIND_ENEMY;
				}
			}

			// set position
			"2014/06/28 3:29:48".TimeAssert(10000, "theBaseの位置を動かしてるけどきっとこれ中身空だ。xに特定の値移動してる。");
			if (i >= 1) {
				theBase.transform.position = positions[i-1] + new Vector3(1.3f, 0, 0);
			}
			positions[i] = theBase.transform.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		charaCont.Step();
		
		"2014/06/28 3:58:31".TimeAssert(1000000, "カメラの移動、キャラクターから引き離したい。");
		transform.position = chara.transform.position + before;
		transform.LookAt(chara.transform);
	}

	/**
		ゲームフレームを回す処理
		時間に依存せず、ゲーム中のみ回る。
	*/
	int stageCounter = 0;
	void FixedUpdate () {
		if (stageCounter < 60) {
			stageCounter ++;
			return;
		}

		stageCounter = 0;
		
		if (stageCounter % 120 == 0) {
			"2014/06/25 3:32:01".TimeAssert(1000, "フレームに合わせて地面を落とす処理、同時多発的に複数箇所を落とす、とかがやりたい。");
			if (0 <= dropIndex) baseList[dropIndex].fall = true;
			dropIndex ++;
		}
	}


	void OnGUI () {
		GUI.Label(new Rect(10,10,120,20), "time:"+stageCounter);
	}
}
