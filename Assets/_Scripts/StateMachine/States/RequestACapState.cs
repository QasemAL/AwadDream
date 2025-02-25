using UnityEngine;

public class RequestACapState : State
{
    public override void Enter(AiController ai)
    {
        Debug.Log("Entered RequestACap State ");
        ai.gameObject.GetComponent<NpcInteraction>().canInteractWith = true;
    }

    public override void Update(AiController ai)
    {

    }

    public override void Exit(AiController ai)
    {
        ai.gameObject.GetComponent<NpcInteraction>().canInteractWith = false;
    }


}
