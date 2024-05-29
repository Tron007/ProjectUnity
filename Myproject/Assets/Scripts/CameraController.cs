using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothness = 5f;
    [SerializeField] private float thirdPersonCameraHeight = 3f;
    [SerializeField] private float crouchCameraHeight = 0.5f;
    private bool isFirstPersonCameraActive = false;
    private bool isCameraSwitching = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isCameraSwitching)
        {
            SwitchCamera();
        }

        MoveCamera();
    }

    private void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (isFirstPersonCameraActive)
        {
            player.Rotate(Vector3.up * mouseX);

            float newRotationX = transform.eulerAngles.x - mouseY;
            transform.rotation = Quaternion.Euler(newRotationX, player.eulerAngles.y, 0f);

            Vector3 newPosition = player.position + player.up * GetCameraHeight();
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smoothness);
        }
        else
        {
            player.Rotate(Vector3.up * mouseX);

            float newRotationX = transform.eulerAngles.x - mouseY;
            transform.rotation = Quaternion.Euler(newRotationX, player.eulerAngles.y, 0f);

            Vector3 targetPosition = player.position - player.forward * 5f + Vector3.up * GetCameraHeight();
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothness);

            transform.LookAt(player.position + player.forward * 5f);
        }
    }

    private float GetCameraHeight()
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement != null && playerMovement.isCrouching)
        {
            return crouchCameraHeight;
        }
        else if (isFirstPersonCameraActive)
        {
            return 1.7f;
        }
        else
        {
            return thirdPersonCameraHeight;
        }
    }

    private void SwitchCamera()
    {
        isFirstPersonCameraActive = !isFirstPersonCameraActive;
        isCameraSwitching = true;
        Invoke("ResetCameraSwitchFlag", 1.0f);
    }

    private void ResetCameraSwitchFlag()
    {
        isCameraSwitching = false;
    }
}
