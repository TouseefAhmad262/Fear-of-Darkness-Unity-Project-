using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public float MaxInteractDistance = 5f;
    private InteractableObject CurrentInteraction;

    bool CanInteract;

    void Start()
    {
        InputsManager.Instance.Controls.Player.Interact.performed += Doxfen => OnInteract();
        CanInteract = true;
    }

    void Update()
    {
        if (!CanInteract)
            return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, MaxInteractDistance);
        InteractableObject nearestInteractable = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Interactable") && hitCollider.gameObject.GetComponent<InteractableObject>() != null)
            {
                InteractableObject interactable = hitCollider.gameObject.GetComponent<InteractableObject>();
                float distanceSqr = (hitCollider.transform.position - transform.position).sqrMagnitude;

                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    nearestInteractable = interactable;
                }
            }
        }

        CurrentInteraction = nearestInteractable;

        if (CurrentInteraction != null)
        {
            print("Can Interact with " + CurrentInteraction.Title);
        }
    }

    void OnInteract()
    {
        if (CurrentInteraction != null)
        {
            CurrentInteraction.Action();
        }
        else
        {
            print("Nothing to interact with");
        }
    }

    public void SetCanNotintract()
    {
        CanInteract = false;
    }

    public void SetCanintract()
    {
        CanInteract = true;
    }
}