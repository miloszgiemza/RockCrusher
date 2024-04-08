using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class TextGlowAndDim : BaseTextScript
{
    protected abstract float EffectDuration { get; }
    protected abstract float MaxGlow { get; }
    protected abstract float MinGlow { get; }
    protected abstract float PauseBetweenGlowingUpAndDimming { get; }

    protected float timer;
    bool firstRun = true;
    bool paused = false;

    protected override void Awake()
    {
        base.Awake();
        SetGlowPower(MinGlow);

        timer = 0f;
    }

    protected void Update()
    {
            RunEffect(EffectDuration, MaxGlow, MinGlow);
    }

    protected void OnDisable()
    {
        SetGlowPower(MinGlow);
    }

    protected void SetGlowPower(float currentPower)
    {
        textComponent.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowPower, currentPower);
    }

    protected void RunEffect(float effectDuration, float maxGlow, float minGlow)
    {
        if (firstRun && !paused)
        {
            timer += Time.unscaledDeltaTime;

            float glowCurrentPower = MinGlow + (MaxGlow - MinGlow) * (timer / effectDuration);
            SetGlowPower(Mathf.Clamp(glowCurrentPower, MinGlow, MaxGlow));

            if (timer >= effectDuration)
            {
                timer = 0f;
                firstRun = false;
                paused = true;
            }
        }
        if (paused)
        {
            timer += Time.unscaledDeltaTime;
            if(timer >= PauseBetweenGlowingUpAndDimming)
            {
                timer = 0f;
                paused = false;
            }
        }
        if (!firstRun && !paused)
        {
            timer += Time.unscaledDeltaTime;

            float glowCurrentPower = MaxGlow - (MaxGlow - MinGlow) * (timer / effectDuration);
            SetGlowPower(Mathf.Clamp(glowCurrentPower, MinGlow, MaxGlow));

            if (timer >= effectDuration)
            {
                timer = 0f;
                firstRun = true;
                paused = true;
            }
        }
    }
}
