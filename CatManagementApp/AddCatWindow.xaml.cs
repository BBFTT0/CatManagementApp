using System.Windows;

namespace CatManagementApp
{
    public partial class AddCatWindow : Window
    {
        public Cat NewCat { get; set; }

        public AddCatWindow()
        {
            InitializeComponent();
            NewCat = new Cat();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewCat.Name = nameTextBox.Text;
            NewCat.Age = int.Parse(ageTextBox.Text);
            NewCat.Breed = breedTextBox.Text;
            DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрывает окно добавления
        }
    }
}