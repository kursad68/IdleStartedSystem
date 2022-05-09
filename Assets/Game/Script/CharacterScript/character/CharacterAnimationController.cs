using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController :MonoBehaviour
{
  private  CharacterComponents characterComponents;
  public CharacterComponents CharacterComponents { get { return (characterComponents == null) ? characterComponents = GetComponentInParent<CharacterComponents>() : characterComponents; } }
    private Animator animator;
    public Animator Animator { get { return (animator == null) ? animator = GetComponentInParent<Animator>() : animator; } }
    string previous = "Idle";
   
    void Start()
    {
        previous = "Idle";
    }
    private void OnEnable()
    {
      CharacterComponents.onAnimationPlay += AnimationPlayTriger;
    }
    private void OnDisable()
    {
      CharacterComponents.onAnimationPlay -= AnimationPlayTriger;
    }
    private void AnimationPlayTriger(string Triger)
    {
        SetTriger(Triger, previous);
        previous = Triger;
    }
    private void SetTriger(string value, string previous)
    {

       Animator.SetTrigger(value);
        if (value != previous)
        Animator.ResetTrigger(previous);
    }
}
