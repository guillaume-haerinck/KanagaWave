// ---------------------------
// Copyright DUCKY GAMES, 2016.
// Samy BADACHE.
// ---------------------------

using UnityEngine;
using System.Collections;
//using UnityEngine.UI;
//using XInputDotNetPure;

public class SC_PulsationSystem : MonoBehaviour
{
    #region Public Fields
	public AudioSource AS_Music;
    public int i_Bpm;
    public SpriteRenderer SprR_Pulse;
    public bool Rumble = false;
	public Sprite[] Spr_Moon;
    #endregion

    #region Private Fields
    private static SC_PulsationSystem _instance;
    private float f_StartTime;
    private int i_Increment;
	private bool b_Enabled = false;
    #endregion

    #region VisualPulsation
    float f_lastPulse;
    float f_PulsePersistence = 0.05f;
    bool b_ShowPersitence;
    #endregion

    #region Unity Methods
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if (b_Enabled)
        {
            if (Time.timeSinceLevelLoad >= f_StartTime + ((60f / (float)i_Bpm) * i_Increment))
            {
                SC_SoundManager.PlaySoundEffect("PERCU", 1f);
				SprR_Pulse.sprite = Spr_Moon[1];
                i_Increment += 1;
                f_lastPulse = Time.timeSinceLevelLoad;
                b_ShowPersitence = true;

//                if (Rumble)
//                {
//                    GamePad.SetVibration(PlayerIndex.One, 06f, 0.6f);
//                    GamePad.SetVibration(PlayerIndex.Two, 06f, 0.6f);
//                }
            }

            if (b_ShowPersitence && Time.timeSinceLevelLoad >= f_lastPulse + f_PulsePersistence)
            {
				SprR_Pulse.sprite = Spr_Moon[0];
                b_ShowPersitence = false;

//                if (Rumble)
//                {
//                    GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
//                    GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
//                }
            }

        }
    }
    #endregion

	public static void EnablePulse()
	{
		_instance.f_StartTime = Time.timeSinceLevelLoad;
		_instance.b_Enabled = true;
		_instance.AS_Music.Play ();
	}
}