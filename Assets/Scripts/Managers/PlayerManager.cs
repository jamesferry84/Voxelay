using UnityEngine;
using TextMeshProUGUI = TMPro.TextMeshProUGUI;
using DG.Tweening;

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

        public float barrelRollDoubleClickSpeed = 1f;
        private bool pressedFirstTime = false;
        private float lastPressedTime;
        

        private float elapsed = 0f;
        
        private float xValue;
        private float yValue = 0f;
        private float zValue;


        public float rollFactor = -40f;
    

        void Start()
        {
            DOTween.SetTweensCapacity(500, 50);
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

            if (Input.GetKeyDown("q"))
            {
                if (pressedFirstTime)
                {
                    bool isDoublePress = Time.time - lastPressedTime <= barrelRollDoubleClickSpeed;

                    if (isDoublePress)
                    {
                        Debug.Log("DO A BARREL ROLL");
                        BarrelRoll();
                        pressedFirstTime = false;
                    }
                }
                else
                {
                    pressedFirstTime = true;
                }

                lastPressedTime = Time.time;
            }

            if (pressedFirstTime && Time.time - lastPressedTime > barrelRollDoubleClickSpeed)
            {
                pressedFirstTime = false;
            }

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

        void BarrelRoll()
        {
             transform.DORotate(new Vector3(0f, 0f, 360f), .5f, RotateMode.LocalAxisAdd);
            // float angle = Mathf.Lerp(0, 360, 0.2f);
            // transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        
    }
}