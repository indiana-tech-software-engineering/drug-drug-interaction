using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;

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
		public String doTheThing(string drugName) {
			 WebRequest request = WebRequest.Create (BaseUri+"/REST/rxcui.json?name"+drugName+"&search=1");
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse ();
            // Display the status.
            Console.WriteLine (response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream ();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader (dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd ();
            // Display the content.
            Console.WriteLine (responseFromServer);
            // Cleanup the streams and the response.
            reader.Close ();
            dataStream.Close ();
            response.Close ();
			return "corn";
		}
	}
}