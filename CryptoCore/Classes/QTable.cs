using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Classes
{
    public class QTable
    {

        public string Name;
        public List<QTableColumn> Columns;

        public QTable(string name)
        {
            Name = name;
            Columns = new List<QTableColumn>();
        }

        public string GenerateCreateSQL()
        {
            string s;
            var nl = Environment.NewLine;

            var uniques = new List<string>();

            s = "CREATE TABLE " + Name + " (";
            foreach (var column in Columns)
            {

                var temp = "\"" + column.Name + "\" " + column.Type;


                if (column.IsPrimary)
                {
                    temp += " PRIMARY KEY";
                }

                if (column.IsAutoincrement)
                {
                    temp += " AUTOINCREMENT";
                }

                

                if (column.CanBeNull)
                {
                    temp += " NULL";
                }
                else
                {
                    temp += " NOT NULL";
                }

                if (column.IsUnique)
                {
                    uniques.Add(column.Name);
                }

                s += nl + temp + ",";
            }



            s = s.Substring(0, s.Length - 1); // remove the last comma

            if (uniques.Count > 0)
            {
                s += nl + ",UNIQUE(";

                foreach (var item in uniques)
                {
                    s += "\"" + item + "\"," ;
                }

                s = s.Substring(0, s.Length - 1);
                s += ")";
            }

            s += nl + ");";



            return s;
        }


    }

    public class QTableColumn
    {
        public string Name;
        public string Type;
        public bool CanBeNull;
        public bool IsPrimary;
        public bool IsAutoincrement;
        public bool IsUnique;

        public QTableColumn(string name, string type, bool canBeNull, bool isPrimary, bool isIdentity, bool isUnique= false)
        {
            Name = name;
            Type = type;
            CanBeNull = canBeNull;
            IsPrimary = isPrimary;
            IsAutoincrement = isIdentity;
            IsUnique = isUnique;
        }
    }
}
