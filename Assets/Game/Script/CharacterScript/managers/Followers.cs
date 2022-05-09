using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
[Serializable]
public class listToFollowers
{
    public GameObject firstObject;
    public List<GameObject> followers;
}
public enum FollowerType
{
    steady,
    relax,
    lookPrevius,
    lookPlayer
}
public class Followers : MonoBehaviour
{
    private CharacterComponents characterComponents;
    public CharacterComponents CharacterComponents { get { return (characterComponents == null) ? characterComponents = GetComponentInParent<CharacterComponents>() : characterComponents; } }
    [SerializeField]
    public listToFollowers[] FollowersList;
    private GameObject Character;
    private CharacterControl ch;
    public FollowerType fp;
    public float  followSpeed = 0.2f,lookPlayerDistance=100f,yExenPointPluss=1f;
    private delegate void FollowDelegate();
    FollowDelegate followDelegate;
    private void Start()
    {
        ch = EventManager.getCharacter.Invoke();
        Character = ch.gameObject;
        if (fp==FollowerType.relax)
        {
            followDelegate = follows;
        }
        else if(fp==FollowerType.steady)
        {
            followDelegate = Steady;
        }
        else if (fp==FollowerType.lookPrevius)
        {
            followDelegate = lookPrevius;
        }
        else if (fp==FollowerType.lookPlayer)
        {
            followDelegate = lookPlayerStill;
        }
    }
    void Update()
    {
        followDelegate.Invoke();
    }
    private void OnEnable()
    {
        EventManager.onUpdateListCollectableObject += addColorBox;
        CharacterComponents.addListFollower += addColorBox;
        CharacterComponents.removeListFollower += removeBox;
    }
    private void OnDisable()
    {
        EventManager.onUpdateListCollectableObject -= addColorBox;
        CharacterComponents.addListFollower -= addColorBox;
        CharacterComponents.removeListFollower -= removeBox;
    }
    private GameObject removeBox()
    {
        GameObject obj;
        if (FollowersList[0].followers.Count > 0)
        {
            obj = FollowersList[0].followers[FollowersList[0].followers.Count - 1];
            FollowersList[0].followers.RemoveAt(FollowersList[0].followers.Count - 1);
            return obj;
        }
        else return null;
    }
    private void addColorBox(GameObject obj,int colorint)
    {
        if (FollowersList[colorint].followers.Count>0)
        {
            obj.transform.SetParent(transform);
            Vector3 toWhere = FollowersList[colorint].followers[FollowersList[colorint].followers.Count - 1].transform.localPosition;
            Vector3 toLocal = FollowersList[colorint].followers[0].transform.localPosition;
            obj.transform.DOLocalJump(new Vector3(toLocal.x,toWhere.y,toLocal.z) + Vector3.up * yExenPointPluss, 4f, 1, .3f).OnComplete(() => {
                if (!FollowersList[colorint].followers.Contains(obj))
                {
                    FollowersList[colorint].followers.Add(obj);
                }
            });
        }
        else
        {
            obj.transform.SetParent(transform);
            obj.transform.DOLocalJump(FollowersList[colorint].firstObject.transform.position + Vector3.up * yExenPointPluss, 4f, 1, .3f).OnComplete(() => {
                if (!FollowersList[colorint].followers.Contains(obj))
                {
                    FollowersList[colorint].followers.Add(obj);
                }
            });
        }
    
    
   
    }
    private void follows() {
        for (int j = 0; j < FollowersList.Length; j++)
        {
            for (int i = 0; i < FollowersList[j].followers.Count; i++)
            {
                if (i == 0)
                {
                    FollowersList[j].followers[0].transform.position = Vector3.Lerp(FollowersList[j].followers[0].transform.position, FollowersList[j].firstObject.transform.position, followSpeed*Time.deltaTime);

                }
                else
                {
                    FollowersList[j].followers[i].transform.position = Vector3.Lerp(FollowersList[j].followers[i].transform.position, new Vector3(FollowersList[j].followers[i-1].transform.position.x,
                     FollowersList[j].followers[i - 1].transform.position.y + FollowersList[j].followers[i - 1].transform.localScale.y, FollowersList[j].followers[i-1].transform.position.z), followSpeed * Time.deltaTime);
                }
            }
        }
    }
    private void Steady()
    {
        for (int j = 0; j < FollowersList.Length; j++)
        {
            for (int i = 0; i < FollowersList[j].followers.Count; i++)
            {
                if (i == 0)
                {
                    FollowersList[j].followers[0].transform.position = Vector3.Lerp(FollowersList[j].followers[0].transform.position, FollowersList[j].firstObject.transform.position+ Vector3.up * yExenPointPluss, followSpeed*Time.deltaTime);
                    FollowersList[j].followers[0].transform.rotation = Character.transform.rotation;
                }
                else
                {
                    FollowersList[j].followers[i].transform.position = Vector3.Lerp(FollowersList[j].followers[i].transform.position, new Vector3(FollowersList[j].followers[i - 1].transform.position.x,
                      FollowersList[j].followers[i - 1].transform.position.y + (FollowersList[j].followers[i].transform.localScale.y+ FollowersList[j].followers[i-1].transform.localScale.y)/2f, FollowersList[j].followers[i - 1].transform.position.z), followSpeed * Time.deltaTime);
                    FollowersList[j].followers[i].transform.localRotation = Character.transform.localRotation;
                }
            }
        }
    }
    private void lookPrevius()
    {
        for (int j = 0; j < FollowersList.Length; j++)
        {
            for (int i = 0; i < FollowersList[j].followers.Count; i++)
            {
                if (i == 0)
                {
                    FollowersList[j].followers[0].transform.position = Vector3.Lerp(FollowersList[j].followers[0].transform.position, FollowersList[j].firstObject.transform.position+Vector3.up*yExenPointPluss, followSpeed * Time.deltaTime);
                    FollowersList[j].followers[i].transform.LookAt(FollowersList[j].firstObject.transform.position-Vector3.up*yExenPointPluss);
                }
                else
                {
                    FollowersList[j].followers[i].transform.position = Vector3.Lerp(FollowersList[j].followers[i].transform.position, new Vector3(FollowersList[j].followers[i - 1].transform.position.x,
                     FollowersList[j].followers[i - 1].transform.position.y + FollowersList[j].followers[i].transform.localScale.y, FollowersList[j].followers[i - 1].transform.position.z), followSpeed * Time.deltaTime);
                    FollowersList[j].followers[i].transform.LookAt(FollowersList[j].followers[i-1].transform.position);
                }
            }
        }
    }
    private void lookPlayerStill()
    {
        for (int j = 0; j < FollowersList.Length; j++)
        {
            for (int i = 0; i < FollowersList[j].followers.Count; i++)
            {
                if (i == 0)
                {
                    FollowersList[j].followers[0].transform.position = Vector3.Lerp(FollowersList[j].followers[0].transform.position, FollowersList[j].firstObject.transform.position, followSpeed * Time.deltaTime);
                    FollowersList[j].followers[0].transform.LookAt(Character.transform.position+Character.transform.forward*lookPlayerDistance);
                }
                else
                {
                    FollowersList[j].followers[i].transform.position = Vector3.Lerp(FollowersList[j].followers[i].transform.position, new Vector3(FollowersList[j].followers[i - 1].transform.position.x,
                     FollowersList[j].followers[i - 1].transform.position.y + yExenPointPluss, FollowersList[j].followers[i - 1].transform.position.z), followSpeed * Time.deltaTime);
                    FollowersList[j].followers[i].transform.LookAt(Character.transform.forward*lookPlayerDistance+Character.transform.position);
                }
            }
        }
    }

}