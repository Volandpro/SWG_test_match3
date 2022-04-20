using UnityEngine;

namespace Infrastructure.Services
{
    public class InputService : MonoBehaviour
    {
        private RaycastHit2D hit;
        private Ray ray;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.collider != null && hit.collider.TryGetComponent<IClickable>(out IClickable clickable))
                {
                    clickable.Click();
                }
            }
        }
    }
}