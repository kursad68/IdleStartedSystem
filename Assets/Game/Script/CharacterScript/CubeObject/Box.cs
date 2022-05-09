using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorType
{
    Type1,
    Type2,
    Type3,
    Type4,
    Type5,
}
public class Box : MonoBehaviour
{
    private Collider collider;
    public Collider Collider { get { return (collider == null) ? collider = GetComponent<Collider>() : collider; } }

    private  delegate void myFonction();
    [SerializeField]
    private ColorType myColorType;
    int x = 0;
    private void Start()
    {
        switch (myColorType)
        {
            case ColorType.Type1:
                x = 0;
                break;
            case ColorType.Type2:
                x = 1;
                break;
            case ColorType.Type3:
                x = 2;
                break;
            case ColorType.Type4:
                x = 3; break;
            case ColorType.Type5:
                x = 4; break;
            default:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {IProgress progress=other.gameObject.GetComponent<IProgress>();
        if (progress!=null)
        {
            addToList(progress);
        }
    }
    private void addToList(IProgress progress)
    {
        Collider.enabled = false;
        progress.addMyList(gameObject, 0);
    }
}
