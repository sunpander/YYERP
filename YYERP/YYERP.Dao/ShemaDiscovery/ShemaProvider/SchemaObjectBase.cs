namespace SchemaExplorer
{
     
    using System;
    using System.ComponentModel;
    using System.Data;

    [Serializable ]
    public abstract class SchemaObjectBase
    {
        protected DatabaseSchema _database;
        private bool _deepLoad;
        protected string _description = string.Empty;
        protected ExtendedPropertyCollection _extendedProperties;
        protected bool _gotExtendedProperties;
        private bool _includeFunctions;
        protected string _name = string.Empty;

        public virtual void MarkLoaded()
        {
            this._gotExtendedProperties = true;
        }

        public virtual void Refresh()
        {
            this._extendedProperties = null;
            this._gotExtendedProperties = false;
        }

        [Browsable(false)]
        public virtual DatabaseSchema Database
        {
            get
            {
                return this._database;
            }
        }

        public bool DeepLoad
        {
            get
            {
                return this._deepLoad;
            }
            set
            {
                this._deepLoad = value;
            }
        }

        [Description("The description of the schema object. The description can be set if the provider supports it.")]
        public virtual string Description
        {
            get
            {
                if (!this.ExtendedProperties.Contains("CS_Description"))
                {
                    return string.Empty;
                }
                goto Label_0015;
               
            Label_0015:;
                return this.ExtendedProperties["CS_Description"].Value.ToString();
            }
            set
            {
                // This item is obfuscated and can not be translated.
                int num = 2;
            Label_000D:
                switch (num)
                {
                    case 0:
                        if (!(((string) this.ExtendedProperties["CS_Description"].Value) == value))
                        {
                            this.ExtendedProperties["CS_Description"].Value = value;
                            this.ExtendedProperties["CS_Description"].PropertyState = PropertyStateEnum.Dirty;
                            num = 4;
                        }
                        else
                        {
                            num = 3;
                        }
                        goto Label_000D;

                    case 1:
                    case 4:
                        this.Database.Provider.SetExtendedProperties(this);
                        return;

                    case 3:
                        return;

                    case 5:
                        num = 0;
                        goto Label_000D;
                }
                if (!this.ExtendedProperties.Contains("CS_Description"))
                {
                    
                    this.ExtendedProperties.Add(new ExtendedProperty("CS_Description", value, DbType.String, PropertyStateEnum.New));
                    num = 1;
                }
                else
                {
                    num = 5;
                }
                goto Label_000D;
            }
        }

        [Description("Used to store any additional information about the schema object. The meta data can be set if the provider supports it.")]
        public virtual ExtendedPropertyCollection ExtendedProperties
        {
            get
            {
                //ExtendedProperty[] extendedProperties;
               
                //        if (this._extendedProperties != null)
                //        {
                //            extendedProperties = this.Database.Provider.GetExtendedProperties(this.Database.ConnectionString, this);
                            
                //        }
                        
                //        this._extendedProperties[index] = extendedProperties[num];
                       
                //            this._extendedProperties.Add(extendedProperties[num]);
                            
                //        this._extendedProperties = new ExtendedPropertyCollection(this.Database.Provider.GetExtendedProperties(this.Database.ConnectionString, this));
                //        num3 = 0;
                       
                //        if (num < extendedProperties.Length)
                //        {
                //            index = this._extendedProperties.IndexOf(extendedProperties[num].Name);
                //            num3 = 5;
                //        }
                        
                //        this._extendedProperties = new ExtendedPropertyCollection();
                       
                //        this.Database.ValidateProvider();
                        
                //        if (this._extendedProperties != null)
                //        {
                //            break;
                //        }
                       
                //        if (this._gotExtendedProperties)
                //        {
                //            goto Label_022B;
                //        }
                      
                //}
                //this._gotExtendedProperties = true;
               
                return this._extendedProperties;
            }
            set
            {
                this._extendedProperties = value;
            }
        }

        public bool IncludeFunctions
        {
            get
            {
                return this._includeFunctions;
            }
            set
            {
                this._includeFunctions = value;
            }
        }

        [Description("The Name of the schema object.")]
        public virtual string Name
        {
            get
            {
                return this._name;
            }
        }
    }
}

