using UnityEngine;

public class AnimatorAnimatonEvent : MonoBehaviour
{
    CharModelSelector charModelSelector;

    private void Awake()
    {
        charModelSelector = GetComponentInParent<CharModelSelector>();
    }
    public void AnimationEvent()
    {
        charModelSelector.movement.OnPlayerStandUp?.Invoke();
    }
}