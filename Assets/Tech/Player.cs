using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Spear _spear;
    [SerializeField] private Vector3 _minScale = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private Vector3 _maxScale = new Vector3(1.75f, 1.75f, 1.75f);
    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _blowUpSpeed = 1;
    [SerializeField] private float _deflateSpeed = 1;
    [SerializeField] private AnimationCurve _inflateCurve;

    public bool Popped = false;

    private void Start()
    {
        _targetScale = transform.localScale;
    }

    void Update()
    {
        // Close your eyes, this is going to be a bumpy ride :D
        // Jan 2022: I'm sorry, I can't do this anymore. I'm going to have to stop here.
        // Brought to you by the letter 'I' for 'I'm sorry'
        // and the number '1' for '1 more line of code and I'm going to lose it'
        // and the number '2' for '2 more lines of code and I'm going to lose it'
        // Written by Copilot, the AI that's going to take my job
        if (Popped)
        {
            _spear.Renderer.enabled = false;
            _spear.Renderer2.enabled = false;
            _meshRenderer.enabled = false;
            return;
        }
        else
        {
            _spear.Renderer.enabled = true;
            _spear.Renderer2.enabled = true;
            _meshRenderer.enabled = true;
        }
        
        if (transform.localScale.x > _maxScale.x)
        {
            Popped = true;
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            _spear.Throw();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BlowUpBubble();
        }
        
        if (transform.localScale.magnitude < _minScale.magnitude)
        {
            transform.localScale = _minScale;
            _targetScale = _minScale;
            return;
        }
        
        _targetScale -= Vector3.one * (Time.deltaTime * _deflateSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, _inflateCurve.Evaluate(Time.deltaTime * _blowUpSpeed));
    }

    public void BlowUpBubble(float amount = 0.5f)
    {
        if (_targetScale.magnitude > _maxScale.magnitude)
        {
            Debug.Log("already at max, returning");
            return;
        }
        
        _targetScale += Vector3.one * amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spear"))
        {
            Debug.Log("Player hit spear");
        }
    }
}
