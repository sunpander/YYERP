namespace SchemaExplorer
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ExtendedProperty : INotifyPropertyChanged
    {
        private DbType _dataType;
        private string _name;
        private PropertyStateEnum _propertyState;
        private object _value;

        public event PropertyChangedEventHandler PropertyChanged;

        public ExtendedProperty()
        {
            this._propertyState = PropertyStateEnum.New;
        }

        public ExtendedProperty(string name, object value, DbType dataType)
        {
            this._name = name;
            this._value = value;
            this._dataType = dataType;
            this._propertyState = PropertyStateEnum.Unmodified;
        }

        public ExtendedProperty(string name, object value, DbType dataType, PropertyStateEnum state)
        {
            this._name = name;
            this._value = value;
            this._dataType = dataType;
            this._propertyState = state;
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            // This item is obfuscated and can not be translated.
            int num = 5;
        Label_000D:
            switch (num)
            {
                case 0:
                    this.PropertyChanged(this, e);
                    num = 2;
                    goto Label_000D;

                case 1:
                    if (this.PropertyChanged == null)
                    {
                        return;
                    }
                    num = 0;
                    goto Label_000D;

                case 2:
                    return;

                case 3:
                    break;

                case 4:
                     
                    this._propertyState = PropertyStateEnum.Dirty;
                    num = 3;
                    goto Label_000D;

                default:
                    if (this._propertyState == PropertyStateEnum.Unmodified)
                    {
                        num = 4;
                        goto Label_000D;
                    }
                    break;
            }
            num = 1;
            goto Label_000D;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public static ExtendedProperty Readonly(string name, bool value)
        {
            return new ExtendedProperty(name, value, DbType.Boolean, PropertyStateEnum.ReadOnly);
        }

        public static ExtendedProperty Readonly(string name, int value)
        {
            return new ExtendedProperty(name, value, DbType.Int32, PropertyStateEnum.ReadOnly);
        }

        public static ExtendedProperty Readonly(string name, long value)
        {
            return new ExtendedProperty(name, value, DbType.Int64, PropertyStateEnum.ReadOnly);
        }

        public static ExtendedProperty Readonly(string name, string value)
        {
            if (value != null)
            {
            }
            return new ExtendedProperty(name, string.Empty, DbType.AnsiString, PropertyStateEnum.ReadOnly);
        }

        public DbType DataType
        {
            get
            {
                return this._dataType;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        [Bindable(false)]
        public PropertyStateEnum PropertyState
        {
            get
            {
                return this._propertyState;
            }
            set
            {
                this._propertyState = value;
            }
        }

        public object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                int num = 1;
            Label_000D:
                switch (num)
                {
                    case 0:
                        this._value = value;
                        this.OnPropertyChanged("Value");
                        num = 2;
                        goto Label_000D;

                    case 1:
                        break;

                    case 2:
                        return;

                    default:
                        break;
                      
                }
                if (value == this._value)
                {
                    return;
                }
                num = 0;
                goto Label_000D;
            }
        }
    }
}

