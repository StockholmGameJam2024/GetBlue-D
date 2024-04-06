using UnityEngine;

public class BlobController : MonoBehaviour
{
    private Rigidbody2D _rbigidbody2D;
    private bool gameOver = false;

    [SerializeField] private float movementForce;
    [SerializeField] private int spawnInterval; // Frequency of Spawn

    private void Start()
    {
        _rbigidbody2D = GetComponent<Rigidbody2D>();
        
        if (_rbigidbody2D == null) Debug.LogError("Rigidbody2D component is missing on the GameObject!");

        while (!gameOver) BlobSpawn();
    }

    private void BlobSpawn()
    { 
        spawnInterval = Random.Range(1, 6);
        // random colour also
    
        float xSpawn = 0f;
        float ySpawn = 0f;

        while (true)
        {
            xSpawn = UnityEngine.Random.Range(-150f, 150f);
            ySpawn = UnityEngine.Random.Range(-150f, 150f);

            if (Mathf.Abs(xSpawn) > 100f || Mathf.Abs(ySpawn) > 100f) break;
        }

        Vector2 spawnPoint = new Vector2(xSpawn, ySpawn);
        Debug.Log("Spawn point: " + spawnPoint);

        MoveTo(spawnPoint);
    }

    private void MoveTo(Vector2 spawnPoint)
    {
        movementForce = Random.Range(50f, 100f);
        
        float oppositeX = -spawnPoint.x;
        float oppositeY = -spawnPoint.y;

        Vector2 moveToPosition = new Vector2(oppositeX, oppositeY);

        transform.position = Vector2.MoveTowards(transform.position, moveToPosition, movementForce * Time.deltaTime);
    }
    
    // Destroy blob after certain time? on collision?

}
