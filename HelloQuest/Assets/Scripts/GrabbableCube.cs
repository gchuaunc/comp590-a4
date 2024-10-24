using UnityEngine;

public class GrabbableCube : MonoBehaviour, IPointableObject
{
    [SerializeField] private Rigidbody cubeRigidbody;
    [SerializeField] private float moveAmount = 5f;

    public void OnRTrigger()
    {
        Transform rightController = ControllerManager.instance.rightControllerTransform;
        transform.SetPositionAndRotation(rightController.position + rightController.TransformDirection(Vector3.forward) * moveAmount, rightController.rotation);
        cubeRigidbody.velocity = Vector3.zero;
    }

    public void OnRTriggerDown()
    {
        
    }

    public void OnRTriggerUp()
    {
        //cubeRigidbody.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RHand);
        Vector3 velocity = ControllerManager.instance.RightControllerVelocity;
        Debug.Log(velocity);
        cubeRigidbody.velocity = velocity;
    }
}
