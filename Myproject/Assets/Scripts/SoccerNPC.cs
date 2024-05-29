using UnityEngine;

public class SoccerNPC : MonoBehaviour
{
    public Transform ball; // ���
    public Transform goal; // ������, ���� NPC ����� ���������� ������ ���
    public Transform player; // �����
    public float dribbleDistance = 2f; // ����������, �� ������� NPC ����� �������� ���
    public float kickForce = 10f; // ���� ����� ����

    private UnityEngine.AI.NavMeshAgent agent; // ��������� ��� ����������� NPC
    private bool isKicking = false; // ���� ��� ����������� ���������� �����
    private GoalDetector goalDetector; // ���������� ��� ������� � GoalDetector

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.enabled = true;
        if (ball == null || goal == null || player == null)
        {
            Debug.LogError("Ball, Goal, or Player is not assigned to NPC!");
            enabled = false; // ��������� ������, ���� �� ��� ������� ���������
        }

        // �������� ������ � GoalDetector
        goalDetector = FindObjectOfType<GoalDetector>();
    }

    void Update()
    {
        if (!goalDetector.IsGameOver()) // ���������, �� ��������� �� ����
        {
            // ���� ��� �����, ��� ������������
            if (Vector3.Distance(transform.position, ball.position) <= dribbleDistance)
            {
                DribbleBall();
            }
            else // ����� ��������� � ����
            {
                agent.SetDestination(ball.position);
            }

            // ���� NPC ���������� ������ � ����, ���������� ������ ���
            if (Vector3.Distance(transform.position, ball.position) <= dribbleDistance && !isKicking)
            {
                KickBall();
            }
        }
        else // ���� ���� ���������, ���������� NPC � ������ ����
        {
            // ���������� �������� NPC
            agent.isStopped = true;

            // ����������� NPC � ����� ����
            agent.SetDestination(ball.position);
        }
    }

    public void DribbleBall()
    {
        // ��������� ������ ��������� � ������� ����
        agent.SetDestination(ball.position);
    }

    void KickBall()
    {
        // ����������� ��� � ������� �����
        Vector3 goalDirection = (goal.position - ball.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(ball.position, goalDirection, out hit))
        {
            if (hit.transform == goal)
            {
                ball.GetComponent<Rigidbody>().AddForce(goalDirection * kickForce, ForceMode.Impulse);
                isKicking = true;
                // ���� ��������� ����� ����� ���, ��� ����� �������� ������ ���
                Invoke("ResetKick", 2.0f);
                return;
            }
        }

        // ���� ����������� � ������� ����������, ������ ���
        Vector3 newDestination = transform.position + Vector3.Cross(Vector3.up, goalDirection).normalized * dribbleDistance * 2f;
        agent.SetDestination(newDestination);
    }

    void ResetKick()
    {
        isKicking = false;
    }
}
