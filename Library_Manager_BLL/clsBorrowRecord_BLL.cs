using Library_Manager_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Manager_BLL
{
    public class clsBorrowRecord_BLL
    {
        public int? RecordID { get; set; }

        [Required(ErrorMessage = "Book Title is Required")]
        public int? BookID { get;set; }

        [Required(ErrorMessage = "Borrower Name is Required")]
        public string BorrowerName { get; set; }
        public string? Title { get; set; }


        [Required(ErrorMessage = "Borrow Date is Required")]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string Status { get; set; }


        public clsBorrowRecord_BLL()
        {

        }

        public clsBorrowRecord_BLL(int RecordID,int BookID,string Title, string BorrowerName, DateTime BorrowDate, DateTime? ReturnDate,string Status)
        {
           this.RecordID = RecordID;
            this.BookID = BookID;
            this.Title= Title;
            this.BorrowerName = BorrowerName;
            this.BorrowDate = BorrowDate;
            this.ReturnDate = ReturnDate;
            this.Status = Status;


        }

        public static List<clsBorrowRecord_BLL>? GetAllBorrowRecord()
        {
            DataTable? dt = clsBorrowRecords_DAL.GetAllBorrowRecords();
            List<clsBorrowRecord_BLL>? Records = null;
            if (dt != null)
            {
                Records = new List<clsBorrowRecord_BLL>();

                foreach (DataRow dr in dt.Rows)
                {
                    DateTime? RD;
                    if (dr["ReturnDate"] != DBNull.Value)
                        RD = (DateTime)dr["ReturnDate"];
                    else
                        RD = null;

                        Records.Add(new clsBorrowRecord_BLL((int)dr["RecordID"], (int)dr["BookID"], dr["Title"].ToString(), dr["BorrowerName"].ToString(),
                           (DateTime)dr["BorrowDate"],RD, dr["Status"].ToString()));
                }
            }

            return Records;
        }

        public static clsBorrowRecord_BLL? GetBorrowRecordByID(int RecordID)
        {
            DataTable? dt =clsBorrowRecords_DAL.GetBorrowRecordByID(RecordID);
            clsBorrowRecord_BLL? Record = null;
            if (dt != null)
            {
                DateTime? RD;
                if (dt.Rows[0]["ReturnDate"] != DBNull.Value)
                    RD = (DateTime)dt.Rows[0]["ReturnDate"];
                else
                    RD = null;

                Record = new clsBorrowRecord_BLL((int)dt.Rows[0]["RecordID"], (int)dt.Rows[0]["BookID"],null
                   , dt.Rows[0]["BorrowerName"].ToString(),(DateTime) dt.Rows[0]["BorrowDate"], RD,
                     (string)dt.Rows[0]["Status"]);
            }
            return Record;
        }

        public bool AddBorrowRecord()
        {
            int? RecordID = clsBorrowRecords_DAL.AddBorrowRecord(this.BookID??0,this.BorrowerName);

            return RecordID!=null;
        }

        public bool UpdateBorrowRecord()
        {
            if (this.RecordID == null)
            {
                return false;
            }

            return clsBorrowRecords_DAL.UpdateBorrowRecord(this.RecordID ?? 0, this.BookID??0,
                this.BorrowerName,this.BorrowDate,this.ReturnDate,this.Status);
        }

        public static bool DeleteBorrowRecord(int RecordID)
        {
            return clsBorrowRecords_DAL.DeleteBorrowRecord(RecordID);
        }


    }

}
