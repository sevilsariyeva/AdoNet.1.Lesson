using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdoNet._1.Lesson.Commands;
using System.Windows;
using System.Diagnostics;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace AdoNet._1.Lesson.ViewModels
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public int YearPress { get; set; }
        public int Themes { get; set; }
        public int Category { get; set; }
        public int Author { get; set; }
        public int Press { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }
        public string Title { get; internal set; }
    }
    public class HomeUCViewModel:BaseViewModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;OnPropertyChanged(); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;OnPropertyChanged(); }
        }

        private int pages;

        public int Pages
        {
            get { return pages; }
            set { pages = value;OnPropertyChanged(); }
        }

        private int yearPress;

        public int YearPress
        {
            get { return yearPress; }
            set { yearPress = value;OnPropertyChanged(); }
        }

        private int themes;

        public int Themes
        {
            get { return themes; }
            set { themes = value;OnPropertyChanged(); }
        }

        private int category;

        public int Category
        {
            get { return category; }
            set { category = value;OnPropertyChanged(); }
        }

        private int author;

        public int Author
        {
            get { return author; }
            set { author = value;OnPropertyChanged(); }
        }

        private int press;

        public int Press
        {
            get { return press; }
            set { press = value;OnPropertyChanged(); }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value;OnPropertyChanged(); }
        }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value;OnPropertyChanged(); }
        }

        private ObservableCollection<Book> books=new ObservableCollection<Book>();

        public ObservableCollection<Book> Books
        {
            get { return books; }
            set { books = value;OnPropertyChanged(); }
        }

        public void Insert()
        {
            using (var conn = new SqlConnection())
            {
                var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                conn.ConnectionString = connectionString;
                conn.Open();


                string query = $@" INSERT INTO Books(Id,Name,Pages,YearPress,Id_Themes,Id_Category,Id_Author,Id_Press,Comment,Quantity)
                        VALUES(@id,@bookName,@pages,@yearPress,@themes,@category,@author,@press,@comment,@quantity)";

                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = Id;

                var paramName = new SqlParameter();
                paramName.ParameterName = "@bookName";
                paramName.SqlDbType = SqlDbType.NVarChar;
                paramName.Value = Name;

                var paramPage = new SqlParameter();
                paramPage.ParameterName = "@pages";
                paramPage.SqlDbType = SqlDbType.Int;
                paramPage.Value = Pages;

                var paramYear = new SqlParameter();
                paramYear.ParameterName = "@yearPress";
                paramYear.SqlDbType = SqlDbType.Int;
                paramYear.Value = YearPress;

                var paramThemes = new SqlParameter();
                paramThemes.ParameterName = "@themes";
                paramThemes.SqlDbType = SqlDbType.Int;
                paramThemes.Value = Themes;

                var paramCategory = new SqlParameter();
                paramCategory.ParameterName = "@category";
                paramCategory.SqlDbType = SqlDbType.Int;
                paramCategory.Value = Category;

                var paramAuthor = new SqlParameter();
                paramAuthor.ParameterName = "@author";
                paramAuthor.SqlDbType = SqlDbType.Int;
                paramAuthor.Value = Author;

                var paramPress = new SqlParameter();
                paramPress.ParameterName = "@press";
                paramPress.SqlDbType = SqlDbType.Int;
                paramPress.Value = Press;

                var paramComment = new SqlParameter();
                paramComment.ParameterName = "@comment";
                paramComment.SqlDbType = SqlDbType.NVarChar;
                paramComment.Value = Comment;

                var paramQuantity = new SqlParameter();
                paramQuantity.ParameterName = "@quantity";
                paramQuantity.SqlDbType = SqlDbType.Int;
                paramQuantity.Value = Quantity;

                using (var command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add(paramId);
                    command.Parameters.Add(paramName);
                    command.Parameters.Add(paramPage);
                    command.Parameters.Add(paramYear);
                    command.Parameters.Add(paramThemes);
                    command.Parameters.Add(paramCategory);
                    command.Parameters.Add(paramAuthor);
                    command.Parameters.Add(paramPress);
                    command.Parameters.Add(paramComment);
                    command.Parameters.Add(paramQuantity);

                    var result = command.ExecuteNonQuery();
                    MessageBox.Show($"{result} row affected");
                }
            }
        }
        public void Show()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Books";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.Id = reader.GetInt32(0);  
                        book.Name = reader.GetString(1);  
                        book.Pages = reader.GetInt32(2);
                        book.YearPress = reader.GetInt32(3); 
                        book.Themes = reader.GetInt32(4);
                        book.Category = reader.GetInt32(5);
                        book.Author = reader.GetInt32(6); 
                        book.Press = reader.GetInt32(7); 
                        book.Comment = reader.GetString(8);
                        book.Quantity = reader.GetInt32(9);

                        Books.Add(book);
                    }
                }
            }
        }
        public RelayCommand InsertClickCommand { get; set; }
        public RelayCommand ShowClickCommand { get; set; }
        public HomeUCViewModel()
        {
            InsertClickCommand = new RelayCommand(obj =>
            {
                Insert();
            });
            ShowClickCommand = new RelayCommand(obj =>
            {
                Show();
            });
        }
    }
    
}
