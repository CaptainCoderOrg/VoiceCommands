using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumpController : MonoBehaviour
{
    public float JumpPower = 100f;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectedJumpInput())
        {
            DoJump();
        }
    }

    private bool DetectedJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;   
        }
        return false;
    }

    public void DoJump()
    {
        Debug.Log("Doing Jump!");
        _rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }
}
