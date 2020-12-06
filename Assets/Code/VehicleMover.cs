using UnityEngine;

public class VehicleMover : MonoBehaviour
{

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "MovementPlane")
                {
                    MoveToPosition(hit.point);
                }
            }
        }

        UpdatePosition();
        UpdateRotation();
    }

    private void MoveToPosition(Vector3 point)
    {
        Vector3 direction = point - transform.position;
        direction = transform.parent.InverseTransformDirection(direction);

        if (direction.magnitude < .1f)
        {
            return;
        }

        var newRotation = Quaternion.LookRotation(direction);

        targetRotation = newRotation;
        targetRotation.x = 0;
        targetRotation.z = 0;
        targetPosition = transform.parent.InverseTransformPoint(point);
    }

    private void UpdatePosition()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime);
    }

    private void UpdateRotation()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime);
    }

    public void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
        targetPosition = Vector3.zero;

        transform.localRotation = Quaternion.identity;
        targetRotation = Quaternion.identity;
    }

}
