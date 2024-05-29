using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private Animator _animator;
    private float hitForce = 10f; // Сила удара
    private float hitRadius = 1f; // Радиус удара
    public GameObject hitParticlesPrefab; // Префаб частиц удара

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            // Поиск объектов в указанном радиусе
            Collider[] colliders = Physics.OverlapSphere(transform.position, hitRadius);

            // Проверяем каждый найденный объект
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Ball"))
                {
                    _animator.SetTrigger("isStrike");
                    // Применяем силу удара к мячу
                    Rigidbody ballRigidbody = collider.GetComponent<Rigidbody>();
                    Vector3 direction = collider.transform.position - transform.position;
                    ballRigidbody.AddForce(direction.normalized * hitForce, ForceMode.Impulse);

                    // Создаем эффект частиц удара
                    if (hitParticlesPrefab != null)
                    {
                        // Определяем позицию частиц в задней части мяча
                        Vector3 particlesPosition = collider.transform.position - direction.normalized * 0.5f;
                        GameObject particles = Instantiate(hitParticlesPrefab, particlesPosition, Quaternion.identity);
                    }

                    break; // Выходим из цикла после удара по первому мячу
                }
            }
        }
    }
    public void Strike()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, hitRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ball"))
            {
                _animator.SetTrigger("isStrike");
                Rigidbody ballRigidbody = collider.GetComponent<Rigidbody>();
                Vector3 direction = collider.transform.position - transform.position;
                ballRigidbody.AddForce(direction.normalized * hitForce, ForceMode.Impulse);

                // Создаем эффект частиц удара
                if (hitParticlesPrefab != null)
                {
                    // Определяем позицию частиц в задней части мяча
                    Vector3 particlesPosition = collider.transform.position - direction.normalized * 0.5f;
                    GameObject particles = Instantiate(hitParticlesPrefab, particlesPosition, Quaternion.identity);
                }

                break;
            }
        }
    }
}