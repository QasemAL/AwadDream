using UnityEngine.InputSystem.XR;

public abstract class State
{
    public abstract void Enter(AiController ai);
    public abstract void Update(AiController ai);
    public abstract void Exit(AiController ai);
}