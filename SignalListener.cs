using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public SignalSender signal;
    public UnityEvent signalEvent;

    private void OnEnable(){
        signal.RegisterListener(this);
    }

    private void OnDisable(){
        signal.DeRegisterListener(this);
    }

    public void OnSignalRaised(){
        signalEvent.Invoke();
    }

    

}
