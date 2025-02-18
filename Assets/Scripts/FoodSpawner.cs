using System.Collections;
using Unity.BossRoom.Infrastructure;
using Unity.Netcode;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    private const int MaxPrefabCount = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NetworkManager.Singleton.OnServerStarted += SpawnFoodStart;
    }

    private void SpawnFoodStart()
    {
        NetworkManager.Singleton.OnServerStarted -= SpawnFoodStart;
        NetworkObjectPool.Singleton.RegisterPrefabInternal(prefab, 30);
        for (int i = 0; i < 30; i++)
        {
            SpawnFood();
        }

        StartCoroutine(SpawnOverTime());
    }

    private void SpawnFood()
    {
        NetworkObject networkObject = NetworkObjectPool.Singleton.GetNetworkObject(prefab, GetRandomPositionOnMap(), Quaternion.identity);
        networkObject.GetComponent<Food>().prefab = prefab;
        if (!networkObject.IsSpawned) networkObject.Spawn(true);
    }

    private Vector3 GetRandomPositionOnMap()
    {
        return new Vector3(Random.Range(-9f, 9f), Random.Range(-5, 5f), 0f);
    }

    private IEnumerator SpawnOverTime()
    {
        while (NetworkManager.Singleton.ConnectedClients.Count > 0)
        {
            yield return new WaitForSeconds(2f);
            if (NetworkObjectPool.Singleton.GetCurrentPrefabCount(prefab) < MaxPrefabCount)
                SpawnFood();
        }
    }
}
