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
        
        // Настройка обработчиков событий для кнопок, если они добавляются динамически
        jumpButton.onClick.AddListener(movingObject.TryJump);
        crouchButton.onClick.AddListener(movingObject.ToggleCrouch);
        runButton.onClick.AddListener(ToggleRun);
        attackButton.onClick.AddListener(playerHit.Strike);
        grabButton.onClick.AddListener(ToggleGrab);
    }


    private void FixedUpdate()
    {
        // Управление движением через джойстик
        float horizontalInput = joystick.Horizontal * sensitivityMultiplier;
        float verticalInput = joystick.Vertical * sensitivityMultiplier;
        movingObject.Move(horizontalInput, verticalInput, isRunning);
    }

    public void ToggleRun()
    {
        isRunning = !isRunning; // Переключение состояния
    }


    void ToggleGrab() // Обработчик для кнопки захвата
    {
        if (pickUpObject.isHoldingObject) // Если объект удерживается
        {
            pickUpObject.ReleaseObject(); // Отпускаем объект
        }
        else
        {
            pickUpObject.PickObjectFromFloor(); // Захватываем объект
        }
    }
}
