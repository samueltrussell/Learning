using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Networking;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LoginStatus {
	
	public int success;
	public string message;
	public string token;

	public static LoginStatus CreateFromJSON(string JSONString)
	{
		return JsonUtility.FromJson<LoginStatus> (JSONString);
	}
}

public class Login : MonoBehaviour {
	
	//public Texture test;
	public InputField UserName;
	public InputField PassWord;
	public Text DebugOutput;

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

		//Test initial response from web server
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

		WWWForm formData = new WWWForm ();
		formData.AddField ("given", UserName.text);
		formData.AddField ("family", PassWord.text);
		Debug.Log (formData.data);

		//Construct new UnityWebRequest
		UnityWebRequest www2 = UnityWebRequest.Post ("http://155.101.206.136/webservice/login.php", formData);
		yield return www2.Send ();

		if(www2.isError) {
			Debug.Log(www2.error);
		}
		else {
			// Show results as text
			Debug.Log(www2.downloadHandler.text);
			DebugOutput.text = www2.downloadHandler.text;
			//Parse JSON data from response into usable object
			LoginStatus login = LoginStatus.CreateFromJSON(www2.downloadHandler.text);

			if (login.success == 1) {

                UserData.Instance.AccessToken = login.token;
				SceneManager.LoadScene ("Med Access");

			}
		}
	}
}
