using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f;
    private IInteractable closestInteractable;

    public string[] interactableTags = { "Interactable" };


    public GameObject panel;
    public float InteractionButtonOffest = 150;


    void Update()
    {
        FindClosestInteractable();

        if (closestInteractable != null)
        {
            GameObject interactableGameObject = ((MonoBehaviour)closestInteractable).gameObject;


            if (panel != null && interactableGameObject.CompareTag(Tags.Interactable))
            {
                ShowPanel(interactableGameObject);
            }

            if (Input.GetKeyDown(KeyCode.E) && interactableGameObject.CompareTag(Tags.Interactable))
            {
                closestInteractable.Interact();
            }
   
        }
        else
        {
            if (panel != null)
            {
                HidePanel();
            }
        }

    }

    void FindClosestInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange);
        float closestDistance = Mathf.Infinity;
        closestInteractable = null;

        foreach (var hit in hits)
        {
            foreach (string tag in interactableTags)
            {
                if (hit.CompareTag(tag))
                {
                    if (hit.TryGetComponent(out IInteractable interactable) && hit.GetComponent<NpcInteraction>().canInteractWith)
                    {
                        float distance = Vector2.SqrMagnitude((Vector2)transform.position - (Vector2)hit.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestInteractable = interactable;
                        }
                    }
                    break;
                }
            }
        }
    }

    public void ShowPanel(GameObject closestInteractable)
    {
        panel.SetActive(true);

        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();

        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, closestInteractable.transform.position);

        screenPosition.y += InteractionButtonOffest; 

        panelRectTransform.position = screenPosition;
    }



    public void HidePanel()
    {
        panel.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}