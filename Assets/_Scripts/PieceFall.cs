using UnityEngine;

public class PieceFall : MonoBehaviour
{
    public float targetY, stopThreshold = 0.05f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.y - targetY) <= stopThreshold)
        {
            rb.linearVelocity = Vector3.zero; //Stops the piece from falling further
            rb.isKinematic = true; //Makes the piece kinematic, so it won't be affected by physics anymore
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            Destroy(this); //Destroys this script, as it's no longer needed
        }
    }
}
