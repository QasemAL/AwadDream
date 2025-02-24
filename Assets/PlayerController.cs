using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public Button leftButton;   // Reference to the left arrow button
    public Button rightButton;  // Reference to the right arrow button
    //public GameObject clientPanel; // UI panel for accepting/rejecting clients
    //public Button acceptButton;    // Button to accept the client
    //public Button rejectButton;    // Button to reject the client
    //private GameObject player;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool isInteractingWithClient = false;

    void Start()
    {
        //player = GetComponent<GameObject>();
    }

    void Update()
    {
        if (!isInteractingWithClient)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        if (isMovingLeft)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            //player.layer = 3;////////////////////
        }
        if (isMovingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            //player.layer = 4;///////////////////
        }
    }

    public void OnLeftButtonPressed()
    {
        isMovingLeft = true;
    }

    public void OnLeftButtonReleased()
    {
        isMovingLeft = false;
    }

    public void OnRightButtonPressed()
    {
        isMovingRight = true;
    }

    public void OnRightButtonReleased()
    {
        isMovingRight = false;
    }

    //public void OnClientClicked()
    //{
    //    isInteractingWithClient = true;
    //    clientPanel.SetActive(true); // Show the accept/reject panel
    //}

    //public void OnAcceptButtonClicked()
    //{
    //    Debug.Log("Client Accepted");
    //    clientPanel.SetActive(false);
    //    isInteractingWithClient = false;
    //}

    //public void OnRejectButtonClicked()
    //{
    //    Debug.Log("Client Rejected");
    //    clientPanel.SetActive(false);
    //    isInteractingWithClient = false;
    //}
}