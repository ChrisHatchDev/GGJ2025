using UnityEngine;

public class PlayerCollisionRelay : MonoBehaviour
{
    public Player Player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spear"))
        {
            Debug.Log("Player hit spear");
            Player.HitBySpear();
        }
    }
}
