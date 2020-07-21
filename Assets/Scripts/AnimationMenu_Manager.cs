using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMenu_Manager : MonoBehaviour
{


    public Animator okBtnAnim;
    public Animator inputNameAnim;

 
    public void PlayAnimation_GoAway()
    {
        okBtnAnim.Play("Pressed");
        inputNameAnim.Play("InputNameAnimation");
    }



}
