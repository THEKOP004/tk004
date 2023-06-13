using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [Header("Drone Performance")]
    [SerializeField] float pitchForce;
    [SerializeField] float turnForce;
    [SerializeField] float thrustForce;
    [SerializeField] float thrustAxisSpeed;
    float thrustAxis;
    float curThurst;
    [SerializeField] float selfRight;

    //GameComponent
    Rigidbody rb;
    //Control Axis
    float horizontal;
    float vertical;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input();

    }
    private void FixedUpdate()
    {
        apply_force();
    }
    void input()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.K))
        {
            thrustAxis += Time.deltaTime * thrustAxisSpeed;
        }
        if(Input.GetKey(KeyCode.L))
        {
            thrustAxis -= Time.deltaTime * thrustAxisSpeed;
        }
        thrustAxis = Mathf.Clamp01(thrustAxis);
    }
    void apply_force()
    {
        //Pitch
        rb.AddTorque(transform.right * vertical * pitchForce);
        rb.AddTorque(Vector3.up * horizontal * turnForce);

        rb.AddForce(thrustForce * transform.forward * thrustAxis);

        Vector3 trueRight = new Vector3(transform.right.x, 0, transform.right.z);
        rb.AddTorque(Vector3.Cross(transform.right, trueRight) * selfRight);
    }
}
