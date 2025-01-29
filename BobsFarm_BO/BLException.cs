using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobsFarm_BO
{
    public class BLException : Exception
    {
        #region public fields
        public BOError _error;
        #endregion
        #region Constructors
        public BLException() { }
        public BLException(BOError error) : base(error.Message)
        {
            this._error = error;
        }
        #endregion
    }
}
