// ---------------------------
// Copyright DUCKY GAMES, 2016.
// Samy BADACHE.
// ---------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SC_Clignote : MonoBehaviour 
{
	#region Public Fields
	public Sprite[] Li_Sprs;
	public Image Img;
    #endregion

    #region Private Fields
    #endregion

	#region Unity Methods
	void Awake()
	{
		
	}

	void Start ()
	{
		StartCoroutine ("Coroutine2");
	}
	
	void Update ()
	{
	
	}
	#endregion

	#region Private Methods
    #endregion

    #region Public Methods
	public IEnumerator Coroutine2()
	{
		while(true)
		{
			Img.sprite = Li_Sprs [0];
			yield return new WaitForSeconds (0.5f);
			Img.sprite = Li_Sprs [1];
			yield return new WaitForSeconds (0.5f);
			yield return null;
		}

	}
    #endregion
}