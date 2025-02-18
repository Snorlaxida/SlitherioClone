using System;
using Unity.Netcode;
using UnityEngine;

public class ConnectionApprovalHandler : MonoBehaviour
{
    private const int maxPlayers = 10;

    private void Start()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        Debug.Log("Connection approval");
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.PlayerPrefabHash = null;
        if (NetworkManager.Singleton.ConnectedClients.Count >= maxPlayers)
        {
            response.Approved = false;
            response.Reason = "Server is Full";

        }
        response.Pending = false;
    }
}
