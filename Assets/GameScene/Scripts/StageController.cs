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
	CharacterController charaCont;
	
	bool tap = false;
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
		charaCont = chara.GetComponent<CharacterController>();


		before = chara.transform.position + transform.position;

		tap = true;
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
			baseCont.kind = (int)Random.Range(1, 10);

			if (baseCont.kind == 8 || baseCont.kind == 9 || baseCont.kind == 10) {
				baseCont.kind = 100;
			}

			// set position
			if (i >= 1) {
				theBase.transform.position = positions[i-1] + new Vector3(1.3f, 0, 0);
			}
			positions[i] = theBase.transform.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (tap) {
				currentSycro = (int)Random.Range(1, 6);
				tap = false;

				// 出た目の数だけ、キャラクターの位置インデックスを動かす
				stepCount = currentSycro * 10;
			}
		}

		// 移動が終わったら、マス判定
		if (stepCount <= 0) {
			if (!tap) {
				
				stepCount = 0;
				tap = true;

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

				chara.transform.position = baseObjList[index++].transform.position + new Vector3(0, 1, 0);
			}

			stepCount --;
		}

		transform.position = chara.transform.position + before;
		transform.LookAt(chara.transform);
	}

	void FixedUpdate () {
		if (gameCount % 120 == 0) {
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
