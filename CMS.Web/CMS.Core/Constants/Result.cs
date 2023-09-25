using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Constants
{
    public static class Result
    {
        public static object AddSuccessResult() {
        return new {status  = 1 , msg = "s: تم عملية الاضافة العنصر بنجاح", close=1};
         
        }
        public static object DublicateUsername()
        {
            return new { status = 1, msg = " e: رقم الجوال موجود سبقا", close = 1 };

        }
        public static object DublicateEmail()
        {
            return new { status = 1, msg = " e: رقم البريد موجود سبقا ", close = 1 };

        }

        public static object AddFailureResult() {
            return new { status = 1, msg = "s: تم عملية الاضافة العنصر بنجاح", close = 1 };

        }
        public static object EditSuccessResult()
        {
            return new { status = 1, msg = " s: تم تحديث بيانات العنصر بنجاح ", close = 1 };

            }
        public static object EditStataesSuccessResult()
        {
            return new { status = 1, msg = "s: تم تحديث حالة التقرير بنجاح", close = 1 };

        }
        public static object DeleteSuccessResult()
        {
            return new { status = 1, msg = "s: تم حذف العنصر  بنجاح", close = 1 };

        }
        public static object NoCapacityInClassroom()
        {
            return new { status = 1, msg = "e: لا يوجد سعة داخل هذا الصف", close = 1 };

        }
    }
}
