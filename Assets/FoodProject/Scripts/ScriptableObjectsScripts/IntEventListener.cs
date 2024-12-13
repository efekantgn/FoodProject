using UnityEngine;
using UnityEngine.Events;

public class IntEventListener : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO intEventChannel;


    private void OnEnable()
    {
        if (intEventChannel != null)
        {
            intEventChannel.OnEventRaised += RespondToEvent;
        }
    }

    private void OnDisable()
    {
        if (intEventChannel != null)
        {
            intEventChannel.OnEventRaised -= RespondToEvent;
        }
    }

    private void RespondToEvent(int value)
    {
        Debug.Log($"Event triggered with value: {value}");
    }
}
