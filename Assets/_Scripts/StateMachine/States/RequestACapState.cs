using UnityEngine;

public class RequestACapState : State
{

    private float timer;
    


    public override void Enter(AiController ai)
    {
         ai.gameObject.GetComponent<NpcInteraction>().canInteractWith = true;
        
    }

    public override void Update(AiController ai)
    {
        timer += Time.deltaTime;

        if (timer >= ai.ThinkingBeforeLeaving)
        {
            if (ai.AngreyThreashHold >= 2)
            {
                ai.DestroyPlayer();
            }

            else if (ai.ShouldRequestACap())
                ai.ChangeState(new RequestACapState());
            else
                ai.ChangeState(new IdleState());
        }
    }

    public override void Exit(AiController ai)
    {
        ai.gameObject.GetComponent<NpcInteraction>().canInteractWith = false;
        ai.AngreyThreashHold++;
        timer = 0;
    }


}
