using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpawnPoints : MonoBehaviour
{
    // Start is called before the first frame update
    public int gridWidth = 97;  // (48 - (-48)) + 1
    public int gridHeight = 35; // (17 - (-17)) + 1
    public GameObject emptyObjectPrefab; // The empty GameObject prefab to instantiate

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        Vector3 spawnPosition = transform.position - new Vector3(gridWidth / 2, gridHeight / 2, 0); // Center the grid around this object

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 spawnPoint = spawnPosition + new Vector3(x, y, 0);

                GameObject newEmptyObject = Instantiate(emptyObjectPrefab, spawnPoint, Quaternion.identity);
                newEmptyObject.transform.parent = transform; // Make it a child of this object
            }
        }
    }
}
