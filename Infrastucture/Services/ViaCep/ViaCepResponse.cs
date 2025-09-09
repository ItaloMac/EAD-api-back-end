using System.Text.Json;

namespace Infrastucture.Services.ViaCep;

public class ConsultaViaCepService
{
      public class ViaCepResponse
      {
            public string cep { get; set; } = null!;
            public string logradouro { get; set; } = null!;
            public string complemento { get; set; } = null!;
            public string unidade { get; set; } = null!;
            public string bairro { get; set; } = null!;
            public string localidade { get; set; } = null!;
            public string uf { get; set; } = null!;
            public string estado { get; set; } = null!;
            public string regiao { get; set; } = null!;
            public string ibge { get; set; } = null!;
      }

      public async Task<string> GetIbgeCodeFromCepAsync(string cep)
      {
            using (var client = new HttpClient())
            {
                  var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                  response.EnsureSuccessStatusCode();

                  var content = await response.Content.ReadAsStringAsync();
                  var result = JsonSerializer.Deserialize<ViaCepResponse>(content);

                  return result!.ibge;
            };
      }
}
