// ---------------------------
// Copyright DUCKY GAMES, 2016.
// Samy BADACHE.
// ---------------------------

using UnityEngine;
using System.Collections;
using SpriterDotNetUnity;

public class SC_MerRemou : MonoBehaviour 
{
	#region Public Fields
    #endregion

    #region Private Fields
	SpriterDotNetBehaviour SpAnim_Mer;
    #endregion

	#region Unity Methods
	void Awake()
	{
		SpAnim_Mer = GetComponent<SpriterDotNetBehaviour> ();
	}

	void Start ()
	{
		StartCoroutine ("AnimatorCoroutine");
	}
	
	void Update ()
	{
	}
	#endregion

	#region Private Methods
	private IEnumerator AnimatorCoroutine()
	{
		while(SpAnim_Mer.Animator == null)
		{
			yield return null;
		}
		SpAnim_Mer.Animator.Speed = 0.2f;
	}
    #endregion

    #region Public Methods
    #endregion
}