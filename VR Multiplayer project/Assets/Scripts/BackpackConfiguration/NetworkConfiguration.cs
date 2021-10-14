using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.IO;


public class NetworkConfiguration : MonoBehaviour
{
    public static settings GameSettings;
    public  NetworkManager _networkManager;
    public bool IsServer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        string jsonPath = File.ReadAllText(@"C:/KDGConfig/Backpackconfiguration.json");

        GameSettings = JsonUtility.FromJson<settings>(jsonPath);

        //Set wether this device is the server
        if(GameSettings.ID == 0)
        {
            IsServer = true;
        }
        else
        {
            IsServer = false;
        }

        //When this device is the server start hosting the session, if it is not join the session
        if (IsServer)
        {
            _networkManager.StartHost();
        }
        else
        {
            _networkManager.networkAddress = "192.30.20.200";
            _networkManager.StartClient();
        }
    }

    //Settings that are specific to each backpack. This information can be found in the Backpackconfiguration.json file located at C:/KDGConfig/Backpackconfiguration.json
    public class settings
    {
        public int Rigidbodybody;
        public int ID;
        public string IPAdress;
    }
}
