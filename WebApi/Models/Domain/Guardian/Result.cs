namespace WebApi.Models.Domain.Guardian
{
    using Newtonsoft.Json;
    using System;

    public class Result
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string SectionId { get; set; }

        public string SectionName { get; set; }

        public DateTime WebPublicationDate { get; set; }

        public string WebTitle { get; set; }

        public Uri WebUrl { get; set; }

        public Uri ApiUrl { get; set; }

        public bool IsHosted { get; set; }

        public string PillarId { get; set; }

        public string PillarName { get; set; }
    }
}