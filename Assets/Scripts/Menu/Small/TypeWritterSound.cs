using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TypeWritterSound : MonoBehaviour
{

    public AudioSource audioType;
    public InputField inputField;
    


    // Update is called once per frame
    void Update()
    {
     if (Input.anyKeyDown && inputField.isFocused)
        {
            audioType.Play();
        }   
    }
}
