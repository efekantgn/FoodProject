using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animator doorAnim;
    public int PlayerCount = 0;

    public string NPCCount = "NPCCount";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            PlayerCount++;
            doorAnim.SetInteger(NPCCount, PlayerCount);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            PlayerCount--;
            doorAnim.SetInteger(NPCCount, PlayerCount);

        }
    }

}