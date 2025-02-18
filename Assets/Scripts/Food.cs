using Unity.BossRoom.Infrastructure;
using Unity.Netcode;
using UnityEngine;


public class Food : NetworkBehaviour
{
    public GameObject prefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (!NetworkManager.Singleton.IsServer) return;
        if (other.TryGetComponent(out PlayerLength playerLength))
        {
            playerLength.AddLength();
        }
        else if (other.TryGetComponent(out Tail tail))
        {
            tail.networkedOwner.GetComponent<PlayerLength>().AddLength();
        }
        // NetworkObjectPool.Singleton.ReturnNetworkObject(NetworkObject, prefab);
        NetworkObject.Despawn();
    }
}
