// ---------------------------
// Copyright DUCKY GAMES, 2016.
// Samy BADACHE.
// ---------------------------

using UnityEngine;
using System.Collections;
using SpriterDotNetUnity;

public class sc_vage : MonoBehaviour 
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
		
	}
	
	void Update ()
	{
	
	}
	#endregion

	#region Private Methods
	private IEnumerator AnimatorCoroutine(string _s_)
	{
		while(SpAnim_Mer.Animator == null)
		{
			yield return null;
		}
		SpAnim_Mer.Animator.Play (_s_);
	}
    #endregion

    #region Public Methods
	public void CallAnimation(string _s_)
	{
		StartCoroutine ("AnimatorCoroutine", _s_);
	}
    #endregion
}