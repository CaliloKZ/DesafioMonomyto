using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private PlayerInputActions m_playerInputActions;
    private PlayerWeaponSelection m_playerWeaponSelection;
    private PlayerHealthController m_playerHealthController;

    [SerializeField] private float m_moveSpeed;
    private Vector2 m_inputVector;

    private Vector2 m_mousePosition;
    private Vector2 m_lookDirection;
    private float m_angle;
    private Camera m_mainCam;


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_playerWeaponSelection = GetComponent<PlayerWeaponSelection>();
        ActivatePlayerInput();
    }

    private void ActivatePlayerInput()
    {
        m_playerInputActions = new PlayerInputActions();
        m_playerInputActions.Player.Enable();
    }

    private void Start()
    {
        m_mainCam = GameManager.mainCamera;
    }

    private void Update()
    {
        m_mousePosition = m_mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //movimentação
        m_inputVector = m_playerInputActions.Player.Movement.ReadValue<Vector2>();
        m_rigidbody.AddForce(new Vector2(m_inputVector.x, m_inputVector.y) * m_moveSpeed, ForceMode2D.Force);

        //rotação de acordo com o mouse
        m_lookDirection = m_mousePosition - m_rigidbody.position;
        m_angle = Mathf.Atan2(m_lookDirection.y, m_lookDirection.x) * Mathf.Rad2Deg;
        m_rigidbody.rotation = m_angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.collider;

        if (other.CompareTag("Ammo"))
        {
            var ammoScript = other.GetComponent<AmmoCollectable>();
            var weapon = m_playerWeaponSelection.weaponsList[(int)ammoScript.ammoType];
            weapon.AmmoPickup(ammoScript.ammoAmount);
            Destroy(other.gameObject);
        }
    }

}
