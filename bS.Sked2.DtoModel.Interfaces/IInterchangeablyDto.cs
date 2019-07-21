using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.DtoModel.Interfaces
{
   public interface IInterchangeablyDto<T>
    {
        bool IsValid { get; set; }
        bool IsInit { get; set; }

        void Init();
        T Get();
        void Set(T value);

    }
}
