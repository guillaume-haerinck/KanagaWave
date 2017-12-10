// ---------------------------
// Copyright DUCKY GAMES, 2016.
// Samy BADACHE.
// ---------------------------

using UnityEngine;
using System.Collections;
using SpriterDotNetUnity;

public class SC_CharController : MonoBehaviour 
{
	public AnimationCurve AC_JumpStart;
	public AnimationCurve AC_JumpEnd;
	public float f_JumpHeight;
    public bool isJumping;
    public GameObject dustParticles;

    Transform T_MainChar;


    static public float f_JumpAscTime = 0.2f;
    static public float f_JumpPauseTime = 0.15f;
    static public float f_JumpDscTime = 0.1f;

	public SpriterDotNetBehaviour SpChar;


	void Awake()
	{
		T_MainChar = transform;
		isJumping = false;
	}

	void Start ()
	{

	}

	void Update ()
	{

	}

	public IEnumerator JumpCoroutine(int player)
	{
        bool playerIsInsideLeftTeam = (player < 8);
		SC_SoundManager.PlayJumpSoundEffect (playerIsInsideLeftTeam, 1f);

		// Start Anim Jump
		SpChar.Animator.Transition("sautDebut", 0.1f);

		float f_StartTime = Time.timeSinceLevelLoad;

		Vector3 V3_Position = T_MainChar.localPosition;
		float f_StartYpos = T_MainChar.localPosition.y;

		while(Time.timeSinceLevelLoad < f_StartTime + f_JumpAscTime )
		{
			V3_Position.y = Mathf.Lerp(f_StartYpos, f_StartYpos + f_JumpHeight, AC_JumpStart.Evaluate ((Time.timeSinceLevelLoad - f_StartTime) / f_JumpAscTime));
			T_MainChar.localPosition = V3_Position;
			yield return null;
		}

		yield return new WaitForSeconds (f_JumpPauseTime);
		SpChar.Animator.Transition ("sautIdle", 0.1f);

		f_StartTime = Time.timeSinceLevelLoad;

		while(Time.timeSinceLevelLoad < f_StartTime + f_JumpDscTime )
		{
			V3_Position.y = Mathf.Lerp(f_StartYpos, f_StartYpos + f_JumpHeight, AC_JumpEnd.Evaluate((Time.timeSinceLevelLoad - f_StartTime) / f_JumpDscTime));
			T_MainChar.localPosition = V3_Position;
			yield return null;
		}
		isJumping = false;
		SC_SoundManager.PlayRetombeSoundEffect(playerIsInsideLeftTeam, 1f);
		SC_PlayersManager.GenerateDust(player);

		SpChar.Animator.Transition ("sautImpact", 0.1f);


		//End Anim Jump
		StartCoroutine ("RestartIdle");
	}

	public IEnumerator RestartIdle()
	{
		yield return new WaitForSeconds (0.5f);
		if (!isJumping)
			SpChar.Animator.Transition ("idle", 0.1f);
	}
}