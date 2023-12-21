using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Dtos.Product
{
    public record ItemProductDto(int id, string name, decimal price, string sku, string? description);
}
