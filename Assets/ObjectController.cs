using UnityEngine;

namespace Watermelon
{
    public class ObjectController : MonoBehaviour
    {
        public void DisableObject()
        {
            transform.gameObject.SetActive(false);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() { }

        // Update is called once per frame
        void Update() { }
    }
}
