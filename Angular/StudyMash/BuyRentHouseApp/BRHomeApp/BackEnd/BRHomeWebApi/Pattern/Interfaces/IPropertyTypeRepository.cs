using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRHomeWebApi.Models;

namespace BRHomeWebApi.Pattern.Interfaces
{
    public interface IPropertyTypeRepository
    {

        Task<IEnumerable<PropertyType>> GetPropertyTypeAsync();
    }
}