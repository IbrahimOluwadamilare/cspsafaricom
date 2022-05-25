using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.CspApiModels.UserResponse
{
    public partial class UsersResponse
    {
        public string ContinuationToken { get; set; }
        public int TotalCount { get; set; }
        public List<Item> Items { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string ObjectType { get; set; }
    }

    public partial class Item
    {
        public string UsageLocation { get; set; }
        public string Id { get; set; }
        public string UserPrincipalName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public object PasswordProfile { get; set; }
        public object LastDirectorySyncTime { get; set; }
        public string UserDomainType { get; set; }
        public string State { get; set; }
        public object SoftDeletionTime { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Links
    {
        public Self Self { get; set; }
    }

    public partial class Self
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public List<object> Headers { get; set; }
    }

}
