using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovieInfo.Models
{
   public class ActorTvModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Cast
        {
            public bool adult { get; set; }
            public string backdrop_path { get; set; }
            public List<int> genre_ids { get; set; }
            public int id { get; set; }
            public List<string> origin_country { get; set; }
            public string original_language { get; set; }
            public string original_name { get; set; }
            public string overview { get; set; }
            public double popularity { get; set; }
            public string poster_path { get; set; }
            public string first_air_date { get; set; }
            public string name { get; set; }
            public double vote_average { get; set; }
            public int vote_count { get; set; }
            public string character { get; set; }
            public string credit_id { get; set; }
            public int episode_count { get; set; }
        }

        public class Root
        {
            public List<Cast> cast { get; set; }
            public List<object> crew { get; set; }
            public int id { get; set; }
        }


    }
}
