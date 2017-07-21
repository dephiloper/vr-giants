using UnityEngine;

/// <summary>
/// Represents a behaviour which activates the renderer of its tutorial planes (children) if the player looks in 
/// there direction.
/// </summary>
public class TutorialVisibleBehaviour : MonoBehaviour
{
    private void Update(){
        if (TutorialBehaviour.Instance)
            TutorialBehaviour.Instance.Hide();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, float.PositiveInfinity)) {
            if (TagUtility.IsTutorialPlane(hit.transform.tag)) {
                hit.transform.GetComponentInChildren<MeshRenderer>().enabled = true;
            }
        }
    }
}