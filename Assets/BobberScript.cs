using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class BobberScript : MonoBehaviour
{

    enum BobberState 
    {
    CASTING,
    CAST,
    UNCAST
    }

    // angle in degrees
    [SerializeField] private float castAngle = 45;
    [SerializeField] private float bobberCheckRadius = 0.5f;
    [SerializeField] private Rigidbody _rbBobber;
    [SerializeField] private Transform bobberResetPosition;
    [SerializeField] private LayerMask groundLayer;   
    [SerializeField] private LayerMask waterLayer;   


    private BobberState bobberIs = BobberState.UNCAST;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // Changed from OnDrawGizmosSelected so it always shows, not just when selected
        Gizmos.color = Color.orange;
        Gizmos.DrawSphere(_rbBobber.position, bobberCheckRadius);
    }
#endif

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {


        groundLayer = LayerMask.GetMask("Ground");
        waterLayer = LayerMask.GetMask("Water");
    }

    bool hitGround()
    {
        return Physics.CheckSphere(_rbBobber.position, bobberCheckRadius, groundLayer);
    }

    bool hitWater()
    {
        return Physics.CheckSphere(_rbBobber.position, bobberCheckRadius, waterLayer);
    }

    // This is the casting action 👍
    void OnAttack(InputValue value)
    {
        Debug.Log("Cast Initiated");

        if (bobberIs == BobberState.UNCAST) {
            bobberIs = BobberState.CASTING;
        } else if (bobberIs == BobberState.CAST || bobberIs == BobberState.CASTING) 
        {
            bobberIs = BobberState.UNCAST;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (bobberIs == BobberState.UNCAST)
        {
            _rbBobber.MovePosition(bobberResetPosition.position);
        } else if (bobberIs == BobberState.CASTING)
        {
            if (!hitWater())
            {
                _rbBobber.MovePosition(_rbBobber.position + transform.forward * 0.1f + Vector3.down * 0.1f);
            }
            if (hitGround())
            {
                bobberIs = BobberState.UNCAST;
            }
        } 
    }
}
