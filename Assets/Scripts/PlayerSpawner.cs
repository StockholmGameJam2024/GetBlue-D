using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public List<Player> players;
    public int playerNumber = 4;
    public Player playerPrefab;
    public Transform[] spawnPositions;
    public Image[] playerHUDs;
    private Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        colors = HueHelper.GenerateEvenlyDistributedColors(GetComponent<PlayerInputManager>().maxPlayerCount);
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        int i = players.Count;
        players.Add(playerInput.GetComponent<Player>());
        players[i].CurrentColor = colors[i];
        Color.RGBToHSV(colors[i], out var hue, out var s, out var v);
        players[i].targetColor = Color.HSVToRGB((hue + 0.5f) % 1f, s, v);
        players[i].SetHUD(playerHUDs[i]);
        playerHUDs[i].color = players[i].targetColor;
        if (i == 0)
        {
            FindObjectOfType<GameScoreController>().StartGame();
        }
        FindObjectOfType<GameScoreController>().RegisterPlayer(players[i]);
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        
    }
}
