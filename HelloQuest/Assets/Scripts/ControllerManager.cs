using UnityEngine;
using TMPro;
using System.Text;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TMP_Text debugText;

    public Transform leftControllerTransform;
    public Transform rightControllerTransform;

    public static ControllerManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Warning: More than one ControllerManager detected. Disabling this one.");
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StringBuilder debug = new StringBuilder("");
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) {
            debug.Append("DOWN");
            if (Physics.Raycast(rightControllerTransform.position, rightControllerTransform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                debug.Append(" HIT ").Append(hit.collider.gameObject.name);
                if (hit.collider.gameObject.TryGetComponent<IPointableObject>(out IPointableObject pointableObject)) {
                    debug.Append(" pointableObject");
                    pointableObject.OnRTriggerDown();
                }
            }
            debug.Append("\n");
        } else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) {
            debug.Append("HOLD");
            if (Physics.Raycast(rightControllerTransform.position, rightControllerTransform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                debug.Append(" HIT ").Append(hit.collider.gameObject.name);
                if (hit.collider.gameObject.TryGetComponent<IPointableObject>(out IPointableObject pointableObject)) {
                    debug.Append(" pointableObject");
                    pointableObject.OnRTrigger();
                }
            }
            debug.Append("\n");
        } else if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) {
            debug.Append("UP");
            if (Physics.Raycast(rightControllerTransform.position, rightControllerTransform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                debug.Append(" HIT ").Append(hit.collider.gameObject.name);
                if (hit.collider.gameObject.TryGetComponent<IPointableObject>(out IPointableObject pointableObject)) {
                    debug.Append(" pointableObject");
                    pointableObject.OnRTriggerUp();
                }
            }
        }
        debugText.text = debug.ToString();
        //debugText.GetComponent<TextMeshPro>().text = debug.ToString();
        Debug.Log(debug.ToString());
    }
}
