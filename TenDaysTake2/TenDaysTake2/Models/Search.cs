using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TenDaysTake2.Models
{
    public class Meta
    {
        public int code { get; set; }
        public string requestId { get; set; }
    }

    public class LabeledLatLng
    {
        public string label { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public IList<LabeledLatLng> labeledLatLngs { get; set; }
        public int distance { get; set; }
        public string cc { get; set; }
        public string country { get; set; }
        public IList<string> formattedAddress { get; set; }
        // add to the Location class
        private string coordinates;
        public string Coordinates
        {
            get { return $"{lat:0.000}, {lng:0.000}"; }
        }
    }

    public class Icon
    {
        public string prefix { get; set; }
        public string suffix { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pluralName { get; set; }
        public string shortName { get; set; }
        public Icon icon { get; set; }
        public bool primary { get; set; }
    }

    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public IList<Category> categories { get; set; }
        public string referralId { get; set; }
        public bool hasPerk { get; set; }
        private string mainCategory;
        public string MainCategory
        {
            // added using System.Linq;
            get { return categories.FirstOrDefault()?.name; }
        }
    }

    public class Response
    {
        public IList<Venue> venues { get; set; }
        public bool confident { get; set; }
    }

    public class Search
    {
        public Meta meta { get; set; }
        public Response response { get; set; }

        static string YOUR_CLIENT_ID = "XQC3TAKIKKKW14VWXWZTR55WD5RDZ433NYRPR4D2VVDAZ2T3";
        static string YOUR_CLIENT_SECRET = "3IKN1P30HVGAKIVBO33Q5D4UJWJEQDLCGMNK1VICTWIMX0VV";

        public static async Task<Response> SearchRequest(double lat, double lon, int radius, string query, int limit = 3)
        {
            string url = $"https://api.foursquare.com/v2/venues/search?ll={lat},{lon}&radius={radius}&query={query}&limit={limit}&client_id={YOUR_CLIENT_ID}&client_secret={YOUR_CLIENT_SECRET}&v={DateTime.Now.ToString("yyyyMMdd")}";
            using (HttpClient httpClient = new HttpClient())
            {
                string json = await httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<Search>(json).response;
            }
        }
    }
}
