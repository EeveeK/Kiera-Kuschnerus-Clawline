using UnityEngine;
using UnityEngine.InputSystem;

public class HornetScript : MonoBehaviour
{
    public Vector2 moveInput;
    [SerializeField] private float speed = 5f;
    [SerializeField] private ClawlineScript clawlineScript;

    private SpriteRenderer spriteRenderer;

    public Vector2 direction;
    public Vector3 prevPos;

    public bool isFlipped;

    Rigidbody2D hornetRb;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Input received: " + moveInput);
    }

    public void OnClawline(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Input received: " + moveInput);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hornetRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        if (clawlineScript == null)
        {
            Debug.LogError("ClawlineScript is MISSING/UNASSIGNED in HornetScript Inspector!", this);
            return;
        }

        if (clawlineScript.ClawLineActive)
        {
            Debug.LogWarning("Movement blocked because ClawLineActive is TRUE!");
            return;
        }

       
            Vector3 moveDirection = new Vector3(moveInput.x, moveInput.y, 0f);
             Vector2 distance = moveDirection * speed * Time.fixedDeltaTime;


            Debug.Log($"Moving Hornet from {hornetRb.position} to {hornetRb.position + distance}");

            hornetRb.MovePosition(hornetRb.position + distance);
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
