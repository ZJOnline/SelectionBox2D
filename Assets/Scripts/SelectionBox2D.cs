using UnityEngine;

public class SelectionBox2D : MonoBehaviour
{
    public string sortingLayer = "UI";
    public MouseButton mouseSelectionButton;
    public enum MouseButton { Left, Right, Middle };
    Vector2 clickStart;
    Vector2 clickEnd;
    LineRenderer lineRenderer;
    bool mouseDragging;
    bool mousePressed;
    int mouseButtonID;

    public int MouseButtonID
    {
        get { return (int) mouseSelectionButton; }
    }

    Vector2 ClickEnd
    {
        get { return clickEnd; }
    }

    Vector2 ClickStart
    {
        get { return clickStart; }
    }

    protected Vector3 MouseWorldPosition
    {
        get {
            return new Vector3(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                0
            );
        }
    }

    public bool MouseClicked
    {
        get { return Input.GetMouseButtonDown(MouseButtonID); }
    }

    public bool MouseDown
    {
        get { return Input.GetMouseButton(MouseButtonID); }
    }

    public bool MouseDragging
    {
        get { return mouseDragging; }
    }

    protected LineRenderer LineRenderer
    {
        get { return lineRenderer; }
    }

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.sortingOrder = 4;
        lineRenderer.sortingLayerName = sortingLayer;
    }

    protected void Update()
    {
        if (MouseClicked && !MouseDragging) StartDragging();
        if (MouseDragging) HandleDragging();
        if (!MouseDown && MouseDragging) StopDragging();
    }

    protected virtual void StartDragging()
    {
        clickStart = MouseWorldPosition;
        mouseDragging = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, ClickStart);
        lineRenderer.SetPosition(4, ClickStart);
    }

    protected virtual void HandleDragging()
    {
        clickEnd = MouseWorldPosition;
        lineRenderer.SetPosition(1, new Vector3(ClickEnd.x, ClickStart.y, 0));
        lineRenderer.SetPosition(2, new Vector3(ClickEnd.x, ClickEnd.y, 0));
        lineRenderer.SetPosition(3, new Vector3(ClickStart.x, ClickEnd.y, 0));
    }

    protected virtual void StopDragging()
    {
        mouseDragging = false;
        lineRenderer.enabled = false;
    }
}
