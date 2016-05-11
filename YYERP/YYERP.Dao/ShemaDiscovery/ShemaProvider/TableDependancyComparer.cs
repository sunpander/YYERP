namespace SchemaExplorer
{
    using System;
    using System.Collections;

    public class TableDependancyComparer : IComparer
    {
        private SchemaExplorer.TableSortOrder tableSortOrder;

        public TableDependancyComparer()
        {
            this.tableSortOrder = SchemaExplorer.TableSortOrder.DependantsLast;
        }

        public TableDependancyComparer(SchemaExplorer.TableSortOrder tableSortOrder)
        {
            this.tableSortOrder = SchemaExplorer.TableSortOrder.DependantsLast;
            this.tableSortOrder = tableSortOrder;
        }

        public int Compare(object x, object y)
        {
            TableSchema schema = x as TableSchema;
            TableSchema schema2 = y as TableSchema;
            if (schema2 == null)
            {
                throw new ArgumentException("Objects must be TableSchemas.");
            }

            if (schema == null)
            {
                throw new ArgumentException("Objects must be TableSchemas.");
            }


           
                    //if (this.tableSortOrder != SchemaExplorer.TableSortOrder.DependantsLast)
                    //{
                       
                    //}
                   
                    //if (schema.IsDependantOf(schema2))
                    //{
                    //    return 1;
                    //}
                   
                   
                  
                    //if (schema != schema2)
                    //{
                       
                    //}
                   
                   
                    //if (schema.IsDependantOf(schema2))
                    //{
                    //    return -1;
                    //}
            return 20;
        }

        public SchemaExplorer.TableSortOrder TableSortOrder
        {
            get
            {
                return this.tableSortOrder;
            }
        }
    }
}

