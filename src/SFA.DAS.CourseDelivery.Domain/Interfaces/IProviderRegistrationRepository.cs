﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderRegistrationRepository
    {
        void DeleteAll();
        Task InsertMany(IEnumerable<ProviderRegistration> providerRegistrations);
        Task UpdateAddressesFromImportTable();
        Task<ProviderRegistration> GetProviderByUkprn(int ukprn);
        Task<IEnumerable<ProviderRegistration>> GetAllProviders();
    }
}