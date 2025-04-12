using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // EventSystem support for touch input

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    private List<string> tagsToCheck = new List<string> { "SSD", "MOBO", "PSU", "CPU", "GPU", "Cooler", "RAM", "Case" };

    public RotateObject rotateObjectScript;
    public GameObject activeGameObject; // Reference to the GameObject to activate/deactivate

    void Start()
    {
        activeGameObject.SetActive(false); // Initially deactivate GameObject
    }

    void Update()
    {
        // Highlight Logic
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        }
#endif

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (tagsToCheck.Contains(highlight.tag) && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }

        // Selection Logic
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() ||
#if UNITY_ANDROID
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#endif
        )
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = raycastHit.transform;
                selection.gameObject.GetComponent<Outline>().enabled = true;

                // Activate the specified GameObject
                activeGameObject.SetActive(true);
                rotateObjectScript.SetSelectedObject(selection);
                highlight = null;
            }
            else
            {
                if (selection && !EventSystem.current.IsPointerOverGameObject())
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;

                    // Deactivate the GameObject
                    activeGameObject.SetActive(false);
                    rotateObjectScript.SetSelectedObject(null);
                }
            }
        }
    }
}
