using UnityEngine;

/// <summary> Camera swing. </summary>
/// <remarks>
/// This code is designed for a simple demo, not for production environments.
/// </remarks>
[RequireComponent(typeof(Camera))]
public sealed class DoubleVisionDemoCamera : MonoBehaviour
{
  [SerializeField]
  private Transform target;

  [SerializeField]
  private Vector3 lookAtOffset;

  [SerializeField]
  private Vector3 swingStrength;

  [SerializeField]
  private Vector3 swingVelocity;

  private Camera cam;

  private Vector3 originalPosition;

  private void Awake()
  {
    cam = GetComponent<Camera>();
    originalPosition = cam.transform.position;
  }

  private void Update()
  {
    Vector3 position = originalPosition;
    position.x += Mathf.Sin(Time.time * swingVelocity.x) * swingStrength.x;
    position.y += Mathf.Cos(Time.time * swingVelocity.y) * swingStrength.y;
    position.z += Mathf.Sin(Time.time * swingVelocity.z) * swingStrength.z;

    cam.transform.position = position;

    cam.transform.LookAt(target.position + lookAtOffset);
  }
}
