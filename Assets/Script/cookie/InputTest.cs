using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{

    [SerializeField] private InputActionProperty testActionValue;
    [SerializeField] private InputActionProperty testActionBButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float value = testActionValue.action.ReadValue<float>();
        Debug.Log("Value : " + value);

        bool button = testActionValue.action.IsPressed();
        Debug.Log("Button : " + button);
    }
}
