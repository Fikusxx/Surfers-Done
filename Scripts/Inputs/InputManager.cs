using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    // Singleton
    public static InputManager Instance { get; private set; }

    // Action schemes
    private RunnerInputAction actionScheme;

    // Configuration
    [SerializeField] private float sqrSwipeDeadzone = 50f;


    // Public props
    public Vector2 TouchPosition { get; private set; }
    public bool Tap { get; private set; }
    public bool SwipeRight { get; private set; }
    public bool SwipeLeft { get; private set; }
    public bool SwipeUp { get; private set; }
    public bool SwipeDown { get; private set; }


    // Privates
    private Vector2 startDrag;
    

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SetUpControls();
    }

    private void LateUpdate()
    {
        ResetInputs();
    }

    private void ResetInputs()
    {
        Tap = false;
        SwipeRight = false;
        SwipeLeft = false;
        SwipeDown = false;
        SwipeUp = false;
    }

    private void SetUpControls()
    {
        actionScheme = new RunnerInputAction();

        // Register different action
        actionScheme.Gameplay.Click.performed += ctx => OnTap(ctx);
        actionScheme.Gameplay.TouchPosition.performed += ctx => OnPosition(ctx);
        actionScheme.Gameplay.StartDrag.performed += ctx => OnStartDrag(ctx);
        actionScheme.Gameplay.EndDrag.performed += ctx => OnEndDrag(ctx);
    }

    private void OnEndDrag(InputAction.CallbackContext ctx)
    {
        // distance between where our cursor now (TouchPosition) and when we started drag (startDrag)
        Vector2 delta = TouchPosition - startDrag;

        float sqrDistance = delta.sqrMagnitude;

        // Confirmed swipe
        if (sqrDistance > sqrSwipeDeadzone)
        {
            float x = Mathf.Abs(delta.x);
            float y = Mathf.Abs(delta.y);

            // we either going Left or Right
            if (x > y)
            {
                if (delta.x > 0)
                {
                    SwipeRight = true;
                }
                else
                {
                    SwipeLeft = true;
                }
            }
            else // either Up or Down
            {
                if (delta.y > 0)
                {
                    SwipeUp = true;
                }
                else
                {
                    SwipeDown = true;
                }
            }
        }

        // Once we're donw with the swipe - reset startDrag
        startDrag = Vector2.zero;
    }

    private void OnStartDrag(InputAction.CallbackContext ctx)
    {
        startDrag = TouchPosition;
    }

    private void OnPosition(InputAction.CallbackContext ctx)
    {
        TouchPosition = ctx.ReadValue<Vector2>();
    }

    private void OnTap(InputAction.CallbackContext ctx)
    {
        Tap = true;
    }

    public void OnEnable()
    {
        actionScheme.Enable();
    }

    public void OnDisable()
    {
        actionScheme.Disable();
    }
}
