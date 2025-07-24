public class EnemyProjectile : Projectile
{
    int hitCount;
    private void OnEnable()
    {
        hitCount = 1;
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player") && hitCount == 1)
        {
            GameManager.Instance.DescreaseHealth(GetDamage());
            hitCount = 0;
            gameObject.SetActive(false);
        }
    }
}