using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    public void TickInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector2 input = new Vector2(horizontal, vertical);
        MoveInput = input.sqrMagnitude > 1f ? input.normalized : input;
        LookInput = new Vector2(mouseX, mouseY);
    }
}
