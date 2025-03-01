using UnityEngine;

public class ReplaceSpriteDirectionNPC : MonoBehaviour
{
    [SerializeField] private MovementNPC _movementNpc;

    [Header("Sprite Renderer NPC")]
    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private SpriteRenderer _leg_right;
    [SerializeField] private SpriteRenderer _leg_left;
    [SerializeField] private SpriteRenderer _arm_right;
    [SerializeField] private SpriteRenderer _arm_left;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [Header("Sprite NPC Head")]
    public Sprite HeadReplacedDown;
    public Sprite HeadReplacedUp;
    public Sprite HeadReplacedRight;
    public Sprite HeadReplacedLeft;
    [Header("Sprite NPC Body")]
    public Sprite BodyReplaced;
    [Header("Sprite NPC Leg")]
    public Sprite LegReplaced;
    [Header("Sprite NPC Arm")]
    public Sprite ArmReplaced;

 
    private void FixedUpdate()
    {
        Vector2 direction = _movementNpc.Velocity;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle >= -45 && angle < 45)
        {
            ReplaceSprites(HeadReplacedRight, BodyReplaced, LegReplaced, ArmReplaced);
        }
        else if (angle >= 45 && angle < 135)
        {
            ReplaceSprites(HeadReplacedUp, BodyReplaced, LegReplaced, ArmReplaced);
        }
        else if (angle >= 135 || angle < -135)
        {
            ReplaceSprites(HeadReplacedLeft, BodyReplaced, LegReplaced, ArmReplaced);
        }
        else
        {
            ReplaceSprites(HeadReplacedDown, BodyReplaced, LegReplaced, ArmReplaced);
        }

    }
    public void ReplaceSprites(Sprite head_replaced, Sprite body_replaced, Sprite leg_replaced, Sprite arm_replaced)
    {
        _head.sprite = head_replaced;
        _body.sprite = body_replaced;
        _leg_left.sprite = leg_replaced;
        _leg_right.sprite = leg_replaced;
        _leg_right.sprite = leg_replaced;
        _arm_right.sprite = arm_replaced;
        _arm_left.sprite = arm_replaced;
    }
    public void ReplaceSpriteBody(Sprite index)
    {
        _body.sprite = index;
    }
    public void ReplaceSpriteLeg(Sprite index)
    {
        _leg_left.sprite = index;
        _leg_right.sprite = index;
    }
}