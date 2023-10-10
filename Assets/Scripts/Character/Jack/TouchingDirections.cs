
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{

    public ContactFilter2D castFilter;
    public ContactFilter2D rampFilter;
    public float groundDistance = 0.025f;
    public float wallDistance = 0.1f;
    public float ceilingDistance = 0.025f;
    public float rampDistance = 0.025f;

    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    RaycastHit2D[] rampHits = new RaycastHit2D[5];


    [SerializeField]
    private bool _isGrounded;
    [SerializeField]

    private bool _isOnRamp;
    public bool IsGrounded { get{
        return _isGrounded;
    } private set {
        _isGrounded = value;
    } }

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall { get{
        return _isOnWall;
    } private set {
        _isOnWall = value;
    } }

    [SerializeField]
    private bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling { get{
        return _isOnCeiling;
    } private set {
        _isOnCeiling = value;
    } }

    public bool IsOnRamp { get{
        return _isOnRamp;
    } private set {
        _isOnRamp= value;
    } }

    private JackController jackController;

    // Start is called before the first frame update
    void Start()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        jackController = GetComponent<JackController>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
        IsOnRamp = touchingCol.Cast(Vector2.down, rampFilter, rampHits, rampDistance) > 0;
    }
}
