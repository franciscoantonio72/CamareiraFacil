using CamareiraFacil.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace CamareiraFacil.Service
{
    public class ApiCamareiraFacil
    {
        private const string BaseURL = "http://192.168.0.12:6051";
        private const string BaseURLConsumo = "http://192.168.0.12:6051/datasnap/rest/TServerMethods1/LancaConsumo";

        public ApiCamareiraFacil()
        {
            if (!CrossConnectivity.Current.IsConnected)
                throw new System.ArgumentException("Sem Conectividade", "Erro");
        }

        public List<ItemPDV> GetItensPDV(string pdv)
        {
            try
            {
                string prsPdv = "0007";
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(BaseURL);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync("datasnap/rest/TServerMethods1/BuscarItensPDVsPorPDV/" + prsPdv).Result;

                if (response.IsSuccessStatusCode)
                {
                    var teste = response.Content.ReadAsStringAsync().Result;

                    var resultado = JsonConvert.DeserializeObject<DataSnapResponse<List<ItemPDV>>>(teste);

                    return resultado.result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Apartamento> GetApartamentos()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(BaseURL);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync("datasnap/rest/TServerMethods1/BuscarApartamentoOcupados").Result;

                if (response.IsSuccessStatusCode)
                {
                    var teste = response.Content.ReadAsStringAsync().Result;

                    var resultado = JsonConvert.DeserializeObject<DataSnapResponse<List<Apartamento>>>(teste);

                    return resultado.result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> LancaConsumo(ObservableCollection<ItemLancamento> listaItens)
        {
            try
            {
                var jsonLista = JsonConvert.SerializeObject(new { itens = listaItens });

                using (var httpClient = new HttpClient())
                {
                    HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, BaseURL + "/datasnap/rest/TServerMethods1/LancaConsumo");
                    message.Content = new StringContent(jsonLista, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.SendAsync(message);

                    if (response.IsSuccessStatusCode)
                    {
                        var teste = response.Content.ReadAsStringAsync().Result;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> GravaRecado(Recado itemRecado)
        {
            try
            {
                var jsonRecado = JsonConvert.SerializeObject(itemRecado);

                using (var httpClient = new HttpClient())
                {
                    HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, BaseURL + "/datasnap/rest/TServerMethods1/GravaRecado");
                    message.Content = new StringContent(jsonRecado, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.SendAsync(message);

                    if (response.IsSuccessStatusCode)
                    {
                        var teste = response.Content.ReadAsStringAsync().Result;
                        return true; 
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> GravaOrdemServico(OrdemServico ordemServico)
        {
            try
            {
                var jsonRecado = JsonConvert.SerializeObject(ordemServico);

                using (var httpClient = new HttpClient())
                {
                    HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, BaseURL + "/datasnap/rest/TServerMethods1/GravaServico");
                    message.Content = new StringContent(jsonRecado, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.SendAsync(message);

                    if (response.IsSuccessStatusCode)
                    {
                        var teste = response.Content.ReadAsStringAsync().Result;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> ComecaFaxina(Faxina faxina)
        {
            try
            {
                var jsonRecado = JsonConvert.SerializeObject(faxina);

                using (var httpClient = new HttpClient())
                {
                    HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, BaseURL + "/datasnap/rest/TServerMethods1/ComecaFaxina");
                    message.Content = new StringContent(jsonRecado, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.SendAsync(message);

                    if (response.IsSuccessStatusCode)
                    {
                        var teste = response.Content.ReadAsStringAsync().Result;
                        if (teste.Contains("ERRO"))
                            return false;
                        else
                            return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> TerminaFaxina(Faxina faxina)
        {
            try
            {
                var jsonRecado = JsonConvert.SerializeObject(faxina);

                using (var httpClient = new HttpClient())
                {
                    HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, BaseURL + "/datasnap/rest/TServerMethods1/FinalizaFaxina");
                    message.Content = new StringContent(jsonRecado, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.SendAsync(message);

                    if (response.IsSuccessStatusCode)
                    {
                        var teste = response.Content.ReadAsStringAsync().Result;
                        if (teste.Contains("ERRO"))
                            return false;
                        else
                            return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Funcionario> GetFuncionarios()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(BaseURL);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync("datasnap/rest/TServerMethods1/BuscarFuncionarios").Result;

                if (response.IsSuccessStatusCode)
                {
                    var teste = response.Content.ReadAsStringAsync().Result;

                    var resultado = JsonConvert.DeserializeObject<DataSnapResponse<List<Funcionario>>>(teste);

                    return resultado.result;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<LocaisManutencao> GetLocaisManutencao()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(BaseURL);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync("datasnap/rest/TServerMethods1/BuscarLocaisManutencao").Result;

                if (response.IsSuccessStatusCode)
                {
                    var teste = response.Content.ReadAsStringAsync().Result;

                    var resultado = JsonConvert.DeserializeObject<DataSnapResponse<List<LocaisManutencao>>>(teste);

                    return resultado.result;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
