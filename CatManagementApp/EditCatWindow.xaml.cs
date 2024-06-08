using System.Windows;

namespace CatManagementApp
{
    public partial class EditCatWindow : Window
    {
        public Cat EditedCat { get; set; }

        public EditCatWindow(Cat cat)
        {
            InitializeComponent();
            EditedCat = cat;
            nameTextBox.Text = cat.Name;
            ageTextBox.Text = cat.Age.ToString();
            breedTextBox.Text = cat.Breed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            EditedCat.Name = nameTextBox.Text;
            EditedCat.Age = int.Parse(ageTextBox.Text);
            EditedCat.Breed = breedTextBox.Text;
            DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрывает окно редактирования
        }
    }
}