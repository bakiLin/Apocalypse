using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class JsonDataService
{
    public T LoadData<T>(string Path)
    {
        string path = Application.persistentDataPath + Path;
        T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        return data;
    }

    public void SaveData<T>(string Path, T Data)
    {
        string path = Application.persistentDataPath + Path;

        if (File.Exists(path))
            File.Delete(path);
        using FileStream stream = File.Create(path);
        stream.Close();
        File.WriteAllText(path, JsonConvert.SerializeObject(Data));
    }
}
