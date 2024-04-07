using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCreditUI : MonoBehaviour
{
    public GameObject endCreditText;

    [SerializeField] private float _scrollSpeed = 20f;
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSettings audioSettings;

    private bool hasReachedEnd = false;

    private void Update()
    {
        transform.Translate(Camera.main.transform.up * (_scrollSpeed * Time.deltaTime));

        if (!hasReachedEnd && endCreditText.transform.position.y >= 1050f)
        {
            hasReachedEnd = true;
            ReturnToMainScene();
        }
    }

    private void Awake()
    {
        musicPlayer.clip = audioSettings.creditsMusic;
        musicPlayer.Play();
    }

    private void ReturnToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}

