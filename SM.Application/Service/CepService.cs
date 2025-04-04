﻿using System.Text.Json;
using SM.Domaiin.Entities;

namespace SM.Application.Service
{
    public class CepService
    {
        private readonly HttpClient _httpClient;

        public CepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Endereco> BuscarEnderecoPorCepAsync(string cep)
        {
            var url = $"https://viacep.com.br/ws/{cep}/json/";

            var response = await _httpClient.GetStringAsync(url);

            var endereco = JsonSerializer.Deserialize<Endereco>(response);

            return endereco;
        }
    }
}
