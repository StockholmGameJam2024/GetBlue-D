using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    private EdgeCollider2D _edgeCollider;

    private void Awake()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();
        var edges = new List<Vector2>
        {
            Camera.main!.ScreenToWorldPoint(Vector2.zero),
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)),
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)),
            Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)),
            Camera.main.ScreenToWorldPoint(Vector2.zero),
        };
        _edgeCollider.SetPoints(edges);
    }
}
