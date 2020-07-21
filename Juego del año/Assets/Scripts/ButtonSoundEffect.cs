using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundEffect : EventTrigger
{

    public AudioClip onHover;
    public AudioClip onClick;

    private AudioSource click;
    private AudioSource hover;


    private void Start()
    {
        click = new AudioSource();
        hover = new AudioSource();
        click.clip = onClick;
        hover.clip = onHover;
    }

    public void PlayHoverSound() => hover.Play();

    public void PlayClickSound() => click.Play();

    public override void OnPointerClick(PointerEventData data)
    {
        PlayClickSound();
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        PlayHoverSound();
    }

}
