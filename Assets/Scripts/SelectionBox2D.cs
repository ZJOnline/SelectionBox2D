using UnityEngine;

public class SelectionBox2D : MonoBehaviour
{
    public string sortingLayer = "UI";
    public MouseButton mouseSelectionButton;
    public enum MouseButton { Left, Right, Middle };
    Vector3 clickStart;
    Vector3 clickEnd;
    Rect selectedRect;
    LineRenderer lineRenderer;
    bool mouseDragging;
    bool mousePressed;
    int mouseButtonID;

    public int MouseButtonID
    {
        get { return (int) mouseSelectionButton; }
    }

    public Rect SelectedRect
    {
        get { return selectedRect; }
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
        lineRenderer.SetPositions(new Vector3[5] {
            Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero
        });
        selectedRect = Rect.zero;
    }

    void Update()
    {
        if (MouseClicked && !MouseDragging) StartDragging();
        if (MouseDragging) HandleDragging();
        if (!MouseDown && MouseDragging) StopDragging();
    }

    protected void StartDragging()
    {
        selectedRect.x = MouseWorldPosition.x;
        selectedRect.y = MouseWorldPosition.y;
        mouseDragging = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, MouseWorldPosition);
        lineRenderer.SetPosition(4, MouseWorldPosition);
    }

    protected void HandleDragging()
    {
        selectedRect.width = MouseWorldPosition.x;
        selectedRect.height = MouseWorldPosition.y;
        lineRenderer.SetPosition(1, new Vector3(SelectedRect.width, SelectedRect.y, 0));
        lineRenderer.SetPosition(2, new Vector3(SelectedRect.width, SelectedRect.height, 0));
        lineRenderer.SetPosition(3, new Vector3(SelectedRect.x, SelectedRect.height, 0));
    }

    protected void StopDragging()
    {
        mouseDragging = false;
        lineRenderer.enabled = false;
    }
}
