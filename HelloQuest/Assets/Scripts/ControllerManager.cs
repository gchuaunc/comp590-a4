using UnityEngine;
using TMPro;
using System.Text;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TMP_Text debugText;

    public Transform leftControllerTransform;
    public Transform rightControllerTransform;
    public Vector3 LeftControllerVelocity { get; private set; }
    public Vector3 RightControllerVelocity { get; private set; }

    private Vector3 prevLeftControllerPos;
    private Vector3 prevRightControllerPos;
    private IPointableObject grabbedObject;

    public static ControllerManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Warning: More than one ControllerManager detected. Disabling this one.");
            enabled = false;
        }
    }

    private void Start()
    {
        prevLeftControllerPos = leftControllerTransform.position;
        prevRightControllerPos = rightControllerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // calculate and update velocity
        RightControllerVelocity = (rightControllerTransform.position - prevRightControllerPos) / Time.deltaTime;
        LeftControllerVelocity = (leftControllerTransform.position - prevLeftControllerPos) / Time.deltaTime;
        prevRightControllerPos = rightControllerTransform.position;
        prevLeftControllerPos = leftControllerTransform.position;

        // pointing and grabbing handling
        StringBuilder debug = new StringBuilder("");
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) {
            debug.Append("DOWN");
            if (Physics.Raycast(rightControllerTransform.position, rightControllerTransform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                debug.Append(" HIT ").Append(hit.collider.gameObject.name);
                if (hit.collider.gameObject.TryGetComponent<IPointableObject>(out IPointableObject pointableObject)) {
                    debug.Append(" pointableObject");
                    pointableObject.OnRTriggerDown();
                    grabbedObject = pointableObject;
                }
            }
            debug.Append("\n");
        } else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) {
            debug.Append("HOLD\n");
            grabbedObject?.OnRTrigger();
        } else if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) {
            debug.Append("UP\n");
            grabbedObject?.OnRTriggerUp();
            grabbedObject = null;
        }
        debugText.text = debug.ToString();
        //Debug.Log(debug.ToString());
    }
}
