using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.utils
{
    public static class Function
    {
        public static string GetSlug(string name){
            return name.Trim().ToLower().Replace(" ","-");
        }
    }
}