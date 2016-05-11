namespace YYERP.Dao.SqlServerSchemaProvider
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    //[CompilerGenerated, DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    internal class SqlScripts
    {
        //private static CultureInfo resourceCulture;
        //private static System.Resources.ResourceManager resourceMan;
        private static string sqlStr = null;
        internal SqlScripts()
        {
        }

        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //internal static CultureInfo Culture
        //{
        //    get
        //    {
        //        return resourceCulture;
        //    }
        //    set
        //    {
        //        resourceCulture = value;
        //    }
        //}

        internal static string GetAllCommandParameters
        {
            get
            {
                sqlStr = @"SELECT tbl.[name] AS [CommandName],
	                stbl.[name] AS [SchemaName],
	                clmns.[name] AS [ParameterName],
	                CAST(clmns.colid AS int) AS [ParameterID],
	                CAST(clmns.xprec AS tinyint) AS [Precision],
	                usrt.[name] AS [TypeName],
	                ISNULL(baset.[name], N'') AS [BaseTypeName],
	                CAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],
	                CAST(clmns.xscale AS tinyint) AS [Scale],
	                CAST(clmns.isoutparam AS bit) AS [IsOutput],
	                defaults.text AS [DefaultValue]	
                    FROM dbo.sysobjects AS tbl WITH (NOLOCK)
	                INNER JOIN dbo.sysusers AS stbl WITH (NOLOCK) ON stbl.[uid] = tbl.[uid]
	                INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id = tbl.id
	                LEFT JOIN dbo.systypes AS usrt WITH (NOLOCK) ON usrt.xusertype = clmns.xusertype
	                LEFT JOIN dbo.sysusers AS sclmns WITH (NOLOCK) ON sclmns.uid = usrt.uid
	                LEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype
	                LEFT JOIN dbo.syscomments AS defaults WITH (NOLOCK) ON defaults.id = clmns.cdefault
                    WHERE tbl.[type] IN ('P', 'PC', 'FN', 'FS', 'IF', 'TF')
                    ORDER BY tbl.[name], clmns.colorder";
                return sqlStr;
                // return ResourceManager.GetString("GetAllCommandParameters", resourceCulture);
            }
        }

        internal static string GetAllCommandParameters2005
        {
            get
            {
                sqlStr = @"SELECT [t].[name] AS [CommandName], 
	                [sc].[name] AS [SchemaName], 
	                [c].[name] AS [ParameterName], 
	                [c].[parameter_id] AS [ParameterID], 
	                [c].[precision] AS [Precision],
	                [types].[name] AS [TypeName],
	                [basetypes].[name] AS [BaseTypeName],
	                CASE WHEN [c].[max_length] >= 0
		                AND [types].[name] IN (N'nchar', N'nvarchar') THEN [c].[max_length]/2 
		                ELSE [c].[max_length] 
	                END AS [Length],
	                [c].[scale] AS [Scale],
	                [is_output] as [IsOutput],
	                [default_value] as [DefaultValue]\nFROM [sys].[parameters] [c] WITH (NOLOCK)
	                INNER JOIN [sys].[objects] [t] WITH (NOLOCK) ON [c].[object_id] = [t].[object_id]
	                LEFT JOIN [sys].[schemas] [sc] WITH (NOLOCK) ON [t].[schema_id] = [sc].[schema_id]
	                LEFT JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [c].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
	                LEFT JOIN [sys].[types] [types] WITH (NOLOCK) ON [c].[user_type_id] = [types].[user_type_id]
	                LEFT JOIN [sys].[schemas] [st] WITH (NOLOCK) ON [st].[schema_id] = [types].[schema_id]
                    WHERE [t].[type] in ('P', 'RF', 'PC', 'FN', 'FS', 'IF', 'TF')
                    ORDER BY [t].[name], [c].[parameter_id]";
                return sqlStr;
                // return ResourceManager.GetString("GetAllCommandParameters2005", resourceCulture);
            }
        }

        internal static string GetAllTableColumns
        {
            get
            {
                sqlStr = @"SELECT clmns.[name] AS [Name],
	            usrt.[name] AS [DataType],
	            ISNULL(baset.[name], N'') AS [SystemType],
	            CAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],
	            CAST(clmns.xprec AS tinyint) AS [NumericPrecision],
	            CAST(clmns.xscale AS int) AS [NumericScale],
	            CAST(clmns.isnullable AS bit) AS [IsNullable],
	            defaults.text AS [DefaultValue],
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') AS int) AS [Identity],
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsRowGuidCol') AS int) AS IsRowGuid,
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsComputed') AS int) AS IsComputed,
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsDeterministic') AS int) AS IsDeterministic,
	            CAST(CASE COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') WHEN 1 THEN IDENT_SEED(QUOTENAME(stbl.[name]) + '.' + QUOTENAME(tbl.[name])) ELSE 0 END AS NVARCHAR(40)) AS [IdentitySeed],
	            CAST(CASE COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') WHEN 1 THEN IDENT_INCR(QUOTENAME(stbl.[name]) + '.' + QUOTENAME(tbl.[name])) ELSE 0 END AS NVARCHAR(40)) AS [IdentityIncrement],
	            cdef.[text] AS ComputedDefinition,
	            clmns.[collation] AS Collation,
	            CAST(clmns.colid AS int) AS ObjectId,
	            stbl.[name] AS [SchemaName],
	            tbl.[name] AS [TableName]
                FROM dbo.sysobjects AS tbl WITH (NOLOCK)
	            INNER JOIN dbo.sysusers AS stbl WITH (NOLOCK) ON stbl.[uid] = tbl.[uid]
	            INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id=tbl.id
	            LEFT JOIN dbo.systypes AS usrt WITH (NOLOCK) ON usrt.xusertype = clmns.xusertype
	            LEFT JOIN dbo.sysusers AS sclmns WITH (NOLOCK) ON sclmns.uid = usrt.uid
	            LEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype
	            LEFT JOIN dbo.syscomments AS defaults WITH (NOLOCK) ON defaults.id = clmns.cdefault
	            LEFT JOIN dbo.syscomments AS cdef WITH (NOLOCK) ON cdef.id = clmns.id AND cdef.number = clmns.colid
                WHERE (tbl.[type] = 'U')
                ORDER BY tbl.[name], clmns.colorder";
                return sqlStr;
                // return ResourceManager.GetString("GetAllTableColumns", resourceCulture);
            }
        }

        internal static string GetAllTableColumns2005
        {
            get
            {
                sqlStr = @"SELECT clmns.[name] AS [Name],usrt.[name] AS [DataType],
                ISNULL(baset.[name], N'') AS [SystemType],
                CAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') 
                THEN clmns.prec ELSE clmns.length END AS int) AS [Length],	
                CAST(clmns.xprec AS tinyint) AS [NumericPrecision],CAST(clmns.xscale AS int) AS [NumericScale],
                CAST(clmns.isnullable AS bit) AS [IsNullable],defaults.text AS [DefaultValue],
                CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') AS int) AS [Identity],
                CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsRowGuidCol') AS int) AS IsRowGuid,
                CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsComputed') AS int) AS IsComputed,
                CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsDeterministic') AS int) AS IsDeterministic,
                CAST(CASE COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') 
                WHEN 1 THEN IDENT_SEED(QUOTENAME(SCHEMA_NAME(tbl.uid)) + '.' + QUOTENAME(tbl.[name])) ELSE 0 END AS nvarchar(40)) AS [IdentitySeed],
                CAST(CASE COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') WHEN 1 THEN IDENT_INCR(QUOTENAME(SCHEMA_NAME(tbl.uid)) + '.' + QUOTENAME(tbl.[name])) ELSE 0 END AS nvarchar(40)) AS [IdentityIncrement],
                cdef.[text] AS ComputedDefinition,clmns.[collation] AS Collation,
                CAST(clmns.colid AS int) AS ObjectId,SCHEMA_NAME(tbl.uid) AS [SchemaName],
                tbl.[name] AS [TableName] FROM dbo.sysobjects AS tbl WITH (NOLOCK) INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id=tbl.id	
                LEFT JOIN dbo.systypes AS usrt WITH (NOLOCK) ON usrt.xusertype = clmns.xusertype LEFT JOIN dbo.sysusers AS sclmns 
                WITH (NOLOCK) ON sclmns.uid = usrt.uid LEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype 
                LEFT JOIN dbo.syscomments AS defaults WITH (NOLOCK) ON defaults.id = clmns.cdefault 
                LEFT JOIN dbo.syscomments AS cdef WITH (NOLOCK) ON cdef.id = clmns.id AND cdef.number = clmns.colid WHERE (tbl.[type] = 'U') 
                ORDER BY tbl.[name], clmns.colorder";
                return sqlStr;
                // return ResourceManager.GetString("GetAllTableColumns2005", resourceCulture);
            }
        }

        internal static string GetAllViewColumns
        {
            get
            {
                sqlStr = @"SELECT clmns.[name] AS [Name],
	            usrt.[name] AS [DataType],
	            ISNULL(baset.[name], N'') AS [SystemType],
	            CAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],
	            CAST(clmns.xprec AS tinyint) AS [NumericPrecision],
	            CAST(clmns.xscale AS int) AS [NumericScale],
	            CAST(clmns.isnullable AS bit) AS [IsNullable],
	            defaults.text AS [DefaultValue],
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsComputed') AS int) AS IsComputed,
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsDeterministic') AS int) AS IsDeterministic,
	            cdef.[text] AS ComputedDefinition,
	            clmns.[collation] AS Collation,
	            CAST(clmns.colid AS int) AS ObjectId,
	            stbl.[name] AS [SchemaName],
	            tbl.[name] AS [ViewName]
                FROM dbo.sysobjects AS tbl WITH (NOLOCK)
	            INNER JOIN dbo.sysusers AS stbl WITH (NOLOCK) ON stbl.[uid] = tbl.[uid]
	            INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id=tbl.id
	            LEFT JOIN dbo.systypes AS usrt WITH (NOLOCK) ON usrt.xusertype = clmns.xusertype
	            LEFT JOIN dbo.sysusers AS sclmns WITH (NOLOCK) ON sclmns.uid = usrt.uid
	            LEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype
	            LEFT JOIN dbo.syscomments AS defaults WITH (NOLOCK) ON defaults.id = clmns.cdefault
	            LEFT JOIN dbo.syscomments AS cdef WITH (NOLOCK) ON cdef.id = clmns.id AND cdef.number = clmns.colid
                WHERE (tbl.[type] = 'V')
                ORDER BY tbl.[name], clmns.colorder";
                return sqlStr;
                // return ResourceManager.GetString("GetAllViewColumns", resourceCulture);
            }
        }

        internal static string GetAllViewColumns2005
        {
            get
            {
                sqlStr = @"SELECT clmns.[name] AS [Name],usrt.[name] AS [DataType],
	            ISNULL(baset.[name], N'') AS [SystemType],
	            CAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],
	            CAST(clmns.xprec AS tinyint) AS [NumericPrecision],
	            CAST(clmns.xscale AS int) AS [NumericScale],
	            CAST(clmns.isnullable AS bit) AS [IsNullable],
	            defaults.text AS [DefaultValue],
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsComputed') AS int) AS IsComputed,
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsDeterministic') AS int) AS IsDeterministic,
	            cdef.[text] AS ComputedDefinition,
	            clmns.[collation] AS Collation,
	            CAST(clmns.colid AS int) AS ObjectId,
	            SCHEMA_NAME(tbl.uid) AS [SchemaName],
	            tbl.[name] AS [ViewName]
                FROM dbo.sysobjects AS tbl WITH (NOLOCK)
	            INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id=tbl.id
	            LEFT JOIN dbo.systypes AS usrt WITH (NOLOCK) ON usrt.xusertype = clmns.xusertype
	            LEFT JOIN dbo.sysusers AS sclmns WITH (NOLOCK) ON sclmns.uid = usrt.uid
	            LEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype
	            LEFT JOIN dbo.syscomments AS defaults WITH (NOLOCK) ON defaults.id = clmns.cdefault
	            LEFT JOIN dbo.syscomments AS cdef WITH (NOLOCK) ON cdef.id = clmns.id AND cdef.number = clmns.colid
                WHERE (tbl.[type] = 'V')
                ORDER BY tbl.[name], clmns.colorder";
                return sqlStr;
                //  return ResourceManager.GetString("GetAllViewColumns2005", resourceCulture);
            }
        }

        internal static string GetColumnConstraints
        {
            get
            {
                sqlStr = @"SELECT [tbl].[name] AS [TableName],
                  [stbl].[name] AS [SchemaName], 
                  [clmns].[name] AS [ColumnName],
                  OBJECT_NAME([const].[constid]) AS ConstraintName,
                  CASE
                    WHEN [const].[status] & 5 = 5 THEN 'DEFAULT'
                    WHEN [const].[status] & 4 = 4 THEN 'CHECK'
                    ELSE ''
                  END AS ConstraintType,
                  [constdef].[text] AS ConstraintDef
                  FROM
                  dbo.sysobjects AS tbl WITH (NOLOCK)
                  INNER JOIN dbo.sysusers AS stbl WITH (NOLOCK) ON [stbl].[uid] = [tbl].[uid]
                  INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON [clmns].[id] = [tbl].[id]
                  INNER JOIN dbo.sysconstraints const WITH (NOLOCK) ON [clmns].[id] = [const].[id] and [clmns].[colid] = [const].[colid]
                  LEFT OUTER JOIN dbo.syscomments constdef WITH (NOLOCK) ON [const].[constid] = [constdef].[id]
                  WHERE ([const].[status] & 4 = 4 OR [const].[status] & 5 = 5)";
                return sqlStr;
                // return ResourceManager.GetString("GetColumnConstraints", resourceCulture);
            }
        }

        internal static string GetColumnConstraints2005
        {
            get
            {
                sqlStr = @"SELECT [tbl].[name] AS [TableName],
              SCHEMA_NAME([tbl].[uid]) AS [SchemaName], 
              [clmns].[name] AS [ColumnName],
              OBJECT_NAME([const].[constid]) AS ConstraintName,
              CASE
                WHEN [const].[status] & 5 = 5 THEN 'DEFAULT'
                WHEN [const].[status] & 4 = 4 THEN 'CHECK'
                ELSE ''
              END AS ConstraintType,
              [constdef].[text] AS ConstraintDef FROM
              dbo.sysobjects AS tbl WITH (NOLOCK)
              INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON [clmns].[id] = [tbl].[id]
              INNER JOIN dbo.sysconstraints const WITH (NOLOCK) ON [clmns].[id] = [const].[id] and [clmns].[colid] = [const].[colid]
              LEFT OUTER JOIN dbo.syscomments constdef WITH (NOLOCK) ON [const].[constid] = [constdef].[id] WHERE ([const].[status] & 4 = 4 OR [const].[status] & 5 = 5)";
                return sqlStr;
                // return ResourceManager.GetString("GetColumnConstraints2005", resourceCulture);
            }
        }

        internal static string GetCommandParameters
        {
            get
            {
                sqlStr = @"SELECT tbl.[name] AS [CommandName],
	            stbl.[name] AS [SchemaName],
	            clmns.[name] AS [ParameterName],
	            CAST(clmns.colid AS int) AS [ParameterID],
	            CAST(clmns.xprec AS tinyint) AS [Precision],
	            usrt.[name] AS [TypeName],
	            ISNULL(baset.[name], N'') AS [BaseTypeName],
	            CAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],
	            CAST(clmns.xscale AS tinyint) AS [Scale],
	            CAST(clmns.isoutparam AS bit) AS [IsOutput],
	            defaults.text AS [DefaultValue]	
                FROM dbo.sysobjects AS tbl WITH (NOLOCK)
	            INNER JOIN dbo.sysusers AS stbl WITH (NOLOCK) ON stbl.[uid] = tbl.[uid]
	            INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id = tbl.id
	            LEFT JOIN dbo.systypes AS usrt WITH (NOLOCK) ON usrt.xusertype = clmns.xusertype
	            LEFT JOIN dbo.sysusers AS sclmns WITH (NOLOCK) ON sclmns.uid = usrt.uid
	            LEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype
	            LEFT JOIN dbo.syscomments AS defaults WITH (NOLOCK) ON defaults.id = clmns.cdefault
                WHERE tbl.[type] IN ('P', 'PC', 'FN', 'FS', 'IF', 'TF')
	            AND tbl.[name] = @CommandName
	            AND stbl.[name]= @SchemaName
                ORDER BY tbl.[name], clmns.colorder";
                return sqlStr;
                // return ResourceManager.GetString("GetCommandParameters", resourceCulture);
            }
        }

        internal static string GetCommandParameters2005
        {
            get
            {
                sqlStr = @"SELECT [t].[name] AS [CommandName], 
	            [sc].[name] AS [SchemaName], 
	            [c].[name] AS [ParameterName], 
	            [c].[parameter_id] AS [ParameterID], 
	            [c].[precision] AS [Precision],
	            [types].[name] AS [TypeName],
	            [basetypes].[name] AS [BaseTypeName],
	            CASE WHEN [c].[max_length] >= 0
		            AND [types].[name] IN (N'nchar', N'nvarchar') THEN [c].[max_length]/2 
		            ELSE [c].[max_length] 
	            END AS [Length],
	            [c].[scale] AS [Scale],
	            [is_output] as [IsOutput],
	            [default_value] as [DefaultValue]
                FROM [sys].[parameters] [c] WITH (NOLOCK)
	            INNER JOIN [sys].[objects] [t] WITH (NOLOCK) ON [c].[object_id] = [t].[object_id]
	            LEFT JOIN [sys].[schemas] [sc] WITH (NOLOCK) ON [t].[schema_id] = [sc].[schema_id]
	            LEFT JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [c].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
	            LEFT JOIN [sys].[types] [types] WITH (NOLOCK) ON [c].[user_type_id] = [types].[user_type_id]
	            LEFT JOIN [sys].[schemas] [st] WITH (NOLOCK) ON [st].[schema_id] = [types].[schema_id]
                WHERE [t].[type] in ('P', 'RF', 'PC', 'FN', 'FS', 'IF', 'TF')
	            AND [t].[name] = @CommandName	
                AND [sc].[name]= @SchemaName
                ORDER BY [t].[name], [c].[parameter_id]";
                return sqlStr;
                // return ResourceManager.GetString("GetCommandParameters2005", resourceCulture);
            }
        }

        internal static string GetCommands
        {
            get
            {
                sqlStr = @"SELECT
                  object_name(id) AS OBJECT_NAME,
                  user_name(uid) AS USER_NAME,
                  crdate AS DATE_CREATED,
                  id as OBJECT_ID,
                  type as COMMAND_TYPE
                  FROM
                  sysobjects
                  WHERE
                  type IN (
		                N'P', -- SQL Stored Procedure
		                N'PC', --Assembly (CLR) stored-procedure
		                N'FN', --SQL scalar function
		                N'FS', --Assembly (CLR) scalar-function
		                N'IF', --SQL inline table-valued function
		                N'TF' --SQL table-valued-function
	                  )
                   --AND permissions(id) & 32 <> 0 
                   AND ObjectProperty(id, N'IsMSShipped') = 0
                   ORDER BY object_name(id)
                ";
                return sqlStr;
                // return ResourceManager.GetString("GetCommands", resourceCulture);
            }
        }

        internal static string GetCommands2005
        {
            get
            {
                sqlStr = @"SELECT
                  object_name(id) AS OBJECT_NAME,
                  schema_name(uid) AS USER_NAME,
                  crdate AS DATE_CREATED,
                  id as OBJECT_ID,
                  type as COMMAND_TYPE
                  FROM
                  sysobjects
                   WHERE
	                type IN (
		                N'P', -- SQL Stored Procedure
		                N'PC', --Assembly (CLR) stored-procedure
		                N'FN', --SQL scalar function
		                N'FS', --Assembly (CLR) scalar-function
		                N'IF', --SQL inline table-valued function
		                N'TF' --SQL table-valued-function
	                  )
	                  --AND permissions(id) & 32 <> 0 
	                  AND ObjectProperty(id, N'IsMSShipped') = 0
	                  AND NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = id AND name = 'microsoft_database_tools_support' AND value = 1)
                    ORDER BY object_name(id)";
                return sqlStr;
                // return ResourceManager.GetString("GetCommands2005", resourceCulture);
            }
        }

        internal static string GetExtendedData2005
        {
            get
            {
                sqlStr = @"SELECT  [sp].[major_id] AS [ID],         
                [so].[name] AS [ObjectName],        
                SCHEMA_NAME([so].[schema_id]) AS [ObjectOwner],    
                [so].[type] AS [ObjectType],     
                [sp].[minor_id] AS [Minor],     
                [sp].[name] AS [PropertyName],       
                [sp].[value] AS [PropertyValue],      
                SQL_VARIANT_PROPERTY([sp].[value],'BaseType') AS [PropertyBaseType],
                CASE [sp].[class] WHEN 4 THEN USER_NAME([sp].[major_id]) END AS [UserName], 
                CASE [sp].[class]   WHEN 2 THEN [spar].[name]  ELSE [sc].[name]      
                END AS [FieldName],[si].[name] AS [IndexName],[sop].[name] AS [ParentName],   
                SCHEMA_NAME([sop].[schema_id]) AS [ParentOwner],[sop].[type] AS [ParentType],     
                [sp].[class] AS [Type] FROM [sys].[extended_properties] AS [sp] WITH (NOLOCK)	
                LEFT JOIN [sys].[objects] AS [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id]
                LEFT JOIN [sys].[columns] AS [sc] WITH (NOLOCK) ON [sc].[object_id] = [sp].[major_id] AND [sc].[column_id] = [sp].[minor_id]
                LEFT JOIN [sys].[parameters] AS [spar] WITH (NOLOCK) ON [spar].[object_id] = [sp].[major_id] AND [spar].[parameter_id] = [sp].[minor_id]
                LEFT JOIN [sysindexes] [si] WITH (NOLOCK) ON [si].[id] = [sp].[major_id] AND [si].[indid] = [sp].[minor_id]
                LEFT JOIN [sys].[objects] [sop] WITH (NOLOCK) ON [so].[parent_object_id] = [sop].[object_id]";
                return sqlStr;
                // return ResourceManager.GetString("GetExtendedData2005", resourceCulture);
            }
        }

        internal static string GetExtendedProperties
        {
            get
            {
                //p.name AS [PropertyName],
                //p.value AS [PropertyValue],
                sqlStr = @"SELECT 
                SQL_VARIANT_PROPERTY(p.value,'BaseType') AS [PropertyBaseType],
                SQL_VARIANT_PROPERTY(p.value,'MaxLength') AS [PropertyMaxLength],
                SQL_VARIANT_PROPERTY(p.value,'Precision') AS [PropertyPrecision],
                SQL_VARIANT_PROPERTY(p.value,'Scale') AS [PropertyScale]
                FROM fn_listextendedproperty(NULL, @level0type, @level0name, @level1type, @level1name, @level2type, @level2name) p";
                return sqlStr;
                // return ResourceManager.GetString("GetExtendedProperties", resourceCulture);
            }
        }

        internal static string GetExtenedData
        {
            get
            {
                sqlStr = @"SELECT [sp].[id] AS [id], 
	            [so].[name] AS [ObjectName], 
	            [su].[name] AS [ObjectOwner],  
	            [so].[type] AS [ObjectType], 
                CAST([sp].[smallid] AS INT) AS [Minor],
	            [sp].[type] AS [type], 
	            [sp].[name] AS [PropertyName], 
	            [sp].[value] AS [PropertyValue],
                SQL_VARIANT_PROPERTY([sp].[value],'BaseType') AS [PropertyBaseType],
                CASE [sp].[type] WHEN 2 THEN USER_NAME([sp].[smallid]) END AS [UserName],
                CASE [sp].[type] WHEN 1 THEN (SELECT TOP 1 [name] FROM [dbo].[systypes] WHERE [xusertype] = [sp].[smallid]) END AS [UDTName],
                CASE [sp].[type] WHEN 1 THEN (SELECT TOP 1 [sysusers].[name] FROM [dbo].[sysusers] INNER JOIN [systypes] ON [systypes].[uid] = [sysusers].[uid] WHERE [xusertype] = [sp].[smallid]) END AS [UDTOwner],
                [sc].[name] AS [FieldName],
                [si].[name] AS [IndexName],
                [sop].[name] AS [ParentName],
                [sup].[name] AS [ParentOwner],
                [sop].[type] AS [ParentType]
                FROM  [dbo].[sysproperties] [sp] WITH (NOLOCK)
                LEFT JOIN [dbo].[sysobjects] [so] WITH (NOLOCK) ON [so].[id] = [sp].[id]
                LEFT JOIN [dbo].[sysusers] [su] WITH (NOLOCK) ON [su].[uid] = [so].[uid]
                LEFT JOIN [dbo].[syscolumns] [sc] WITH (NOLOCK) ON [sc].[id] = [sp].[id] AND [sc].[colid] = [sp].[smallid]
                LEFT JOIN [dbo].[sysindexes] [si] WITH (NOLOCK) ON [si].[id] = [sp].[id] AND [si].[indid] = [sp].[smallid]
                LEFT JOIN [dbo].[sysobjects] [sop] WITH (NOLOCK) ON [so].[parent_obj] = [sop].[id]
                LEFT JOIN [dbo].[sysusers] [sup] WITH (NOLOCK) ON [sop].[uid] = [sup].[uid]
                    -- eliminate the combination: (column and type 5 (Parameter)
                WHERE    NOT    (NOT    (    (    [sc].[number] = 1
                                OR (    [sc].[number] = 0
                                    AND OBJECTPROPERTY([sc].[id], N'IsScalarFunction') = 1
                                    and ISNULL([sc].[name], '') != ''
                                    )
                                )
                            AND (    [sc].[id] =[so].[id])
                            )
                        AND [sp].[type] = 5
                        )
                  -- eliminate the combination: (param and type 4 (column)
                  AND	NOT	(	(	(	[sc].[number] = 1 
					            OR	(	[sc].[number] = 0 
						            and OBJECTPROPERTY([sc].[id], N'IsScalarFunction')= 1 
						            and ISNULL([sc].[name], '') != ''
						            )
					            ) 
				            AND	(	[sc].[id]=[so].[id])
				            ) 
			            and		[sp].[type] = 4
		            )
                ORDER   BY [sp].[id], [sp].[smallid], [sp].[type], [sp].[name]";
                return sqlStr;
                // return ResourceManager.GetString("GetExtenedData", resourceCulture);
            }
        }

        internal static string GetIndexes
        {
            get
            {
                sqlStr = @"SELECT  [sysindexes].[name] AS [IndexName], 
		        CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsClustered')) AS [IsClustered],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsUnique')) AS [IsUnique],
                CONVERT(bit, CASE WHEN ([sysindexes].[status] & 4096) = 0 THEN 0 ELSE 1 END) AS [IsUniqueConstraint],
                CONVERT(bit, CASE WHEN ([sysindexes].[status] & 2048) = 0 THEN 0 ELSE 1 END) AS [IsPrimary],
                CONVERT(bit, CASE WHEN ([sysindexes].[status] & 0x1000000) = 0 THEN 0 ELSE 1 END) AS [NoRecompute],
                CONVERT(bit, CASE WHEN ([sysindexes].[status] & 0x1) = 0 THEN 0 ELSE 1 END) AS [IgnoreDupKey],
                CONVERT(bit, CASE WHEN ([sysindexes].[status] & 6144) = 0 THEN 0 ELSE 1 END) AS [IsIndex],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsPadIndex')) AS [IsPadIndex],
                CONVERT(bit, OBJECTPROPERTY([sysindexes].[id], N'IsTable')) AS [IsTable],
                CONVERT(bit, OBJECTPROPERTY([sysindexes].[id], N'IsView')) AS [IsView],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsFulltextKey')) AS [IsFullTextKey],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsStatistics')) AS [IsStatistics],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsAutoStatistics')) AS [IsAutoStatistics],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsHypothetical')) AS [IsHypothetical],
                [sysfilegroups].[groupname] AS [FileGroup],
                [sysobjects].[name] AS [ParentName], 
                [sysusers].[name] AS [SchemaName], 
                [sysindexes].[OrigFillFactor] AS [FillFactor], 
                [sysindexes].[status] as [Status], 
                [syscolumns].[name] AS [ColumnName],
                CONVERT(bit, ISNULL(INDEXKEY_PROPERTY([syscolumns].[id], [sysindexkeys].[indid], [keyno], N'IsDescending'), 0)) AS [IsDescending],
                CONVERT(bit, ISNULL(INDEXKEY_PROPERTY([syscolumns].[id], [sysindexkeys].[indid], [keyno], N'IsComputed'), 0)) AS [IsComputed]
                FROM [dbo].[sysindexes] WITH (NOLOCK) 
	            INNER JOIN [dbo].[sysindexkeys] WITH (NOLOCK) ON [sysindexes].[indid] = [sysindexkeys].[indid] AND [sysindexkeys].[id] = [sysindexes].[id]
	            INNER JOIN [dbo].[syscolumns] WITH (NOLOCK) ON [syscolumns].[colid] = [sysindexkeys].[colid] AND [syscolumns].[id] = [sysindexes].[id]
	            INNER JOIN [dbo].[sysobjects] WITH (NOLOCK) ON [sysobjects].[id] = [sysindexes].[id] 
	            LEFT JOIN [dbo].[sysusers] WITH (NOLOCK) ON [sysusers].[uid] = [sysobjects].[uid]
	            LEFT JOIN [dbo].[sysfilegroups] WITH (NOLOCK) ON [sysfilegroups].[groupid] = [sysindexes].[groupid]
                WHERE   (OBJECTPROPERTY([sysindexes].[id], N'IsTable') = 1 OR OBJECTPROPERTY([sysindexes].[id], N'IsView') = 1)
	            AND OBJECTPROPERTY([sysindexes].[id], N'IsSystemTable') = 0 
	            AND INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsAutoStatistics') = 0
	            AND INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsHypothetical') = 0
	            AND [sysindexes].[name] IS NOT NULL
                ORDER  BY [sysindexes].[id], [sysindexes].[name], [sysindexkeys].[keyno]";
                return sqlStr;
                //return ResourceManager.GetString("GetIndexes", resourceCulture);
            }
        }

        internal static string GetIndexes2005
        {
            get
            {
                sqlStr = @"SELECT  [i].[name] AS [IndexName],
                CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) AS [IsClustered],  
                 [i].[is_unique] AS [IsUnique],[i].[is_unique_constraint] AS [IsUniqueConstraint],
                 [i].[is_primary_key] AS [IsPrimary],[no_recompute] AS [NoRecompute], 
                [i].[ignore_dup_key] AS [IgnoreDupKey],CONVERT(bit, 0) AS [IsIndex], 
                [i].[is_padded] AS [IsPadIndex],  CONVERT(bit, CASE WHEN [o].[type] = 'U' THEN 1 ELSE 0 END) AS [IsTable],  CONVERT(bit, CASE WHEN [o].[type] = 'V' THEN 1 ELSE 0 END) AS [IsView],
                CONVERT(bit, INDEXPROPERTY([i].[object_id], [i].[name], N'IsFulltextKey')) AS [IsFullTextKey],
                CONVERT(bit, 0) AS [IsStatistics], CONVERT(bit, 0) AS [IsAutoStatistics],[i].[is_hypothetical] AS [IsHypothetical],[fg].[name] AS [FileGroup], [o].[name] AS [ParentName],
                [os].[name] AS [SchemaName],[i].[fill_factor] AS [FillFactor],
                0 as [Status], [c].[name] AS [ColumnName],
                [ic].[is_descending_key] AS [IsDescending],
                CONVERT(bit, 0) AS [IsComputed] FROM [sys].[indexes] i WITH (NOLOCK) LEFT JOIN [sys].[data_spaces] [fg] WITH (NOLOCK) ON [fg].[data_space_id] = [i].[data_space_id] 
                  LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
                  LEFT JOIN [sys].[schemas] [os] WITH (NOLOCK) ON [os].[schema_id] = [o].[schema_id]
                  LEFT JOIN [sys].[index_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [i].[object_id] AND [ic].[index_id] = [i].[index_id] 
                 AND [ic].[is_included_column] = 0	LEFT JOIN [sys].[columns] [c] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] 
                 AND [c].[column_id] = [ic].[column_id] LEFT JOIN [sys].[stats] [s] WITH (NOLOCK) ON [s].[object_id] = [i].[object_id] 
                 AND [s].[name] = [i].[name] WHERE [i].[type] IN (0, 1, 2, 3) AND [o].[type] IN ('U', 'V', 'TF') ORDER BY [i].[object_id], [i].[name], [ic].[key_ordinal], [ic].[index_column_id]";
                return sqlStr;
                // return ResourceManager.GetString("GetIndexes2005", resourceCulture);
            }
        }

        internal static string GetKeys
        {
            get
            {
                sqlStr = @"SELECT  [fs].[name] AS [ForeignTableName],
                [fsysusers].[name] AS [ForeignTableOwner],      
                [rs].[name] AS [PrimaryTableName],        
                [rsysusers].[name] AS [PrimaryTableOwner],      
                [cs].[name] AS [ConstraintName],      
                [fc].[name] AS [ForeignColumnName],     
                [rc].[name] AS [PrimaryColumnName],      
                CONVERT(bit, OBJECTPROPERTY([constid], N'CnstIsDisabled')) AS [Disabled],  
                CONVERT(bit, OBJECTPROPERTY([constid], N'CnstIsNotRepl')) AS [IsNotForReplication],  
                CONVERT(tinyint, ISNULL(OBJECTPROPERTY([constid], N'CnstIsUpdateCascade'), 0)) AS [UpdateReferentialAction], 
                CONVERT(tinyint, ISNULL(OBJECTPROPERTY([constid], N'CnstIsDeleteCascade'), 0)) AS [DeleteReferentialAction],     
                CONVERT(bit, OBJECTPROPERTY([constid], N'CnstIsNotTrusted')) AS [WithNoCheck]\nFROM [dbo].[sysforeignkeys] WITH (NOLOCK) 
                INNER JOIN [dbo].[sysobjects] [fs] WITH (NOLOCK) ON [sysforeignkeys].[fkeyid] = [fs].[id]	
                INNER JOIN [dbo].[sysobjects] [rs] WITH (NOLOCK) ON [sysforeignkeys].[rkeyid] = [rs].[id]	
                INNER JOIN [dbo].[sysobjects] [cs] WITH (NOLOCK) ON [sysforeignkeys].[constid] = [cs].[id]	
                LEFT JOIN [dbo].[sysusers] [fsysusers] WITH (NOLOCK) ON [fsysusers].[uid] = [fs].[uid] 	
                LEFT JOIN [dbo].[sysusers] [rsysusers] WITH (NOLOCK) ON [rsysusers].[uid] = [rs].[uid]	
                INNER JOIN [dbo].[syscolumns] [fc] WITH (NOLOCK) ON [sysforeignkeys].[fkey] = [fc].[colid] AND [sysforeignkeys].[fkeyid] = [fc].[id]
                INNER JOIN [dbo].[syscolumns] [rc] WITH (NOLOCK) ON [sysforeignkeys].[rkey] = [rc].[colid] AND [sysforeignkeys].[rkeyid] = [rc].[id]
                WHERE [fs].[category] = 0 OR [fs].[category] = 32 --Added to check for replication.\nORDER BY [cs].[name], [sysforeignkeys].[keyno]";
                return sqlStr;
                // return ResourceManager.GetString("GetKeys", resourceCulture);
            }
        }

        internal static string GetKeys2005
        {
            get
            {
                sqlStr = @"SELECT  [fs].[name] AS [ForeignTableName],
             [fschemas].[name] AS [ForeignTableOwner],
             [rs].[name] AS [PrimaryTableName], 
              [rschemas].[name] AS [PrimaryTableOwner],
              [sfk].[name] AS [ConstraintName],[fc].[name] AS [ForeignColumnName],
             [rc].[name] AS [PrimaryColumnName], [sfk].[is_disabled] AS [Disabled],
             [sfk].[is_not_for_replication] AS [IsNotForReplication],[sfk].[update_referential_action] AS [UpdateReferentialAction],
             [sfk].[delete_referential_action] AS [DeleteReferentialAction],[sfk].[is_not_trusted] AS [WithNoCheck]
             FROM  [sys].[foreign_keys] AS [sfk] WITH (NOLOCK) INNER JOIN [sys].[foreign_key_columns] AS [sfkc] WITH (NOLOCK) ON [sfk].[object_id] = [sfkc].[constraint_object_id]
             INNER JOIN [sys].[objects] [fs] WITH (NOLOCK) ON [sfk].[parent_object_id] = [fs].[object_id]
             INNER JOIN [sys].[objects] [rs] WITH (NOLOCK) ON [sfk].[referenced_object_id] = [rs].[object_id]
             LEFT JOIN [sys].[schemas] [fschemas] WITH (NOLOCK) ON [fschemas].[schema_id] = [fs].[schema_id]
             LEFT JOIN [sys].[schemas] [rschemas] WITH (NOLOCK) ON [rschemas].[schema_id] = [rs].[schema_id] 
              INNER JOIN [sys].[columns] [fc] WITH (NOLOCK) ON [sfkc].[parent_column_id] = [fc].[column_id] 
            AND [fc].[object_id] = [sfk].[parent_object_id] INNER JOIN [sys].[columns] [rc] WITH (NOLOCK) ON [sfkc].[referenced_column_id] = [rc].[column_id] 
            AND [rc].[object_id] = [sfk].[referenced_object_id] WHERE [sfk].[is_ms_shipped] = 0 ORDER BY [sfk].[name],[sfkc].[constraint_column_id]";
                return sqlStr;
                // return ResourceManager.GetString("GetKeys2005", resourceCulture);
            }
        }

        internal static string GetTableColumns
        {
            get
            {
                sqlStr = @"SELECT clmns.[name] AS [Name],
	            usrt.[name] AS [DataType],ISNULL(baset.[name], N'') AS [SystemType],
	            CAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],
	            CAST(clmns.xprec AS tinyint) AS [NumericPrecision],
	            CAST(clmns.xscale AS int) AS [NumericScale],
	            CAST(clmns.isnullable AS bit) AS [IsNullable],
	            defaults.text AS [DefaultValue],
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') AS int) AS [Identity],
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsRowGuidCol') AS int) AS IsRowGuid,
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsComputed') AS int) AS IsComputed,
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsDeterministic') AS int) AS IsDeterministic,
	            CAST(CASE COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') WHEN 1 THEN IDENT_SEED(QUOTENAME(stbl.[name]) + '.' + QUOTENAME(tbl.[name])) ELSE 0 END AS NVARCHAR(40)) AS [IdentitySeed],
	            CAST(CASE COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') WHEN 1 THEN IDENT_INCR(QUOTENAME(stbl.[name]) + '.' + QUOTENAME(tbl.[name])) ELSE 0 END AS NVARCHAR(40)) AS [IdentityIncrement],
	            cdef.[text] AS ComputedDefinition,
	            clmns.[collation] AS Collation,
	            CAST(clmns.colid AS int) AS ObjectId,
	            stbl.[name] AS [SchemaName],
	            tbl.[name] AS [TableName]
                FROM dbo.sysobjects AS tbl WITH (NOLOCK)
	            INNER JOIN dbo.sysusers AS stbl WITH (NOLOCK) ON stbl.[uid] = tbl.[uid]
	            INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id=tbl.id
	            LEFT JOIN dbo.systypes AS usrt WITH (NOLOCK) ON usrt.xusertype = clmns.xusertype
	            LEFT JOIN dbo.sysusers AS sclmns WITH (NOLOCK) ON sclmns.uid = usrt.uid
	            LEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype
	            LEFT JOIN dbo.syscomments AS defaults WITH (NOLOCK) ON defaults.id = clmns.cdefault
	            LEFT JOIN dbo.syscomments AS cdef WITH (NOLOCK) ON cdef.id = clmns.id AND cdef.number = clmns.colid
                WHERE (tbl.[type] = 'U') 
	            AND stbl.[name] = @SchemaName
	            AND tbl.[name] = @TableName
                ORDER BY tbl.[name], clmns.colorder";
                return sqlStr;
                //  return ResourceManager.GetString("GetTableColumns", resourceCulture);
            }
        }

        internal static string GetTableColumns2005
        {
            get
            {
                //	            CAST(clmns.isnullable AS bit) AS [IsNullable],

                sqlStr = @"SELECT clmns.[name] AS [Name],
	            usrt.[name] AS [DataType],
	            ISNULL(baset.[name], N'') AS [SystemType],
	            CAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],
	            CAST(clmns.xprec AS tinyint) AS [NumericPrecision],
	            CAST(clmns.xscale AS int) AS [NumericScale],
                clmns.isnullable as [IsNullable],
	            defaults.text AS [DefaultValue],
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') AS int) AS [Identity],
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsRowGuidCol') AS int) AS IsRowGuid,
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsComputed') AS int) AS IsComputed,
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsDeterministic') AS int) AS IsDeterministic,
	            CAST(CASE COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') WHEN 1 THEN IDENT_SEED(QUOTENAME(SCHEMA_NAME(tbl.uid)) + '.' + QUOTENAME(tbl.[name])) ELSE 0 END AS nvarchar(40)) AS [IdentitySeed],
	            CAST(CASE COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') WHEN 1 THEN IDENT_INCR(QUOTENAME(SCHEMA_NAME(tbl.uid)) + '.' + QUOTENAME(tbl.[name])) ELSE 0 END AS nvarchar(40)) AS [IdentityIncrement],
	            cdef.[text] AS ComputedDefinition,
	            clmns.[collation] AS Collation,
	            CAST(clmns.colid AS int) AS ObjectId,
	            SCHEMA_NAME(tbl.uid) AS [SchemaName],
	            tbl.[name] AS [TableName]
              FROM dbo.sysobjects AS tbl WITH (NOLOCK)
	            INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id=tbl.id
	            LEFT JOIN dbo.systypes AS usrt WITH (NOLOCK) ON usrt.xusertype = clmns.xusertype
	            LEFT JOIN dbo.sysusers AS sclmns WITH (NOLOCK) ON sclmns.uid = usrt.uid
	            LEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype
	            LEFT JOIN dbo.syscomments AS defaults WITH (NOLOCK) ON defaults.id = clmns.cdefault
	            LEFT JOIN dbo.syscomments AS cdef WITH (NOLOCK) ON cdef.id = clmns.id AND cdef.number = clmns.colid
             WHERE tbl.[type] = 'U'
            AND SCHEMA_NAME(tbl.uid) = @SchemaName
	            AND tbl.[name] = @TableName
             ORDER BY tbl.[name], clmns.colorder";
                return sqlStr;
                // return ResourceManager.GetString("GetTableColumns2005", resourceCulture);
            }
        }

        internal static string GetTableIndexes
        {
            get
            {
                sqlStr = @"SELECT  [sysindexes].[name] AS [IndexName],
		        CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsClustered')) AS [IsClustered],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsUnique')) AS [IsUnique],
                CONVERT(bit, CASE WHEN ([sysindexes].[status] & 4096) = 0 THEN 0 ELSE 1 END) AS [IsUniqueConstraint],
                CONVERT(bit, CASE WHEN ([sysindexes].[status] & 2048) = 0 THEN 0 ELSE 1 END) AS [IsPrimary],
                CONVERT(bit, CASE WHEN ([sysindexes].[status] & 0x1000000) = 0 THEN 0 ELSE 1 END) AS [NoRecompute],
                CONVERT(bit, CASE WHEN ([sysindexes].[status] & 0x1) = 0 THEN 0 ELSE 1 END) AS [IgnoreDupKey],
                CONVERT(bit, CASE WHEN ([sysindexes].[status] & 6144) = 0 THEN 0 ELSE 1 END) AS [IsIndex],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsPadIndex')) AS [IsPadIndex],
                CONVERT(bit, OBJECTPROPERTY([sysindexes].[id], N'IsTable')) AS [IsTable],
                CONVERT(bit, OBJECTPROPERTY([sysindexes].[id], N'IsView')) AS [IsView],        
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsFulltextKey')) AS [IsFullTextKey],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsStatistics')) AS [IsStatistics],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsAutoStatistics')) AS [IsAutoStatistics],
                CONVERT(bit, INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsHypothetical')) AS [IsHypothetical],
                [sysfilegroups].[groupname] AS [FileGroup],
                [sysobjects].[name] AS [ParentName], 
                [sysusers].[name] AS [SchemaName], 
                [sysindexes].[OrigFillFactor] AS [FillFactor], 
                [sysindexes].[status] as [Status], 
                [syscolumns].[name] AS [ColumnName],
                CONVERT(bit, ISNULL(INDEXKEY_PROPERTY([syscolumns].[id], [sysindexkeys].[indid], [keyno], N'IsDescending'), 0)) AS [IsDescending],
                CONVERT(bit, ISNULL(INDEXKEY_PROPERTY([syscolumns].[id], [sysindexkeys].[indid], [keyno], N'IsComputed'), 0)) AS [IsComputed]
                FROM [dbo].[sysindexes] WITH (NOLOCK) 
	            INNER JOIN [dbo].[sysindexkeys] WITH (NOLOCK) ON [sysindexes].[indid] = [sysindexkeys].[indid] AND [sysindexkeys].[id] = [sysindexes].[id]
	            INNER JOIN [dbo].[syscolumns] WITH (NOLOCK) ON [syscolumns].[colid] = [sysindexkeys].[colid] AND [syscolumns].[id] = [sysindexes].[id]
	            INNER JOIN [dbo].[sysobjects] WITH (NOLOCK) ON [sysobjects].[id] = [sysindexes].[id] 
	            LEFT JOIN [dbo].[sysusers] WITH (NOLOCK) ON [sysusers].[uid] = [sysobjects].[uid]
	            LEFT JOIN [dbo].[sysfilegroups] WITH (NOLOCK) ON [sysfilegroups].[groupid] = [sysindexes].[groupid] 
               WHERE   (OBJECTPROPERTY([sysindexes].[id], N'IsTable') = 1 OR OBJECTPROPERTY([sysindexes].[id], N'IsView') = 1)
	           AND OBJECTPROPERTY([sysindexes].[id], N'IsSystemTable') = 0 
	           AND INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsAutoStatistics') = 0
	           AND INDEXPROPERTY([sysindexes].[id], [sysindexes].[name], N'IsHypothetical') = 0
	           AND [sysindexes].[name] IS NOT NULL	AND [sysobjects].[name] = @tableName
	           AND [sysusers].[name] = @schemaName
               ORDER BY [sysindexes].[id], [sysindexes].[name], [sysindexkeys].[keyno]";
                return sqlStr;
                // return ResourceManager.GetString("GetTableIndexes", resourceCulture);
            }
        }

        internal static string GetTableIndexes2005
        {
            get
            {
                sqlStr = @"SELECT  [i].[name] AS [IndexName],
                CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END AS [IsClustered], 
                CASE WHEN [i].is_primary_key=1 then 1 else 0 end as [IsPrimary],
                CASE WHEN [i].is_unique=1 then 1 else 0 end  AS [IsUnique],
                CASE WHEN [i].ignore_dup_key=1 then 1 else 0 end  AS [IgnoreDupKey],
                CASE WHEN [i].is_hypothetical=1 then 1 else 0 end  AS [IsHypothetical],
                CASE WHEN [i].is_padded=1 then 1 else 0 end  AS [IsPadIndex],
                CASE WHEN [i].is_unique_constraint=1 then 1 else 0 end  AS [IsUniqueConstraint],
                CASE WHEN [ic].is_descending_key=1 then 1 else 0 end  AS [IsDescending],
                [o].[name] AS [ParentName],
                [os].[name] AS [SchemaName],
                [fg].[name] AS [FileGroup],
                [c].[name] AS [ColumnName],
                [i].[fill_factor] AS [FillFactor],
                0 AS [IsIndex],
                CASE WHEN [o].[type] = 'U' THEN 1 ELSE 0 END AS [IsTable],
                CASE WHEN [o].[type] = 'V' THEN 1 ELSE 0 END AS [IsView],
                INDEXPROPERTY([i].[object_id], [i].[name], N'IsFulltextKey') AS [IsFullTextKey],
                0 AS [Status],
                0 AS [IsComputed],
                0 AS [IsStatistics],
                0 AS [IsAutoStatistics] 
                FROM [sys].[indexes] [i] WITH(NOLOCK)
                LEFT JOIN [sys].[data_spaces] [fg] WITH (NOLOCK) ON [fg].[data_space_id] = [i].[data_space_id]
                LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
                LEFT JOIN [sys].[schemas] [os] WITH (NOLOCK) ON [os].[schema_id] = [o].[schema_id]
                LEFT JOIN [sys].[index_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [i].[object_id] AND [ic].[index_id] = [i].[index_id] AND [ic].[is_included_column] = 0
                LEFT JOIN [sys].[columns] [c] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
                LEFT JOIN [sys].[stats] [s] WITH (NOLOCK) ON [s].[object_id] = [i].[object_id] AND [s].[name] = [i].[name]
                WHERE [i].[type] IN (0, 1, 2, 3)
                AND [o].[type] IN ('U', 'V', 'TF')
                AND [o].[name] = @tableName
                AND [os].[name] = @schemaName
                ORDER BY [i].[object_id], [i].[name], [ic].[key_ordinal], [ic].[index_column_id]";
                //                sqlStr = @"SELECT  [i].[name] AS [IndexName],
                //                CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END AS [IsClustered],
                //                [i].[is_unique] AS [IsUnique],
                //                [i].[is_unique_constraint] AS [IsUniqueConstraint],
                //                [i].[is_primary_key] AS [IsPrimary],
                //                [no_recompute] AS [NoRecompute], 
                //                [i].[ignore_dup_key] AS [IgnoreDupKey],
                //                CONVERT(bit, 0) AS [IsIndex],
                //                [i].[is_padded] AS [IsPadIndex],
                //                CONVERT(bit, CASE WHEN [o].[type] = 'U' THEN 1 ELSE 0 END) AS [IsTable],
                //                CONVERT(bit, CASE WHEN [o].[type] = 'V' THEN 1 ELSE 0 END) AS [IsView],
                //                CONVERT(bit, INDEXPROPERTY([i].[object_id], [i].[name], N'IsFulltextKey')) AS [IsFullTextKey],
                //                CONVERT(bit, 0) AS [IsStatistics], 
                //                CONVERT(bit, 0) AS [IsAutoStatistics],
                //                [i].[is_hypothetical] AS [IsHypothetical], 
                //                [fg].[name] AS [FileGroup],
                //                [o].[name] AS [ParentName],
                //                [os].[name] AS [SchemaName],
                //                [i].[fill_factor] AS [FillFactor],
                //                0 as [Status],
                //                [c].[name] AS [ColumnName],
                //                [ic].[is_descending_key] AS [IsDescending],
                //                CONVERT(bit, 0) AS [IsComputed]
                //                FROM [sys].[indexes] i WITH (NOLOCK)
                //                LEFT JOIN [sys].[data_spaces] [fg] WITH (NOLOCK) ON [fg].[data_space_id] = [i].[data_space_id]
                //                LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
                //                LEFT JOIN [sys].[schemas] [os] WITH (NOLOCK) ON [os].[schema_id] = [o].[schema_id]
                //                LEFT JOIN [sys].[index_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [i].[object_id] AND [ic].[index_id] = [i].[index_id] AND [ic].[is_included_column] = 0
                //                LEFT JOIN [sys].[columns] [c] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
                //                LEFT JOIN [sys].[stats] [s] WITH (NOLOCK) ON [s].[object_id] = [i].[object_id] AND [s].[name] = [i].[name]
                //                WHERE [i].[type] IN (0, 1, 2, 3)	AND [o].[type] IN ('U', 'V', 'TF')
                //                AND [o].[name] = @tableName
                //                AND [os].[name] = @schemaName
                //                ORDER BY [i].[object_id], [i].[name], [ic].[key_ordinal], [ic].[index_column_id]";
                return sqlStr;
                // return ResourceManager.GetString("GetTableIndexes2005", resourceCulture);
            }
        }

        internal static string GetTableKeys
        {
            get
            {
                sqlStr = @"SELECT  [fs].[name] AS [ForeignTableName], 
                [fsysusers].[name] AS [ForeignTableOwner], 
                [rs].[name] AS [PrimaryTableName],
                [rsysusers].[name] AS [PrimaryTableOwner], 
                [cs].[name] AS [ConstraintName], 
                [fc].[name] AS [ForeignColumnName],
                [rc].[name] AS [PrimaryColumnName],
                CONVERT(bit, OBJECTPROPERTY([constid], N'CnstIsDisabled')) AS [Disabled],
                CONVERT(bit, OBJECTPROPERTY([constid], N'CnstIsNotRepl')) AS [IsNotForReplication],
                CONVERT(tinyint, ISNULL(OBJECTPROPERTY([constid], N'CnstIsUpdateCascade'), 0)) AS [UpdateReferentialAction],
                CONVERT(tinyint, ISNULL(OBJECTPROPERTY([constid], N'CnstIsDeleteCascade'), 0)) AS [DeleteReferentialAction],
                CONVERT(bit, OBJECTPROPERTY([constid], N'CnstIsNotTrusted')) AS [WithNoCheck]
                FROM [dbo].[sysforeignkeys] WITH (NOLOCK) 
	            INNER JOIN [dbo].[sysobjects] [fs] WITH (NOLOCK) ON [sysforeignkeys].[fkeyid] = [fs].[id]
	            INNER JOIN [dbo].[sysobjects] [rs] WITH (NOLOCK) ON [sysforeignkeys].[rkeyid] = [rs].[id]
	            INNER JOIN [dbo].[sysobjects] [cs] WITH (NOLOCK) ON [sysforeignkeys].[constid] = [cs].[id]
	            LEFT JOIN [dbo].[sysusers] [fsysusers] WITH (NOLOCK) ON [fsysusers].[uid] = [fs].[uid] 
	            LEFT JOIN [dbo].[sysusers] [rsysusers] WITH (NOLOCK) ON [rsysusers].[uid] = [rs].[uid]
	            INNER JOIN [dbo].[syscolumns] [fc] WITH (NOLOCK) ON [sysforeignkeys].[fkey] = [fc].[colid] AND [sysforeignkeys].[fkeyid] = [fc].[id]
	            INNER JOIN [dbo].[syscolumns] [rc] WITH (NOLOCK) ON [sysforeignkeys].[rkey] = [rc].[colid] AND [sysforeignkeys].[rkeyid] = [rc].[id]
                WHERE ([fs].[name] = @tableName AND [fsysusers].[name] = @schemaName)
	            OR ([rs].[name] = @tableName AND [rsysusers].[name] = @schemaName)
                ORDER BY [cs].[name], [sysforeignkeys].[keyno]";
                return sqlStr;
                // return ResourceManager.GetString("GetTableKeys", resourceCulture);
            }
        }

        internal static string GetTableKeys2005
        {
            get
            {
                sqlStr = @"SELECT  [fs].[name] AS [ForeignTableName],
                [fschemas].[name] AS [ForeignTableOwner],
                CASE WHEN [sfk].is_not_trusted=1 THEN 1 ELSE 0 END AS [WithNoCheck],
                [rs].[name] AS [PrimaryTableName],
                [rschemas].[name] AS [PrimaryTableOwner],
                [sfk].[name] AS [ConstraintName],
                [fc].[name] AS [ForeignColumnName],
                [rc].[name] AS [PrimaryColumnName],
                CASE WHEN [sfk].[is_disabled]=1 THEN 1 ELSE 0 END AS [Disabled],
                CASE WHEN [sfk].[is_not_for_replication]=1 THEN 1 ELSE 0 END AS [IsNotForReplication],
                [sfk].[update_referential_action] AS [UpdateReferentialAction],
                [sfk].[delete_referential_action] AS [DeleteReferentialAction]
                FROM    [sys].[foreign_keys] AS [sfk] WITH (NOLOCK)
	            INNER JOIN [sys].[foreign_key_columns] AS [sfkc] WITH (NOLOCK) ON [sfk].[object_id] = [sfkc].[constraint_object_id]
	            INNER JOIN [sys].[objects] [fs] WITH (NOLOCK) ON [sfk].[parent_object_id] = [fs].[object_id]
	            INNER JOIN [sys].[objects] [rs] WITH (NOLOCK) ON [sfk].[referenced_object_id] = [rs].[object_id] 
	            LEFT JOIN [sys].[schemas] [fschemas] WITH (NOLOCK) ON [fschemas].[schema_id] = [fs].[schema_id]
	            LEFT JOIN [sys].[schemas] [rschemas] WITH (NOLOCK) ON [rschemas].[schema_id] = [rs].[schema_id]
	            INNER JOIN [sys].[columns] [fc] WITH (NOLOCK) ON [sfkc].[parent_column_id] = [fc].[column_id] AND [fc].[object_id] = [sfk].[parent_object_id]
	            INNER JOIN [sys].[columns] [rc] WITH (NOLOCK) ON [sfkc].[referenced_column_id] = [rc].[column_id] AND [rc].[object_id] = [sfk].[referenced_object_id]
                WHERE ([fs].[name] = @tableName AND [fschemas].[name] = @schemaName)
	            OR ([rs].[name] = @tableName AND [rschemas].[name] = @schemaName)	
                ORDER BY [sfk].[name],[sfkc].[constraint_column_id]";
                return sqlStr;
                //return ResourceManager.GetString("GetTableKeys2005", resourceCulture);
            }
        }

        internal static string GetTables
        {
            get
            {
                sqlStr = @"SELECT
                  object_name(id)	AS [OBJECT_NAME],
                  user_name(uid)	AS [USER_NAME],
                  type				AS TYPE,
                  crdate			AS DATE_CREATED,
                  ''				AS FILE_GROUP,
                  id				as [OBJECT_ID]
                  FROM
                  sysobjects
                  WHERE
                  type = N'U'
                  AND permissions(id) & 4096 <> 0
                  AND ObjectProperty(id, N'IsMSShipped') = 0
                  ORDER BY user_name(uid), object_name(id)";
                return sqlStr;
                // return ResourceManager.GetString("GetTables", resourceCulture);
            }
        }

        //        internal static string GetTables2000
        //        {
        //            get
        //            {
        //                sqlStr = @"SELECT
        //                  object_name(so.id) AS [OBJECT_NAME],
        //                  user_name(so.uid)  AS [USER_NAME],
        //                  so.type            AS TYPE,
        //                  so.crdate          AS DATE_CREATED,
        //                  fg.file_group      AS FILE_GROUP,
        //                  so.id              AS [OBJECT_ID]
        //                  FROM 
        //                  dbo.sysobjects so
        //                  LEFT JOIN (
        //                    SELECT 
        //                        s.groupname AS file_group,
        //                        i.id        AS id
        //                    FROM dbo.sysfilegroups s
        //                    INNER JOIN dbo.sysindexes i
        //                        ON i.groupid = s.groupid 
        //                    WHERE i.indid < 2                           
        //                  ) AS fg
        //                  ON so.id = fg.id
        //                  WHERE
        //                  so.type = N'U'
        //                  AND permissions(so.id) & 4096 <> 0
        //                  AND ObjectProperty(so.id, N'IsMSShipped') = 0
        //                  ORDER BY user_name(so.uid), object_name(so.id)";
        //                return sqlStr;
        //                //return ResourceManager.GetString("GetTables2000", resourceCulture);
        //            }
        //        }

        internal static string GetTables2005
        {
            get
            {
                sqlStr = @"SELECT object_name(so.id) AS [OBJECT_NAME],schema_name(so.uid) AS [USER_NAME],
                so.type AS TYPE,so.crdate AS DATE_CREATED,fg.file_group AS FILE_GROUP,
                so.id as [OBJECT_ID] FROM dbo.sysobjects so 
                LEFT JOIN (SELECT s.groupname AS file_group,i.id AS id FROM dbo.sysfilegroups s	
                INNER JOIN dbo.sysindexes i	ON i.groupid = s.groupid WHERE i.indid < 2) AS fg ON so.id = fg.id 
                WHERE so.type = N'U'	AND permissions(so.id) & 4096 <> 0 AND ObjectProperty(so.id, N'IsMSShipped') = 0 
                AND NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = so.id AND name = 'microsoft_database_tools_support' AND value = 1) 
                ORDER BY schema_name(so.uid), object_name(so.id)";
                return sqlStr;
                // return ResourceManager.GetString("GetTables2005", resourceCulture);
            }
        }

        internal static string GetViewColumns
        {
            get
            {
                sqlStr = @"SELECT
	            clmns.[name] AS [Name],
	            usrt.[name] AS [DataType],
	            ISNULL(baset.[name], N'') AS [SystemType],
	            CAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],
	            CAST(clmns.xprec AS tinyint) AS [NumericPrecision],
	            CAST(clmns.xscale AS int) AS [NumericScale],
	            CAST(clmns.isnullable AS bit) AS [IsNullable],
	            defaults.text AS [DefaultValue],
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsComputed') AS int) AS IsComputed,
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsDeterministic') AS int) AS IsDeterministic,
	            cdef.[text] AS ComputedDefinition,
	            clmns.[collation] AS Collation,
	            CAST(clmns.colid AS int) AS ObjectId,
	            stbl.[name] AS [SchemaName],
	            tbl.[name] AS [ViewName]
                FROM dbo.sysobjects AS tbl WITH (NOLOCK)
	            INNER JOIN dbo.sysusers AS stbl WITH (NOLOCK) ON stbl.[uid] = tbl.[uid]
	            INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id=tbl.id
	            LEFT JOIN dbo.systypes AS usrt WITH (NOLOCK) ON usrt.xusertype = clmns.xusertype
	            LEFT JOIN dbo.sysusers AS sclmns WITH (NOLOCK) ON sclmns.uid = usrt.uid
	            LEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype
	            LEFT JOIN dbo.syscomments AS defaults WITH (NOLOCK) ON defaults.id = clmns.cdefault
	            LEFT JOIN dbo.syscomments AS cdef WITH (NOLOCK) ON cdef.id = clmns.id AND cdef.number = clmns.colid
                WHERE (tbl.[type] = 'V')
	            AND stbl.[name] = @SchemaName
	            AND tbl.[name] = @ViewName
                ORDER BY tbl.[name], clmns.colorder";
                return sqlStr;
                //return ResourceManager.GetString("GetViewColumns", resourceCulture);
            }
        }

        internal static string GetViewColumns2005
        {
            get
            {
                sqlStr = @"SELECT clmns.[name] AS [Name],usrt.[name] AS [DataType],ISNULL(baset.[name], N'') AS [SystemType],
	            CAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],
	            CAST(clmns.xprec AS tinyint) AS [NumericPrecision], CAST(clmns.xscale AS int) AS [NumericScale],
	            clmns.isnullable AS [IsNullable],defaults.text AS [DefaultValue],
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsComputed') AS int) AS IsComputed,
	            CAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsDeterministic') AS int) AS IsDeterministic,
	            cdef.[text] AS ComputedDefinition,clmns.[collation] AS Collation,
	            CAST(clmns.colid AS int) AS ObjectId,SCHEMA_NAME(tbl.uid) AS [SchemaName],tbl.[name] AS [ViewName]
                FROM dbo.sysobjects AS tbl WITH (NOLOCK) INNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id=tbl.id
	            LEFT JOIN dbo.systypes AS usrt WITH (NOLOCK) ON usrt.xusertype = clmns.xusertype
	            LEFT JOIN dbo.sysusers AS sclmns WITH (NOLOCK) ON sclmns.uid = usrt.uid
	            LEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype
	            LEFT JOIN dbo.syscomments AS defaults WITH (NOLOCK) ON defaults.id = clmns.cdefault
	            LEFT JOIN dbo.syscomments AS cdef WITH (NOLOCK) ON cdef.id = clmns.id AND cdef.number = clmns.colid
                WHERE (tbl.[type] = 'V') AND SCHEMA_NAME(tbl.uid) =@SchemaName AND tbl.[name] =@ViewName ORDER BY tbl.[name], clmns.colorder";

                return sqlStr;
                // return ResourceManager.GetString("GetViewColumns2005", resourceCulture);
            }
        }

        internal static string GetViews
        {
            get
            {
                sqlStr = @"SELECT
                  object_name(id) AS OBJECT_NAME,
                  user_name(uid) AS USER_NAME,
                  type AS TYPE,
                  crdate AS DATE_CREATED,
                  id as OBJECT_ID
                  FROM sysobjects
                  WHERE type = N'V'
                  AND permissions(id) & 4096 <> 0
                  AND ObjectProperty(id, N'IsMSShipped') = 0
                  ORDER BY object_name(id)";
                return sqlStr;
                //return ResourceManager.GetString("GetViews", resourceCulture);
            }
        }

        internal static string GetViews2005
        {
            get
            {
                sqlStr = @"SELECT object_name(id) AS OBJECT_NAME,
                            schema_name(uid) AS USER_NAME,
                            type AS TYPE,crdate AS DATE_CREATED,id as OBJECT_ID
                          FROM sysobjects WHERE type = N'V' AND permissions(id) & 4096 <> 0 AND ObjectProperty(id, N'IsMSShipped') = 0
                          AND NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = id AND name = 'microsoft_database_tools_support' AND value = 1)
                           ORDER BY object_name(id)";
                return sqlStr;
                //  return ResourceManager.GetString("GetViews2005", resourceCulture);
            }
        }

        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //internal static System.Resources.ResourceManager ResourceManager
        //{
        //    get
        //    {
        //        if (object.ReferenceEquals(resourceMan, null))
        //        {
        //            System.Resources.ResourceManager manager = new System.Resources.ResourceManager("SchemaExplorer.SqlScripts", typeof(SqlScripts).Assembly);
        //            resourceMan = manager;
        //        }
        //        return resourceMan;
        //    }
        //}
    }
}

