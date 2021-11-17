using System;
using System.IO;
using System.Net;
using System.Net.Http;
using DDI.Models;


namespace DDI.DrugApi
{
	public class NatLibMedicineHttpClient
	{	
		private readonly HttpClient _httpClient;

		private const string BaseUri = "https://rxnav.nlm.nih.gov";

		public NatLibMedicineHttpClient()
		{
			_httpClient = new HttpClient();
		
		}
		public void FunctionRun(){}
		public void FetchDrugInteractions(){}
		public String fetchDrugID(string drugName) {
			WebRequest request = WebRequest.Create (BaseUri+"/REST/rxcui.json?name="+drugName+"&search=1");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse ();
            Stream dataStream = response.GetResponseStream ();
            StreamReader reader = new StreamReader (dataStream);
            string responseFromServer = reader.ReadToEnd ();
			var id = Utf8ReaderFromAPI.idTranslation(responseFromServer);
			Console.WriteLine(id);

            reader.Close ();
            dataStream.Close ();
            response.Close ();
			return id;
		}
	}
}