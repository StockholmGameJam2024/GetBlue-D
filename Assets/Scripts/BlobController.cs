using UnityEngine;

public class BlobController : MonoBehaviour
{
    public Vector2 destination; 
    private readonly float moveSpeed = 2f;

    private void Update()
    {
        MoveTowardsDestination();
    }

    private void MoveTowardsDestination()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, destination) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}