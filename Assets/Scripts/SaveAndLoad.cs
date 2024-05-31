using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    string savePath;

    public void Save(int highScore)
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveFile");

        using (var writer = new BinaryWriter(File.Open(savePath, FileMode.Create)))
        {
            writer.Write(highScore);
        }
    }

    public int Load()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveFile");

        using (var reader = new BinaryReader(File.Open(savePath, FileMode.Open)))
        {
            int highScore = reader.ReadInt32();
            return highScore;
        }
    }
}
