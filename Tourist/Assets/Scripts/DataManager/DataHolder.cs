using System.Collections.Generic;

public static class DataHolder
{
    // �������� ��������� �������
    // 0 - ����, 1 - ���, 2 - �����, 3 - ����
    public static int IdLocation { get; set; }

    // �������� ���������� ������� ����
    // 0 - �����, 1 - ����, 2 - �����, 3 - ����
    public static int IdSeason { get; set; }

    // �������� �������������� ���������� �������
    // 0 - ���������, 1 - �������, 2 - �������
    public static int IdBackpack { get; set; }

    // �������� ����� � �������
    public static List<string> Items { get; set; }

    // ���� ���������� ��������� ����� ��������
    public static bool IsAfterRoute { get; set; }

    // ���� �� �������� � ������ ������
    public static bool IsNotifyStart { get; set; }

    // ���� �� �������� � ����� ������
    public static bool IsNotifyEnd { get; set; }
}