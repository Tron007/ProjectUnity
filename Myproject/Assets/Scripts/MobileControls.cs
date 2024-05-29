using UnityEngine;
using UnityEngine.UI;

public class MobileControls : MonoBehaviour
{
    public MovingObject movingObject;
    public PlayerHit playerHit;
    public PickUpObject pickUpObject;
    public Joystick joystick;
    public Button jumpButton;
    public Button crouchButton;
    public Button runButton;
    public Button attackButton;
    public Button grabButton;
    private bool isRunning = false;
    public float sensitivityMultiplier = 1.6f;


    private void Start()
    {
        
        // ��������� ������������ ������� ��� ������, ���� ��� ����������� �����������
        jumpButton.onClick.AddListener(movingObject.TryJump);
        crouchButton.onClick.AddListener(movingObject.ToggleCrouch);
        runButton.onClick.AddListener(ToggleRun);
        attackButton.onClick.AddListener(playerHit.Strike);
        grabButton.onClick.AddListener(ToggleGrab);
    }


    private void FixedUpdate()
    {
        // ���������� ��������� ����� ��������
        float horizontalInput = joystick.Horizontal * sensitivityMultiplier;
        float verticalInput = joystick.Vertical * sensitivityMultiplier;
        movingObject.Move(horizontalInput, verticalInput, isRunning);
    }

    public void ToggleRun()
    {
        isRunning = !isRunning; // ������������ ���������
    }


    void ToggleGrab() // ���������� ��� ������ �������
    {
        if (pickUpObject.isHoldingObject) // ���� ������ ������������
        {
            pickUpObject.ReleaseObject(); // ��������� ������
        }
        else
        {
            pickUpObject.PickObjectFromFloor(); // ����������� ������
        }
    }
}
