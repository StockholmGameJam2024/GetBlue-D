using UnityEngine;
using System.Collections.Generic;

public class BlobSpawner : MonoBehaviour
{
    public GameObject blobPrefab;
    public int poolSize = 20;

    private List<GameObject> blobPool;
    private bool gameOver = false;
    private float nextSpawnTime;
    
    [SerializeField] private int spawnInterval;
    private void Start()
    {
        InitializeObjectPool();
    }

    private void Update()
    {
        if (!gameOver && Time.time > nextSpawnTime)
        { 
            BlobSpawn();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void InitializeObjectPool()
    {
        blobPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject blob = Instantiate(blobPrefab, Vector3.zero, Quaternion.identity);
            blob.SetActive(false);
            blobPool.Add(blob);
        }
    }

    private GameObject GetPooledBlob()
    {
        foreach (GameObject blob in blobPool)
        {
            if (!blob.activeInHierarchy)
            {
                blob.SetActive(true);
                return blob;
            }
        }
        
        GameObject newBlob = Instantiate(blobPrefab, Vector3.zero, Quaternion.identity);
        blobPool.Add(newBlob);
        return newBlob;
    }

    private void BlobSpawn()
    { 
        spawnInterval = Random.Range(1, 3);
        // random colour also

        float xSpawn = 0f;
        float ySpawn = 0f;

        var camera = Camera.main;
        float maxY = -camera.orthographicSize;
        float aspectRatio = (float)Screen.currentResolution.width / Screen.currentResolution.height;
        var maxX = maxY * aspectRatio;

        var diagonalLength = Mathf.Sqrt(maxX * maxX + maxY * maxY);

        var angle = Random.Range(0f, 360);
        var angleRad = angle * Mathf.Deg2Rad;

        var x = Mathf.Cos(angleRad) * diagonalLength;
        var y = Mathf.Sin(angleRad) * diagonalLength;

        Vector2 spawnPoint = new Vector2(x, y);

        GameObject newBlob = GetPooledBlob();
        newBlob.transform.position = spawnPoint;

        var directionAngle = angle + 180 + Random.Range(-40, 40);
        var directionAngleRad = directionAngle * Mathf.Deg2Rad;

        var xTarget = Mathf.Cos(directionAngleRad) * diagonalLength;
        var yTarget = Mathf.Sin(directionAngleRad) * diagonalLength;
        
        BlobController blobController = newBlob.GetComponent<BlobController>();
        if (blobController != null)
        {
            blobController.destination = new Vector2(xTarget, yTarget);
        }
        else
        {
            Debug.LogError("BlobController component not found on the spawned blob.");
        }
    }
}

