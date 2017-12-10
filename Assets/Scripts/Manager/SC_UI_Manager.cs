// ---------------------------
// Copyright DUCKY GAMES, 2016.
// Samy BADACHE.
// ---------------------------

using UnityEngine;
using System.Collections;

public class SC_UI_Manager : MonoBehaviour 
{
	#region Public Fields
	public GameObject GO_YeahDroite;
	public GameObject GO_YeahGauche;
    #endregion

    #region Private Fields
	private static SC_UI_Manager _instance;
    #endregion

	#region Unity Methods
	void Awake()
	{
		_instance = this;
		GO_YeahDroite.SetActive (false);
		GO_YeahGauche.SetActive (false);
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    public static void ShowYeahLeft()
    {
        _instance.GO_YeahGauche.SetActive(false);
        _instance.GO_YeahGauche.SetActive(true);
        SC_SoundManager.PlaySoundEffect("YEAH-GAUCHE", 1f);
    }

    public static void ShowYeahRight()
	{
		_instance.GO_YeahDroite.SetActive (false);
		_instance.GO_YeahDroite.SetActive (true);
		SC_SoundManager.PlaySoundEffect ("YEAH-DROITE",1f);
	}
    #endregion
}