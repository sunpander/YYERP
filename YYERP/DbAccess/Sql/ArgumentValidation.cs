using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIExtend.ExceptionEX
{
    // 摘要:
    //     参数验证的通用校验辅助类
    public sealed class ArgumentValidation
    {
        // 摘要:
        //     检查variable是否一个有效的enumType枚举类型
        //
        // 参数:
        //   variable:
        //     待检查的值
        //
        //   enumType:
        //     参数variable的枚举类型
        //
        //   variableName:
        //     变量variable的名称
        public static void CheckEnumeration(Type enumType, object variable, string variableName)
        {

        }
        //
        // 摘要:
        //     检查参数variable是否符合指定的类型。
        //
        // 参数:
        //   variable:
        //     待检查的值
        //
        //   type:
        //     参数variable的类型
        public static void CheckExpectedType(object variable, Type type)
        {

        }
        //
        // 摘要:
        //     检查参数variable是否为空字符串。
        //
        // 参数:
        //   variable:
        //     待检查的值
        //
        //   variableName:
        //     参数的名称
        public static void CheckForEmptyString(string variable, string variableName)
        {

        }
        //
        // 摘要:
        //     验证输入的参数messageName非空字符串，也非空引用
        //
        // 参数:
        //   name:
        //     参数名称
        //
        //   messageName:
        //     参数的值
        public static void CheckForInvalidNullNameReference(string name, string messageName)
        {

        }
        //
        // 摘要:
        //     检查参数variable是否为空引用(Null)。
        //
        // 参数:
        //   variable:
        //     待检查的值
        //
        //   variableName:
        //     待检查变量的名称
        public static void CheckForNullReference(object variable, string variableName)
        {
            if (variable == null)
            {
                throw new ArgumentNullException(variableName, "参数不能为空.");
            }
        }
        //
        // 摘要:
        //     验证参数bytes非零长度，如果为零长度，则抛出异常System.ArgumentException。
        //
        // 参数:
        //   bytes:
        //     待检查的字节数组
        //
        //   variableName:
        //     待检查参数的名称
        public static void CheckForZeroBytes(byte[] bytes, string variableName)
        {

        }
    }
}
