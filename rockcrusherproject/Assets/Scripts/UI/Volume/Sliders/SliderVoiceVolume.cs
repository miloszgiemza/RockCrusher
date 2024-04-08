using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderVoiceVolume : BaseVolumeSlider
{
    public override AudioMixerGroup SliderAudioMixerGroup => AudioMixerGroup.Voice;
}
