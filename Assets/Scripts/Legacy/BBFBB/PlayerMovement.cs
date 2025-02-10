using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    private float SlowZone = 1; // slow zone starts at 1, then gets made into a percentage later

    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] static public Transform spawnPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip jumpSound, pickupSound, healthSound, hitSound, PorridgeSound;
    [SerializeField] private GameObject coinParticle, jumpParticle;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthImage;
    // [SerializeField] private Color greenHealth, redHealth;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text PorridgeText;

    [SerializeField] public int cageGoal = 1;
    private float horizontalValue;
    private Rigidbody2D rgbd;
    private Animator anim;
    private bool IfGrounded;
    static public bool canMove = true;
    private bool canDoubleJump = true;
    private bool onHighJumpFaceLeft = false;
    private bool onHighJumpFaceRight = false;
    private SpriteRenderer rend;
    private float rayDistance = 0.25f;
    private int startingHealth = 5;
    private int currentHealth = 0;
    public int CoinsCollected = 0;
    public int PorridgeCollected = 0;
    private float conveyorEffect = 0;
    private float transferMomentumUp;
    
    


    private AudioSource ljud;
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        coinText.text = "" + CoinsCollected;
        PorridgeText.text = "" + PorridgeCollected;
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        canMove = true;
        ljud  = GetComponent<AudioSource>();
        // sE2D = GetComponent<SurfaceEffector2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if (horizontalValue < 0)
        {
            FlipSprite(true);
        }

        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }

        CheckIfGrounded(); 

        if (Input.GetButtonDown("Jump") && CheckIfGrounded() == true)
        {
            Jump();
        }

        if (Input.GetButtonDown("Jump") && CheckIfGrounded() != true && canDoubleJump == true)
        {
            
            rgbd.velocity = new Vector2(rgbd.velocity.x ,0);
            Jump();
            canDoubleJump = false;
            // conveyorEffect = 0; // This removes conveyorEffect upon double jump. Whether we want this or not? It's arguable.
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.velocity.x));
        anim.SetFloat("VerticalSpeed", rgbd.velocity.y);
        anim.SetBool("IfGrounded", CheckIfGrounded());

        
    }
    private void FixedUpdate()
    {
        if (canMove == true)
        {
            rgbd.velocity = new Vector2((horizontalValue * moveSpeed * Time.deltaTime + conveyorEffect) * SlowZone, rgbd.velocity.y);

            if (onHighJumpFaceLeft == true && horizontalValue < 0)
            {
                rgbd.AddForce(rgbd.velocity * transferMomentumUp);
            }

            if (onHighJumpFaceRight == true && horizontalValue > 0)
            {
                rgbd.AddForce(rgbd.velocity * transferMomentumUp);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (other.gameObject.TryGetComponent<SurfaceEffector2D>(out SurfaceEffector2D speedVariable))
            {
                Debug.Log("conveyor ENTER!");
                speedVariable = other.gameObject.GetComponent<SurfaceEffector2D>();
                conveyorEffect = speedVariable.speed;
            }
        }

        if (!other.gameObject.TryGetComponent<SurfaceEffector2D>(out SurfaceEffector2D temporaryVariable))
        {
            conveyorEffect = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (other.gameObject.TryGetComponent<SurfaceEffector2D>(out SurfaceEffector2D temporaryVariable))
            {
                Debug.Log("conveyor LEAVE");
                if (horizontalValue < 0 && conveyorEffect > 0 || horizontalValue > 0 && conveyorEffect < 0)
                {
                    conveyorEffect = 0;
                    Debug.Log("conveyor OPPOSITE DIRECTION JUMP");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ADDED A RED KEY WHICH GRANTS 10 KEYS!
        if (other.CompareTag("coin10"))
        {
            ljud.PlayOneShot(pickupSound, 0.4f);
            ljud.pitch = Random.Range(0.8f, 1.2f);
            Destroy(other.gameObject);
            CoinsCollected = CoinsCollected + 10;
            coinText.text = "" + CoinsCollected;
            Instantiate(coinParticle, other.transform.position, Quaternion.identity);
        }

        if (other.CompareTag("coin"))
        {
            ljud.PlayOneShot(pickupSound, 0.4f);
            ljud.pitch = Random.Range(0.8f, 1.2f);
            Destroy(other.gameObject);
            CoinsCollected++;
            coinText.text = "" + CoinsCollected;
            Instantiate(coinParticle, other.transform.position, Quaternion.identity);
        }

        if (other.CompareTag("Porridge") && CoinsCollected >= cageGoal)
        {
            ljud.PlayOneShot(PorridgeSound, 0.4f);
            ljud.pitch = Random.Range(0.8f, 1.2f);
            Destroy(other.gameObject);
        PorridgeCollected++;
            PorridgeText.text = "" + PorridgeCollected;
            Instantiate(coinParticle, other.transform.position, Quaternion.identity);
        }
         if (other.CompareTag("Cage") && CoinsCollected >= cageGoal)
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Health"))
        {
           
            RestoreHealth(other.gameObject);
            ljud.PlayOneShot(healthSound, 0.4f);
            ljud.pitch = Random.Range(0.8f, 1.2f);

        }

        if (other.gameObject.TryGetComponent<SlowZoneScript>(out SlowZoneScript uselessVariable))
        {
            SlowZone = 0.3f; // example: 0.7 provides a 30% slow.
            Debug.Log("SLOW ZONE'D!");
        }
        
        if (other.gameObject.TryGetComponent<CallUponJumpBoostLeftFacing>(out CallUponJumpBoostLeftFacing uselessVariable2))
        {
            onHighJumpFaceLeft = true;
            Debug.Log("LEFT UP BOOST!");
            transferMomentumUp = uselessVariable2.jumpBoostLeft;
        }

        if (other.gameObject.TryGetComponent<CallUponJumpBoostRightFacing>(out CallUponJumpBoostRightFacing uselessVariable3))
        {
            onHighJumpFaceRight = true;
            Debug.Log("RIGHT UP BOOST!");
            transferMomentumUp = uselessVariable3.jumpBoostRight;
        }

        if (other.CompareTag("Checkpoint"))
        {
            spawnPosition = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<SlowZoneScript>(out SlowZoneScript uselessVariable))
        {
            SlowZone = 1;
            // Debug.Log("NO ZONE'D!");
        }

        if (other.gameObject.TryGetComponent<CallUponJumpBoostLeftFacing>(out CallUponJumpBoostLeftFacing uselessVariable2))
        {
            onHighJumpFaceLeft = false;
            // Debug.Log("LEFT BOOST UP GONE!");
        }

        if (other.gameObject.TryGetComponent<CallUponJumpBoostRightFacing>(out CallUponJumpBoostRightFacing uselessVariable3))
        {
            onHighJumpFaceRight = false;
            // Debug.Log("RIGHT BOOST UP GONE!");
        }
    }
    private void FlipSprite(bool direction)
    {
        rend.flipX = direction;
    }

    private void Jump()
    {
        rgbd.AddForce(new Vector2(0, jumpForce));
        ljud.PlayOneShot(jumpSound, 0.4f);
        ljud.pitch = Random.Range(0.8f, 1.2f);
        Instantiate(jumpParticle, transform.position, Quaternion.identity);
    }
  
    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);

       // Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, 0.25f);
       // Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            canDoubleJump = true;
            return true;
        }
        else
        {
            return false;
        }
      
    }

    public void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount;
        UpdateHealthBar();
        ljud.PlayOneShot(hitSound, 0.4f);

        if (currentHealth <= 0)
        {
            Respawn();
           
        }
    }

    public void TakeKnockback(float knockbackForce, float upwards)
    {
        canMove = false;
        rgbd.AddForce(new Vector2(knockbackForce, upwards));
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    private void UpdateHealthBar()
    {

        healthSlider.value = currentHealth;

       // if (currentHealth >= 3)
      //  {
     //       healthImage.color = greenHealth;
      //  }
       // else
       // {
      //      healthImage.color = redHealth;
      //  }
    }

    private void RestoreHealth(GameObject Health)
    {
       if(currentHealth >= startingHealth)
        {
            return;
        }
        else
        {
            int healthToRestore = Health.GetComponent<HealthPickup>().healthAmount;
            currentHealth += healthToRestore;
            UpdateHealthBar();
            Destroy(Health);

            if(currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
            }
          
        }
    }
        private void Respawn()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //currentHealth = startingHealth;
       // UpdateHealthBar();
        //transform.position = spawnPosition.position;
        //rgbd.velocity = Vector2.zero;
    }

}