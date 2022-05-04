using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private PlayerInputActions m_playerInputActions;
    private PlayerWeaponSelection m_playerWeaponSelection;
    private PlayerHealthController m_playerHealthController;

    [SerializeField] private float m_moveSpeed;
    private Vector2 m_inputVector;

    [SerializeField] private Transform m_bodyTransform;
    private Vector2 m_mousePosition;
    private Vector2 m_lookDirection;
    private float m_angle;
    private Camera m_mainCam;



    private bool m_canShoot = true;
    private bool m_isPhotonViewMine;

    private PhotonView m_photonView;

    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_playerWeaponSelection = GetComponent<PlayerWeaponSelection>();

        m_isPhotonViewMine = m_photonView.IsMine;

        if (m_isPhotonViewMine)
        {
            ActivatePlayerInput();
        }
        else
        {
            Destroy(m_rigidbody);
        }

    }

    private void ActivatePlayerInput()
    {
        m_playerInputActions = new PlayerInputActions();
        m_playerInputActions.Player.Enable();
        m_playerInputActions.Player.Shoot.started += ShootInput;
        m_playerInputActions.Player.Shoot.canceled += ShootInput;
    }

    private void DeactivatePlayerInput()
    {
        m_playerInputActions.Player.Disable();
        m_playerInputActions.Player.Shoot.started -= ShootInput;
        m_playerInputActions.Player.Shoot.canceled -= ShootInput;
    }

    private void Start()
    {
        m_mainCam = GameManager.mainCamera;
    }

    private void Update()
    {
        if (!m_isPhotonViewMine)
            return;

            m_mousePosition = m_mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if (!m_isPhotonViewMine)
            return;

        //movimentação
        m_inputVector = m_playerInputActions.Player.Movement.ReadValue<Vector2>();
        m_rigidbody.AddForce(new Vector2(m_inputVector.x, m_inputVector.y) * m_moveSpeed, ForceMode2D.Force);

        //rotação de acordo com o mouse
        m_lookDirection = m_mousePosition - m_rigidbody.position;
        m_angle = Mathf.Atan2(m_lookDirection.y, m_lookDirection.x) * Mathf.Rad2Deg;
        m_bodyTransform.rotation = Quaternion.AngleAxis(m_angle, Vector3.forward);
        //m_rigidbody.rotation = m_angle;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (! m_isPhotonViewMine)
            return;

        var other = collision.collider;

        if (other.CompareTag("Ammo"))
        {
            var ammoScript = other.GetComponent<AmmoCollectable>();
            var weapon = m_playerWeaponSelection.weaponsList[(int)ammoScript.ammoType];
            weapon.AmmoPickup(ammoScript.ammoAmount);
            Destroy(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (m_isPhotonViewMine)
            DeactivatePlayerInput();
    }

    private void ShootInput(InputAction.CallbackContext context)
    {
        if (!m_canShoot || !m_isPhotonViewMine)
            return;

        if (context.started)
            m_playerWeaponSelection.selectedWeapon.ShootCalled(true);
        else if (context.canceled)
            m_playerWeaponSelection.selectedWeapon.ShootCalled(false);
    }

}
