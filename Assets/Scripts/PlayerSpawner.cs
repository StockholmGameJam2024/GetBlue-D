using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public Player[] players;
    public int playerNumber = 4;
    public Player playerPrefab;
    public Transform[] spawnPositions;
    public Image[] playerTargetColor;
    
    // Start is called before the first frame update
    void Start()
    {
        var colors = HueHelper.GenerateEvenlyDistributedColors(4);
        players = new Player[playerNumber];
        for (var i = 0; i < players.Length; i++)
        {
            players[i] = Instantiate(playerPrefab, spawnPositions[i].position, Quaternion.identity);
            players[i].CurrentColor = colors[i];
            Color.RGBToHSV(colors[i], out var hue, out var s, out var v);
            players[i].targetColor = Color.HSVToRGB((hue + 0.5f) % 1f, s, v);
            playerTargetColor[i] 
        }
    }
}
