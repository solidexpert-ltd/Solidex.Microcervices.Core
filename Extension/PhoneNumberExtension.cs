using System;
using PhoneNumbers;

namespace Microcervices.Core.Extension
{
    public static class PhoneNumberExtension
    {
        public static string GetPhoneNumber(this string number)
        {
            if (!number.StartsWith("+"))
            {
                number = $"+{number}";
            }

            try
            {
                return PhoneNumberUtil.GetInstance()
                    .Format(PhoneNumberUtil.GetInstance().Parse(number, ""), PhoneNumberFormat.E164);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}