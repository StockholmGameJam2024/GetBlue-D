using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudController : MonoBehaviour
{
    List<Player> players = new();
    public HudElement HUDprefab;
    public Transform parent;

    public void RegisterPlayer(Player player)
    {
        players.Add(player);
        CreatePlayerHud(player);
    }

    private void CreatePlayerHud(Player player)
    {
        HudElement element = Instantiate(HUDprefab, parent);
        player.SetHUD(element);
    }
}
