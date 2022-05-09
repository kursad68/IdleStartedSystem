using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Unloading : MonoBehaviour, IUnloading
{
    private List<GameObject> MyListItem=new List<GameObject>();
    [SerializeField]
    private Transform startBoxTransform;
    [SerializeField]
    private int X=2, Y=2, Z=2;
  private List<Vector3> vector3s=new List<Vector3>();
    private bool _isFull;
    private void Start()
    {
 
        for (int i = 0; i < Y; i++)
        {
            for (int j = 0; j < X; j++)
            {
                for (int k = 0; k < Z; k++)
                {
                    vector3s.Add ( new Vector3(j, i + 1, k));
                }
            }
        }
    }
    public void unloadİng(GameObject obj)
    {
        CreativeBuıldBox(obj);
    }
    private void CreativeBuıldBox(GameObject boxObje)
    {
        if (boxObje != null)
        {
            if (vector3s.Count <= MyListItem.Count)
            {
                _isFull = true;
            }
            else
            {
                addMyFiilingList(boxObje, vector3s[MyListItem.Count]);
            }
        }
     
    }
    private void addMyFiilingList(GameObject boxObje,Vector3 vector3)
    {
        if (!MyListItem.Contains(boxObje)) MyListItem.Add(boxObje);
        boxObje.transform.SetParent(this.transform);
        boxObje.transform.DOLocalJump(vector3, 5f, 1, 1f).OnComplete(()=>boxObje.transform.localRotation=Quaternion.identity);
    }

    public bool isFull()
    {
        return _isFull;
    }
}
