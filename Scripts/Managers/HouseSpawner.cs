using Mirror;
using UnityEngine;

public class HouseSpawner : NetworkBehaviour
{

    [SerializeField] private GameObject housePrefab;
    public static HouseSpawner instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (isServer)
            {
                InstantiateHouses();
            }
        }
        else
        {
            Debug.LogWarning("More than one instance of HouseSpawner found!");
            return;
        }
    }

    [Server]
    public void InstantiateHouses()
    {
        int numberOfPlayers = NetworkServer.connections.Count;
        float houseWidth = 12.5f;
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject house = Instantiate(housePrefab, new Vector3(i * houseWidth, 0, 0), Quaternion.identity);
            house.transform.SetParent(transform);

            NetworkServer.Spawn(house);
        }
    }
}
