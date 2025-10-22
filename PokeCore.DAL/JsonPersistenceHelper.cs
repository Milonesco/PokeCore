// Esta classe é sua antiga "JsonDatabase"
using System.Text.Json;
using System.Xml;

public static class JsonPersistenceHelper
{
    public static List<T> Load<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"AVISO: Arquivo JSON não encontrado em '{filePath}'. Retornando lista vazia.");
            return new List<T>();
        }
        try
        {
            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                Console.WriteLine($"AVISO: Arquivo JSON '{filePath}' está vazio ou contém apenas espaços. Retornando lista vazia.");
                return new List<T>();
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<T> result = JsonSerializer.Deserialize<List<T>>(json, options);

            if (result == null)
            {
                Console.WriteLine($"AVISO: Desserialização de '{filePath}' resultou em null. Retornando lista vazia.");
                return new List<T>();
            }
            return result;
        }
        catch (JsonException jsonEx)
        {
            Console.WriteLine($"ERRO CRÍTICO: Falha ao desserializar JSON em '{filePath}'. Conteúdo inválido. Erro: {jsonEx.Message}");
            return new List<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO CRÍTICO: Falha geral ao ler ou processar arquivo '{filePath}'. Erro: {ex.Message}");
            return new List<T>();
        }
    }


    public static void Save<T>(string filePath, List<T> data)
    {
        string tempFile = filePath + ".tmp";

        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(tempFile, json);
        File.Delete(filePath);
        File.Move(tempFile, filePath);
    }
}