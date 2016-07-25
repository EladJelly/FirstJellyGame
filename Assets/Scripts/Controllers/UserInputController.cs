using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class UserInputController : MonoBehaviour
    {
        void Update()
        {
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
