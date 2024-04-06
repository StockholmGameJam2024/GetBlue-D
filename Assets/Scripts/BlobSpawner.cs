using UnityEngine;
using Random = UnityEngine.Random;

public class BlobSpawner : MonoBehaviour
{
    public GameObject blobPrefab;
    
    private bool gameOver = false;
    private float nextSpawnTime;

    [SerializeField] private int spawnInterval; // Frequency of Spawn
    
    private void Update()
    {
        if (!gameOver && Time.time > nextSpawnTime)
        { 
            BlobSpawn();
            nextSpawnTime = Time.time + spawnInterval;
        }
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
        Debug.Log("Spawn point: " + spawnPoint);

        GameObject newBlob = Instantiate(blobPrefab, spawnPoint, Quaternion.identity);

        var directionAngle = angle + 180 + Random.Range(-40, 40);
        var directionAngleRad = directionAngle * Mathf.Deg2Rad;

        var xTarget = Mathf.Cos(directionAngleRad) * diagonalLength;
        var yTarget = Mathf.Sin(directionAngleRad) * diagonalLength;

        // Assigning destination to BlobController component of the spawned blob
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
