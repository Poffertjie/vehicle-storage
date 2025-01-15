using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class AdditionalContactPerson
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string IdentificationNumber { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string Relationship { get; set; } = null!;

    public Guid MODIFIED_BY { get; set; }

    public int CompanyId { get; set; }

    public string FullName { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
