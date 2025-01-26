using UnityEngine;
using UnityEngine.Serialization;

public class MatchTwoPositions : MonoBehaviour
{
    // The cube on the mouse or whatever else is the "aim target"
    [SerializeField] private Transform LookAtTarget;
    
    // These two are the hands
    [FormerlySerializedAs("target1")] [SerializeField] private Transform LeftHand;
    [FormerlySerializedAs("target2")] [SerializeField] private Transform RightHand;
    // [SerializeField] private Vector3 upDirection = Vector3.up;

    void Update()
    {
        if (LeftHand != null && RightHand != null)
        {
            Vector3 averagePosition = (LeftHand.position + RightHand.position) / 2;
            transform.position = averagePosition;
            
            // Make rotation look at LookAtTarget
            if (LookAtTarget != null)
            {
                var direction = LookAtTarget.position - transform.position;
                transform.rotation = Quaternion.LookRotation(direction, transform.up);
            }

            // Vector3 averageForward = (target1.forward + target2.forward).normalized;
            // transform.rotation = Quaternion.LookRotation(averageForward, transform.forward);
        }
    }
}
