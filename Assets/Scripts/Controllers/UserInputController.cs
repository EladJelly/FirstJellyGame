using Assets.Scripts.View;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class UserInputController : MonoBehaviour
    {
        void Update()
        {
			if (Input.anyKey && GameSessionData.CurrentState == GameSessionData.GameState.Intro)
			{
				GameEventsController.OnStartGame();
			}

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.GetComponent<ClickAreaView>())
                    {
                        GameEventsController.OnClickAreaTriggered(hit.point);
                    }
                }
            }


        }
    }
}
