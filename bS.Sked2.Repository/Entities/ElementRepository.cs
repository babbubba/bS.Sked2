using bS.Sked2.Model.Elements.Base;
using bS.Sked2.Model.Interfaces.Elements;
using bS.Sked2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bS.Sked2.Repository.Entities
{
    public class ElementRepository : Repository
    {
        public ElementRepository(IUnitOfWork mainUnitOfWork) : base(mainUnitOfWork)
        {
        }

        public IExecutableElement GetElementById(Guid id)
        {
            var result = GetQuery<ExecutableElementBase>().FirstOrDefault(x=> x.Id == id);
            return result;
        }
    }
}
