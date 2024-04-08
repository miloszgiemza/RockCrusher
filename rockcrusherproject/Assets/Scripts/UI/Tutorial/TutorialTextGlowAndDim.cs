using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextGlowAndDim : TextGlowAndDim
{
    protected override float EffectDuration => 4f;

    protected override float MaxGlow => 0.15f;

    protected override float MinGlow => 0.003031786f;

    protected override float PauseBetweenGlowingUpAndDimming => 0.4f;
}
