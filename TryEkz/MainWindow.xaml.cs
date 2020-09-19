using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;

namespace TryEkz
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["TryEkz.Properties.Settings.TryEkzConnectionString"].ConnectionString)) // подключение к БД
                {
                    connect.Open(); // открытие подключения
                    SqlCommand command = new SqlCommand("select * from Пользователи where Логин='" + loginText.Text + "' and Пароль='" + passwordText.Password + "'", connect); // запрос на пользователя
                    SqlDataReader reader = command.ExecuteReader(); // выполнение запроса
                    if (reader.HasRows) // проверка если есть данные
                    {
                        reader.Read();//  считывание данныъ
                        string rol = reader.GetValue(3  ).ToString(); //Сохранение должности
                        MessageBox.Show("Добро пожаловать! " + rol); // Ображение к пользователю
                        switch (rol) // Отктрытие формы в зависимости от должности
                        {
                            case "Менеджер":
                                managerForm f2 = new managerForm(); f2.Show(); this.Close(); //открытие формы менеджера
                                break;
                            default:
                                MessageBox.Show("Неизветсная роль");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль"); // если данных нет вывод сообщения
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
                throw;
            }
        }
    }
}
