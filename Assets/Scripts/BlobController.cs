using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlobController : MonoBehaviour
{
    public Vector2 destination; 
    private float moveSpeed;
    public float colorStrength = 0.05f;
    public SpriteRenderer blobSpriteRenderer;

    private void Start()
    {
        // Randomly select scale between 1 and 2
        float randomScale = Random.Range(1f, 2f);
        transform.localScale = new Vector3(randomScale, randomScale, 1f);

        // Adjust colorStrength based on scale increase
        colorStrength *= randomScale;

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
            //Play blob death sound here.
            //It would require an AudioSource component on the blob prefab, a reference to the audio clip and then loading the audio clip in the audio source and .Play();
            ReturnToPool();
        }
    }
}