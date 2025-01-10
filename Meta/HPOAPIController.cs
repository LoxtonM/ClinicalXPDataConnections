using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Meta;
using ClinicalXPDataConnections.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace ClinicalXPDataConnections.Meta
{
    public class HPOAPIController : Controller
    {
        private readonly ClinicalContext _clinContext;
        private readonly DocumentContext _docContext;
        private readonly IConfiguration _config;
        private readonly IPatientData _patientData;
        private readonly IConstantsData _constants;
        private readonly HPOCodeData _hpo;
        private string apiURLBase;
        private string apiURL;
        private string authKey;
        private string apiKey;
        //private readonly ICRUD _crud;

        public HPOAPIController(ClinicalContext clinContext, DocumentContext docContext, IConfiguration config)
        {
            _clinContext = clinContext;
            _docContext = docContext;
            _patientData = new PatientData(_clinContext);
            _constants = new ConstantsData(_docContext);
            _hpo = new HPOCodeData(_clinContext);
            apiURLBase = _constants.GetConstant("PhenotipsURL", 1).Trim();
            authKey = _constants.GetConstant("PhenotipsAPIAuthKey", 1).Trim();
            apiKey = _constants.GetConstant("PhenotipsAPIAuthKey", 2).Trim();
            _config = config;
            //_crud = new CRUD(_config);
        }

        public bool CheckResponseValid(string response)
        {
            if (response.Contains("<html>")) //this seems to be the only way to check - if no results, it will return "<html>"
            {
                return false;
            }

            return true;
        }

        public async Task<List<HPOTerm>> GetHPOCodes(string searchTerm)
        {
            List<HPOTerm> hpoCodes = new List<HPOTerm>();

            searchTerm = searchTerm.Replace(" ", "%20");
            int pageNo = 1;
            int setSize = 10;

            apiURL = $"https://ontology.jax.org/api/hp/search?q={searchTerm}&page={pageNo.ToString()}&limit={setSize.ToString()}";  //I don't know what the page and limit actually means!

            var options = new RestClientOptions(apiURL);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");

            var response = await client.GetAsync(request);

            dynamic dynJson = JsonConvert.DeserializeObject(response.Content);
            int totalCount = dynJson.totalCount;

            int i = 0;

            if (totalCount > setSize) //because I can't just have a number that returns all results - that would be far too convenient!
            {
                var batch = BatchInteger(totalCount, setSize); 

                foreach (var item in batch)
                {
                    foreach (var term in dynJson.terms)
                    {
                        i += 1; //the model needs an ID field, but as this is not supplied by ontology themselves, we need to fabricate it here
                        string hpoID = term.id;
                        if (hpoCodes.Where(t => t.TermCode == hpoID).Count() == 0) //because it's duplicating them all for some reason!!!
                        {
                            hpoCodes.Add(new HPOTerm { TermCode = term.id, Term = term.name, ID = i });
                        }
                    }
                    pageNo++;
                    apiURL = $"https://ontology.jax.org/api/hp/search?q={searchTerm}&page={pageNo.ToString()}&limit={item.ToString()}";
                    options = new RestClientOptions(apiURL);
                    client = new RestClient(options);
                    request = new RestRequest("");
                    request.AddHeader("accept", "application/json");

                    response = await client.GetAsync(request);

                    dynJson = JsonConvert.DeserializeObject(response.Content);
                    

                }
            }
            else
            {
                foreach (var term in dynJson.terms)
                {
                    i += 1;
                    Console.WriteLine(term.id);
                    hpoCodes.Add(new HPOTerm { TermCode = term.id, Term = term.name, ID = i });
                }
            }


             return hpoCodes.OrderBy(c => c.TermCode).ToList();
        }

        public async Task<HPOTerm> GetHPODataByTermCode(string hpoTermCode)
        {
            HPOTerm hPOTerm;

            apiURL = $"https://ontology.jax.org/api/hp/terms/{hpoTermCode.Replace(":", "%3A")}";  
                                                                                                
            var options = new RestClientOptions(apiURL);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");

            var response = await client.GetAsync(request);


            dynamic dynJson = JsonConvert.DeserializeObject(response.Content);
            
            hPOTerm = new HPOTerm{ ID = 1, TermCode = dynJson.id, Term = dynJson.name };


            return hPOTerm;
        }

        public async Task<List<string>> GetHPOSynonymsByTermCode(string hpoTermCode)
        {
            List<string> hpoSynonyms = new List<string>();

            apiURL = $"https://ontology.jax.org/api/hp/terms/{hpoTermCode.Replace(":", "%3A")}";

            var options = new RestClientOptions(apiURL);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");

            var response = await client.GetAsync(request);


            dynamic dynJson = JsonConvert.DeserializeObject(response.Content);

            foreach (var item in dynJson.synonyms)
            {                
                hpoSynonyms.Add(item.ToString()); 
            }

            return hpoSynonyms.ToList();
        }

        public async Task<IActionResult> GetAllHPOTerms()
        {
            apiURL = $"https://ontology.jax.org/api/hp/terms";

            var options = new RestClientOptions(apiURL);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");

            var response = await client.GetAsync(request);

            dynamic dynJson = JsonConvert.DeserializeObject(response.Content);
            
            foreach (var item in dynJson)
            {   
                string hpoID = item.id;
                string hpoName = item.name;                

                if (_hpo.GetHPOTermByTermCode(hpoID) == null)
                {
                    _hpo.AddHPOTermToDatabase(hpoID, hpoName.Replace("'", "''"), User.Identity.Name, _config);

                    int hpocodeid = _hpo.GetHPOTermByTermCode(hpoID).ID;

                    foreach (var synonym in item.synonyms)
                    {
                        string syn = synonym.ToString();
                        syn = syn.Replace("{", "").Replace("}", "");
                        _hpo.AddHPOSynonymToDatabase(hpocodeid, syn, User.Identity.Name, _config);
                    }                    
                }                
            }           

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<int> BatchInteger(int total, int batchSize)
        { //this is supposed to split the total into batches of 10, so each one can be run as a separate page - but the API itself is a bit janky
            if (batchSize == 0)
            {
                yield return 0;
            }

            if (batchSize >= total)
            {
                yield return total;
            }
            else
            {
                int rest = total % batchSize;
                int divided = total / batchSize;
                if (rest > 0)
                {
                    divided += 1;
                }

                for (int i = 0; i < divided; i++)
                {
                    if (rest > 0 && i == divided - 1)
                    {
                        yield return rest;
                    }
                    else
                    {
                        yield return batchSize;
                    }
                }
            }
        }

    }
}
