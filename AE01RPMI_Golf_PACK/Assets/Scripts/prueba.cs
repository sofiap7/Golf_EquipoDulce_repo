
using UnityEngine;
using UnityEngine.InputSystem;

public class prueba : MonoBehaviour
{
    [SerializeField] private float shotPower;
    [SerializeField] private float stopVelocity = 0.05f;
    [SerializeField] private LineRenderer lineRenderer;

    private bool isIdle;
    private bool isAiming;
    private bool hasAimPoint;

    private Vector3 lastAimPoint;

    private Rigidbody rigidbody;
    private Collider playerCollider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();

        isIdle = false;
        isAiming = false;
        hasAimPoint = false;

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (!isIdle && rigidbody.linearVelocity.magnitude < stopVelocity)
        {
            Stop();
        }

        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame && isIdle)
        {
            isAiming = true;
            hasAimPoint = false;
        }

        ProcessAim();
    }

    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
            return;

        Vector3? worldPoint = CastMouseClickRay();

        if (worldPoint.HasValue)
        {
            lastAimPoint = worldPoint.Value;
            hasAimPoint = true;

            DrawLine(lastAimPoint);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (hasAimPoint)
            {
                Shoot(lastAimPoint);
            }
            else
            {
                CancelAim();
            }
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 direction = (worldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, worldPoint);

        rigidbody.AddForce(direction * strength * shotPower);

        isIdle = false;
    }

    private void DrawLine(Vector3 worldPoint)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, worldPoint);

        lineRenderer.enabled = true;
    }

    private void CancelAim()
    {
        isAiming = false;
        lineRenderer.enabled = false;
    }

    private void Stop()
    {
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        isIdle = true;
    }

    private Vector3? CastMouseClickRay()
    {
        Camera cam = Camera.main;

        if (cam == null)
            return null;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = cam.ScreenPointToRay(mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray);

        float closestDistance = float.MaxValue;
        Vector3? closestPoint = null;

        foreach (RaycastHit hit in hits)
        {
            // Ignore the player's own collider
            if (hit.collider == playerCollider)
                continue;

            if (hit.distance < closestDistance)
            {
                closestDistance = hit.distance;
                closestPoint = hit.point;
            }
        }

        return closestPoint;
    }
}

