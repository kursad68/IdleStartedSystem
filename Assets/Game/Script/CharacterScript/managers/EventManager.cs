using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public static class EventManager
{

    //action in
    public static Action<string, string> onAnimatorAction;
    public static Action<GameObject,int> onDeleteListCollectableObject;
    public static Action<GameObject,int> onUpdateListCollectableObject;

    public static Func<CharacterControl> getCharacter;
    public static Func<Joystick> getJoystick;


    //unityEvent
}
