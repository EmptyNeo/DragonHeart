using System.Collections;
using UnityEngine;

public class MagicStaff : Magic
{

    private void Start()
    {
        if (transform.parent != null)
        {
            _circle = transform.parent.transform.GetChild(0);
        }
    }
    public override void OnAttack()
    {
        if (MagicAbility == null)
        {
            Notification.Instance.SetNotification("Экипируйте магию");
            Notification.Instance.TurnBackground(true);
            StartCoroutine(Notification.Instance.TurnOffBackgroundOverTime(3));
        }
        else
            base.OnAttack();
    }
    public override IEnumerator IAttack()
    {
        IsDelay = true;
        MagicAbility ablity = Instantiate(MagicAbility.gameObject, _circle.position, Quaternion.identity).GetComponent<MagicAbility>();
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)_circle.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        ablity.transform.SetPositionAndRotation(_circle.position, Quaternion.Euler(0, 0, angle - 45));
        ablity.Rigidbody2D.linearVelocity = 5 * direction;
        ablity.Damage = Damage;
        MagicAbility.Ability();
        yield return new WaitForSeconds(Delay);
        IsDelay = false;
    }
}
