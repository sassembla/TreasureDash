using UnityEngine;

/**
	サイコロのコントロールを行う。
	サイコロ振り中、内容、サイコロへの効果発生、入力受付がこのクラスの責務。
*/
class XaisController {
	public bool IsTappable;
	public int currentCount = -1;


	public XaisController () {
		IsTappable = true;
	}

	public void Tapped () {
		if (!IsTappable) return;

		currentCount = (int)Random.Range(1, 6);
		
		IsTappable = false;
	}

	/**
		１を出力する約束をしているので、true/false、
	*/
	public bool IsConsumed () {
		currentCount--;
			
		if (0 == currentCount) {
			return false;
		}
		
		return true;
	}


	
}