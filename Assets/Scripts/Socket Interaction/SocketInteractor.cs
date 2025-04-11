using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UniversalSocketInteractor : XRSocketInteractor
{
    public string requiredTag; // Set the tag in the Inspector.

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // Check if the interactable's tag matches the required tag.
        return base.CanSelect(interactable) && interactable.transform.CompareTag(requiredTag);
    }
}
