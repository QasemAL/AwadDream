using UnityEngine;
using UnityEngine.UI;

public class ArrowDirection : MonoBehaviour
{
    public GameObject player;
    public Transform targetTransform; // Store Transform instead of Vector2
    public GameObject targetObject;
    public Image arrowImage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        arrowImage = GetComponent<Image>();

   
    }

    void Update()
    {
        if (player == null) return;

        if (targetObject == null)
        {
            targetObject = GameObject.FindGameObjectWithTag("TargetPoint");

            if (targetObject == null)
            {
                arrowImage.enabled = false;
                return;

            }
            arrowImage.enabled = true;

            targetTransform = targetObject.transform;
        }

        if (targetTransform.position.x > player.transform.position.x)
        {
            arrowImage.transform.localScale = new Vector3(1, 1, 1); // Normal
        }
        else
        {
            arrowImage.transform.localScale = new Vector3(-1, 1, 1); // Flipped
        }
    }
}
