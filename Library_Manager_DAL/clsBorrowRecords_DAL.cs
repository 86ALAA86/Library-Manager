using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Manager_DAL
{
    public class clsBorrowRecords_DAL
    {
        public static DataTable? GetAllBorrowRecords()
        {
            DataTable? dt = new DataTable();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllBorrowRecords", connection))
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

        public static DataTable? GetBorrowRecordByID(int RecordID)
        {
            DataTable? dt = new DataTable();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetBorrowRecordByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RecordID", RecordID);

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

        public static int? AddBorrowRecord(int BookID,string BorrowerName)
        {
            int? NewRecordID = null;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddBorrowRecord", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BorrowerName", BorrowerName);
                        command.Parameters.AddWithValue("@BookID", BookID);

                        SqlParameter outputAddParam = new SqlParameter("@NewRecordID", DbType.Int32)
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
                            NewRecordID = (int)command.Parameters["@NewRecordID"].Value;
                        }
                        else
                        {
                            NewRecordID = null;
                        }


                    }
                }

            }
            catch (SqlException)
            {
                NewRecordID = null;
            }
            return NewRecordID;

        }

        public static bool UpdateBorrowRecord(int RecordID,int BookID, string BorrowerName
            , DateTime BorrowDate, DateTime? ReturnDate,string Status)
        {
            bool IsUpdated = false;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateBorrowRecord", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RecordID", RecordID);
                        command.Parameters.AddWithValue("@BorrowerName", BorrowerName);
                        command.Parameters.AddWithValue("@BorrowDate",BorrowDate);
                        if(ReturnDate != null)
                        command.Parameters.AddWithValue("@ReturnDate", ReturnDate);
                        else
                            command.Parameters.AddWithValue("@ReturnDate",DBNull.Value);

                        command.Parameters.AddWithValue("@BookID", BookID);
                        command.Parameters.AddWithValue("@Status",Status);


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
                            IsUpdated = false;
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


        public static bool DeleteBorrowRecord(int RecordID)
        {
            bool IsDeleted = false;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_DeleteBorrowRecord", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RecordID", RecordID);


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
                IsDeleted = false;
            }
            return IsDeleted;

        }



    }

}
