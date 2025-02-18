using System;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class StartNetwork : MonoBehaviour
{
    [SerializeField] private TMP_InputField ipField;
    [SerializeField] private TMP_InputField portField;

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        ConnectToServer();
        // SpawnPlayer();
        NetworkManager.Singleton.StartClient();
    }


    private void SpawnPlayer()
    {
        GameObject playerObject = Instantiate(NetworkManager.Singleton.NetworkConfig.PlayerPrefab);

        playerObject.transform.position = new Vector3(5, 5, 0);

        playerObject.GetComponent<NetworkObject>().Spawn();
    }

    private void ConnectToServer()
    {
        Debug.Log($"{ipField.text} + {portField.text}");
        string ipAddress = ipField.text;
        string portString = portField.text;
        ushort port = ushort.Parse(portString);
        Debug.Log($"{port}");
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetConnectionData(ipAddress, port);
    }
}
