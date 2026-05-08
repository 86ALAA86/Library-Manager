using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Library_Manager_DAL
{
    public  class clsBook_DAL
    {
        public static DataTable? GetAllBooks()
        {
            DataTable? dt = new DataTable();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllBooks", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);

                            }
                            else
                            {
                                dt = null;
                            }
                        }
                    }
                }

            }
            catch (SqlException)
            {
                dt = null;
            }
            return dt;

        }

        public static DataTable? GetBookByID(int BookID)
        {
            DataTable? dt = new DataTable();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetBookByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookID", BookID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);

                            }
                            else
                            {
                                dt = null;
                            }
                        }
                    }
                }

            }
            catch (SqlException)
            {
                dt = null;
            }
            return dt;

        }

        public static int? AddBook(string Title,string Auther,string Genre)
        {
            int? NewBookID=null;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddBook", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Title", Title);
                        command.Parameters.AddWithValue("@Auther", Auther);
                        command.Parameters.AddWithValue("@Genre", Genre);

                        SqlParameter outputAddParam = new SqlParameter("@NewBookID", DbType.Int32)
                        { 
                            Direction = ParameterDirection.Output,
                        };

                        command.Parameters.Add(outputAddParam);

                        SqlParameter returnAddParam = new SqlParameter("@ReturnVal", DbType.Int32)
                        {
                            Direction = ParameterDirection.ReturnValue,
                        };

                        command.Parameters.Add(returnAddParam);

                        command.ExecuteNonQuery();
                            
                        if ((int)command.Parameters["@ReturnVal"].Value == 1)
                        {
                            NewBookID = (int)command.Parameters["@NewBookID"].Value;
                        }
                        else
                        {
                            NewBookID = null;
                        }

                        
                    }
                }

            }
            catch (SqlException)
            {
               NewBookID = null;
            }
            return NewBookID;

        }

        public static bool UpdateBook(int BookID,string Title, string Auther, string Genre,bool IsAvailable)
        {
            bool IsUpdated = false;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateBook", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Title", Title);
                        command.Parameters.AddWithValue("@Auther", Auther);
                        command.Parameters.AddWithValue("@Genre", Genre);
                        command.Parameters.AddWithValue("@BookID", BookID);
                        command.Parameters.AddWithValue("@IsAvailable",IsAvailable);


                        SqlParameter returnUpdateParam = new SqlParameter("@ReturnVal", DbType.Int32)
                        {
                            Direction = ParameterDirection.ReturnValue,
                        };

                        command.Parameters.Add(returnUpdateParam);

                        command.ExecuteNonQuery();

                        if ((int)command.Parameters["@ReturnVal"].Value == 1)
                        {
                            IsUpdated = true;
                        }
                        else
                        {
                            IsUpdated=false;
                        }


                    }
                }

            }
            catch (SqlException)
            {
                IsUpdated = false;
            }
            return IsUpdated;

        }


        public static bool DeleteBook(int BookID)
        {
            bool IsDeleted = false;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_DeleteBook", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@BookID", BookID);


                        SqlParameter returnDeleteParam = new SqlParameter("@ReturnVal", DbType.Int32)
                        {
                            Direction = ParameterDirection.ReturnValue,
                        };

                        command.Parameters.Add(returnDeleteParam);

                        command.ExecuteNonQuery();

                        if ((int)command.Parameters["@ReturnVal"].Value == 1)
                        {
                            IsDeleted = true;
                        }
                        else
                        {
                            IsDeleted = false;
                        }


                    }
                }

            }
            catch (SqlException)
            {
                IsDeleted= false;
            }
            return IsDeleted;

        }



    }
}
