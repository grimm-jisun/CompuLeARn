using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RAMSocketInteractor : XRSocketInteractor
{
    public string requiredTag = "RAM";

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.CompareTag(requiredTag);
    }
}
