// Interface for all gameobjects that must react when the controller
// is pointed at them and an action is taken.
public interface IPointableObject
{
    void OnRTriggerDown();
    void OnRTrigger();
    void OnRTriggerUp();
}
