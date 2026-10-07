using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ClawlineScript : MonoBehaviour
{

    [SerializeField] private GameObject Hornet;    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float hornetSpeed = 10.0f;

    private HornetScript hornetScript;
    public Rigidbody2D clawlineRb;

    public Rigidbody2D hornetRb;



    float maxDistance = 4f;

    public bool ClawLineActive = false;

    bool hitWall = false;

    public SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckWallCollision(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckWallCollision(collision);
    }

    

    private void CheckWallCollision(Collider2D collision)
    {


        if (collision.TryGetComponent<IHittable>(out var wasHit) && ClawLineActive == true)
        {
            hitWall = true;
        }

        if (collision.TryGetComponent<IDamageable>(out var target) && ClawLineActive == true)
        {
            target.TakeDamage(5f);


        }
    }


    private IEnumerator ClawlineTravel()
    {

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Hide the sprite
        spriteRenderer.enabled = true;

        Vector3 startingClawLinePos = transform.position;
        ClawLineActive = true;
        maxDistance = 4f;

        
            while (Vector3.Distance(startingClawLinePos, transform.position) < maxDistance)
            {

                if (!hitWall)
                {
                    moveClawline();
                }
                else { break; }

            yield return new WaitForFixedUpdate();
            }

            Vector3 startingHornetPos = Hornet.transform.position;
        float currTime = 0f;
        float timeOut = 2f;

        if (hitWall)
        {
            while (Vector2.Distance(hornetRb.position, transform.position) > 0.6f && currTime < timeOut)
            {
                moveHornet();
                currTime += Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }
        } else
        {
            Vector2 overtakeTarget = (Vector2)transform.position + (Vector2.left * 1.5f);
            while (Vector2.Distance(hornetRb.position, overtakeTarget) > 0.2f && currTime < timeOut)
                        {
                            moveHornet();
                        currTime += Time.fixedDeltaTime;

                        yield return new WaitForFixedUpdate();
                        }
        }

        
      
        hitWall = false;
        ClawLineActive = false;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Hide the sprite
        spriteRenderer.enabled = false;


    }

    public void onClawline(InputAction.CallbackContext context)
    {
        if (context.performed && ClawLineActive == false)
        {
            StartCoroutine(ClawlineTravel());


        } 
    }

    void moveClawline()
    {
        
           transform.Translate(Vector3.left * Time.fixedDeltaTime * speed);
       
        
    }

    void moveHornet()
    {

        //Hornet.transform.Translate(Vector3.left * Time.fixedDeltaTime * hornetSpeed);
        Vector2 displacement = (Vector3.left * Time.fixedDeltaTime * hornetSpeed);
        hornetRb.MovePosition(hornetRb.position + displacement);
        
    }
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        clawlineRb = GetComponent<Rigidbody2D>();

        
}

    void Start()
    {
        hornetScript = Hornet.GetComponent<HornetScript>();
        hornetRb = Hornet.GetComponent<Rigidbody2D>();


        // Hide the sprite
        spriteRenderer.enabled = false;

    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ClawLineActive && Hornet != null)
        {
            transform.position = Hornet.transform.position;
        }


    }


    
}
