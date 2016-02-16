using UnityEngine;
using System.Collections;

public class Movements : MonoBehaviour {

    [SerializeField]private Transform elements;

    private bool isDragActive = false;
    private bool downInPreviousFrame = false;
    private GameObject selected = null;

    #region Behaviour
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isMouse && e.button == 0) {
            downInPreviousFrame = true;
            /*if (e.control)
                VerticalRotation(elephant);
            else if (e.shift)
                VerticalTranslation(elephant, 1); //positiv => up && negatif => down
            else
                HorizontalRotation(elephant);*/
        } else {
            if (isDragActive)
            {
                selected = null;
                isDragActive = false;
                OnDraggingEnd();
            }
            downInPreviousFrame = false;
        }
    }

    void Update() {
        if (downInPreviousFrame)
        {
            if (isDragActive)
                OnDragging();
            else
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "elem")
                    {
                        selected = hit.collider.gameObject;
                        isDragActive = true;
                        OnDraggingStart();
                    }
                }
            }
        }
    }
    #endregion

    #region Drag&Drop
    public virtual void OnDraggingStart() {
        print(selected.name);
    }

    public virtual void OnDragging() {
        
    }

    public virtual void OnDraggingEnd() {

    }
    #endregion

    #region Movements
    private void HorizontalRotation(Transform form) {
        form.Rotate(new Vector3(0, 1, 0), 15, Space.World);
    }

    private void VerticalRotation(Transform form) {
        form.Rotate(new Vector3(1, 0, 0), 15, Space.World);
    }

    private void VerticalTranslation(Transform form, int y) {
        form.Translate(0, y, 0, Space.World);
    }
    #endregion
}