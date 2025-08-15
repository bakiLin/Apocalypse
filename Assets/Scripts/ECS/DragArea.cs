using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragArea : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas canvas;

    private Filter filter;

    private Stash<PlayerRotate> playerStash;

    private PointerEventData pointerEventData;

    private void Start()
    {
        filter = World.Default.Filter.With<PlayerRotate>().Build();
        playerStash = World.Default.GetStash<PlayerRotate>();
    }

    private void Update()
    {
        if (pointerEventData != null)
            SetRotation(pointerEventData.delta / canvas.scaleFactor);
    }

    private void SetRotation(Vector2 rotation)
    {
        foreach (var entity in filter)
        {
            ref var playerComponent = ref playerStash.Get(entity);
            playerComponent.rotation = rotation;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        pointerEventData = eventData;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        pointerEventData = null;
        SetRotation(Vector2.zero);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
