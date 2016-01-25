using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Networking;
using System.Collections;
using System.Collections.Generic;

public class Login : MonoBehaviour {
	
	//public Texture test;
	public InputField UserName;
	public InputField PassWord;

	public void ExecuteLogin()
	{
		UserName.text = "Jason";
		PassWord.text = "Argonaut";

		Debug.Log("Username: " + UserName.text.ToString());
		Debug.Log ("Password: " + PassWord.text.ToString());

		StartCoroutine (TestWampAccess ());

	}

	IEnumerator TestWampAccess()
	{
		UnityWebRequest www = UnityWebRequest.Get("http://155.101.206.136/webservice/login.php");
		yield return www.Send ();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			// Show results as text
			Debug.Log(www.downloadHandler.text);
			
			// Or retrieve results as binary data
			//byte[] results = www.downloadHandler.data;
		}

		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		string credentialString = "Credential String: " + "given=" + UserName.text.ToString () + "&family=" + PassWord.text.ToString ();
		Debug.Log ("Credential String: " + credentialString);
		formData.Add (new MultipartFormDataSection ("given=" + UserName.text.ToString()));
		formData.Add (new MultipartFormDataSection ("family=" + PassWord.text.ToString()));
		//formData.Add(new MultipartFormDataSection("token=Tbt3KuCY0B5PSrJvCu2j-PlK.aiHsu2xUjUM8bWpetXoB"));

		UnityWebRequest www2 = UnityWebRequest.Post ("http://155.101.206.136/webservice/login.php", formData);
		yield return www2.Send ();

		if(www2.isError) {
			Debug.Log(www2.error);
		}
		else {
			// Show results as text
			Debug.Log(www2.downloadHandler.text);
			
			// Or retrieve results as binary data
			//byte[] results = www.downloadHandler.data;
		}
	}
}
