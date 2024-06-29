using UnityEngine;
using TextMeshProUGUI = TMPro.TextMeshProUGUI;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float firingDelay = .5f;

        [SerializeField] private Projectile projectile; 
        private int currentGun;

        [SerializeField] private TextMeshProUGUI[] guns;

        private Projectile currentProjectile;
        private Projectile currentProjectileTwo;




        private float elapsed = 0f;
        
        private float xValue;
        private float yValue = 0f;
        private float zValue;


        public float rollFactor = -40f;
    

        void Start()
        {
            if (guns == null)
            {
                guns = new TextMeshProUGUI[] {};
            }

            currentGun = 0;
            guns[currentGun].color = Color.red;
        }

        void ChangeGun()
        {
            guns[currentGun].color = Color.white;

            currentGun++;
            if (currentGun >= guns.Length)
            {
                currentGun = 0;
            }
            guns[currentGun].color = Color.red;
            WeaponManager.Instance.ChangeGun(currentGun);
        }

        // Update is called once per frame
        void Update()
        {
            elapsed += Time.time;

            if (Input.GetButtonDown("Fire3"))
            {
                ChangeGun();
            }
            float xRoll = Input.GetAxis("Horizontal");
            xValue = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            zValue = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            float roll = xRoll * rollFactor;
            transform.localRotation = Quaternion.Euler(0f, 0f, roll);
            transform.position = transform.position += new Vector3(xValue, 0, zValue);
        }

        
    }
}