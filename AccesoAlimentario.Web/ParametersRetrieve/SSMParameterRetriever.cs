namespace AccesoAlimentario.Web.ParametersRetrieve;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;


public class SSMParameterRetriever
{
    public string Region { get; set; } = "us-east-1";

    public string GetString(string parameterName)
    {
        using var client = new AmazonSimpleSystemsManagementClient(Amazon.RegionEndpoint.GetBySystemName(Region));
        
        var request = new GetParameterRequest
        {
            Name = parameterName,
            WithDecryption = true // Permite desencriptar si el parámetro está encriptado
        };

        try
        {
            var response = client.GetParameterAsync(request).Result;
            return response.Parameter.Value;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener el parámetro: {ex.Message}");
            throw;
        }
    }

    public async Task<string> GetStringAsync(string parameterName)
    {
        using var client = new AmazonSimpleSystemsManagementClient(Amazon.RegionEndpoint.GetBySystemName(Region));
        
        var request = new GetParameterRequest
        {
            Name = parameterName,
            WithDecryption = true
        };

        try
        {
            var response = await client.GetParameterAsync(request);
            return response.Parameter.Value;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener el parámetro: {ex.Message}");
            throw;
        }
    }
}
