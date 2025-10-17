// Esta classe é sua antiga "JsonDatabase"
using System.Text.Json;
using System.Xml;

public static class JsonPersistenceHelper
{
    // Método para Ler
    public static List<T> Load<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<T>(); // Retorna lista vazia se o arquivo não existe
        }
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    // Método para Salvar (com salvamento atômico!)
    public static void Save<T>(string filePath, List<T> data)
    {
        string tempFile = filePath + ".tmp"; // Cria arquivo temporário

        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(tempFile, json);

        // Se a escrita no .tmp deu certo, substitui o original
        // Isso é o "Salvamento Atômico"
        File.Delete(filePath);
        File.Move(tempFile, filePath);
    }
}