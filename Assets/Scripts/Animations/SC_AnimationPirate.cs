//// ---------------------------
//// Copyright DUCKY GAMES, 2016.
//// Samy BADACHE.
//// ---------------------------
//
//using UnityEngine;
//using System.Collections;
//using SpriterDotNetUnity;
//
//public class SC_AnimationPirate : MonoBehaviour 
//{
//	#region Public Fields
//    #endregion
//
//    #region Private Fields
//	SpriterDotNetBehaviour SpAnim_Pirate;
//    #endregion
//
//	#region Unity Methods
//	void Awake()
//	{
//		SpAnim_Pirate = GetComponent<SpriterDotNetBehaviour> ();
//	}
//
//	void Start ()
//	{
//		StartCoroutine ("AnimatorCoroutine");
//	}
//	
//	void Update ()
//	{
//	
//	}
//	#endregion
//
//	#region Private Methods
//	private IEnumerator AnimatorCoroutine()
//	{
//		while(SpAnim_Pirate.Animator == null)
//		{
//			yield return null;
//		}
//		SpAnim_Pirate.Animator.Speed = 1f;
//		SpAnim_Pirate.Animator.Play ("sautDebut");
//	}
//    #endregion
//
//    #region Public Methods
//    #endregion
//}