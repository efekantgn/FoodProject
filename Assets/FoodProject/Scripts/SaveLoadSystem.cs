using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveLoadSystem
{
    private static readonly string SaveFolderPath = Application.persistentDataPath + "/Saves/";
    private static readonly string EncryptionKey = "qqqqqqqqqqqqqqqq"; // 16 karakter uzunluğundaki bir key kullanın.

    static SaveLoadSystem()
    {
        if (!Directory.Exists(SaveFolderPath))
        {
            Directory.CreateDirectory(SaveFolderPath);
        }
    }

    /// <summary>
    /// Veriyi belirli bir anahtarla kaydeder.
    /// </summary>
    public static void Save<T>(string key, T data, bool useEncryption = false)
    {
        string filePath = SaveFolderPath + key + ".json";
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        if (useEncryption)
        {
            json = Encrypt(json);
        }

        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Belirtilen anahtar için kaydedilmiş veriyi yükler.
    /// </summary>
    public static bool TryLoad<T>(string key, out T t, bool useEncryption = false)
    {
        string filePath = SaveFolderPath + key + ".json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            if (useEncryption)
            {
                json = Decrypt(json);
            }
            t = JsonUtility.FromJson<T>(json);

            return true;
        }

        Debug.LogWarning($"Save file not found: {filePath}");
        t = default;
        return false;
    }

    /// <summary>
    /// Belirli bir anahtara ait kaydı siler.
    /// </summary>
    public static void Delete(string key)
    {
        string filePath = SaveFolderPath + key + ".json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    /// <summary>
    /// Tüm kayıtları siler.
    /// </summary>
    public static void DeleteAll()
    {
        if (Directory.Exists(SaveFolderPath))
        {
            Directory.Delete(SaveFolderPath, true);
            Directory.CreateDirectory(SaveFolderPath);
        }
    }

    /// <summary>
    /// Metni AES ile şifreler.
    /// </summary>
    private static string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = new byte[16]; // Default IV (sıfırlarla dolu).

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var writer = new StreamWriter(cs))
                {
                    writer.Write(plainText);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    /// <summary>
    /// Şifrelenmiş metni AES ile çözer.
    /// </summary>
    private static string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = new byte[16]; // Default IV (sıfırlarla dolu).

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var reader = new StreamReader(cs))
            {
                return reader.ReadToEnd();
            }
        }
    }
}