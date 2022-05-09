using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
public class TransporterMAnager : MonoBehaviour, ITransfer
{
    private bool isDestroy = false;
    public void transfer(GameObject obj)
    {

    }
    private async Task WaitTransfer(float time, Action onUpdate, Action OnComplete) {
       await Task.Yield();
    }
    private void OnDestroy()
    {
        isDestroy = true;
    }
}
