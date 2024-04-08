using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMasterVolume : BaseVolumeSlider
{
    public override AudioMixerGroup SliderAudioMixerGroup { get => AudioMixerGroup.Master; }
}
