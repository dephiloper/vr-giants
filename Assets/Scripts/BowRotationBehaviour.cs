using UnityEngine;

public class BowRotationBehaviour : MonoBehaviour {

    private void Update ()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (ArrowManagerBehaviour.IsArrowAttached)
        {
            // look away from right Controller
            var rotate = Quaternion.LookRotation(transform.position - ArrowManagerBehaviour.Instance.ArrowControllerPosition);
            // only move z axis when rotating left controller
            var desiredRotation = Quaternion.Euler(rotate.eulerAngles.x, rotate.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = desiredRotation;
        }
        else
        {
            transform.rotation = transform.parent.rotation;
            transform.Rotate(90, 180, 0);
        }
    }
}
