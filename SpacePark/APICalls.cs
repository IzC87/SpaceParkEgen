using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark
{
    public class APICalls
    {

        public async Task<SpaceshipOrVehicle> GetSpaceShipInfo(string link)
        {
            var client = new RestClient(link);
            var request = new RestRequest("", DataFormat.Json);
            var spaceShipResponse = await client.ExecuteAsync<SpaceshipOrVehicle>(request);

            if (spaceShipResponse.Data != null)
            {
                spaceShipResponse.Data.doubleLength = double.Parse(spaceShipResponse.Data.Length);
                return spaceShipResponse.Data;
            }
            return null;
        }

        public async Task<List<SpaceshipOrVehicle>> GetSpaceshipsOrVehiclesByPage(string requestInfo, int pageNumber)
        {
            var client = new RestClient("https://swapi.co/api/");
            var request = new RestRequest($"{requestInfo}/?page={pageNumber}", DataFormat.Json);
            var spaceshipResponse = await client.ExecuteAsync<SpaceshipOrVehicleSearch>(request);

            if (spaceshipResponse.Data != null)
            {
                return spaceshipResponse.Data.Results;
            }

            return null;
        }

        public async Task<PersonResult> GetPersonInfoByName(string name)
        {
            var client = new RestClient("https://swapi.co/api/");
            var request = new RestRequest($"people/?search={name}", DataFormat.Json);
            var peopleResponse = await client.ExecuteAsync<PersonSearch>(request);

            foreach (var p in peopleResponse.Data.Results)
            {
                if (p.Name == name)
                {
                    return p;
                }
            }
            return null;
        }

        public async Task<PersonResult> GetPersonInfoByID(int id)
        {
            var client = new RestClient("https://swapi.co/api/");
            var request = new RestRequest($"people/{id}", DataFormat.Json);
            var peopleResponse = await client.ExecuteAsync<PersonResult>(request);

            if (peopleResponse.Data != null)
            {
                return peopleResponse.Data;
            }
            return null;
        }

        public async Task<int> GetNumberOfValidNames()
        {
            var client = new RestClient("https://swapi.co/api/");
            var request = new RestRequest($"people/", DataFormat.Json);
            var peopleResponse = await client.ExecuteAsync<PersonSearch>(request);
            return peopleResponse.Data.Count;
        }
    }
}
