using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class PlayerMove : MonoBehaviour
    {
        [Header("Shooting Settings")] [SerializeField]
        private float maxForce = 20f;

        [SerializeField] private float minForce = 5f;
        [SerializeField] private float maxDistance = 5f;

        [Header("Visual Feedback")] [SerializeField]
        private LineRenderer aimLine;

        [SerializeField] private float lineLength = 2f;
        public bool isDead = false; 
        private Rigidbody2D rb;
        private bool isCharging = false;
        private float currentForce;
        private Vector2 shootDirection;
        private Camera mainCamera;
        private Vector2 dragStartPosition;
        private bool canCharge = false;
        private Vector2 screenBounds;
        private float objectWidth;
        private float objectHeight;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            mainCamera = Camera.main;

            if (aimLine == null)
            {
                aimLine = gameObject.AddComponent<LineRenderer>();
                aimLine.startWidth = 0.1f;
                aimLine.endWidth = 0.1f;
                aimLine.material = new Material(Shader.Find("Sprites/Default"));
            }

            aimLine.enabled = false;

            screenBounds =
                mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
                    mainCamera.transform.position.z));

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                objectWidth = spriteRenderer.bounds.size.x / 2;
                objectHeight = spriteRenderer.bounds.size.y / 2;
            }
        }

        private void Update()
        {
            if (Time.timeScale != 0)
            {
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                shootDirection = (Vector2)transform.position - mousePosition;
                shootDirection.Normalize();

                // Đảo hướng mặt theo hướng bắn
                UpdateFacingDirection();

                if (canCharge && Input.GetMouseButton(0))
                {
                    if (!isCharging)
                    {
                        StartCharging(mousePosition);
                    }

                    ChargeShot(mousePosition);
                    UpdateAimLine();
                }

                if (Input.GetMouseButtonUp(0) && isCharging)
                {
                    Shoot();
                    canCharge = false;
                }

                CheckScreenWrap();
            }
          
        }

        private void UpdateFacingDirection()
        {
            // Lấy scale hiện tại
            Vector3 localScale = transform.localScale;

            // Nếu đang hướng về bên phải (shootDirection.x < 0 vì hướng bắn ngược với hướng nhìn)
            if (shootDirection.x < 0)
            {
                localScale.x = Mathf.Abs(localScale.x); // Mặt phải
            }
            else
            {
                localScale.x = -Mathf.Abs(localScale.x); // Mặt trái
            }

            transform.localScale = localScale;
        }

        private void CheckScreenWrap()
        {
            Vector2 currentPosition = transform.position;
            bool wasWrapped = false;

            if (currentPosition.x + objectWidth < -screenBounds.x)
            {
                currentPosition.x = screenBounds.x + objectWidth;
                wasWrapped = true;
            }
            else if (currentPosition.x - objectWidth > screenBounds.x)
            {
                currentPosition.x = -screenBounds.x - objectWidth;
                wasWrapped = true;
            }

            // if (currentPosition.y + objectHeight < -screenBounds.y)
            // {
            //     currentPosition.y = screenBounds.y + objectHeight;
            //     wasWrapped = true;
            // }
            // else if (currentPosition.y - objectHeight > screenBounds.y)
            // {
            //     currentPosition.y = -screenBounds.y - objectHeight;
            //     wasWrapped = true;
            // }

            if (wasWrapped)
            {
                transform.position = currentPosition;
            }
        }

        private void OnMouseDown()
        {
            canCharge = true;
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            dragStartPosition = mousePosition;
        }

        private void StartCharging(Vector2 mousePosition)
        {
            isCharging = true;
            currentForce = minForce;
            aimLine.enabled = true;
        }

        private void ChargeShot(Vector2 currentMousePosition)
        {
            float dragDistance = Vector2.Distance(dragStartPosition, currentMousePosition);
            float normalizedDistance = Mathf.Clamp01(dragDistance / maxDistance);
            currentForce = Mathf.Lerp(minForce, maxForce, normalizedDistance);
        }

        private void UpdateAimLine()
        {
            Vector2 startPos = transform.position;
            Vector2 endPos = startPos + shootDirection * lineLength * (currentForce / maxForce);

            aimLine.SetPosition(0, startPos);
            aimLine.SetPosition(1, endPos);
        }

        private void Shoot()
        {
            rb.velocity = shootDirection * currentForce;

            isCharging = false;
            currentForce = minForce;
            aimLine.enabled = false;
        }

        private void OnDrawGizmos()
        {
            if (isCharging)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(
                    transform.position,
                    (Vector2)transform.position + shootDirection * lineLength * (currentForce / maxForce)
                );
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DeadZone") && !isDead)
            {
                Debug.Log("die");
                isDead = true;
                UIManager.Instance.PauseGame();
                UIManager.Instance.OpenUI<LoseCanvas>();
            }
        }
    }
}

