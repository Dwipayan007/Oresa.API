using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using oresa.API.Models;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;

namespace oresa.API.Commons
{
    public class DbUtility
    {
        public static bool CreateMemberShip(MembershipModel mSignup)
        {
            //MySqlCommand scmd = new  MySqlCommand();
            bool res = false;
            MySqlCommand scmd = new MySqlCommand();
            DbConnection dbInstance = new DbConnection();
            MySqlConnection scon = dbInstance.OpenConnection();
            //scon.Open();
            scmd.Connection = scon;
            try
            {
                scmd.CommandText = "INSERT INTO membership "
                    + "(Membership_ID,Enrollment_Type,Organisation_Name,Pan_No,Chairman_MD,"
                    + "Mailing_Address,Company_Telephone_No,Fax,Mobile_No,Email,Website,"
                    + "Repre_Desig,Repre_Name,Repre_Mobile,Repre_Email,Category,PriceRange,TermsCondition)"
                    + "VALUES(@Membership_ID,@Enrollment_Type,@Organisation_Name,@Pan_No,@Chairman_MD,"
                    + "@Mailing_Address,@Company_Telephone_No,@Fax,@Mobile_No,@Email,@Website,"
                    + "@Repre_Desig,@Repre_Name,@Repre_Mobile,@Repre_Email,@Category,@PriceRange,@TermsCondition)";
                scmd.Parameters.AddWithValue("Membership_ID", mSignup.Membership_ID);
                scmd.Parameters.AddWithValue("Enrollment_Type", mSignup.Enrollment_Type);
                scmd.Parameters.AddWithValue("Organisation_Name", mSignup.Organization);
                scmd.Parameters.AddWithValue("Pan_No", mSignup.Pan);
                scmd.Parameters.AddWithValue("Chairman_MD", mSignup.ChairMan);
                scmd.Parameters.AddWithValue("Mailing_Address", mSignup.Mailing_Address);
                scmd.Parameters.AddWithValue("Company_Telephone_No", mSignup.Company_Telephone_No);
                scmd.Parameters.AddWithValue("Fax", mSignup.Fax);
                scmd.Parameters.AddWithValue("Mobile_No", mSignup.Mobile_No);

                scmd.Parameters.AddWithValue("Email", mSignup.Email);
                scmd.Parameters.AddWithValue("Website", mSignup.Website);
                scmd.Parameters.AddWithValue("Repre_Desig", mSignup.Repre_Desig);
                scmd.Parameters.AddWithValue("Repre_Name", mSignup.Repre_Name);
                scmd.Parameters.AddWithValue("Repre_Mobile", mSignup.Repre_Mobile);
                scmd.Parameters.AddWithValue("Repre_Email", mSignup.Repre_Email);

                scmd.Parameters.AddWithValue("Category", mSignup.Category);
                scmd.Parameters.AddWithValue("PriceRange", mSignup.PriceRange);
                scmd.Parameters.AddWithValue("TermsCondition", mSignup.TermsCondition);
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = true;

            }
            catch (Exception ex)
            {
                res = false;
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static bool SaveUpcoming(Dictionary<string, List<string>> myData, Dictionary<string, string> Mydata)
        {
            bool res = false;
            MySqlCommand scmd = new MySqlCommand();
            DbConnection dbInstance = new DbConnection();
            MySqlConnection scon = dbInstance.OpenConnection();
            //scon.Open();
            scmd.Connection = scon;
            try
            {
                //string id = scmd.LastInsertedId.ToString();
                scmd.CommandText = "insert into ores.upcoming_projects(Owner_ID,Project_Name,Location,Project_Type,No_Of_Units,Project_Photo) value"
                    + "(@Owner_ID, @Project_Name, @Location, @Project_Type, @No_Of_Units, @Project_Photo)";
                foreach (KeyValuePair<string, List<string>> entry in myData)
                {

                    if (entry.Key == "img")
                    {
                        var a = entry.Value;
                        foreach (var imgname in a)
                        {
                            scmd.Parameters.AddWithValue("Project_Photo", imgname);
                            scmd.Parameters.AddWithValue("Owner_ID", Mydata["Membership_ID"]);
                            scmd.Parameters.AddWithValue("Project_Name", Mydata["ProjectName"]);
                            scmd.Parameters.AddWithValue("Location", Mydata["Location"]);
                            scmd.Parameters.AddWithValue("Project_Type", Mydata["ProjectType"]);
                            scmd.Parameters.AddWithValue("No_Of_Units", Mydata["noofunit"]);
                            scmd.Prepare();
                            scmd.ExecuteNonQuery();
                            scmd.Parameters.Clear();
                        }
                    }
                }
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static DataTable GetMembershipData(Guid id)
        {
            MySqlCommand scmd = new MySqlCommand();
            DbConnection dbInstance = new DbConnection();
            MySqlConnection scon = dbInstance.OpenConnection();
            DataTable dt = new DataTable();
            scmd.Connection = scon;
            try
            {
                scmd.CommandText = "SELECT Organisation_Name,Chairman_MD,Mailing_Address,Website,Company_Telephone_No FROM ores.membership where Membership_ID=@Membership_ID";
                scmd.Parameters.AddWithValue("Membership_ID", id);
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                //res = false;
               
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static string GetUserId(string userName)
        {
            string memId = "";
            bool res = false;
            MySqlCommand scmd = new MySqlCommand();
            DbConnection dbInstance = new DbConnection();
            MySqlConnection scon = dbInstance.OpenConnection();
            //scon.Open();
            scmd.Connection = scon;
            try
            {
                scmd.CommandText = "SELECT Membership_ID FROM ores.signup WHERE username=@username";
                scmd.Parameters.AddWithValue("username", userName);
                scmd.Prepare();
                //memId= scmd.ExecuteNonQuery().ToString();
                //res = true;

                using (MySqlDataReader reader = scmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        memId = reader["Membership_ID"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                //res = false;
                memId = "";
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return memId;
        }

        public static bool SaveMemberData(Dictionary<string, List<string>> myData, Dictionary<string, string> Mydata)
        {
            bool res = false;
            MySqlCommand scmd = new MySqlCommand();
            DbConnection dbInstance = new DbConnection();
            MySqlConnection scon = dbInstance.OpenConnection();
            //scon.Open();
            scmd.Connection = scon;
            try
            {
                //string id = scmd.LastInsertedId.ToString();
                scmd.CommandText = "insert into ores.completed_projects(Owner_ID,Project_Name,Location,Project_Type,No_Of_Units,Project_Photo) value"
                    + "(@Owner_ID, @Project_Name, @Location, @Project_Type, @No_Of_Units, @Project_Photo)";
                foreach (KeyValuePair<string, List<string>> entry in myData)
                {
                    if (entry.Key == "img")
                    {
                        var a = entry.Value;
                        foreach (var imgname in a)
                        {
                            scmd.Parameters.AddWithValue("Project_Photo", imgname);
                            scmd.Parameters.AddWithValue("Owner_ID", Mydata["Membership_ID"]);
                            scmd.Parameters.AddWithValue("Project_Name", Mydata["ProjectName"]);
                            scmd.Parameters.AddWithValue("Location", Mydata["Location"]);
                            scmd.Parameters.AddWithValue("Project_Type", Mydata["ProjectType"]);
                            scmd.Parameters.AddWithValue("No_Of_Units", Mydata["noofunit"]);
                            scmd.Prepare();
                            scmd.ExecuteNonQuery();
                            scmd.Parameters.Clear();
                        }
                    }
                }
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static bool UserLogin(MemberLogin memlogin)
        {
            //MySqlCommand scmd = new  MySqlCommand();
            bool res = false;
            MySqlCommand scmd = new MySqlCommand();
            DbConnection dbInstance = new DbConnection();
            MySqlConnection scon = dbInstance.OpenConnection();
            //scon.Open();
            scmd.Connection = scon;
            try
            {
                scmd.CommandText = "SELECT * FROM ores.signup WHERE  usertype=@usertype and password=@password and username=@username";
                scmd.Parameters.AddWithValue("usertype", memlogin.UserType);
                scmd.Parameters.AddWithValue("password", memlogin.Password);
                scmd.Parameters.AddWithValue("username", memlogin.UserName);
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = true;

            }
            catch (Exception ex)
            {
                res = false;
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static bool DeleteMemberShipe(string membership_ID)
        {
            bool res = true;
            MySqlCommand scmd = new MySqlCommand();
            DbConnection dbInstance = new DbConnection();
            MySqlConnection scon = dbInstance.OpenConnection();
            //scon.Open();
            scmd.Connection = scon;
            try
            {
                scmd.CommandText = "DELETE FROM membership WHERE Membership_ID=@Membership_ID";
                scmd.Parameters.AddWithValue("Membership_ID", membership_ID);
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = false;

            }
            catch (Exception ex)
            {
                res = true;
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static bool RegisterUser(MembershipModel mSignup)
        {
            bool res = false;
            MySqlCommand scmd = new MySqlCommand();
            DbConnection dbInstance = new DbConnection();
            MySqlConnection scon = dbInstance.OpenConnection();
            //scon.Open();
            scmd.Connection = scon;
            try
            {
                scmd.CommandText = "INSERT INTO signup "
                    + "(Membership_ID,usertype,username,password)"
                    + "VALUES(@Membership_ID,@usertype,@username,@password)";
                scmd.Parameters.AddWithValue("Membership_ID", mSignup.Membership_ID);
                scmd.Parameters.AddWithValue("usertype", mSignup.Enrollment_Type);
                scmd.Parameters.AddWithValue("username", mSignup.Email);
                scmd.Parameters.AddWithValue("password", mSignup.Password);
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = true;

            }
            catch (Exception ex)
            {
                res = false;
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }
    }
}