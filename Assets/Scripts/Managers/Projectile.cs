using UnityEngine;

namespace Managers
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        [SerializeField] private bool rotateShoot = false;
    
        [SerializeField] Transform player;

        private Vector3 moveDirection;
        private Vector3 initialVelocity;


        public void Initialize(Vector3 direction, Vector3 playerVelocity)
        {
            initialVelocity = playerVelocity;
            moveDirection = direction.normalized;
            Debug.Log("initial velocity: " + playerVelocity);
            Debug.Log("move direction: " + moveDirection);
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            //transform.position += transform.forward * speed * Time.deltaTime;
            transform.position += (moveDirection * speed + initialVelocity) * Time.deltaTime;
        }
    }
}