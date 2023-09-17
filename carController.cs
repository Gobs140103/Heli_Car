using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    private float timeSinceReleasedGas = 0f;
    private bool isEngineSoundPlaying = false;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    // Audio
    [SerializeField] private AudioSource engineSound;
    [SerializeField] private float minPitch = 0.5f;
    [SerializeField] private float maxPitch = 2.0f;
    [SerializeField] private float pitchMultiplier = 0.2f;
    [SerializeField] private float soundFadeSpeed = 1.0f; // Adjust the fade speed

    private float targetVolume = 0f; // Target volume for the engine sound

    private void Start()
    {
        engineSound.loop = true;
        engineSound.playOnAwake = false;
        engineSound.Stop();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        UpdateEngineSound();
    }

    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        // Apply a small, constant negative torque to simulate rolling resistance.
        const float rollingResistance = 2.0f;
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce - rollingResistance;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce - rollingResistance;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void UpdateEngineSound()
    {
        float speed = Mathf.Abs(rearLeftWheelCollider.rpm) * 0.5f;

        float pitch = Mathf.Lerp(minPitch, maxPitch, speed * pitchMultiplier);
        engineSound.pitch = pitch;

        // Determine the target volume based on whether the gas pedal is pressed or not
        if (verticalInput != 0f)
        {
            targetVolume = 1f; // Full volume when gas pedal is pressed
            timeSinceReleasedGas = Time.time; // Reset the timer
        }
        else
        {
            targetVolume = 0f; // No volume when gas pedal is released

            // Check if the timer exceeds 2 seconds before stopping the sound
            if (Time.time - timeSinceReleasedGas > 4f)
            {
                targetVolume = 0f; // Gradually reduce volume if 2 seconds have passed
            }
        }

        // Gradually adjust the audio source volume towards the target volume
        engineSound.volume = Mathf.MoveTowards(engineSound.volume, targetVolume, Time.deltaTime * soundFadeSpeed);

        if (engineSound.volume > 0.05f)
        {
            if (!engineSound.isPlaying)
                engineSound.Play();
        }
        else
        {
            engineSound.Stop();
        }
    }
}
