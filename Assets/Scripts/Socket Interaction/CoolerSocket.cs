using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoolerSocketInteractor : XRSocketInteractor
{
    public string requiredTag = "Cooler";

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.CompareTag(requiredTag);
    }
}
