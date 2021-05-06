using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] private Camera target;
    [SerializeField] private Camera[] cameras;

    private void OnTriggerEnter(Collider other)
    {
        foreach (Camera camera in cameras)
        {
            camera.GetComponent<Camera>().enabled = camera.Equals(target);
        }
    }
}
