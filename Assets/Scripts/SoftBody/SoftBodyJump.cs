using UnityEngine;

namespace SoftBody
{
    public class SoftBodyJump : MonoBehaviour
    {
        public float jumpForce = 20;

        public void OnSpring()
        {
            // Add the force to all rigid bodies (inner and outer)
            foreach (var body in GetComponentsInChildren<Rigidbody2D>())
            {
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}