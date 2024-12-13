using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/Int Event Channel", fileName = "NewIntEventChannel")]
public class IntEventChannelSO : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }
        else
        {
            Debug.LogWarning($"No listeners for {name} event!");
        }
    }
}
