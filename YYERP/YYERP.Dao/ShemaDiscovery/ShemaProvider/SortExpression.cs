namespace SchemaExplorer
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SortExpression
    {
        private Hashtable a;
        public string PropertyName;
        public ListSortDirection SortDirection;
        public System.ComponentModel.PropertyDescriptor PropertyDescriptor;
        public SortExpression(string propertyName)
        {
            this.PropertyName = propertyName;
            this.SortDirection = ListSortDirection.Ascending;
            this.PropertyDescriptor = null;
            this.a = new Hashtable();
        }

        public SortExpression(string propertyName, ListSortDirection sortDirection)
        {
            this.PropertyName = propertyName;
            this.SortDirection = sortDirection;
            this.PropertyDescriptor = null;
            this.a = new Hashtable();
        }

        public object GetPropertyValue(object component)
        {
            int num = 4;
        Label_000D:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    if (!this.a.Contains(component))
                    {
                        object obj2 = this.PropertyDescriptor.GetValue(component);
                        this.a.Add(component, obj2);
                        return obj2;
                    }
                    num = 3;
                    goto Label_000D;

                case 2:
                    goto Label_008A;

                case 3:
                    return this.a[component];

                case 5:
                    this.a = new Hashtable(100);
                    num = 0;
                    goto Label_000D;

                case 6:
                    if (this.a != null)
                    {
                        break;
                    }
                    num = 5;
                    goto Label_000D;

                default:
                    if (this.PropertyDescriptor != null)
                    {
                        num = 6;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;
                   
                   
            }
            num = 1;
            goto Label_000D;
        Label_008A:
            return null;
        }
    }
}

