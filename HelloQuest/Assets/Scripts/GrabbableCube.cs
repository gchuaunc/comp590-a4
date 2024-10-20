using UnityEngine;

public class GrabbableCube : MonoBehaviour, IPointableObject
{
    [SerializeField] private Rigidbody cubeRigidbody;

    public void OnRTrigger()
    {
        Transform rightController = ControllerManager.instance.rightControllerTransform;
        transform.SetPositionAndRotation(rightController.position, rightController.rotation);
    }

    public void OnRTriggerDown()
    {
        
    }

    public void OnRTriggerUp()
    {
        cubeRigidbody.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RHand);
    }
}
