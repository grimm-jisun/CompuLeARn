using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour
{
    public Button rotateButton;
    public Button resetRotationButton;
    public Button sidewardsButton;
    public Button upwardsButton;
    public Button deleteButton;
    private Transform selectedObject;

    void Start()
    {
        rotateButton.onClick.AddListener(RotateSelectedObject);
        resetRotationButton.onClick.AddListener(ResetRotation);
        sidewardsButton.onClick.AddListener(RotateSidewards);
        upwardsButton.onClick.AddListener(RotateUpwards);
        deleteButton.onClick.AddListener(DeleteSelectedObject);
    }

    public void SetSelectedObject(Transform obj)
    {
        selectedObject = obj;
    }

    private void RotateSelectedObject()
    {
        if (selectedObject != null)
        {
            selectedObject.Rotate(90f, 0f, 0f, Space.Self);
        }
    }

    private void ResetRotation()
    {
        if (selectedObject != null)
        {
            selectedObject.rotation = Quaternion.identity;
        }
    }

    private void RotateSidewards()
    {
        if (selectedObject != null)
        {
            selectedObject.Rotate(0f, 90f, 0f, Space.Self);
        }
    }

    private void RotateUpwards()
    {
        if (selectedObject != null)
        {
            selectedObject.Rotate(0f, 0f, 90f, Space.Self);
        }
    }

    private void DeleteSelectedObject()
    {
        if (selectedObject != null)
        {
            Destroy(selectedObject.gameObject);
            selectedObject = null;
        }
    }
}
