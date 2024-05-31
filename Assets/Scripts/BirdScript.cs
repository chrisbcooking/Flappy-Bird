using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BirdScript : MonoBehaviour
{
    public GameObject birdWings;
    public GameObject death;
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicManager logic;
    public bool birdIsAlive = true;
    private float despawnLevel = -1000;
    private bool flagDeathFx;
    public Canvas GamePrecursorCanvas;
    public PlayerInput playerInput;
    public InputActionAsset actionAsset;
    public GameObject Arrow;
    private int maxArrows = 3;
    public int currentArrows = 0;

    private void Awake()
    {
        actionAsset.FindActionMap("Player").FindAction("Flap").performed += OnJump;
        actionAsset.FindActionMap("Player").FindAction("ShootArrow").performed += SpawnArrowProjectile;
        actionAsset.FindActionMap("UI").FindAction("StartGame").performed += GamePrecursor;
    }

    public void SpawnArrowProjectile(InputAction.CallbackContext callback)
    {
        if (currentArrows < maxArrows)
        {
            int arrowSpeed = 10;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - transform.position;
            float angleRad = Mathf.Atan2(direction.y, direction.x);
            float angleDeg = angleRad * Mathf.Rad2Deg;

            // Check if Arrow prefab is assigned
            if (Arrow != null)
            {
                GameObject newArrow = Instantiate(Arrow, transform.position, Quaternion.Euler(0, 0, angleDeg), transform.parent);
                Rigidbody2D arrowRigidbody = newArrow.GetComponent<Rigidbody2D>();

                // Check if Rigidbody2D component is found
                if (arrowRigidbody != null)
                {
                    arrowRigidbody.velocity = newArrow.transform.right * arrowSpeed;
                }
                else
                {
                    Debug.LogError("Rigidbody2D component not found on Arrow prefab.");
                }

                currentArrows += 1;
            }
            else
            {
                Debug.LogError("Arrow prefab is not assigned.");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.mousePosition); Type is Vector3
        // x is height 1920 Screen.width
        // y is width 1080 Screen.height
        // z is not tracked

        // Normalized mouse positions x, y
        //Debug.Log("x" + Input.mousePosition.x / Screen.width + " : " + "y" + Input.mousePosition.y / Screen.height);

        /*
        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }*/

        if (gameObject.transform.position.y <= despawnLevel)
        {
            logic.despawnBird();
        }
        if (birdIsAlive == false)
        {
            if (flagDeathFx == false)
            {
                flagDeathFx = true;
                birdDeath();
                actionAsset.FindActionMap("Player").FindAction("Flap").performed -= OnJump;
                actionAsset.FindActionMap("UI").FindAction("StartGame").performed -= GamePrecursor;
                actionAsset.FindActionMap("Player").FindAction("ShootArrow").performed -= SpawnArrowProjectile;
            }
            myRigidbody.velocity = myRigidbody.velocity + (Vector2.down * 100 * Time.deltaTime);
        }
    }
    private void GamePrecursor(InputAction.CallbackContext context)
    {
        Time.timeScale = 1f;
        gameObject.GetComponent<PlayerInput>().defaultActionMap = "Player";
        GamePrecursorCanvas.enabled = false;
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        if (birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6)
        {
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            endGame();
        }
    }

    private void birdDeath()
    {
        death = Instantiate(death, transform.position, transform.rotation);
        death.transform.parent = transform;

        for (int i = 0; i < death.transform.childCount; i++)
        {
            Rigidbody2D temp = death.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>();
            
            int negOrPos = Random.Range(0, 2) == 0 ? -1 : 1;
            float randDegrees = Random.Range(50, 101) / 100f * 360;
            temp.velocity = new Vector2((Random.Range(50, 101) / 100f) * 10 * negOrPos, transform.position.y >= 10 ? 0 : Random.Range(50, 101) / 100f * 30);
            temp.angularVelocity = randDegrees;
        }

        birdWings.SetActive(false);
    }

    private void endGame()
    {
        logic.gameOver();
        birdIsAlive = false;
        
    }
    private void OnEnable()
    {
        actionAsset.Enable();
    }
    private void OnDisable()
    {
        actionAsset.Disable();
    }
}
