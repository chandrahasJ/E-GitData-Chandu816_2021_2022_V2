﻿using Newtonsoft.Json;
using SimpleTraderApp.Domain.Models;
using SimpleTraderApp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTraderApp.FMPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        public string aPIKey { get; private set; }
        public MajorIndexService()
        {

        }

        public MajorIndexService(string _APIKey)
        {
            aPIKey = _APIKey;
        }

        public void SetApiKey(string _APIKey)
        {
            aPIKey = _APIKey;
        }


        public async Task<MajorIndex> GetMajorIndex(MajorIndexType majorIndexType)
        {            
            using (FMPrepHttpClient client = new FMPrepHttpClient())
            {
                string urlMajorIndex = $"majors-indexes/{GetUriSuffix(majorIndexType)}?{GetAPIQuery()}";

                MajorIndex majorIndex = await client.GetTaskAsync<MajorIndex>(urlMajorIndex);
                majorIndex.Type = majorIndexType;
                return majorIndex; 
            }
        }

        private string GetUriSuffix(MajorIndexType majorIndexType)
        {
            switch (majorIndexType)
            {
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.Nasdaq:
                    return ".IXIC";
                case MajorIndexType.SP500:
                    return ".INX";
                default:
                    throw new Exception("Major Index Type doesn't have suffix defined.");
            }
        }

        public string GetAPIQuery()
        {
            return $"apikey={aPIKey}";
        }



    }
}