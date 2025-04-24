using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Camera mainCamera;
    public Vector3 targetCameraPosition;

    private void Start()
    {
        mainCamera = Camera.main;
        targetCameraPosition = mainCamera.transform.position;
    }

    private void LateUpdate()
    {
        if (mainCamera != null)
        {
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetCameraPosition, ref velocity, smoothTime);
        }
    }

    public void SetCameraTarget(Vector3 position)
    {
        targetCameraPosition = new Vector3(position.x, position.y, mainCamera.transform.position.z);
    }
}