using UnityEngine;

public class SwordMotion : MonoBehaviour
{
    public float floatAmplitude = 0.1f;
    public float floatSpeed = 2f;

    public float attackUpAmount = 0.5f;
    public float attackDuration = 0.15f;

    private bool isAttacking;
    private float attackTimer;

    private Vector3 startLocalPos;
    private SpriteRenderer playerSprite;

    public Vector3 rightSidePosition;
    public Vector3 leftSidePosition;

    private bool facingLeft;

    void Start()
    {
        startLocalPos = transform.localPosition;
        playerSprite =
    GetComponentInParent<SpriteRenderer>();
    }

    void Update()
    {
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;

            float t = attackTimer / attackDuration;

            // Smooth slash curve
            float curve = Mathf.Sin(t * Mathf.PI);

            // Rotate sword from -90 to +90 (180 total)
            float angle;

            if (facingLeft)
            {
                angle = Mathf.Lerp(0f, -180f, t);
            }
            else
            {
                angle = Mathf.Lerp(0f, 180f, t);
            }

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            // Optional upward motion during slash
            float yOffset = curve * attackUpAmount;

            transform.localPosition = startLocalPos + new Vector3(0, yOffset, 0);

            if (attackTimer >= attackDuration)
            {
                isAttacking = false;

                // Reset rotation
                transform.localRotation = Quaternion.identity;
            }

            return;
        }

        float idleOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        transform.localPosition = startLocalPos + new Vector3(0, idleOffset, 0);

        // Idle rotation reset
        transform.localRotation = Quaternion.identity;
    }

    public void PlayAttack()
    {
        isAttacking = true;
        attackTimer = 0f;
    }
    public void SetFacing(bool left)
    {
        facingLeft = left;

        startLocalPos =
            left ? leftSidePosition : rightSidePosition;
    }
}
