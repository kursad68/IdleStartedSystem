using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class CharacterComponents : MonoBehaviour
{

    private Rigidbody rigidbody;
    public Rigidbody Rigidbody { get { return (rigidbody == null) ? rigidbody = GetComponent<Rigidbody>() : rigidbody; } }

    private Collider collider;
    public Collider Collider { get { return (collider == null) ? collider = GetComponent<Collider>() : collider; } }

    [HideInInspector]
    public UnityEvent OnCharacterLevel = new UnityEvent();
    [HideInInspector]
    public Action<string> onAnimationPlay;
    [HideInInspector]
    public Action<GameObject,int> addListFollower;

    [HideInInspector]
    public Func<GameObject> removeListFollower;
}
