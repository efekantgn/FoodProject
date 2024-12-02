using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform targetCamera;

    void Start()
    {
        // Eğer bir kamera atanmadıysa, ana kamerayı al
        if (targetCamera == null)
        {
            targetCamera = Camera.main.transform;
        }

    }
    // private void LateUpdate()
    // {
    //     transform.forward = -targetCamera.forward;
    //     transform.up = -targetCamera.up;
    // }
}
