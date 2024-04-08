using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasController : MonoBehaviour
{
    private Camera mainCamera;
    private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera cameraDefault;
    [SerializeField] private CinemachineVirtualCamera cameraPowerfulShake;
    [SerializeField] private CinemachineVirtualCamera cameraWeakShake;

    private bool comboShakeEffectActive = false;
    private float comboShakeEffectDuration = 0.7f;
    private float comboShakeEffectTimer;

    private Vector3 fruitHitCameraEffectDefaultPosition;
    private Rigidbody2D cameraWeakShakeRigidbody;

    private bool fruitHitCameraEffectActive = false;
    private float fruitHitCameraEffectDuration = 0.1f;
    private float fruitHitCameraEffectTimer;

    private float rockHitCameraShakeStrengthModifier = 1.05f;
    private Vector2 rockHitCameraShakeMaxStrength = new Vector2(5f, 5f);
    private Vector2 rockHitCameraShakeMinStrength = new Vector2(-5f, -5f);


    private void Awake()
    {
        mainCamera = Camera.main;
        cinemachineBrain = mainCamera.GetComponent<CinemachineBrain>();

        fruitHitCameraEffectDefaultPosition = cameraWeakShake.transform.position;
        cameraWeakShakeRigidbody = cameraWeakShake.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Update()
    {
        ComboShakeEffect();
        RockHitCameraEffect();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void StartComboCameraShake(int pointsValue)
    {
        if (!comboShakeEffectActive)
        {
            cameraPowerfulShake.gameObject.SetActive(true);
            comboShakeEffectActive = true;
            comboShakeEffectTimer = comboShakeEffectDuration;
        }
    }

    private void StartBombCameraShake(Vector2 bombPosition, float bombPower)
    {
        if (!comboShakeEffectActive)
        {
            cameraPowerfulShake.gameObject.SetActive(true);
            comboShakeEffectActive = true;
            comboShakeEffectTimer = comboShakeEffectDuration;
        }
    }

        private void ComboShakeEffect()
    {
        if(comboShakeEffectActive)
        {
            comboShakeEffectTimer -= Time.deltaTime;
            if(comboShakeEffectTimer<=0)
            {
                comboShakeEffectActive = false;
                cameraPowerfulShake.gameObject.SetActive(false);
            }
        }
    }

    private void StartRockHitCameraShakeEffect(Vector2 hitVector)
    {
        if(!fruitHitCameraEffectActive)
        {
            cameraWeakShake.gameObject.SetActive(true);
            fruitHitCameraEffectActive = true;
            fruitHitCameraEffectTimer = fruitHitCameraEffectDuration;

            cameraWeakShakeRigidbody.velocity = new Vector2(Mathf.Clamp(hitVector.x * rockHitCameraShakeStrengthModifier, rockHitCameraShakeMinStrength.x, rockHitCameraShakeMaxStrength.x), Mathf.Clamp(hitVector.y * rockHitCameraShakeStrengthModifier, rockHitCameraShakeMinStrength.y, rockHitCameraShakeMaxStrength.y));
        }
    }



    private void RockHitCameraEffect()
    {
        if(fruitHitCameraEffectActive)
        {
            fruitHitCameraEffectTimer -= Time.deltaTime;

            if(fruitHitCameraEffectTimer <= 0f)
            {
                fruitHitCameraEffectActive = false;
                cameraWeakShake.transform.position = fruitHitCameraEffectDefaultPosition;
                cameraWeakShake.gameObject.SetActive(false);
            }
        }
    }

    private void SubscribeEvents()
    {
        // GameEvents.OnComboComplited += StartComboCameraShake;
        GameEvents.OnBombTouched += StartBombCameraShake;
        GameEvents.OnRockDestroyedCameraShake += StartRockHitCameraShakeEffect;
    }

    private void UnsubscribeEvents()
    {
            //GameEvents.OnComboComplited -= StartComboCameraShake;
            GameEvents.OnBombTouched -= StartBombCameraShake;
            GameEvents.OnRockDestroyedCameraShake -= StartRockHitCameraShakeEffect;
    }
}
