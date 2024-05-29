using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private Animator _animator;
    private float hitForce = 10f; // ���� �����
    private float hitRadius = 1f; // ������ �����
    public GameObject hitParticlesPrefab; // ������ ������ �����

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            // ����� �������� � ��������� �������
            Collider[] colliders = Physics.OverlapSphere(transform.position, hitRadius);

            // ��������� ������ ��������� ������
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Ball"))
                {
                    _animator.SetTrigger("isStrike");
                    // ��������� ���� ����� � ����
                    Rigidbody ballRigidbody = collider.GetComponent<Rigidbody>();
                    Vector3 direction = collider.transform.position - transform.position;
                    ballRigidbody.AddForce(direction.normalized * hitForce, ForceMode.Impulse);

                    // ������� ������ ������ �����
                    if (hitParticlesPrefab != null)
                    {
                        // ���������� ������� ������ � ������ ����� ����
                        Vector3 particlesPosition = collider.transform.position - direction.normalized * 0.5f;
                        GameObject particles = Instantiate(hitParticlesPrefab, particlesPosition, Quaternion.identity);
                    }

                    break; // ������� �� ����� ����� ����� �� ������� ����
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

                // ������� ������ ������ �����
                if (hitParticlesPrefab != null)
                {
                    // ���������� ������� ������ � ������ ����� ����
                    Vector3 particlesPosition = collider.transform.position - direction.normalized * 0.5f;
                    GameObject particles = Instantiate(hitParticlesPrefab, particlesPosition, Quaternion.identity);
                }

                break;
            }
        }
    }
}