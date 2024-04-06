using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlobController : MonoBehaviour
{
    public Vector2 destination; 
    private float moveSpeed;
    /// <summary>
    /// Depending on the colorStrength, the hue is changed faster.
    /// e.g. 0.15 = 0.15 hue change from current player color to this blob's color
    /// </summary>
    public float colorStrength = 0.05f;

    
    public SpriteRenderer blobSpriteRenderer;

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
        if (other.TryGetComponent(out IColorable colorable))
        {
            colorable.ChangeColorTint(this.blobSpriteRenderer.color, colorStrength);
            ReturnToPool();
        }
    }
}