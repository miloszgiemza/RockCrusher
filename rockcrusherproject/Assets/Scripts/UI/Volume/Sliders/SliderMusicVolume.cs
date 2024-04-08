using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMusicVolume : BaseVolumeSlider
{
    public override AudioMixerGroup SliderAudioMixerGroup => AudioMixerGroup.Music;
}
