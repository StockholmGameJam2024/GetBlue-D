using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudController : MonoBehaviour
{
    List<Player> players = new();
    public GameObject HUDprefab;

    public void RegisterPlayer(Player player)
    {
        players.Add(player);
    }
    
}
