using UnityEngine;

public class IdleState : State
{


    private float timer;

    public override void Enter(AiController ai)
    {
        timer = 0;
        
        ai.animator.Play(AiAnimation.Idle);
    }

    public override void Update(AiController ai)
    {
        timer += Time.deltaTime;

        if (timer >= ai.ThinkingBetweenStates)
        { 
           if(ai.ShouldMove())
            ai.ChangeState(new MoveingState()); 
           else if (ai.ShouldRequestACap())
              ai.ChangeState(new RequestACapState());
           else
            ai.ChangeState(new IdleState());
        }
    }

    public override void Exit(AiController ai)
    {
        timer = 0;
    }


}
