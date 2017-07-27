using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wyc_NEWRK.Service
{
    class serviceBLL
    {
        serviceDAO dal = new serviceDAO();
        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(servicemodel.service model)
        {
            return dal.Add(model);
        }

        #endregion  Method
    }

}
