using UnityEngine;

public class MatchTwoPositions : MonoBehaviour
{
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private Vector3 upDirection = Vector3.up;

    void Update()
    {
        if (target1 != null && target2 != null)
        {
            Vector3 averagePosition = (target1.position + target2.position) / 2;
            transform.position = averagePosition;

            Vector3 averageForward = (target1.forward + target2.forward).normalized;
            transform.rotation = Quaternion.LookRotation(averageForward, upDirection);
        }
    }
}
