using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterController : MonoBehaviour
{
    public CharacterController characterController;
    public KeyCode forwardKeyCode;
    public KeyCode backKeyCode;
    public KeyCode jumpKeyCode;
    public KeyCode attack;
    public KeyCode rotateLeftKeyCode;
    public KeyCode rotateRightKeyCode;
    public KeyCode run;
    public KeyCode use;
    public KeyCode mainMenu;
    public Vector3 movementVector;
    public float movementSpeed = 8;
    public float rotationSpeed = 3;
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    public bool isSprinting = false;
    public float slowFallingSpeed = 0.2f;
    public float jumpSpeed = 0.4f;
    private Vector3 startPosition;
    private Transform thisTransform;   
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movementVector = Vector3.zero;
        startPosition = transform.position;
        m_animator = GetComponent<Animator>();

        //Controller movement position
        thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        ControllerMove();
    }


    private void OnControllerColliderHit (ControllerColliderHit hit ) {
        switch (hit.gameObject.tag) {
            case "Die":
                transform.position = startPosition;
                break;
            case "Spawnpoint":
                startPosition = transform.position;
                break;
        }
    }

    public void movePlayer()
    {
        int x = 0;
        float y = 0;
        int z = 0;

        // Mainmenu
        if (Input.GetKey(mainMenu))
        {
            SceneManager.LoadScene(0);
        }

        // Movement
        if (Input.GetKey(forwardKeyCode))
        {
            z++;
            m_animator.SetTrigger("Walk");
        }

        if (Input.GetKey(backKeyCode))
        {
            z--;
            m_animator.SetTrigger("Walk");
        }

        // Kamera rotate
        if (Input.GetKey(rotateLeftKeyCode) || Input.GetKey("joystick button 4")) 
        {
            transform.Rotate(Vector3.up * -rotationSpeed);
        }

        if (Input.GetKey(rotateRightKeyCode) || Input.GetKey("joystick button 5")) 
        {
            transform.Rotate(Vector3.up * rotationSpeed);
        }

        // Sprint 
        if (Input.GetKey(run) && Input.GetKey(forwardKeyCode) || Input.GetKey("joystick button 1"))
        {
            
            isSprinting = true;
            movementSpeed = 15;
            m_animator.SetTrigger("Run");
        }
        else
        {
            isSprinting = false;
            movementSpeed = 8;
        }

        y = movementVector.y;
        movementVector = ((transform.forward * z) + (transform.right * x)).normalized * movementSpeed * Time.deltaTime;
        movementVector.y = y;

        // Jump
        if (characterController.isGrounded && Input.GetKeyDown(jumpKeyCode) || Input.GetKeyDown("joystick button 0")) 
        {
            SoundEffectScript.PlaySound("jump");
            movementVector.y = jumpSpeed;
        }

        if (Input.GetKey(jumpKeyCode) || Input.GetKeyDown("joystick button 0"))
        {
            movementVector.y += Physics.gravity.y * slowFallingSpeed * Time.deltaTime;
        }

        else
        {
            movementVector.y += Physics.gravity.y * Time.deltaTime;
        }

        //Attack
        if (Input.GetKeyDown(attack) || Input.GetKeyDown("joystick button 2"))
        {
            SoundEffectScript.PlaySound("sword");
            AttackSword();
        }

        characterController.Move(movementVector);

    }

    void ControllerMove()
    {
        // Controller Movement 
        movementVector.z = Input.GetAxis("MoveVertical") * movementSpeed * Time.deltaTime;
        movementVector.x = Input.GetAxis("MoveHorizontal") * movementSpeed * Time.deltaTime;    

        m_animator.SetTrigger("Walk");
        
        thisTransform.position = startPosition + movementVector;

        characterController.Move(movementVector);
    }
    void AttackSword()
    {
        //Animation
        m_animator.SetTrigger("Attack");

        // Range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange , enemyLayers);

        // Damage 
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemiesScript>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
