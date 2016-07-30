using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

namespace Assets.Scripts.View
{
	public class IntroView : MonoBehaviour 
	{
		public GameObject TitleLine1;
		public GameObject TitleLine2;
		public GameObject AnyKey;

		void Start() 
		{
			StartCoroutine(ShowIntro());
		}

		private IEnumerator ShowIntro()
		{
			yield return new WaitForSeconds(1f);
			TitleLine1.SetActive(true);
			yield return new WaitForSeconds(1f);
			TitleLine2.SetActive(true);
			yield return new WaitForSeconds(1f);
			AnyKey.SetActive(true);
			GameSessionData.CurrentState = GameSessionData.GameState.Intro;
		}
	}
}
