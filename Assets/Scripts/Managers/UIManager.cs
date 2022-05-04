using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
     [SerializeField] private EscapeUI m_escapePanel;

    private PlayerInputActions m_playerInput;

    private void OnEnable()
    {
        m_playerInput = new PlayerInputActions();
        m_playerInput.UI.Enable();
        m_playerInput.UI.OpenEscapeMenu.performed += OpenEscapePanel;
    }

    public void OnHostLeft() => m_playerInput.UI.Disable();


    public void OpenEscapePanel(InputAction.CallbackContext context)
    {
        m_escapePanel.OpenPanel();
        m_playerInput.UI.OpenEscapeMenu.performed -= OpenEscapePanel;
        m_playerInput.UI.OpenEscapeMenu.performed += CloseEscapePanel;
    }

    public void CloseEscapePanel()
    {
        m_escapePanel.ClosePanel();
        m_playerInput.UI.OpenEscapeMenu.performed += OpenEscapePanel;
        m_playerInput.UI.OpenEscapeMenu.performed -= CloseEscapePanel;
    }

    public void CloseEscapePanel(InputAction.CallbackContext context)
    {
        m_escapePanel.ClosePanel();
        m_playerInput.UI.OpenEscapeMenu.performed += OpenEscapePanel;
        m_playerInput.UI.OpenEscapeMenu.performed -= CloseEscapePanel;
    }
}
