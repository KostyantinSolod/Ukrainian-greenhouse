using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ukrainian_greenhouse
{
    public class SecureStringToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SecureString secureString)
            {
                IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                SecureString secureString = new SecureString();
                foreach (char c in str)
                {
                    secureString.AppendChar(c);
                }
                return secureString;
            }

            return new SecureString();
        }
    }
}
