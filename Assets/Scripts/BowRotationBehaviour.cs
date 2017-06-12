using UnityEngine;

public class BowRotationBehaviour : MonoBehaviour {
    private GameObject ArrowController;
    private Quaternion LastBowRotation;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (ArrowManagerBehaviour.IsArrowAttached)
        {
            ArrowController = ArrowManagerBehaviour.Instance.ArrowController;
            Debug.Log(ArrowController);
            // look away from right Controller
            var rotate = Quaternion.LookRotation(transform.position - ArrowController.transform.position);
            // only move z axis when rotating left controller
            Quaternion desiredRotation = Quaternion.Euler(rotate.eulerAngles.x, rotate.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = desiredRotation;
        }
        else
        {
            LastBowRotation = transform.rotation;
            transform.rotation = transform.parent.rotation;
            transform.Rotate(90, 180, 0);
        }
    }
}
