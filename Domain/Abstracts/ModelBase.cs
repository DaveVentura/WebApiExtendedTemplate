using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaveVentura.WebApiExtendedTemplate.Domain.Abstracts {
    public class ModelBase<TKey> {
        public TKey? Id { get; set; }
    }
}
