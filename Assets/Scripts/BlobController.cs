using UnityEngine;

public class BlobController : MonoBehaviour
{
    public Vector2 destination; 
    private float moveSpeed;

    private void Start()
    {
        moveSpeed = Random.Range(1f, 5f);
    }
    private void Update()
    {
        MoveTowardsDestination();
    }

    private void MoveTowardsDestination()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, destination) < 0.1f) ReturnToPool();
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ColorComponent>();
            ReturnToPool();
        }
    }
}