using bS.Sked2.DtoModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.DtoModel.Interchangeably
{
    /// <summary>
    /// Base abstracted type for dto passed between element. For example a returned value or a input parameter for one or more elements.
    /// </summary>
    /// <seealso cref="bS.Sked2.DtoModel.Interfaces.IInterchangeablyDto" />
    public abstract class InterchangeablyBase<T> : IInterchangeablyDto<T>
    {
        /// <summary>
        /// Returns true if this istance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialize.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is initialize; otherwise, <c>false</c>.
        /// </value>
        public bool IsInit { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Gets the value of this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract T Get();

     

        /// <summary>
        /// Sets the specified value for this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        public abstract void Set(T value);

        protected void ThrowIfNotValid()
        {
            if (!IsValid) throw new ApplicationException("This istance value is not valid.");
        }

        protected void ThrowIfNotInit()
        {
            if (!IsInit) throw new ApplicationException("This istance is not initialized.");
        }
    }
}
