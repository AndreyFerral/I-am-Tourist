using UnityEngine;

public class TabContentManager : MonoBehaviour
{
    // ������ �������� � ��������� �������
    private GameObject[] tabContents;

    // ������� ��� ������������ ����� ���������
    public void SwitchTab(int index)
    {
        GetTabContents();

        // �������� ��� ������� � ���������
        for (int i = 0; i < tabContents.Length; i++)
        {
            tabContents[i].SetActive(false);
        }

        // ���������� ������ � ���������, ��������������� ��������� �������
        // ������� ��� ����� "����������� ����" ��-�� ������ "�����"
        if (tabContents.Length > index) tabContents[index].SetActive(true);
    }

    // ����� ������� ��� ��������� ID �������� �������
    public int GetActiveTabIndex()
    {
        GetTabContents();

        for (int i = 0; i < tabContents.Length; i++)
        {
            if (tabContents[i].activeInHierarchy)
            {
                return i; // ���������� ������ �������� �������
            }
        }

        return -1; // ���������� -1, ���� �� ���� ������� �� �������
    }

    // ������� ��� ��������������� ���������� ������� tabContents
    private void GetTabContents()
    {
        // ������� ��� �������� ������� �������� ������� (������ ��������)
        tabContents = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tabContents[i] = transform.GetChild(i).gameObject;
        }
    }
}
