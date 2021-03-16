using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tasks.Model.Domain;

namespace Tasks.Service.Persistence.Conversion
{
    public class RoleListToJsonArrayConverter : ValueConverter<IList<string>, string>
    {
        public RoleListToJsonArrayConverter() 
            : base(value => JsonSerializer.Serialize(value, null), value => JsonSerializer.Deserialize<List<string>>(value,  null), null) { }
    }
}