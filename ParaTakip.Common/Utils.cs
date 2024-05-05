using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ParaTakip.Common
{
    public static class Utils
    {
        public static string SafeTrim(this string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return string.Empty;
            }
            return word.Trim();
        }

        public static bool IsNullOrWhiteSpace(this string? word)
        {
            return string.IsNullOrWhiteSpace(word);
        }

        public static string ToDateAgo(this DateTime date)
        {
            string response = date.Date.ToString("dd MMMM");
            DateTime now = DateTime.Now;
            var subtractDate = now.Subtract(date);

            if (subtractDate.Days == 0 && subtractDate.Hours == 0 && subtractDate.Minutes == 0 && subtractDate.Seconds > 0)
            {
                response = subtractDate.Seconds + " saniye önce";
            }
            else if (subtractDate.Days == 0 && subtractDate.Hours == 0 && subtractDate.Minutes > 0)
            {
                response = subtractDate.Minutes + " dakika önce";
            }
            else if (subtractDate.Days == 0 && subtractDate.Hours > 0 && subtractDate.Minutes == 0)
            {
                response = subtractDate.Hours + " saat önce";
            }
            else if (subtractDate.Days == 0 && subtractDate.Hours > 0 && subtractDate.Minutes > 0)
            {
                response = subtractDate.Hours + " saat " + subtractDate.Minutes + " dakika önce";
            }
            else if (subtractDate.Days == 1)
            {
                response = "Dün";
            }
            else if (subtractDate.Days == 2)
            {
                response = "Geçen gün";
            }
            else if (subtractDate.Days > 2 && DateTime.Now.Year - date.Year == 0)
            {
                response = date.ToString("dd MMMM");
            }
            else
            {
                response = date.ToString("dd MMMM yyyy");
            }
            return response;
        }

        public static string ToCustomerSurname(this string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                return "KULLANICI";
            }

            return surname.Trim().ToUpper();
        }

        public static string ToCustomerName(this string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "MİSAFİR";
            }

            return name.Trim().ToUpper();
        }

        public static string ToEmailAdress(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return "";
            }

            return email.Trim().Replace(" ", "").ToLower(new CultureInfo("en-US", false));
        }

        public static ObjectId ToObjectId(this string? id)
        {
            if (ObjectId.TryParse(id, out ObjectId result))
            {
                return result;
            }

            return ObjectId.Empty;
        }

        public static string GetOrderStatus(string orderStatus)
        {
            if (orderStatus.IsNullOrWhiteSpace())
            {
                return "";
            }

            if (orderStatus == "pending")
            {
                return "Ödeme Bekleniyor";
            }

            if (orderStatus == "processing")
            {
                return "Hazırlanıyor";
            }

            if (orderStatus == "on-hold")
            {
                return "Beklemede";
            }

            if (orderStatus == "completed")
            {
                return "Tamamlandı";
            }

            if (orderStatus == "cancelled")
            {
                return "İptal edildi";
            }

            if (orderStatus == "refunded")
            {
                return "İade edildi";
            }

            if (orderStatus == "failed ")
            {
                return "Başarısız";
            }

            return orderStatus;
        }


        public static string ShortText(this string text, int maxLenght)
        {
            string newText = "";
            int currentLenght = 0;
            var splittedContent = text.Split(' ');
            foreach (var item in splittedContent)
            {
                if (currentLenght > maxLenght || currentLenght + item.Length > maxLenght)
                {
                    break;
                }

                newText += item + " ";
                currentLenght += item.Length;
            }

            if (newText.Length < text.Length)
            {
                return newText + "...";
            }
            else
            {
                return newText.Trim();
            }
        }

        public static string RemoveSingleQuotes(this string text)
        {
            if (text != null)
            {
                text = text.Replace("\'", string.Empty);
            }

            return text;
        }

        public static string RemoveDoubleQuotes(this string text)
        {
            if (text != null)
            {
                text = text.Replace("\"", string.Empty);
            }

            return text;
        }

        public static string GetEnumDisplayName(this Type enumType, int value)
        {
            var enumvalues = enumType.GetEnumValues();
            var findedEnum = enumvalues.GetValue(value);
            return findedEnum.GetType().GetMember(enumType.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .Name;
        }

        public static string GenerateSHA256Hash(this string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GetStringFromHash(this byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public static Decimal ToDecimalTR(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            return decimal.Parse(Regex.Replace(value, "[^0-9.,]", ""), new CultureInfo("tr-TR"));
        }

        public static Decimal ToDecimal(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            return decimal.Parse(Regex.Replace(value, "[^0-9.,]", ""), new CultureInfo("en-US"));
        }

        public static DateTime? ToShopierDate(this string date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return null ;
            }

            return DateTime.Parse(date);
        }


        public static int ToInt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            return int.Parse(Regex.Replace(value, "[^0-9]", ""));
        }

        public static string ToPhoneNumber(this string customerPhone)
        {
            if (string.IsNullOrWhiteSpace(customerPhone))
            {
                return "";
            }

            if (customerPhone.Length < 10)
            {
                return "";
            }

            string formattedPhone = Regex.Replace(customerPhone, "[^0-9]", "");
            formattedPhone = formattedPhone.Trim();

            if (formattedPhone[0] != '0')
            {
                formattedPhone = "0" + formattedPhone;
            }
            else if(formattedPhone[0] != '9' )
            {
                formattedPhone = formattedPhone.Substring(1);
            }
            else
            {
                return formattedPhone;
            }

            return formattedPhone;

        }

        public static string ToSimpleText(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }

            return value.Trim().ToUpper();
        }

        public static string ToOrderNumber(this string orderNumber)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
            {
                return "-1";
            }

            return Regex.Replace(orderNumber, "[^0-9]", "");
        }


        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            return items.Contains(item);
        }

        public static DateTime ToDateTime(this long timestamp, bool gmt3 = false)
        {
            var date = (new DateTime(1970, 1, 1)).AddMilliseconds(timestamp);

            if (gmt3)
            {
                date = date.AddHours(3);
            }

            return date;
        }

        public static string ToUniDecimal(this string val)
        {
            return val.Replace(".", "*").Replace(",", ".").Replace("*", ",");
        }

        public static string ToDigit(this string val)
        {
            return Regex.Match(val, @"\d+.+\d").Value;

        }

        public static string ToMaskedCardNumber(this string val)
        {
            return val.Substring(0,4) + " " + val.Substring(4,2) + "XX" + " XXXX " + val.Substring(6,4);

        }

    }
}
