using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using DG.Tweening;
public class Fiilling : MonoBehaviour
{
    public List<GameObject> myListObject;
    private bool isDestroy;
    private int time;
    [SerializeField]
    private int X=0, Y=0, Z=0;
    [SerializeField]
    private float Timer = 1f;
    [SerializeField]
    private Transform startBoxTransform;
    Task task;
    private Coroutine coroutine;
    private bool isTriger=false;
    private void Start()
    {
        
        time = Convert.ToInt32(Timer * 1000f);
       
 task=passWait( () => CreativeBuıldBox());
    }
    private async Task passWait( Action OnUpdate)
    {
       if(!isDestroy)
        {
            OnUpdate?.Invoke();
            await Task.Delay(time);
            task = passWait(() => CreativeBuıldBox());
        }

    }
    private void CreativeBuıldBox()
    {
     int kontrol = 0;
        for (int i = 0; i < Y; i++)
        {
            for (int j = 0; j < X; j++)
            {
                for (int k = 0; k < Z; k++)
                {
                    if (kontrol<myListObject.Count)
                    {
                        kontrol++;
                    }
                    else
                    {
                        GameObject boxObje = Instantiate(ItemManager.Instance.boxObject);
                        addMyFiilingList(boxObje, j, i, k);
                        return;
                    }
                }
            }
        }
    }
    private void addMyFiilingList(GameObject boxObje, int j, int i, int k)
    {

        boxObje.transform.SetParent(this.transform);
        boxObje.transform.position = startBoxTransform.transform.position;
        boxObje.transform.DOLocalMove(new Vector3(j, i + 1, k), 1f).OnComplete(() => {
            if (!myListObject.Contains(boxObje)) myListObject.Add(boxObje);
        });
    }
    private void OnTriggerStay(Collider other)
    {

        IProgress progress = other.gameObject.GetComponent<IProgress>();
        if (progress != null)
        {
            if (myListObject.Count > 0)
            {
                progress.addMyList(myListObject[myListObject.Count - 1], 0);
                myListObject.Remove(myListObject[myListObject.Count - 1]);
            }
        }
    }
   /* private void OnTriggerEnter(Collider other)
    {
        IProgress progress = other.gameObject.GetComponent<IProgress>();
        if (progress!=null)
        {
            if (!isTriger)
            {
                isTriger = true;
                coroutine = StartCoroutine(waitForAddBoxCharacter(progress));
                Debug.Log("doldurma");
            }
          
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Joystick jc = other.gameObject.GetComponent<Joystick>();
        if (jc!=null &&isTriger)
        {
            isTriger = false;
            Debug.Log("doldurma exit");
            StopCoroutine(coroutine);
        }
    }*/
    IEnumerator waitForAddBoxCharacter(IProgress progress)
    {
        yield return null;
        if (myListObject.Count>0)
        {
            progress.addMyList(myListObject[myListObject.Count - 1], 0);
            myListObject.Remove(myListObject[myListObject.Count - 1]);
        }
      
    }
    private void OnDestroy()
    {
        isDestroy = true;
    }
}
