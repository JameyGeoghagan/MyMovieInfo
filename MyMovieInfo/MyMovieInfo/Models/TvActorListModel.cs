﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovieInfo.Models
{
   public class TvActorListModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Cast
        {
            public bool adult { get; set; }
            public int gender { get; set; }
            public int id { get; set; }
            public string known_for_department { get; set; }
            public string name { get; set; }
            public string original_name { get; set; }
            public double popularity { get; set; }
            public string profile_path { get; set; }
            public string character { get; set; }
            public string credit_id { get; set; }
            public int order { get; set; }
        }

        public class Crew
        {
            public bool adult { get; set; }
            public int gender { get; set; }
            public int id { get; set; }
            public string known_for_department { get; set; }
            public string name { get; set; }
            public string original_name { get; set; }
            public double popularity { get; set; }
            public string profile_path { get; set; }
            public string credit_id { get; set; }
            public string department { get; set; }
            public string job { get; set; }
        }

        public class Root
        {
            public List<Cast> cast { get; set; }
            public List<Crew> crew { get; set; }
            public int id { get; set; }
        }


    }
}