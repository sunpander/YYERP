namespace SchemaExplorer
{
    using System;
    using System.Data;
    using System.Xml;

    public class SchemaUtility
    {
        public static Type GetSystemType(DbType type)
        {
            DbType type2;
        Label_0017:
            type2 = type;
            int num = 1;
        Label_0002:
            switch (num)
            {
                case 0:
                    num = 2;
                    goto Label_0002;

                case 1:
                    switch (type2)
                    {
                        case DbType.AnsiString:
                            return typeof(string);

                        case DbType.Binary:
                            return typeof(byte[]);

                        case DbType.Byte:
                            return typeof(byte);

                        case DbType.Boolean:
                            return typeof(bool);

                        case DbType.Currency:
                            return typeof(decimal);

                        case DbType.Date:
                            return typeof(DateTime);

                        case DbType.DateTime:
                            return typeof(DateTime);

                        case DbType.Decimal:
                            return typeof(decimal);

                        case DbType.Double:
                            return typeof(double);

                        case DbType.Guid:
                            return typeof(Guid);

                        case DbType.Int16:
                            return typeof(short);

                        case DbType.Int32:
                            return typeof(int);

                        case DbType.Int64:
                            return typeof(long);

                        case DbType.Object:
                            return typeof(object);

                        case DbType.SByte:
                            return typeof(sbyte);

                        case DbType.Single:
                            return typeof(float);

                        case DbType.String:
                            return typeof(string);

                        case DbType.Time:
                            return typeof(TimeSpan);

                        case DbType.UInt16:
                            return typeof(ushort);

                        case DbType.UInt32:
                            return typeof(uint);

                        case DbType.VarNumeric:
                            return typeof(decimal);

                        case DbType.AnsiStringFixedLength:
                            return typeof(string);

                        case DbType.StringFixedLength:
                            return typeof(string);

                        case (DbType.String | DbType.Double):
                            goto Label_01EC;

                        case DbType.Xml:
                            return typeof(XmlDocument);

                        case DbType.DateTime2:
                            return typeof(DateTime);

                        case DbType.DateTimeOffset:
                            return typeof(DateTimeOffset);
                    }
                    num = 0;
                    goto Label_0002;

                case 2:
                    goto Label_01EC;

                default:
                    goto Label_0017;
                   
            }
            //return typeof(ulong);
        Label_01EC:
            return typeof(object);
        }

        public static Type GetSystemType(SqlDbType type)
        {
            // This item is obfuscated and can not be translated.
            SqlDbType type2;
        Label_0017:
            type2 = type;
            int num = 0;
        Label_0002:
            switch (num)
            {
                case 0:
                    switch (type2)
                    {
                        case SqlDbType.BigInt:
                            return typeof(long);

                        case SqlDbType.Binary:
                            return typeof(byte[]);

                        case SqlDbType.Bit:
                            return typeof(bool);

                        case SqlDbType.Char:
                            return typeof(string);

                        case SqlDbType.DateTime:
                            return typeof(DateTime);

                        case SqlDbType.Decimal:
                            return typeof(decimal);

                        case SqlDbType.Float:
                            return typeof(double);

                        case SqlDbType.Image:
                            return typeof(byte[]);

                        case SqlDbType.Int:
                            return typeof(int);

                        case SqlDbType.Money:
                            return typeof(decimal);

                        case SqlDbType.NChar:
                            return typeof(string);

                        case SqlDbType.NText:
                            return typeof(string);

                        case SqlDbType.NVarChar:
                            return typeof(string);

                        case SqlDbType.Real:
                            return typeof(float);

                        case SqlDbType.UniqueIdentifier:
                            return typeof(Guid);

                        case SqlDbType.SmallDateTime:
                            return typeof(DateTime);

                        case SqlDbType.SmallInt:
                            return typeof(short);

                        case SqlDbType.SmallMoney:
                            return typeof(decimal);

                        case SqlDbType.Text:
                            return typeof(string);

                        case SqlDbType.Timestamp:
                            return typeof(DateTime);

                        case SqlDbType.TinyInt:
                            return typeof(byte);

                        case SqlDbType.VarBinary:
                            return typeof(byte[]);

                        case SqlDbType.VarChar:
                             
                            return typeof(string);

                        case SqlDbType.Variant:
                            return typeof(object);

                        case SqlDbType.Xml:
                            return typeof(XmlDocument);

                        case SqlDbType.Date:
                            return typeof(DateTime);

                        case SqlDbType.Time:
                            return typeof(DateTime);

                        case SqlDbType.DateTime2:
                            return typeof(DateTime);

                        case SqlDbType.DateTimeOffset:
                            return typeof(DateTimeOffset);
                    }
                    num = 1;
                    goto Label_0002;

                case 1:
                    num = 2;
                    goto Label_0002;

                case 2:
                    break;

                default:
                    goto Label_0017;
            }
            return typeof(object);
        }

        public static ExtendedPropertyCollection MergeExtendedProperties(ExtendedPropertyCollection target, ExtendedPropertyCollection source)
        {
            int num=0;
            int num2 = 11;
        Label_000D:
            switch (num2)
            {
                case 0:
                    return target;

                case 1:
                case 9:
                    num2 = 3;
                    goto Label_000D;

                case 2:
                    return target;

                case 3:
                    if (num < source.Count)
                    {
                        num2 = 10;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_000D;

                case 4:
                    target = new ExtendedPropertyCollection();
                    num2 = 6;
                    goto Label_000D;

                case 5:
                    goto Label_0130;

                case 6:
                    break;

                case 7:
                    target.Add(new ExtendedProperty(source[num].Name, source[num].Value, source[num].DataType));
                    num2 = 5;
                    goto Label_000D;

                case 8:
                    if (source != null)
                    {
                        num = 0;
                        num2 = 9;
                    }
                    else
                    {
                        num2 = 0;
                    }
                    goto Label_000D;

                case 10:
                    if (target.Contains(source[num].Name))
                    {
                        goto Label_0130;
                    }
                    num2 = 7;
                    goto Label_000D;

                default:
                    if (target == null)
                    {
                        num2 = 4;
                        goto Label_000D;
                    }
                    break;
            }
            num2 = 8;
            goto Label_000D;
        Label_0130:
            num++;
            goto Label_0137;
           
        Label_0137:
            num2 = 1;
            goto Label_000D;
        }
    }
}

