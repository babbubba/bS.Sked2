using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.DtoModel.Interfaces
{
    public interface IInterchangeablyDto<T> : IInterchangeablyBaseDto
    {
     

        void Init();
        T Get();
        void Set(T value);

    }
}
