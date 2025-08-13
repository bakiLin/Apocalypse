using UnityEngine;
using UnityEngine.EventSystems;

public class DragArea : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private Transform player, pivot;

    [SerializeField]
    private float rotationSpeed, limitAngle;

    [SerializeField]
    private Canvas canvas;

    private Vector3 rotation;

    private float angle;

    public void OnDrag(PointerEventData eventData)
    {
        rotation = eventData.delta / canvas.scaleFactor * Time.deltaTime * rotationSpeed;

        angle = player.rotation.eulerAngles.y + rotation.x;
        player.rotation = Quaternion.Euler(0f, angle, 0f);

        angle = pivot.rotation.eulerAngles.x - rotation.y;
        if (angle > limitAngle && angle < (340f - limitAngle)) angle = limitAngle;
        else if (angle < (360f - limitAngle) && angle > limitAngle) angle = 360f - limitAngle;
        pivot.rotation = Quaternion.Euler(angle, pivot.rotation.eulerAngles.y, 0f);
    }
}
