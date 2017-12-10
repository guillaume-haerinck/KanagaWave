// ---------------------------
// Copyright DUCKY GAMES, 2016.
// Samy BADACHE.
// ---------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SC_PlayersManager : MonoBehaviour 
{
	#region Public Fields
	public static bool b_GameStarted = false;
	public GameObject GO_STARTSCREEN;

	public GameObject GO_Prefab_Char;
    public GameObject GO_Prefab_Char2;
    public Transform T_LeftTeamParent;
	public Transform T_RightTeamParent;
	public GameObject dustParticles;
	public Vector3[] Li_V3_SpawnLeftTeam;
	public Vector3[] Li_V3_SpawnRightTeam;
	#endregion

	#region Private Fields
	private static SC_PlayersManager _instance;

	private List<SC_CharController> Li_go_playersLeftTeam;
	private List<SC_CharController> Li_go_playersRightTeam;

	private List<ParticleSystem> leftTeamParticleSystems;
	private List<ParticleSystem> rightTeamParticleSystems;
	#endregion

	#region Unity Methods
	void Awake()
	{
		_instance = this;
		Li_go_playersLeftTeam = new List<SC_CharController> ();
		Li_go_playersRightTeam = new List<SC_CharController> ();

		leftTeamParticleSystems = new List<ParticleSystem>();
		rightTeamParticleSystems = new List<ParticleSystem>();
	}

	void Start ()
	{
		InstantiatePlayers();
	}

	void Update ()
	{
		if(b_GameStarted)
		{
			for(int i=0; i< 16; i++)
			{
				if (Input.GetButtonDown ("p" + i.ToString ()))
				{

					SC_CharController charController;
					if (i < 8)
					{
						charController = Li_go_playersLeftTeam[i];
					}
					else
					{
						charController = Li_go_playersRightTeam[i - 8];
					}

					if(!charController.isJumping)
					{
						charController.isJumping = true;
						SC_LogicManager.registerJump(i);

						charController.StartCoroutine("JumpCoroutine", i);
					}
				}
			}
		}
		else
		{
			for (int i = 0; i < 16; i++)
			{
				if (Input.GetButtonDown ("p" + i.ToString ()))
				{
					b_GameStarted = true;
					GO_STARTSCREEN.SetActive (false);
					SC_PulsationSystem.EnablePulse ();
					SC_SoundManager.PlaySoundEffect ("VALIDATION", 1f);
					return;
				}
			}
		}

	}
	#endregion

	#region Private Methods
	public void InstantiatePlayers()
	{
		GameObject _objectPlayer;
		GameObject _particleSystem;

		for (int i=0; i < 8; i++)
		{
			_objectPlayer = Instantiate (GO_Prefab_Char, Li_V3_SpawnLeftTeam[i], Quaternion.identity, T_LeftTeamParent);
			Li_go_playersLeftTeam.Add (_objectPlayer.GetComponent<SC_CharController>());

			_particleSystem = Instantiate(dustParticles, Li_V3_SpawnLeftTeam[i], Quaternion.identity, T_LeftTeamParent);
			_particleSystem.transform.localPosition += new Vector3 (0f, -0.3f, 0f);
			_particleSystem.transform.eulerAngles = new Vector3 (0f, 0f, 150f);
			leftTeamParticleSystems.Add(_particleSystem.GetComponent<ParticleSystem>());
		}

		for(int i=0; i < 8; i++)
		{
			_objectPlayer = Instantiate (GO_Prefab_Char2, Li_V3_SpawnRightTeam[i], Quaternion.identity, T_RightTeamParent);
			_objectPlayer.transform.localScale = new Vector3 (-1f, 1f, 1f);
			Li_go_playersRightTeam.Add (_objectPlayer.GetComponent<SC_CharController>());

			_particleSystem = Instantiate(dustParticles, Li_V3_SpawnRightTeam[i], Quaternion.identity, T_RightTeamParent);
			_particleSystem.transform.localPosition += new Vector3 (0f, -0.3f, 0f);
			_particleSystem.transform.eulerAngles = new Vector3 (0f, 0f, 150f);
			_particleSystem.transform.localScale = new Vector3(-1f, 1f, 1f);
			rightTeamParticleSystems.Add(_particleSystem.GetComponent<ParticleSystem>());
		}
	}
	#endregion

	#region Public Methods
	public static void GenerateDust(int player)
	{
        if(player < 0)
        {
            return;
        }
        if(player >= 16)
        {
            return;
        }

  		if(player < 8)
  		{
  			_instance.leftTeamParticleSystems[player].Emit(20);
  		}
  		else
  		{
  			_instance.rightTeamParticleSystems[player - 8].Emit(20);
  		}
	}
	#endregion
}