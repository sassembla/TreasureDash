/**
    Assertion features for Unity.
*/
using System;
using System.Globalization;
using UnityEngine;


static class Assertions {
	/**
		assert which extends bool.
	*/
	public static void Assert (this bool condition, string reason) {
		if (condition) return;

		OutputStackThenDown(reason);
	}


	/**
		assert which extends string of date
	*/
	public static void TimeAssert (this string writtenDate, int limitSec, string reason) {
		DateTime parsedDate;
		
		var fullhead_time_result = DateTime.TryParseExact(writtenDate, "yyyy/MM/dd hh:mm:ss", null, DateTimeStyles.None, out parsedDate);
		if (!fullhead_time_result) {
			var no_head_time_result = DateTime.TryParseExact(writtenDate, "yyyy/MM/dd h:mm:ss", null, DateTimeStyles.None, out parsedDate);
			if (!no_head_time_result) {
				return;
			}
		}

		var now = DateTime.Now;
		var diff = now - parsedDate;
		var diffSec = Math.Floor(diff.TotalSeconds);
		
		if (diffSec < limitSec) {
			return;
		}

		OutputStackThenDown(reason + " passed:" + (limitSec - diffSec) + "sec");
	}






	private static void OutputStackThenDown (string reason) {
		System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);

		// at least 2 stack exists in st. 0 is "System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);", 1 is where the assertion faild.
		var assertFaildPointDescription = st.GetFrame(2).ToString();

		// get specific data from stacktrace.
		var descriptions = assertFaildPointDescription.Split(':');
		var fileName = descriptions[2].Split(' ')[1];
		var line = descriptions[3];

		Debug.LogError("A:" + fileName + ":" + line + ":" + reason);
		
		// broke up
		Debug.Break();
	}
}