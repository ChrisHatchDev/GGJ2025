using System;
using System.Threading.Tasks;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public MeshRenderer Renderer;
    public MeshRenderer Renderer2;

    public Player _player;
    
    [SerializeField] private Rigidbody _rigid;
    [SerializeField] private float _lungeDistance = 2;
    [SerializeField] private int _lungeDelay = 250;

    public bool CanLunge = true;

    private void Start()
    {
        _player = transform.parent.parent.GetComponentInChildren<Player>();
        gameObject.tag = _player.PlayerNumber == Player.Players.One ? "Spear1" : "Spear2";
    }

    public async void Throw()
    {
        if (!CanLunge)
        {
            return;
        }
        
        Debug.Log("Spear thrown!");
        _rigid.transform.localPosition = (transform.localPosition + transform.forward * _lungeDistance);
        CanLunge = false;
        
        await Task.Delay(_lungeDelay);
        
        _rigid.transform.localPosition = (transform.localPosition - transform.forward * _lungeDistance);
        CanLunge = true;
    }
}
