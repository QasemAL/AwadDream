using System.Collections;
using UnityEngine;

public class AiController : MonoBehaviour
{
    
    public float ThinkingBetweenStates = 5;
    public float ThinkingBeforeLeaving = 100;
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

    public float fadeDuration = 1.5f; 
    private SpriteRenderer spriteRenderer;


    public Animator animator;


    void Start()
    {
        ChangeState(new IdleState());
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        float randomX = transform.position.x + Random.Range(-MaxMoveToRange, MaxMoveToRange);
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
        StartCoroutine(FadeAndDestroy());
    }



    private IEnumerator FadeAndDestroy()
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        Destroy(gameObject);
    }

}
