using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(SpawnManager))]
public class CustomNetworkHUD : MonoBehaviour{

    public SpawnManager manager;
    public bool showGUI = true;
    public GameObject ConnectionPanel;
    public GameObject RunningPanel;
    public Text ErrorText;

    public InputField hostIpField;

    // Use this for initialization
    void Start()
    {
        manager = GetComponent<SpawnManager>();
        bool test = manager.isActiveAndEnabled;
        //ConnectionPanel = GameObject.Find("ConnectionPanel");
        //test = ConnectionPanel.isStatic;
        //ConnectionPanel = GameObject.Find("ConnectionPanel");
        //test = ConnectionPanel.isStatic;

    }

    // Update is called once per frame
    void Update ()
    {
        //if (!showGUI)
        //    return;

        //if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
        //{
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        manager.StartServer();
        //    }
        //    if (Input.GetKeyDown(KeyCode.H))
        //    {
        //        manager.StartHost();
        //    }
        //    if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        manager.StartClient();
        //    }
        //}
        //if (NetworkServer.active && NetworkClient.active)
        //{
        //    if (Input.GetKeyDown(KeyCode.X))
        //    {
        //        manager.StopHost();
        //    }
        //}
    }

    public void SetUpHost()
    {
        manager.StartHost();

        ConnectionPanel.SetActive(false);
        RunningPanel.SetActive(true);

        SceneManager.LoadScene("Main");
        ClientScene.Ready(manager.client.connection);
    }

    public void JoinAsClient()
    {

        StartCoroutine(ConnectClient());

        ////Debug.Log(Network.Connect(hostIpField.text, 7777));
        //manager.networkAddress = hostIpField.text;
        //manager.StartClient();


        //if (manager.client.connection != null)
        //{
        //    ConnectionPanel.SetActive(false);
        //    RunningPanel.SetActive(true);

        //    SceneManager.LoadScene("Main");
        //    ClientScene.Ready(manager.client.connection);
        //}
        //else
        //{
        //    ErrorText.text = "Error Connecting to " + manager.networkAddress + "!";
        //}

    }

    IEnumerator ConnectClient()
    {
        ////Debug.Log(Network.Connect(hostIpField.text, 7777));
        manager.networkAddress = hostIpField.text;
        yield return manager.StartClient();


        if (manager.client.connection != null)
        {
            ConnectionPanel.SetActive(false);
            RunningPanel.SetActive(true);

            SceneManager.LoadScene("Main");
            ClientScene.Ready(manager.client.connection);
          
        }
        else
        {
            ErrorText.text = "Error Connecting to " + manager.networkAddress + "!";
        }
    }

    public void ReturnToMenu()
    {
        if(manager.client.connection.isConnected)
        {
            manager.StopClient();
        }
        else
        {
            manager.StopHost();
        }

        SceneManager.LoadScene("Menu");
        RunningPanel.SetActive(false);
        ConnectionPanel.SetActive(true);
    }
}
