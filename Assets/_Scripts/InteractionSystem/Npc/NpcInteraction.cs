using UnityEngine;

public class NpcInteraction : MonoBehaviour,IInteractable
{

    public bool canInteractWith = false;
    public float MaxTargetTripRange = 5f;
    public GameObject PointPreFab;


    private int TripMoney;
    private Vector3 TargetTripPoint;



    public void Interact()
    {
        Debug.Log("Npc Interaction");
        

    }
}
