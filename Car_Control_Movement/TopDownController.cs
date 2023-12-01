using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CarControllerUnit), typeof(CarSoundEffectsHandler))]
public class TopDownController : MonoBehaviour
{
    float driftFactor = 0.95f;
    float accelerationFactor = 15f;
    float turnFactor = 3.5f;
    float maxSpeed = 20f;
    MovementType movementType;
    float accelerationInput = 0;
    float steerInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;
    Rigidbody2D carRigidBody2D;
    CarControllerUnit carControllerUnit;
    public bool isInit = false;
    public void Awake()
    {
        carRigidBody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        SetInputVector(carControllerUnit.CarMovement());
        switch (movementType)
        {
            case MovementType.Linear:
                ApplyEngineForce();
                KillOrthogonalVelocity();
                ApplySteering();
                break;
            case MovementType.Adaptive:
                UpdateMoveAdaptiveRotation();
                KillOrthogonalVelocity();
                break;
        }
    }
    void ApplyEngineForce()
    {
        //Calculate how much forward car is going
        velocityVsUp = Vector2.Dot(transform.up, carRigidBody2D.velocity);
        //Limit to max speed
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;
        //Limit reverse to 50% max speed
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;
        //Limit going any direction while accelerating
        if (carRigidBody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;
        //Stop the car with drag if no input
        if (accelerationInput == 0)
        {
            carRigidBody2D.drag = Mathf.Lerp(carRigidBody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else carRigidBody2D.drag = 0;
        //Force Creation
        Vector2 enigneForceVector = transform.up * accelerationInput * accelerationFactor;
        //Apply Force
        carRigidBody2D.AddForce(enigneForceVector, ForceMode2D.Force);
    }
    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidBody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);
        //Rotation Creation
        rotationAngle -= steerInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        //Apply Rotation
        carRigidBody2D.MoveRotation(rotationAngle);
    }
    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidBody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidBody2D.velocity, transform.right);

        carRigidBody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
    float GetLateralVelocity()
    {
        return Vector3.Dot(transform.right, carRigidBody2D.velocity);
    }
    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        switch (movementType)
        {
            case MovementType.Linear:
                if (accelerationInput < 0 && velocityVsUp > 0)
                {
                    isBraking = true;
                    return true;
                }
                break;
            case MovementType.Adaptive:
                if (velocityVsUp > 0)
                {
                    isBraking = true;
                    return true;
                }
                break;
        }

        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;
        return false;
    }
    void SetInputVector(Vector2 inputVector)
    {
        steerInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
    public float GetVelocityMagnitude()
    {
        return carRigidBody2D.velocity.magnitude;
    }
    private void UpdateMoveAdaptiveRotation()
    {
        //Calculate how much forward car is going
        velocityVsUp = Vector2.Dot(transform.up, carRigidBody2D.velocity);
        //Limit going any direction while accelerating
        if (accelerationInput == 0)
        {
            carRigidBody2D.drag = Mathf.Lerp(carRigidBody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else carRigidBody2D.drag = 0;
        if (carRigidBody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            return;
        Vector2 inputDirection = new Vector2(steerInput, accelerationInput);
        float thrust = Vector2.Dot(inputDirection, transform.up);
        float rotation = Vector2.Dot(inputDirection, transform.right);

        carRigidBody2D.AddForce(thrust * inputDirection.magnitude *
                transform.up * accelerationFactor * Time.deltaTime);
        float rotationAmount = turnFactor * Time.deltaTime * rotation;
        carRigidBody2D.AddTorque(rotationAmount);
    }
    public float DriftFactor { get { return driftFactor; } set { driftFactor = value; } }
    public float AccelerationFactor { get { return accelerationFactor; } set { accelerationFactor = value; } }
    public float TurnFactor { get { return turnFactor; } set { turnFactor = value; } }
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public MovementType MovementType { get {  return movementType; } set { movementType = value; } }
    public CarControllerUnit CarControllerUnit { get {  return carControllerUnit; } set {  carControllerUnit = value; } }
}
