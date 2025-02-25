using UnityEngine;

public class AiController : MonoBehaviour
{
    
    public float ThinkingBetweenStates = 5;
    public float ThinkingBeforeLeaving = 5;
    public int AngreyThreashHold = 2;
    public float speed = 1f;

    private State currentState;
    
    [Range(0f, 1f)]
    public float MovePrecent = 0.5f;

    [Range(0f, 1f)]
    public float RequestAcapPrecent = 0.5f;


    public Vector3 TargetPoint;

    public LayerMask obstacleMask;

    public float MaxMoveToRange = 5f;
    public float NpcYAxis = -0.9f;


    void Start()
    {
        ChangeState(new IdleState());
    }



    void Update()
    {
        currentState?.Update(this);
    }

    public void ChangeState(State newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }

    public void flipsprite(Vector3 target)
    {
        if (target == null)
            return;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = target.x < transform.position.x;
    }




    public void MoveTowards(Vector3 target)
    {
        flipsprite(target);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }


    public bool InLineofSight(Vector3 target)
    {
        if (target == null)
            return false;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, target, obstacleMask);

        return hit.collider == null;
    }

    public void GetRandomPoint()
    {
        float randomX = Random.Range(-MaxMoveToRange, MaxMoveToRange);
        TargetPoint = new Vector3(randomX, NpcYAxis, 0); 
    }

    public bool ReachedDestination(Vector3 point)
    {
        return Vector3.Distance(transform.position, point) < 0.1f;
    }

    public bool ShouldMove()
    {
        return Random.value > MovePrecent;
    }

    public bool ShouldRequestACap()
    {
        return Random.value > RequestAcapPrecent;
    }


    public void DestroyPlayer()
    {
        Debug.Log("Npc Left The City");
        Destroy(gameObject);
    }

}
