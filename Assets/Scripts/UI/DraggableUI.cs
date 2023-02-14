using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector2 mousePosition = new Vector2();
    private Vector2 startPosition = new Vector2();
    private Vector2 differencePoint = new Vector2();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdateMousePosition();
        }
        if (Input.GetMouseButtonDown(0))
        {
            UpdateStartPosition();
            UpdateDifferencePoint();
        }

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*Minus the difference point so you can pick the 
        element from the edges, without any jerk*/

        transform.position = mousePosition - differencePoint;
    }

    private void UpdateMousePosition()
    {
        mousePosition.x = Input.mousePosition.x;
        mousePosition.y = Input.mousePosition.y;
    }

    private void UpdateStartPosition()
    {
        startPosition.x = transform.position.x;
        startPosition.y = transform.position.y;
    }

    private void UpdateDifferencePoint()
    {
        differencePoint = mousePosition - startPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //child class method
    }
}

