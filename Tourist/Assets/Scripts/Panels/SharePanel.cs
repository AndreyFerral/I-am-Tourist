using TMPro;
using UnityEngine;
using System.IO;
using System;

public class SharePanel : MonoBehaviour
{
    private TMP_Text statusText;

    public void LoadButton()
    {
        string levelDataPath = Path.Combine(Application.persistentDataPath, "JSON/LevelDataList.json");
        string questionChoiceDataPath = Path.Combine(Application.persistentDataPath, "JSON/QuestionChoiceDataList.json");

        statusText = transform.GetChild(3).GetComponent<TMP_Text>();

        string loadPath = GetDownloadPath("CombinedData.json");

        if (!File.Exists(loadPath))
        {
            Debug.LogError("CombinedData.json �� ������");
            statusText.text = "������: CombinedData.json �� ������"; ;
            return;
        }

        string jsonData = File.ReadAllText(loadPath);
        CombinedData combinedData = JsonUtility.FromJson<CombinedData>(jsonData);

        File.WriteAllText(levelDataPath, combinedData.levelData);
        File.WriteAllText(questionChoiceDataPath, combinedData.questionChoiceData);

        statusText.text = "������ ������� ���������"; ;
        Debug.Log("������ ��������� � ��������� �� ������");
    }

    public void UnloadButton()
    {
        string levelDataPath = Path.Combine(Application.persistentDataPath, "JSON/LevelDataList.json");
        string questionChoiceDataPath = Path.Combine(Application.persistentDataPath, "JSON/QuestionChoiceDataList.json");
        statusText = transform.GetChild(3).GetComponent<TMP_Text>();

        if (!File.Exists(levelDataPath) || !File.Exists(questionChoiceDataPath))
        {
            Debug.LogError("���� ��� ��� ����� �� �������");
            statusText.text = "������: ���� ������ ��� ������� �� ������";
            return;
        }

        string levelData = File.ReadAllText(levelDataPath);
        string questionChoiceData = File.ReadAllText(questionChoiceDataPath);

        CombinedData combinedData = new CombinedData
        {
            levelData = levelData,
            questionChoiceData = questionChoiceData
        };

        string jsonData = JsonUtility.ToJson(combinedData);
        string downloadPath = GetDownloadPath("CombinedData.json");

        File.WriteAllText(downloadPath, jsonData);
        Debug.Log("�������� ������ ��������� �������: " + downloadPath);
        statusText.text = "�������� ������ ��������� �������";
    }

    private string GetDownloadPath(string filename)
    {
        string path = string.Empty;

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", filename);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            // ��� Android ���������� ����������� ����
            path = Path.Combine(Application.persistentDataPath, filename); 
            using (AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment"))
            {
                AndroidJavaObject downloadsDir = environment.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", environment.GetStatic<string>("DIRECTORY_DOWNLOADS"));
                path = Path.Combine(downloadsDir.Call<string>("getPath"), filename);
            }
        }

        return path;
    }
}

[Serializable]
public class CombinedData
{
    public string levelData;
    public string questionChoiceData;
}