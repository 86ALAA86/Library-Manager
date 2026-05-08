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
    public class clsBook_BLL
    {
        public int? BookID { get; set; }

        [Required(ErrorMessage ="Book Title is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Book Auther is Required")]
        public string Auther {  get; set; }

        [Required(ErrorMessage = "Book Genre is Required")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Answer is Required")]
        public bool IsAvailable { get; set; }

        public clsBook_BLL()
        {

        }

        public clsBook_BLL(int BookID,string Title,string Auther,string Genre,bool IsAvailable)
        {
            this.BookID = BookID;
            this.Title = Title;
            this.Auther = Auther;
            this.Genre = Genre;
            this.IsAvailable = IsAvailable;

        }

        public static List<clsBook_BLL>? GetAllBooks()
        {
            DataTable? dt = clsBook_DAL.GetAllBooks();
            List<clsBook_BLL>? Books = null;
            if (dt != null)
            {
                Books=new List<clsBook_BLL>();

                foreach(DataRow dr in dt.Rows)
                {
                    Books.Add(new clsBook_BLL((int)dr["BookID"], dr["Title"].ToString(),
                        dr["Auther"].ToString(), dr["Genre"].ToString(),true));
                }
            }

            return Books;
        }

        public static clsBook_BLL? GetBookByID(int BookID)
        {
            DataTable? dt =clsBook_DAL.GetBookByID(BookID); 
            clsBook_BLL Book=null;
            if(dt!= null)
            {
                Book = new clsBook_BLL((int)dt.Rows[0]["BookID"], dt.Rows[0]["Title"].ToString(),
                    dt.Rows[0]["Auther"].ToString(), dt.Rows[0]["Genre"].ToString(),(bool) dt.Rows[0]["IsAvailable"]);
            }
            return Book;
        }

        public bool AddBook()
        {
            int? BookID = clsBook_DAL.AddBook(this.Title, this.Auther, this.Genre);

            if (BookID!=null)
            {
                this.BookID=BookID;
                this.IsAvailable=true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateBook()
        {
            if(this.BookID==null)
            {
                return false;
            }

            return clsBook_DAL.UpdateBook(this.BookID ?? 0, this.Title, this.Auther, this.Genre, this.IsAvailable);
        }

        public static bool DeleteBook(int BookID)
        {
            return clsBook_DAL.DeleteBook(BookID);
        }


    }
}
