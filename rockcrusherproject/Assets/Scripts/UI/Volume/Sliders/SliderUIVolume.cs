using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderUIVolume : BaseVolumeSlider
{
    public override AudioMixerGroup SliderAudioMixerGroup => AudioMixerGroup.UI;
}
