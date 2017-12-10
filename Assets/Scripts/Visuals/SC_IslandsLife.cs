// ---------------------------
// Copyright DUCKY GAMES, 2016.
// Samy BADACHE.
// ---------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_IslandsLife : MonoBehaviour 
{
	#region Public Fields
	public float f_StartPosY;
	public float f_EndPosY;

	public Cl_Island cl_IslandLeft;
	public Cl_Island cl_IslandRight;
    #endregion

	[System.Serializable]
	public class Cl_Island
	{
		public Transform T_Island;
		public SpriteRenderer SpR_Island;

		public Image Img_Life;
		public ParticleSystem Part_Island;

		[HideInInspector]
		public float f_IslandLife = 100f;
		[HideInInspector]
		public Vector3 V3_IslandPos;
	}

    #region Private Fields
	private static SC_IslandsLife _instance;
    #endregion

	#region Unity Methods
	void Awake()
	{
		_instance = this;
	}

	void Start ()
	{
		cl_IslandLeft.V3_IslandPos = cl_IslandLeft.T_Island.position;
		cl_IslandRight.V3_IslandPos = cl_IslandRight.T_Island.position;
	}
	
	void Update ()
	{
////		f_IslandOneLife -= 10f * Time.deltaTime;
//		cl_IslandLeft.V3_IslandPos.y = Mathf.Lerp (f_StartPosY, f_EndPosY, 1 - cl_IslandLeft.f_IslandLife / 100f);
//		cl_IslandLeft.T_Island.position = cl_IslandLeft.V3_IslandPos;
//
////		f_IslandTwoLife -= 10f * Time.deltaTime;
//		cl_IslandRight.V3_IslandPos.y = Mathf.Lerp (f_StartPosY, f_EndPosY, 1 - cl_IslandRight.f_IslandLife / 100f);
//		cl_IslandRight.T_Island.position = cl_IslandRight.V3_IslandPos;


		cl_IslandLeft.Img_Life.fillAmount = Mathf.Lerp (1f, 0.2f, 1 - cl_IslandLeft.f_IslandLife / 100f);
		cl_IslandRight.Img_Life.fillAmount = Mathf.Lerp (1f, 0.2f, 1 - cl_IslandRight.f_IslandLife / 100f);

		if (cl_IslandLeft.f_IslandLife <= 0f || cl_IslandRight.f_IslandLife <= 0f)
			SceneManager.LoadScene (0);
	}
	#endregion

	#region Private Methods
	private float f_flashDuration = 0.05f;
	private Color Col_White = new Color (1f, 1f, 1f, 1f);
	private Color Col_Aplha = new Color (0f, 0f, 0f, 0f);
	private IEnumerator DealDamageCoroutine(bool _b_IsLeft)
	{
		for(int i=0; i< 3; i++)
		{
			if(_b_IsLeft)
			{
				cl_IslandLeft.SpR_Island.color = Col_Aplha;
				yield return new WaitForSeconds (f_flashDuration);
				cl_IslandLeft.SpR_Island.color = Col_White;
				yield return new WaitForSeconds (f_flashDuration);
			}
			else
			{
				cl_IslandRight.SpR_Island.color = Col_Aplha;
				yield return new WaitForSeconds (f_flashDuration);
				cl_IslandRight.SpR_Island.color = Col_White;
				yield return new WaitForSeconds (f_flashDuration);
			}
		}
	}
    #endregion

    #region Public Methods
	public static void RemoveLife(bool _b_IsLeft, float _f_force)
	{
		if (_b_IsLeft)
		{
			_instance.cl_IslandLeft.f_IslandLife -= (_f_force * _f_force) / 2f;
			SC_SoundManager.PlaySoundEffect ("EBOULEMENT-GAUCHE", 1f);
			_instance.cl_IslandLeft.Part_Island.Emit (50);
		}
		else
		{
			_instance.cl_IslandRight.f_IslandLife -= (_f_force * _f_force) / 2f;
			SC_SoundManager.PlaySoundEffect ("EBOULEMENT-DROITE", 1f);
			_instance.cl_IslandRight.Part_Island.Emit (50);
		}


		SC_CameraShake.Shake();


		_instance.StartCoroutine ("DealDamageCoroutine", _b_IsLeft);
	}
    #endregion
}