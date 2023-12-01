using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float smoothTime = 0.3f;
    [SerializeField]
    float maxSpeed = 20f;
    float minZoom = 2f;
    float maxZoom = 5f;
    private Camera cameraComponent;
    private Vector3 velocity = Vector3.zero;
    private void Start()
    {
        cameraComponent = GetComponent<Camera>();
    }
    private void Update()
    {
        if (target == null) return;
        float currentZoom = cameraComponent.orthographicSize;
        float targetZoom = (((maxZoom - minZoom) * target.GetComponent<TopDownController>().GetVelocityMagnitude()) / maxSpeed) + minZoom;
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = smoothPosition;
        cameraComponent.orthographicSize = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime);
    }
}
