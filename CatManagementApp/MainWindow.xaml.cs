using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace CatManagementApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Cat> cats;
        private string connectionString = "Server=NOUT;Database=CatManagement;Trusted_Connection=True;";

        public MainWindow()
        {
            InitializeComponent();
            cats = new ObservableCollection<Cat>();
            LoadCats();
            catsListBox.ItemsSource = cats;
        }

        private void LoadCats()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Cats", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cats.Add(new Cat
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Age = reader.GetInt32(2),
                        Breed = reader.GetString(3)
                    });
                }
            }
        }

        private void AddCatButton_Click(object sender, RoutedEventArgs e)
        {
            AddCatWindow addCatWindow = new AddCatWindow();
            if (addCatWindow.ShowDialog() == true)
            {
                Cat newCat = addCatWindow.NewCat;
                cats.Add(newCat);
                SaveCatToDatabase(newCat);
            }
        }

        private void SaveCatToDatabase(Cat cat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Cats (Name, Age, Breed) VALUES (@Name, @Age, @Breed)", connection);
                command.Parameters.AddWithValue("@Name", cat.Name);
                command.Parameters.AddWithValue("@Age", cat.Age);
                command.Parameters.AddWithValue("@Breed", cat.Breed);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void RemoveCatButton_Click(object sender, RoutedEventArgs e)
        {
            if (catsListBox.SelectedItem is Cat selectedCat)
            {
                cats.Remove(selectedCat);
                RemoveCatFromDatabase(selectedCat);
            }
        }

        private void RemoveCatFromDatabase(Cat cat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Cats WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", cat.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void EditCatButton_Click(object sender, RoutedEventArgs e)
        {
            if (catsListBox.SelectedItem is Cat selectedCat)
            {
                EditCatWindow editCatWindow = new EditCatWindow(selectedCat);
                if (editCatWindow.ShowDialog() == true)
                {
                    int index = cats.IndexOf(selectedCat);
                    cats[index] = editCatWindow.EditedCat;
                    UpdateCatInDatabase(editCatWindow.EditedCat);
                }
            }
        }

        private void UpdateCatInDatabase(Cat cat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE Cats SET Name = @Name, Age = @Age, Breed = @Breed WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Name", cat.Name);
                command.Parameters.AddWithValue("@Age", cat.Age);
                command.Parameters.AddWithValue("@Breed", cat.Breed);
                command.Parameters.AddWithValue("@Id", cat.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}