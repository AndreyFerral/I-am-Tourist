using UnityEngine;

public class GetToggle : MonoBehaviour
{
    // ������ ��� ������ �������
    public void PlainToggle() => DataHolder.IdLocation = 0;
    public void ForrestToggle() => DataHolder.IdLocation = 1;
    public void LakeToggle() => DataHolder.IdLocation = 2;
    public void MountainsToggle() => DataHolder.IdLocation = 3;

    // ������ ��� ������ ������� ����
    public void SpringToggle() => DataHolder.IdSeason = 0;
    public void SummerToggle() => DataHolder.IdSeason = 1;
    public void AutumnToggle() => DataHolder.IdSeason = 2;
    public void WinterToggle() => DataHolder.IdSeason = 3;
}