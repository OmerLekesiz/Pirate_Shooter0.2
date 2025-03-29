using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ProjectileThrow : MonoBehaviour
{
    [Header("Ball Prefabs")]
    [SerializeField] private Rigidbody[] ballPrefabs; // 0: Light, 1: Medium, 2: Heavy

    [Header("Throw Settings")]
    [SerializeField] private float force = 20f;
    [SerializeField] private Transform startPosition;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI selectedBallText;

    [Header("Input")]
    public InputAction fire;

    private int currentBallIndex = 0;
    private TrajectoryPredictor trajectoryPredictor;

    private float[] ballCooldowns = { 0.5f, 1.0f, 1.5f }; // Light, Medium, Heavy cooldowns
    private float lastThrowTime = 0f;

    private void OnEnable()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        fire.Enable();
        fire.performed += ThrowObject;

        UpdateBallText("Light");
    }

    private void OnDisable()
    {
        fire.performed -= ThrowObject;
        fire.Disable();
    }

    private void Update()
    {
        HandleBallSwitch();
        PredictTrajectory();
    }

    private void HandleBallSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentBallIndex = 0;
            UpdateBallText("Light");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentBallIndex = 1;
            UpdateBallText("Medium");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentBallIndex = 2;
            UpdateBallText("Heavy");
        }
    }

    private void UpdateBallText(string type)
    {
        if (selectedBallText == null) return;

        selectedBallText.text = "Selected Ball: " + type;

        switch (type)
        {
            case "Light":
                selectedBallText.color = new Color32(255, 230, 0, 255); // Yellow
                break;
            case "Medium":
                selectedBallText.color = new Color32(0, 180, 255, 255); // Blue
                break;
            case "Heavy":
                selectedBallText.color = new Color32(255, 50, 50, 255); // Red
                break;
        }
    }

    private void PredictTrajectory()
    {
        if (ballPrefabs.Length == 0 || startPosition == null || trajectoryPredictor == null) return;

        ProjectileProperties props = new ProjectileProperties
        {
            direction = startPosition.forward,
            initialPosition = startPosition.position,
            initialSpeed = GetAdjustedForce(),
            mass = ballPrefabs[currentBallIndex].mass,
            drag = ballPrefabs[currentBallIndex].drag
        };

        trajectoryPredictor.PredictTrajectory(props);
    }

    private void ThrowObject(InputAction.CallbackContext ctx)
    {
        if (Time.time - lastThrowTime < ballCooldowns[currentBallIndex])
            return;

        if (ballPrefabs.Length == 0 || startPosition == null) return;

        Rigidbody selectedPrefab = ballPrefabs[currentBallIndex];
        Rigidbody newBall = Instantiate(selectedPrefab, startPosition.position, Quaternion.identity);
        newBall.AddForce(startPosition.forward * GetAdjustedForce(), ForceMode.Impulse);

        lastThrowTime = Time.time;
    }

    // Adjusts force based on ball type
    private float GetAdjustedForce()
    {
        if (currentBallIndex == 2) // Heavy
            return force * 1.5f;

        return force;
    }
}
