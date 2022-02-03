using System;
using Venture.IdentityManagement.SharedKernal.Abstractions;

namespace Venture.IdentityManagement.Core
{
    public class Tenant: AggregateRoot
    {
        public Guid TenantId { get; private set; }
        public string Name { get; private set; }

        public Tenant(CreateTenantDomainEvent @event)
        {

        }

        private Tenant()
        {

        }
    }
}
