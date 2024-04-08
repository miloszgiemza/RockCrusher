using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXAudioCLipsNames
{
    RockSuccesfullyDestroyed,
    RockHitUnsuccesfull1,
    RockHitUnsuccesfull2,
    RockHitUnsuccesfull4,
    Bomb
}

public class SFXController : BaseSoundsController<SFXAudioCLipsNames>
{
    public override AudioMixerGroup ControllerAudioMixerGroup => AudioMixerGroup.SoundEffects;

    protected override void SubscribeControllerEvent()
    {
        GameEvents.OnPlaySFXAudioClip += PlayAudioClip;
    }

    protected override void UnsubscribeControllerEvent()
    {
        GameEvents.OnPlaySFXAudioClip -= PlayAudioClip;
    }
}
