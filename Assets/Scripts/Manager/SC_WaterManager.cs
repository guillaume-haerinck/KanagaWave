// ---------------------------
// Copyright DUCKY GAMES, 2016.
// Samy BADACHE.
// ---------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SpriterDotNetUnity;

public class SC_WaterManager : MonoBehaviour 
{
	#region Public Fields
	public ParticleSystem PS_Explosion;
	public GameObject Go_WaveCat0Prefab;
	public GameObject Go_WaveCat1Prefab;
	public GameObject Go_WaveCat2Prefab;

	public Transform T_WavesParent;
    public ParticleSystem leftBubbleParticles;
    public ParticleSystem rightBubbleParticles;
    #endregion

    #region Private Fields
    private static SC_WaterManager _instance;
    #endregion

	public class WaveAttack
	{
		public GameObject obj;
		public int i_WaveCategory;


		public int force;
		public bool hasDamaged;

		public bool b_IsAlive;
		public bool b_GoLeft;


		public WaveAttack(GameObject _obj, int _force, bool _hasDamaged, int _waveCat ,  bool _isAlive = false)
		{
			obj = _obj;
			force = _force;
			hasDamaged = _hasDamaged;
			b_IsAlive = _isAlive;
			i_WaveCategory = _waveCat;
		}
	}



	#region WavePool
	private List<WaveAttack> Li_WavesTotal;
	#endregion

	#region Unity Methods
	void Awake()
	{
		_instance = this;
		GenerateWavesPool();
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
		MoveWaves ();
		ClampWaves ();
	}
	#endregion

	#region WavePoolManager
	private void GenerateWavesPool()
	{
		Li_WavesTotal = new List<WaveAttack>();

		GameObject _go_wave;
		for(int i = 0; i < 10; i++)
		{
			_go_wave = Instantiate(Go_WaveCat0Prefab, T_WavesParent);
			Li_WavesTotal.Add(new WaveAttack(_go_wave, 0, false, 0));
			_go_wave.SetActive(false);
		}

		for(int i = 0; i < 10; i++)
		{
			_go_wave = Instantiate(Go_WaveCat1Prefab, T_WavesParent);
			Li_WavesTotal.Add(new WaveAttack(_go_wave, 0, false, 1));
			_go_wave.SetActive(false);
		}

		for(int i = 0; i < 10; i++)
		{
			_go_wave = Instantiate(Go_WaveCat2Prefab, T_WavesParent);
			Li_WavesTotal.Add(new WaveAttack(_go_wave, 0, false, 2));
			_go_wave.SetActive(false);
		}
	}
	#endregion

	#region Private Methods
	public void MoveWaves()
	{
        float distanceToTravel = 7f * Time.deltaTime;



        // Start by resolving collisions.
        // We could optimise it way better but it doesn't matter since there is never gonna be much more than a couple of waves at any time simultaneously on the screen.
		Li_WavesTotal.ForEach(delegate (WaveAttack leftWave)
        {
			if(!leftWave.b_GoLeft || !leftWave.b_IsAlive)
				return;
			Li_WavesTotal.ForEach(delegate (WaveAttack rightWave)
            {
				if(rightWave.b_GoLeft || !leftWave.b_IsAlive)
					return;

                float distance = Mathf.Abs(leftWave.obj.transform.position.x - rightWave.obj.transform.position.x);
                if ((distance >= 0) && (distance <= distanceToTravel) && !leftWave.hasDamaged && !rightWave.hasDamaged)
                {
                    if(leftWave.force > rightWave.force)
                    {
                        // Left wave wins.
                        int i = Li_WavesTotal.IndexOf(leftWave);
						Li_WavesTotal[i].force = leftWave.force - rightWave.force;

                        // Right wave is removed.
                        rightWave.obj.SetActive(false);
						i = Li_WavesTotal.IndexOf(rightWave);
						Li_WavesTotal[i].b_IsAlive = false;
                    }
                    else if(rightWave.force > leftWave.force)
                    {
                        // Right wave wins.
						int i = Li_WavesTotal.IndexOf(rightWave);
						Li_WavesTotal[i].force = rightWave.force - leftWave.force;

                        // Left wave is removed.
                        leftWave.obj.SetActive(false);
						i = Li_WavesTotal.IndexOf(leftWave);
						Li_WavesTotal[i].b_IsAlive = false;
                    }
                    else // Waves cancel each other out.
                    {

						float collisionPositionX = (leftWave.obj.transform.position.x + rightWave.obj.transform.position.x) / 2f;
						PS_Explosion.transform.position = new Vector3(collisionPositionX, PS_Explosion.transform.position.y, PS_Explosion.transform.position.z);
						PS_Explosion.Emit (50);

                        // Left wave is removed.
						leftWave.obj.SetActive(false);
						int i = Li_WavesTotal.IndexOf(leftWave);
						Li_WavesTotal[i].b_IsAlive = false;

                        // Right wave is removed.
						rightWave.obj.SetActive(false);
						i = Li_WavesTotal.IndexOf(rightWave);
						Li_WavesTotal[i].b_IsAlive = false;
                    }
                }
            });
        });

        // Move the left waves.
		for (int i = 0; i< Li_WavesTotal.Count; i++)
		{
			if(Li_WavesTotal[i].b_IsAlive)
			{
				if (Li_WavesTotal [i].b_GoLeft)
					Li_WavesTotal [i].obj.transform.position += Vector3.left * distanceToTravel;
				else
					Li_WavesTotal [i].obj.transform.position += Vector3.right * distanceToTravel;
			}
		}
	}

	public void ClampWaves()
	{
		for (int i = 0; i < Li_WavesTotal.Count; i++)
		{
			if(Li_WavesTotal[i].b_IsAlive)
			{
				if (Li_WavesTotal [i].b_GoLeft)
				{
					if(!Li_WavesTotal[i].hasDamaged && Li_WavesTotal[i].obj.transform.position.x <= -5f)
					{
						SC_IslandsLife.RemoveLife(true, Li_WavesTotal[i].force);
						Li_WavesTotal [i].hasDamaged = true;
					}

					if(Li_WavesTotal[i].obj.transform.position.x <= -11f)
					{
						Li_WavesTotal[i].obj.SetActive(false);
						Li_WavesTotal [i].b_IsAlive = false;
					}
				}
				else
				{
					if(!Li_WavesTotal[i].hasDamaged && Li_WavesTotal[i].obj.transform.position.x >= 5f)
					{
						SC_IslandsLife.RemoveLife (false, Li_WavesTotal[i].force);
						Li_WavesTotal [i].hasDamaged = true;
					}

					if(Li_WavesTotal[i].obj.transform.position.x >= 11f)
					{
						Li_WavesTotal[i].obj.SetActive(false);
						Li_WavesTotal [i].b_IsAlive = false;
					}
				}
			}

		}
	}
    #endregion

    #region Public Methods
	public static void GenerateWave(bool _b_GoLeft, int _force)
	{
		if (_force < 3)
			return;

		for(int i=0; i< _instance.Li_WavesTotal.Count; i++)
		{
			if(!_instance.Li_WavesTotal[i].b_IsAlive && _instance.Li_WavesTotal[i].i_WaveCategory == Mathf.FloorToInt(((float)_force -3f) / 2f))
			{
				_instance.Li_WavesTotal [i].hasDamaged = false;
				_instance.Li_WavesTotal [i].force = _force;
				_instance.Li_WavesTotal [i].b_GoLeft = _b_GoLeft;
				_instance.Li_WavesTotal [i].b_IsAlive = true;

				_instance.Li_WavesTotal [i].obj.SetActive (true);


				if (Mathf.FloorToInt (((float)_force - 3f) / 2f) == 0)
				{
					_instance.Li_WavesTotal [i].obj.transform.Find ("vague").GetComponent<sc_vage> ().CallAnimation ("faible");
					SC_SoundManager.PlaySoundEffect ("VAGUE-PUISSANCE-PETITE", 1f);
				}
				else if (Mathf.FloorToInt (((float)_force - 3f) / 2f) == 1)
				{
					_instance.Li_WavesTotal [i].obj.transform.Find ("vague").GetComponent<sc_vage> ().CallAnimation ("moyenne");
					SC_SoundManager.PlaySoundEffect ("VAGUE-PUISSANCE-MOYENNE", 1f);
				}
				else if (Mathf.FloorToInt (((float)_force - 3f) / 2f) == 2)
				{
					_instance.Li_WavesTotal [i].obj.transform.Find ("vague").GetComponent<sc_vage> ().CallAnimation ("grande");
					SC_SoundManager.PlaySoundEffect ("VAGUE-PUISSANCE-MAX", 1f);
				}



				if(!_b_GoLeft)
				{
					_instance.Li_WavesTotal [i].obj.transform.position = new Vector3 (-4.59f, -2.7f, 0f);
					_instance.Li_WavesTotal [i].obj.transform.localScale = new Vector3 (1f, 1f, 1f);
				}
				else
				{
					_instance.Li_WavesTotal [i].obj.transform.position = new Vector3 (4.59f, -2.7f, 0f);
					_instance.Li_WavesTotal [i].obj.transform.localScale = new Vector3 (-1f, 1f, 1f);
				}

				break;
			}
		}
	}

    public static void GenerateLeftBubbles(int _force)
    {
        _instance.leftBubbleParticles.Play();
    }

    public static void GenerateRightBubbles(int _force)
    {
        _instance.rightBubbleParticles.Play();
    }
    #endregion
}