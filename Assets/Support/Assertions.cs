/**
    
*/
using UnityEngine;

static class Assertions {
	/**
		assert which extends bool.
	*/
	public static void Assert (this bool condition, string reason) {
		if (condition) return;

		System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);

		// at least 2 stack exists in st. 0 is "System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);", 1 is where the assertion faild.
		var assertFaildPointDescription = st.GetFrame(1).ToString();

		// get specific data from stacktrace.
		var descriptions = assertFaildPointDescription.Split(':');
		var fileName = descriptions[2].Split(' ')[1];
		var line = descriptions[3];

		Debug.LogError("A:" + fileName + ":" + line + ":" + reason);
		
		
		Debug.Break();
	}
}