using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    private Vector3 movement;
    public Rigidbody rb;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
         
        Vector3 targetVelocity = movement * speed;
    
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movement = new Vector3(moveX, 0, moveZ).normalized;

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            10f * Time.deltaTime
            );
        }
    }
    

}