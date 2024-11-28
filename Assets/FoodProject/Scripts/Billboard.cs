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

    void LateUpdate()
    {
        // UI'nin hedef kameraya bakmasını sağla
        Vector3 direction = targetCamera.position - transform.position;
        //direction.y = 0; // Eğer sadece yatay düzlemde bakmasını istiyorsan bu satırı ekle
        transform.rotation = Quaternion.LookRotation(-direction);
    }
}
