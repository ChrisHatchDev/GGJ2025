using System;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Players
    {
        One,
        Two
    }

    public Players PlayerNumber;
    
    [SerializeField] private Spear _spear;
    [SerializeField] private Vector3 _normalScale = Vector3.one;
    [SerializeField] private Vector3 _minScale = new Vector3(0.25f, 0.25f, 0.25f);
    [SerializeField] private Vector3 _maxScale = new Vector3(1.75f, 1.75f, 1.75f);
    [SerializeField] private Renderer _meshRenderer;

    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _blowUpSpeed = 5;
    [SerializeField] private float _deflateSpeed = 10;
    [SerializeField] private AnimationCurve _inflateCurve;
    // [SerializeField] private float StrafeSpeed = 5;
    // [SerializeField] private float _verticalStrafeLimit = 0.5f;
    // [SerializeField] private float _horizontalStrafeLimit = 0.5f;
    // [SerializeField] private float _depthStrafeLimit = 0.5f;
    
    // private float _verticalStrafeLimitCompiled;
    // private float _horizontalStrafeLimitCompiled;
    // private float _depthStrafeLimitCompiled;
    // public bool TwoDimensionalMode = false;

    public bool Popped = false;

    private void Start()
    {
        _targetScale = transform.localScale;
        // _verticalStrafeLimitCompiled = transform.position.y + _verticalStrafeLimit;
        // _horizontalStrafeLimitCompiled = transform.position.x + _horizontalStrafeLimit;
        // _depthStrafeLimitCompiled = transform.position.z + _depthStrafeLimit;
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
            // _spear.Renderer.enabled = false;
            // _spear.Renderer2.enabled = false;
            _meshRenderer.enabled = false;
            return;
        }
        else
        {
            // _spear.Renderer.enabled = true;
            // _spear.Renderer2.enabled = true;
            _meshRenderer.enabled = true;
        }
        
        // if (transform.localScale.x > _maxScale.x)
        // {
        //     Popped = true;
        // }

        // var horizontal = Input.GetAxis("Horizontal");
        // transform.position += (TwoDimensionalMode ? Vector3.forward : Vector3.right) * (horizontal * Time.deltaTime * StrafeSpeed);
        //
        // var vertical = Input.GetAxis("Vertical");
        // transform.position += (TwoDimensionalMode ? Vector3.forward : Vector3.up) * (vertical * Time.deltaTime * StrafeSpeed);
        //
        // transform.position = new Vector3(
        //     Mathf.Clamp(transform.position.x, -_horizontalStrafeLimitCompiled, _horizontalStrafeLimitCompiled),
        //     Mathf.Clamp(transform.position.y, -_verticalStrafeLimitCompiled, _verticalStrafeLimitCompiled),
        //     Mathf.Clamp(transform.position.z, -_depthStrafeLimitCompiled, _depthStrafeLimitCompiled)
        // );
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            _spear.Throw();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            // BlowUpBubble();
            if (transform.localScale.magnitude < _minScale.magnitude)
            {
                transform.localScale = _minScale;
                _targetScale = _minScale;
                return;
            }
        
            _targetScale -= Vector3.one * (Time.deltaTime * _deflateSpeed);
        }
        else
        {
            _targetScale = _normalScale;
        }

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

    public void HitBySpear()
    {
        Popped = true;
        Debug.Log("Player hit spear");
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (PlayerNumber)
        {
            case Players.One:
                if (other.CompareTag("Spear2"))
                {
                    HitBySpear();
                }
                break;
            case Players.Two:
                if (other.CompareTag("Spear1"))
                {
                    HitBySpear();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        

    }
}
