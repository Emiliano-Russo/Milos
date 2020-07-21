using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpawner : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        Instantiate(canvas);
    }
}
