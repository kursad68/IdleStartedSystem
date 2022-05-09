using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum FillingType
{
    Upper,
    Linked
}
public class CharacterControl : MonoBehaviour,IProgress
{
    private CharacterComponents characterComponents;
    public CharacterComponents CharacterComponents { get { return (characterComponents == null) ? characterComponents = GetComponentInParent<CharacterComponents>() : characterComponents; } }
    private List<GameObject> dequeListColor=new List<GameObject>();
    private GameObject temp;
    private  int i = 1;
    private  bool isTriger=false;
    private Coroutine coroutine;
    [SerializeField]
    private FillingType fillingType;

    private void OnEnable()
    {
        EventManager.getCharacter += getCh;
    }
    private void OnDisable()
    {
        EventManager.getCharacter -= getCh;
    }
    private CharacterControl getCh()
    {
        return GetComponent<CharacterControl>();
    }

    public void addMyList(GameObject obj, int x)
    {
        CharacterComponents.addListFollower.Invoke(obj, x);
    }
    public GameObject removeMyList()
    {
        return CharacterComponents.removeListFollower.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        IUnloading unloading = other.gameObject.GetComponent<IUnloading>();
        if (unloading!=null)
        {
            if (!unloading.isFull())
            {
                ToTransferForUnloading(unloading, removeMyList());
            }
           
        }
    }
    private void ToTransferForUnloading(IUnloading unloading, GameObject obj)
    {
        unloading.unload›ng(obj);
 
    }
    private IEnumerator waitForTransfer(IUnloading unloading,GameObject obj)
    {
        if (obj!=null)
        {
            yield return null;
            coroutine = StartCoroutine(waitForTransfer(unloading, removeMyList()));
        }
        else
        {
            isTriger = false;
        }
   
    }
}
