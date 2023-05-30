using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace yetAnotherObfuscator {
    class ChangeMethodsName {
        

        public static void Fire(ModuleDefMD moduleDef, Assembly assembly) {

            IEnumerable<TypeDef> types = moduleDef.GetTypes();
            foreach (dnlib.DotNet.TypeDef type in types.ToList()) {                
                Dictionary<string, string> org_names= new Dictionary<string, string>();
                string type_random = EncodeString(type.Name);
                org_names[type_random] = type.Name;
                if (! type.Name.StartsWith("<"))
                    type.Name = type_random;
                else {
                    continue;
                }
            }
        }

        public static string EncodeString(string str) {
            // get b64 output without "="
            int padding = 3 -(str.Length % 3);
            string curr_str = str + new String('A', padding);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(curr_str);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        
    }
}