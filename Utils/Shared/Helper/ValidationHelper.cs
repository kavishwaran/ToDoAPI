using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Shared.Helper
{
    public class ValidationHelper
    {
        public static bool IsModelEmpty(params object[] models)
        {
            if (models == null || models.Length == 0)
            {
                return true;
            }

            foreach (var model in models)
            {
                if (model == null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
