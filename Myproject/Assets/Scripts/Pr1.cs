using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pr1 : MonoBehaviour
{
    // �����, ��������� � ��������� ��������� �����
    private float elapsedTime = 0f;

    // ��������� �������� ����� ����������� ����� (� ��������)
    private float interval = 20f;

    // ������� �������� ��������
    private static int activeObjectCount = 0;

    void OnEnable()
    {
        // ����������� ������� ��� ��������� �������
        activeObjectCount++;
        Debug.Log("�������� ��������: " + activeObjectCount);
    }

    void OnDisable()
    {
        // ��������� ������� ��� ����������� �������
        activeObjectCount--;
        Debug.Log("�������� ��������: " + activeObjectCount);
    }
    void Start()
    {
        
    }

    void Update()
    {
        // ����������� ��������� ����� �� ����� ��������� � ������� ���������� ����������
        elapsedTime += Time.deltaTime;

        // ���� ������ ���������� ������� ��� ��������� �����
        if (elapsedTime >= interval)
        {
            // ���������� ��������� ����� �� 0 �� 100 (������������)
            int randomNumber = Random.Range(0, 101);

            // ������� ����� � �������
            Debug.Log("��������� �����: " + randomNumber);

            // �������� ��������� �����
            elapsedTime = 0f;
        }
    }
}
