using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CPUSocketInteractor : XRSocketInteractor
{
    public string requiredTag = "CPU";

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.CompareTag(requiredTag);
    }
}
