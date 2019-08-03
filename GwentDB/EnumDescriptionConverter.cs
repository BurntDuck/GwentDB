using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GwentDB
{
    public class EnumDescriptionConverter : IValueConverter
    {
        /// <summary>
        /// Get the value of the description attribute of an enum.
        /// </summary>
        /// <param name="enumObject">The enum to be converted.</param>
        /// <returns></returns>
        private string GetEnumDescription(Enum enumObject)
        {
            if(enumObject == null)
            {
                return string.Empty;
            }
            FieldInfo fieldInfo = enumObject.GetType().GetField(enumObject.ToString());

            object[] attributeArray = fieldInfo.GetCustomAttributes(false);

            if(attributeArray.Length == 0)
            {
                return enumObject.ToString();
            }
            else
            {
                DescriptionAttribute attribute = attributeArray[0] as DescriptionAttribute;
                return attribute.Description;
            }
        }

        /// <summary>
        /// Convert the value of an object.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <param name="targetType">The targeted type of the conversion.</param>
        /// <param name="parameter">Parameter for the conversion.</param>
        /// <param name="culture">Culture info for the conversion.</param>
        /// <returns></returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Enum myEnum = (Enum)value;
            if(myEnum == null)
            {
                return null;
            }
            string description = GetEnumDescription(myEnum);
            return !string.IsNullOrEmpty(description) ? description : myEnum.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => string.Empty;
    }
}
