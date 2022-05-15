using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = "<OrganisationId>,FirstName,LastName@@09740322,Janet,Smith@@09740322,Frank,Bloswick@@09740322,Tonya,Bazinaw@@09740322,Mike,St. Onge@@09740322,Jackie,Jones@@09740322,Darren,Tillbrooke@@09740322,Stephanie,Holsinger@@09740322,Rene,Hughey@@09740322,Robert,Rogers@@09740322,Richard,LaPine@@09740322,Kathy,Summerfield@@09740322,Kathy,Bodwin@@00002065,Mitch,Krause@@00002065,George,Dow@@00002065,Jack,Malone@@00002065,Bill,Schweiz@@00002065,Mark,Gunter@@00002065,Bob,Anderson@@00002065,Scott,Simpson@@00002065,Phil,Dingman@@00002065,Chad,Leiker@@00002065,Ian,Benson@@00002065,Nicole,Lane@@00002065,Steve,Lundeen@@00002065,Erica,Black@@00002065,Xenos,Mathis@@00002065,Kyle,Good@@00002065,Raja,Dejesus@@00002065,Timothy,Frazier@@00002065,Francine,Morrison@@SC095237,Avram,Pate@@SC095237,Hammett,Coffey@@SC095237,Hasad,Wise@@SC095237,Cullen,Riddle@@SC095237,Kato,Delgado@@SC095237,Todd,Wright@@SC095237,Troy,Mccoy@@SC095237,Gil,Duncan@@SC095237,Lionel,Espinoza@@SC095237,Victor,Merrill@@SC001111,Gennifer,Vance@@SC001111,Chancellor,Warner@@SC001111,Davis,Wolf@@00966425,Carlos,Clarke@@00966425,Dolan,Mercado@@00966425,Helen,Guthrie@@00966425,Elmo,Douglas@@00966425,Kane,Rice@@00966425,Colt,Rowland@@00966425,John,Rose@@00966425,Alfonso,Hopkins@@00966425,Ida,Watts@@00966425,Jennifer,Coleman@@00966425,Ciaran,Newton@@00966425,Hiram,Carrillo@@00966425,Devin,Russell@@00966425,Arsenio,Jensen@@00966425,Otto,Gibbs@@00966425,Hiram,Vega@@SC327000,Jarrod,Randolph@@SC327000,Josiah,Gates@@SC327000,Brandon,Stanley@@SC327000,Kennedy,Nunez@@SC327000,Lewis,Sanchez@@SC327000,Kassie,Chaney@@SC327000,Lance,Knox@@SC327000,Lamar,Harrison@@SC327000,Honorato,Montgomery@@00014259,Lisa,Nielsen@@00014259,Layla,Barr@@00014259,Nancy,Mcclain@@00014259,Kato,Delgado@@00014259,Todd,Wright@@00014259,Troy,Mccoy@@00014259,Gil,Duncan@@00014259,Lionel,Espinoza@@";
            var sqlStatement = "INSERT INTO [dbo].[Employees] (";
            var rows = data.Split(new string[] { "@@" }, StringSplitOptions.None);
            List<string> columns = new List<string>();
            columns.AddRange(rows[0].Split(',').ToList());
            var forginTableName = "OrganisationDetails";
            var forginTableColumnName = "OrganisationNumber";
            List<int> forginKeys = new List<int>();
            for(int i =0; i < columns.Count; i++)
            {
                if (columns[i].ToString().StartsWith("<"))
                {
                    var strippedColumnName = columns[i].ToString().Replace("<", "").Replace(">", "").ToString();
                    columns[i] = strippedColumnName;
                    forginKeys.Add(i);
                }
                if (i != (columns.Count - 1))
                    sqlStatement += columns[i] + ",";
                else
                    sqlStatement += columns[i] + ")";
            }
            string[] sqlInsertStatamentsComplete = new string[1000];
            for (int i = 1; i < rows.Length; i++)
            {
                var mainPartSql = sqlStatement + " VALUES (";

                var cells = rows[i].Split(',');
                for (int x = 0; x < cells.Length; x++)
                {
                    if (forginKeys.Contains(x))
                    {
                        if(x == (cells.Length - 1))
                        {
                            mainPartSql += "(SELECT id from " + forginTableName + " WHERE " + forginTableColumnName + " = " + "'" + cells.ElementAt(x) + "')";
                        }
                        else
                        {
                            mainPartSql += "(SELECT id from " + forginTableName + " WHERE " + forginTableColumnName + " = " +"'" + cells.ElementAt(x) + "'" + "), ";
                        }
                    }
                    else
                    {
                        if (x == (cells.Length - 1))
                            mainPartSql += "'"+ cells[x] + "'";
                        else
                            mainPartSql += "'"+cells[x] + "' "+", ";
                    }
                }
                sqlInsertStatamentsComplete[i-1] = mainPartSql += ");";
            }
            var completeSql = "";
            foreach(var sql in sqlInsertStatamentsComplete)
            {
                completeSql+= sql + System.Environment.NewLine;
            }
            var x1 = completeSql;
            var y = completeSql;
        }
    }
}
