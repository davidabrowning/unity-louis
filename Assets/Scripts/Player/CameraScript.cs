using UnityEngine;

namespace FarmerDemo
{
    public class CameraScript : MonoBehaviour
    {
        public GameObject Target;
        void Update()
        {
            float x = Target.transform.position.x;
            float y = Target.transform.position.y;
            float z = -10;
            transform.position = new Vector3(x, y, z);
        }
    }
}