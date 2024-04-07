using System;
using System.Collections;
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
            Camera.main!.ScreenToWorldPoint(Vector2.zero) + Vector3.left + Vector3.down,
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)) + Vector3.right + Vector3.down,
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) + Vector3.right + Vector3.up,
            Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)) + Vector3.left + Vector3.up,
            Camera.main.ScreenToWorldPoint(Vector2.zero) + Vector3.left + Vector3.down,
        };
        _edgeCollider.SetPoints(edges);
    }
}
