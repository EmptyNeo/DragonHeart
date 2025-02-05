using UnityEngine;

public class PlayerReplacedSprite : MonoBehaviour
{
    [Header("Sprite Renderer Player")]
    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private SpriteRenderer _leg_right;
    [SerializeField] private SpriteRenderer _leg_left;
    [Header("Sprite Player Head")]
    public Sprite HeadReplacedDown;
    public Sprite HeadReplacedUp;
    public Sprite HeadReplacedRight;
    public Sprite HeadReplacedLeft;
    [Header("Sprite Player Body")]
    public Sprite BodyReplaced;
    [Header("Sprite Player Leg")]
    public Sprite LegReplaced;
    private void FixedUpdate()
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle >= -45 && angle < 45)
        {
            ReplaceSprites(HeadReplacedRight, BodyReplaced, LegReplaced);
        }
        else if (angle >= 45 && angle < 135)
        {
            ReplaceSprites(HeadReplacedUp, BodyReplaced, LegReplaced);
        }
        else if (angle >= 135 || angle < -135)
        {
            ReplaceSprites(HeadReplacedLeft, BodyReplaced, LegReplaced);
        }
        else
        {
            ReplaceSprites(HeadReplacedDown, BodyReplaced, LegReplaced);
        }

    }
    public void ReplaceSprites(Sprite head_replaced, Sprite body_replaced, Sprite leg_replaced)
    {
        _head.sprite = head_replaced;
        _body.sprite = body_replaced;
        _leg_left.sprite = leg_replaced;
        _leg_right.sprite = leg_replaced;
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
