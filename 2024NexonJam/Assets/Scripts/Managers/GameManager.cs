using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int headSecond;

    private void Start()
    {
        headSecond = 0;
    }

    private void Update()
    {
        headSecond += (int)Time.deltaTime;
    }
}
