using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npgsql;



public class ConnectBDD : MonoBehaviour
{
    public int BO = -1;
    public int Game = -1;
    public void Test()
    {
        Debug.Log("open");
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql =
            "INSERT INTO \"Joueurs\"(\"ID\",\"Name\") VALUES (7,'Test');";
        dbcmd.CommandText = sql;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
        Debug.Log("close");
    }
    
    //Outils généraux :
    public List<int> Addx(List<int> L1, List<int> L2)
    {
        int i = 0;
        foreach(int x in L2)
        {
            L1[i] = (x+L1[i]);
            i++;
        }
        return L1;
    }

    public int Somme(List<int> L)
    {
        int s = 0;
        foreach (int x in L)
        {
            s += x;
        }
        return s;
    }

    public List<int> Trans01(List<int> L1)
    {
        List<int> L = new List<int>();
        foreach (int x in L1)
        {
            if (x >= 0)
            {

                L.Add(1);
            }
            else
            {
                L.Add(0);
            }
        }

        return L;
    }


    //Outils de lecture :
    public string LintoSQL(List<string> list)
    {
        string sql = "'";
        int len = list.Count;
        len /= 3;
        for(int i = 0; i < len; i++)
        {
            if (list[3*i].Length == 1)
            {
                sql += "0"+list[3 * i];
            }
            else
            { 
                sql += list[3 * i];
            }
            for (int o = 0;o< (3-list[3 * i + 1].Length); o++) { sql += "0"; }
            sql += list[3 * i + 1];
            sql +=  list[3*i+2];
        }
        sql += "'";
        return sql;
    }

    public int Get_maxID(string table)
    {
        int id = -1;
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";

        NpgsqlConnection dbcon;
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = "Select Max(\"ID\"+1) FROM \""+table+"\"";
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();
        bool test = reader.Read();
        id = reader.GetInt32(0);
        reader.Dispose();
        dbcmd.Dispose();
        dbcmd = null;
        return id;
    }

    public int RintSQl(string reqsql)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = reqsql;
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();
        reader.Read();
        int id = reader.GetInt32(0);
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
        return id;
    }

    public float RfloatSQl(string reqsql)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = reqsql;
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();
        reader.Read();
        float id = reader.GetFloat(0);
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
        return id;
    }

    public List<int> RLintSQl(string reqsql)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = reqsql;
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();
        
        List<int> Lid = new List<int>();
        while (reader.Read())
        {
            Lid.Add(reader.GetInt32(0));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
        return Lid;
    }

    public List<string> RLstrSQl(string reqsql)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = reqsql;
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();

        List<string> Lid = new List<string>();
        while (reader.Read())
        {
            Lid.Add(reader.GetString(0));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
        return Lid;
    }

    public List<List<string>> RLcontrestrSQl(string j,string game)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = "Select \"" + j + "C\" from \"Matchs\" Where \"ID\"" + game + "ORDER BY \"ID\"";
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();
        int k = 0;
        List<List<string>> Lid = new List<List<string>>();
        while (reader.Read())
        {
            Lid.Add(new List<string>());
            string contres = reader.GetString(0);
            int len = contres.Length;
            len /= 6;
            for (int x = 0; x < len; x++)
            {
                if (contres[6*x] != ' ')
                {
                    Lid[k].Add(contres.Substring(6 * x, 2));
                    Lid[k].Add(contres.Substring(6 * x + 2, 3));
                    Lid[k].Add(contres.Substring(6 * x + 5,1));
                }
            }
            k += 1;
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
        return Lid;
    }

    public string RstrSQL(string reqsql)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = reqsql;
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();
        reader.Read();
        string id = reader.GetString(0);
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
        return id;
    }

    public void AddSQl(string reqsql,string table )
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";

        NpgsqlConnection dbcon;
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        int id = Get_maxID(table);
        string sql2 = reqsql;
        NpgsqlCommand dbcmd2 = dbcon.CreateCommand();
        dbcmd2.CommandText = sql2;
        dbcmd2.ExecuteScalar();
        dbcmd2.Dispose();
        dbcmd2 = null;
        dbcon.Close();
        dbcon = null;
    }

    public void WSQl(string reqsql)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";

        NpgsqlConnection dbcon;
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        string sql2 = reqsql;
        NpgsqlCommand dbcmd2 = dbcon.CreateCommand();
        dbcmd2.CommandText = sql2;
        dbcmd2.ExecuteScalar();
        dbcmd2.Dispose();
        dbcmd2 = null;
        dbcon.Close();
        dbcon = null;
    }

    //Table Joueurs :
    public int GetJ_id(string name)
    {
        return RintSQl("Select \"ID\" FROM \"Joueurs\" WHERE \"Name\" = '" + name + "'");   
    }

    public string GetJ_name(int name)
    {
        return RstrSQL("Select \"Name\" FROM \"Joueurs\" WHERE \"ID\" = " + name);
    }

    public void Readp(List<string> items)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";

        NpgsqlConnection dbcon;
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = "Select \"Name\" FROM \"Joueurs\" where \"ID\" > 0";
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            items.Add(reader.GetString(0));
        }
        reader.Dispose();
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }

    public void Addp(string name)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";

        NpgsqlConnection dbcon;
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        int id = Get_maxID("Joueurs");
        string sql2 = "INSERT INTO \"Joueurs\"(\"ID\",\"Name\") Values (" + id + ", '" + name + "')";
        NpgsqlCommand dbcmd2 = dbcon.CreateCommand();
        dbcmd2.CommandText = sql2;
        dbcmd2.ExecuteScalar();
        dbcmd2.Dispose();
        dbcmd2 = null;
        dbcon.Close();
        dbcon = null;
    }

    //Table BO :

    public string GenerateCondIDJ1(int IDj)
    {
        string Condid = "in ( ";
        List<int> games = RLintSQl("Select \"Matchs\".\"ID\"  From \"BO\" Join \"Matchs\" ON \"BO\"=\"BO\".\"ID\"  Where \"J1\"=" + IDj);
        if(games.Count == 0)
        {
            return "in (-1)";
        }
        foreach (int g in games)
        {
            Condid += g+",";
        }
        return ((Condid.Substring(0,Condid.Length-1) )+ ")");
    }

    public string GenerateCondIDJ2(int IDj)
    {
        string Condid = "in (";
        List<int> games = RLintSQl("Select \"Matchs\".\"ID\"  From \"BO\" Join \"Matchs\" ON \"BO\"=\"BO\".\"ID\"  Where \"J2\"=" + IDj);
        if (games.Count == 0)
        {
            return "in (-1)";
        }
        foreach (int g in games)
        {
            Condid += g + ",";
        }
        return ((Condid.Substring(0, Condid.Length - 1)) + ")");
    }
    public int AddBO(string J1,string J2,string engage,int balle)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";

        NpgsqlConnection dbcon;
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        int id = Get_maxID("BO");
        string sql2 = "INSERT INTO \"BO\"(\"J1\",\"J2\",\"ID\",\"engagt\",\"nbBtour\") Values (" + GetJ_id(J1) + ", " + GetJ_id(J2) + ", " + id + ", " + GetJ_id(engage) + ", " + balle + ")";
        NpgsqlCommand dbcmd2 = dbcon.CreateCommand();
        dbcmd2.CommandText = sql2;
        dbcmd2.ExecuteScalar();
        dbcmd2.Dispose();
        dbcmd2 = null;
        dbcon.Close();
        dbcon = null;
        return id;
    }

    public string GetJ1(int BO)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = "Select \"Name\" FROM \"Joueurs\" JOIN \"BO\" ON  \"J1\"= \"Joueurs\".\"ID\" WHERE \"BO\".\"ID\" = "+BO;
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();
        reader.Read();
        string id = reader.GetString(0);
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
        return id;
    }

    public string GetJ2(int BO)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=password;";
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = "Select \"Name\" FROM \"Joueurs\" JOIN \"BO\" ON  \"J2\"= \"Joueurs\".\"ID\" WHERE \"BO\".\"ID\" = " + BO;
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();
        reader.Read();
        string id = reader.GetString(0);
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
        return id;
    }

    //Table Matches :
    public int AddMa(int BO)
    {
        int id = Get_maxID("Matchs");
        WSQl("INSERT INTO \"Matchs\"(\"BO\",\"ID\") Values (" + BO + ", " + id + ")");
        WSQl("UPDATE\"Matchs\" SET \"J1C\"=' ' Where \"ID\" =" + id);
        WSQl("UPDATE\"Matchs\" SET \"J2C\"=' ' Where \"ID\" =" + id);
        for (int f = 1; f <= 10; f++)
        {
            WSQl("UPDATE\"Matchs\" SET \"J1_"+f+"\"=-1 Where \"ID\" =" + id);
            WSQl("UPDATE\"Matchs\" SET \"J2_" + f + "\"=-1 Where \"ID\" =" + id);
        }
        return id;
    }

    public int Get_BO(int game)
    {
        return (RintSQl("Select \"BO\" From \"Matchs\" WHERE \"ID\" = "+game));
    }

    public int Get_Winner(int game)
    {
        return (RintSQl("Select \"Winner\" From \"Matchs\" WHERE \"ID\" = " + game));
    }

    public List<int> Get_scoreJ1(string condID)
    {
        List<List<string>> contre = RLcontrestrSQl("J1",condID);
        List<int> balle = Trans01( RLintSQl("Select \"J1_1\" from \"Matchs\" WHERE \"ID\" " + condID + " ORDER BY \"ID\""));
        for (int g = 2;g<11;g++)
        {
            balle = Addx(balle, Trans01(RLintSQl("Select \"J1_"+g+"\" from \"Matchs\" WHERE \"ID\" " + condID + " ORDER BY \"ID\"")));
        }
        int lenContres = contre.Count;
        for (int i = 0; i < lenContres;i++)
        {
            int lenContre = contre[i].Count;
            int nb_contre = 0;
            for (int j = 0; j < lenContre; j++)
            {
                if (j%3 == 2) { nb_contre += 1; }
            }
            balle[i] += nb_contre;
        }
        return balle;
    }

    public List<int> Get_scoreJ2(string condID)
    {
        List<List<string>> contre = RLcontrestrSQl("J2", condID);
        List<int> balles = Trans01(RLintSQl("Select \"J2_1\" from \"Matchs\" WHERE \"ID\" " + condID + "GROUP BY \"ID\" ORDER BY \"ID\""));
        for (int g = 2; g < 11; g++)
        {
            Addx(balles, Trans01(RLintSQl("Select \"J2_" + g + "\" from \"Matchs\" WHERE \"ID\" " + condID + "GROUP BY \"ID\" ORDER BY \"ID\"")));
        }
        int lenContres = contre.Count;
        for (int i = 0; i < lenContres; i++)
        {
            int lenContre = contre[i].Count;
            int nb_contre = 0;
            for (int j = 0; j < lenContre; j++)
            {
                if (j % 3 == 2) { nb_contre += 1; }
            }
            balles[i] += nb_contre;
        }
        return balles;
    }

    public List<int> Get_ScoreTour(int game,int J)
    {
        List<int> JTirs = new List<int>();
        for (int f = 1; f <= 10; f++)
        {
            int tour = RintSQl("SELECT \"J"+J+"_"+f+"\" From \"Matchs\" Where \"ID\" = "+game);
            JTirs.Add(tour);
        }
        List<string> contre = RLcontrestrSQl("J" + J, "=" + game)[0];
        int k = 0;
        foreach (string quille in contre)
        {
            if (k == 0)
            {
                JTirs.Add(-1 * int.Parse(quille));
            }
            if (k == 1)
            {
                JTirs.Add(int.Parse(quille));
            }
            k++;
            k %= 3;
        }

        return JTirs;
    }
}


