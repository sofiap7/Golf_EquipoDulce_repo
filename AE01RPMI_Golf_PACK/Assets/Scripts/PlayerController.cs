using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float shotPower;
    [SerializeField] private float stopVelocity = .05f;

    [SerializeField] private LineRenderer lineRenderer;

    private bool isIdle;
    private bool isAiming;

    private Rigidbody rigidbody;

    private Vector2 mousePosition = Mouse.current.position.ReadValue();

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        isAiming = false;
        lineRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame && isIdle)
        {
            isAiming = true;
        }
        
        if (rigidbody.linearVelocity.magnitude < stopVelocity)
        {
            Stop();
        }

        ProcessAim();
    }

    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
            return;
        }

        Vector3? worldPoint = CastMouseClickRay();

        if (!worldPoint.HasValue)
        {
            return;
        }

        DrawLine(worldPoint.Value);

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Shoot(worldPoint.Value);
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(
            worldPoint.x, 
            worldPoint.y,
            worldPoint.z);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        rigidbody.AddForce(direction * strength * shotPower);
        isIdle = false;
    }

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions =
        {
            transform.position,
            worldPoint
        };
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private void Stop()
    {
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        isIdle = true;
    }

    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3
            (
            mousePosition.x,
            mousePosition.y,
            Camera.main.farClipPlane
            );

        Vector3 screenMousePosNear = new Vector3
            (
            mousePosition.x,
            mousePosition.y,
            Camera.main.nearClipPlane
            );

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;

        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }

}
