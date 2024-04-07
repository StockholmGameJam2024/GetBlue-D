using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winscreen : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = Vector2.zero;
    }

    public void Show()
    {
        transform.localScale = new Vector3(5, 4);
    }
}
