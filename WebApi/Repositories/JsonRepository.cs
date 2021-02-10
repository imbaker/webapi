using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WebApi.Models.Domain;

namespace WebApi.Repositories
{
    public class JsonRepository : IJsonRepository
    {
        enum SupportedSections
        {
            None = 0,
            Policy = 2
        }

        private List<Policy> Policies = new List<Policy>()
        {
            new Policy { Id = 1, LastUpdated = DateTime.Now, PolicyNo="PN1", Firstname = "Ian", Surname="Baker" },
            new Policy { Id = 2, LastUpdated = DateTime.Now, PolicyNo="PN2", Firstname = "Jane", Surname="Bennett" },
            new Policy { Id = 3, LastUpdated = DateTime.Now, PolicyNo="PN3", Surname="Smith" }
        };

        private int nextId = 4;

        public Policy GetPolicy(int id)
        {
            return Policies.FirstOrDefault(c => c.Id == id);
        }

        public int AddPolicy(Policy policy)
        {
            policy.Id = nextId++;
            policy.LastUpdated = DateTime.Now;
            Policies.Add(policy);
            return policy.Id;
        }

        public Policy UpdatePolicy(int id, Policy policy)
        {
            var policyToUpdate = Policies.FirstOrDefault(c => c.Id == id);
            if (policyToUpdate == null)
                return null;

            policyToUpdate.LastUpdated = DateTime.Now;
            policyToUpdate.PolicyNo = policy.PolicyNo;
            policyToUpdate.Firstname = policy.Firstname;
            policyToUpdate.Surname = policy.Surname;

            return policyToUpdate;
        }

        public static int TryMapTaskSection(string taskSection)
        {
            SupportedSections returnValue;
            var result = Enum.TryParse<SupportedSections>(taskSection, true, out returnValue);
            if (result)
                return (int)returnValue;
            else
                return -1;
        }
    }
}