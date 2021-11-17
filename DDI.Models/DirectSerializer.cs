using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

namespace DDI.Models
{
    public class Utf8ReaderFromAPI
    {
		//converts string rxnormID to UTF standard bytes
		private static readonly byte[] s_rxnormIdUtf8 = Encoding.UTF8.GetBytes("rxnormId");

		public static string idTranslation(string rawText) {
			//converts the incoming text into a byte array
			byte[] rjsonReadOnlySpan = Encoding.UTF8.GetBytes(rawText);
			//sets some options for the reader  
			var options = new JsonReaderOptions
			{
				AllowTrailingCommas = true,
				CommentHandling = JsonCommentHandling.Skip
			};
			var reader = new Utf8JsonReader(rjsonReadOnlySpan);

			while (reader.Read()) {
				JsonTokenType tokenType = reader.TokenType;
				switch (tokenType) {
					case JsonTokenType.StartArray:
						reader.Read();
						return reader.GetString();

				}
			}
			return null;
		}
    }
}