using UnityEngine;

public class IntEventInvoker : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO intEventChannel;

    public void TriggerEvent(int value)
    {
        if (intEventChannel != null)
        {
            intEventChannel.RaiseEvent(value);
        }
    }
}
