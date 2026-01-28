using System.Collections.Generic;
using UnityEngine;




// �����հ� �ʱ� ũ�⸦ �޾� Ǯ �ʱ�ȭ:

// �����ڿ��� �����հ� �ʱ� ũ�⸦ �޾� Ǯ�� �ʱ�ȭ�մϴ�.
// �ʱ� ũ�⸸ŭ ������Ʈ�� �����Ͽ� Ǯ�� �߰��մϴ�.
// ���ο� ������Ʈ�� �����Ͽ� Ǯ�� �߰�:

// CreateNewObject �޼���� ���ο� ������Ʈ�� �����ϰ� ��Ȱ��ȭ ���·� Ǯ�� �߰��մϴ�.
// Ǯ���� ��� ������ ������Ʈ�� ��������:

// Get �޼���� Ǯ���� ��� ������ ������Ʈ�� �����ɴϴ�.
// Ǯ�� ��������� ���ο� ������Ʈ�� �����մϴ�.
// ������ ������Ʈ�� Ȱ��ȭ ���·� ��ȯ�մϴ�.
// ����� ���� ������Ʈ�� Ǯ�� ��ȯ:

// Return �޼���� ����� ���� ������Ʈ�� ��Ȱ��ȭ ���·� Ǯ�� ��ȯ�մϴ�.
// �� Ŭ������ ������Ʈ Ǯ���� ���� ������Ʈ ������ �ı��� ���� ���� ���ϸ� ���̰�, �޸� ������ ȿ�������� �� �� �ֵ��� �����ݴϴ�.


public class ObjectPool 
{

    // Ǯ���� ������
    private GameObject prefab;

    // ��Ȱ��ȭ�� ������Ʈ���� �����ϴ� ť
    private Queue<GameObject> pool;

    // Ǯ���� ������Ʈ���� �θ� Ʈ������
    private Transform parent;

    // ������: �����հ� �ʱ� ũ�⸦ �޾� Ǯ �ʱ�ȭ
    public ObjectPool(GameObject prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        pool = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }
    // ���ο� ������Ʈ�� �����Ͽ� Ǯ�� �߰��ϴ� private �޼���
    private void CreateNewObject()
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
    // Ǯ���� ��� ������ ������Ʈ�� �������� �޼���
    // Ǯ�� ��������� ���� ����
    public GameObject Get()
    {
        if (pool.Count == 0)
        {
            CreateNewObject();
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }


    // ����� ���� ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
