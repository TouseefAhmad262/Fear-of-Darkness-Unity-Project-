using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class VendingMachine : MonoBehaviour
{
    void Awake()
    {
        InteractableObject Intraction = GetComponent<InteractableObject>();
        Intraction.Title = "Take Cofee";
        Intraction.Action = () => Interacte();
    }

    void Interacte()
    {
        print("A cofee pack is taken");
        GameManager.Instance.Player.gameObject.GetComponent<Animator>().SetTrigger("Take Cofee");
    }
}