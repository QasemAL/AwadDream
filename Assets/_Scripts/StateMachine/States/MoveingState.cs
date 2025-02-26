using UnityEngine;

public class MoveingState : State
{
    public override void Enter(AiController ai)
    {
        ai.GetRandomPoint();
        ai.animator.Play(AiAnimation.Running);

    }

    public override void Update(AiController ai)
    {

        if (ai.InLineofSight(ai.TargetPoint))
        {
            ai.MoveTowards(ai.TargetPoint);
        }
        else
        {
            ai.GetRandomPoint();
        }



        if (ai.ReachedDestination(ai.TargetPoint))
        {
            ai.ChangeState(new IdleState());
        }

  

    }

    public override void Exit(AiController ai)
    {
    }

}
