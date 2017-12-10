using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SC_SoundManager : MonoBehaviour 
{
	private static SC_SoundManager _instance;

	public AudioSource AuS_SoundsEffects;
	public AudioSource AuS_Music;

	public List<Music> Li_Musics;
	public List<SoundEffect> Li_SoundEffects;
	public List<AudioClip> Li_JumpSounds;
	public List<AudioClip> Li_RetombeSounds;

	public List<AudioClip> Li_RetombeCoeurSounds;
	public List<AudioClip> Li_JumpCoeurSounds;

	private bool b_MusicMuted;
	private bool b_SoundMuted;

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public static void PlaySoundEffect(string _SoundName, float _f_Volume = 1f)
	{
		if(!_instance.b_SoundMuted)
		{
			for(int i = 0; i < _instance.Li_SoundEffects.Count; i++)
			{
				if(_instance.Li_SoundEffects[i].s_SoundEffectName == _SoundName)
					_instance.AuS_SoundsEffects.PlayOneShot(_instance.Li_SoundEffects[i].Audio_Clip, _f_Volume);
			}
		}
	}

	public static void PlayJumpSoundEffect(bool isLeft, float _f_Volume = 1f)
	{
		if(isLeft)
            _instance.AuS_SoundsEffects.PlayOneShot(_instance.Li_JumpSounds[Random.Range(14, 27)], _f_Volume);
		else
            _instance.AuS_SoundsEffects.PlayOneShot(_instance.Li_JumpSounds[Random.Range(0, 14)], _f_Volume);

    }

	public static void PlayJumpCoeurSoundEffect(bool isLeft, float _f_Volume = 1f)
	{
		if(isLeft)
            _instance.AuS_SoundsEffects.PlayOneShot(_instance.Li_JumpCoeurSounds[Random.Range(14, 27)], _f_Volume);
		else
            _instance.AuS_SoundsEffects.PlayOneShot(_instance.Li_JumpCoeurSounds[Random.Range(0, 14)], _f_Volume);
    }

	public static void PlayRetombeSoundEffect(bool isLeft, float _f_Volume = 1f)
	{
		if(isLeft)
            _instance.AuS_SoundsEffects.PlayOneShot(_instance.Li_RetombeSounds[Random.Range(5, 10)], _f_Volume);
		else
            _instance.AuS_SoundsEffects.PlayOneShot(_instance.Li_RetombeSounds[Random.Range(0, 5)], _f_Volume);
    }

	public static void PlayRetombeCoeurSoundEffect(bool isLeft, float _f_Volume = 1f)
	{
		if(isLeft)
            _instance.AuS_SoundsEffects.PlayOneShot(_instance.Li_RetombeCoeurSounds[Random.Range(14, 27)], _f_Volume);
		else
            _instance.AuS_SoundsEffects.PlayOneShot(_instance.Li_RetombeCoeurSounds[Random.Range(0, 14)], _f_Volume);
    }

	public void PlaySoundUIButton()
	{
		PlaySoundEffect("Button", 1f);
	}

	void MuteMusic()
	{
		b_MusicMuted = !b_MusicMuted;
		if(b_MusicMuted)
			AuS_Music.volume = 0f;
		else
			AuS_Music.volume = 0.2f;
	}

	void MuteSound()
	{
		b_SoundMuted = !b_SoundMuted;
	}
}

[System.Serializable]
public class Music
{
	public string s_MusicName;
	public AudioClip Audio_Clip;
}

[System.Serializable]
public class SoundEffect
{
	public string s_SoundEffectName;
	public AudioClip Audio_Clip;
}
