using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCreditUI : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 20f;

    private void Update()
    {
        transform.Translate(Camera.main.transform.up * (_scrollSpeed * Time.deltaTime));
    }
}
