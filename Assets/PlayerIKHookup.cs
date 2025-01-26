using Normal.Realtime;
using RootMotion.FinalIK;
using UnityEngine;

public class PlayerIKHookup : MonoBehaviour
{
    public BipedIK IKController;
    [SerializeField] private GameObject MouseAimTarget;

    public RealtimeView _realtimeView;
    
    private void Start()
    {
        if (_realtimeView == null)
        {
            _realtimeView = GetComponentInParent<RealtimeView>();
        }
        
        // IKController.solver.aim.target = MouseAimTarget.transform;
        IKController.solvers.leftHand.target = MouseAimTarget.transform;
        IKController.solvers.rightHand.target = MouseAimTarget.transform;
        IKController.solvers.lookAt.target = MouseAimTarget.transform;
    }
}
