using System;
using System.Drawing;
using System.Windows.Forms;

namespace прототип1
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();

            //отображение уровня и здоровья персонажей
            dragonLevelLabel.Text = Convert.ToString(dragonLevel);
            playerHealthLabel.Text = Convert.ToString(player.health);
            dragonHealthLabel.Text = Convert.ToString(dragon.health);
        }

        //инициализация переменных отсчета времени
        static int sec1 = 0;
        static int sec2 = 0;

        //инициализация базового здоровья дракона
        static int baseDragonHealt = 400; //400 - 2000
        static int dragonLevel;
        //создание объектов класса дракон и игрок
        public static Dragon dragon = new Dragon();
        public static Player player = new Player();

        StartMenu startMenuForm = new StartMenu();

        //инициализация логина
        public Login login = new Login();
        public static string filename = "records.bin";

        private void Game_Load(object sender, EventArgs e)
        {
            //добавление метода для вызова делегата отображения этой формы при закрытии стартовой формы
            startMenuForm.EventVis += Vis;
            startMenuForm.EventCl += Cl;

            //запуск таймеров
            timer1.Tick += timer1_Tick;
            timer2.Tick += timer2_Tick;
            timer1.Start();

            this.Hide();
            startMenuForm.ShowDialog();
        }

        //отобразить эту форму
        void Vis()
        {
            //начало игры, инициализация персонажей
            dragonLevel = 1;
            dragon = new Dragon(baseDragonHealt, 5, dragonLevel);
            player = new Player(100, 10, 1);

            //присвоение класса 
            int C = ChooseClass.C;
            if (C > 0 && C <= 3)
                player.ChangeClass(C);

            //инициализация логина
            login = new Login(ChooseClass.login);

            minusDragonLabel.Text = "";
            minusPlayerLabel.Text = "";
            DialogLabel.Text = "";
            DoButton.Enabled = true;
            MenuButton.Visible = false;

            //отображение уровня и здоровья персонажей
            dragonLevelLabel.Text = Convert.ToString(dragonLevel);
            playerHealthLabel.Text = Convert.ToString(player.health);
            dragonHealthLabel.Text = Convert.ToString(dragon.health);

            //отображение силы атаки на кнопках
            radioButton1.Text = $"Атака";

            //проверка защиты
            if (player.IsDefended)
                radioButton3.Text = "Защита (защищен)";
            else
                radioButton3.Text = "Защита (не защищен)";

            this.Visible = true;
        }

        //Закрыть эту форму
        void Cl()
        {
            if (this != null)
                this.Close();
        }

        //выбор действия и его исполнение
        private void DoButton_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Attack(sender, e);

            if (radioButton2.Checked)
                Heal(sender, e);

            if (radioButton3.Checked)
                Defence(sender, e);
        }

        //атака рыцаря на дракона, нанесение урона
        private void Attack(object sender, EventArgs e)
        {
            //дракон получает урон
            dragon.RecievedAttack(player);
            //отключение кнопки исполнения действия, чтобы нельзя было атаковать много раз подряд
            DoButton.Enabled = false;
            //отображение урона
            minusDragonLabel.Text = $"- {dragon.damageTaken}";
            dragonHealthLabel.Text = Convert.ToString(dragon.health);

            //обнуление таймера
            sec1 = 0;

            //картинка рыцаря меняется на атакующего
            pictureBox2.Image = Properties.Resources.knight2;

            //если дракон этого уровня убит, то
            if (dragon.health <= 0)
            {
                minusDragonLabel.Text = "";

                //если это был дракон меньше 5 уровня, то
                if (dragonLevel < 5)
                {
                    //вывод сообщения
                    MessageBox.Show($"Дракон умер. Вы переходите на следующий уровень {dragonLevel + 1}");

                    //повышение уровня дракона на 1
                    dragonLevel++;

                    //повышение уровня рыцаря
                    player.Upgrade(dragonLevel);

                    //оборажение нового уровня дракона 
                    dragonLevelLabel.Text = Convert.ToString(dragonLevel);
                    //создание нового объекта класса Дракон с новым здоровьем и атакой
                    dragon = new Dragon(dragon, dragonLevel, baseDragonHealt);

                    //отображение нового уровня здоровья рыцаря
                    playerHealthLabel.Text = Convert.ToString(player.health);
                    //отображение нового уровня здоровья дракона
                    dragonHealthLabel.Text = Convert.ToString(dragon.health);

                    //кнопка действия снова доступна
                    DoButton.Enabled = true;
                }
                //если это был дракон 5 уровня, то
                else
                {
                    //игра заканчивается, открывается форма начала игры
                    MessageBox.Show("Дракон умер. Вы победили!");

                    //добавить запись рекорда

                    //показать кнопку "Ок" - конец игры
                    MenuButton.Visible = true;
                    //отключить кнопку действия
                    DoButton.Enabled = false;
                    //
                    startMenuForm.EventCl += Cl;
                    //
                    int rec = player.health;
                    if (rec > login.Record)
                    {
                        login.Record = rec;
                        login.AddNewRecord(filename);
                        login.CorrectResult(filename);
                    }
                    startMenuForm.EventVis += Vis;
                }
            }
            //если дракон не умер от атаки, то
            else
            {
                //запуск таймера атаки дракона
                timer2.Start();
                sec2 = 0;
            }

            //проверка защиты
            if (player.IsDefended)
                radioButton3.Text = "Защита (защищен)";
            else
                radioButton3.Text = "Защита (не защищен)";
        }

        //лечение
        private void Heal(object sender, EventArgs e)
        {
            //если уровень здоровья не выше максимума
            if (player.health < player.maxHealth)
            {
                player.Heal(dragonLevel);
                minusPlayerLabel.ForeColor = Color.Olive;
                minusPlayerLabel.Text = $"+ {player.healingDone}";
                playerHealthLabel.Text = Convert.ToString(player.health);
                //отключение кнопки исполнения действия, чтобы нельзя было лечиться много раз подряд
                DoButton.Enabled = false;

                //атака дракона
                timer2.Start();
                sec2 = 0;
            }
            else
                MessageBox.Show("Ваше здоровье на максимуме");
        }

        //защита
        private void Defence(object sender, EventArgs e)
        {
            if (player.IsDefended)
            {
                radioButton3.Text = "Защита (не защищен)";
            }
            else
            {
                player.Defend();
                radioButton3.Text = "Защита (защищен)";
            }

            timer2.Start();
            sec2 = 0;
        }

        //смена изображения рыцаря на статического (не атакующего) спустя 10 секунд
        private void timer1_Tick(object sender, EventArgs e)
        {
            sec1++;

            if (sec1 > 10)
            {
                pictureBox2.Image = Properties.Resources.knight1;
            }
        }

        //ответная атака дракона
        private void timer2_Tick(object sender, EventArgs e)
        {
            sec2++;

            //на 15 секунде дракон атакует
            if (sec2 == 15)
            {
                minusDragonLabel.Text = "";
                dragon.DoAttack(player);
            }

            //анимация атаки дракона
            if (sec2 > 15)
            {
                pictureBox1.Image = Properties.Resources.dragon1;
                minusPlayerLabel.ForeColor = Color.Firebrick;
                minusPlayerLabel.Text = $"- {player.damageTaken}";
                playerHealthLabel.Text = Convert.ToString(player.health);
            }

            //еще через 5 секунд
            if (sec2 > 20)
            {
                minusPlayerLabel.Text = "";
                //изображение меняется обратно
                pictureBox1.Image = Properties.Resources.dragon2;
                timer2.Stop();
                //если рыцарь умер, то
                if (player.health <= 0)
                {
                    //показать диалог
                    DialogLabel.Text = $"Кто-то:\nО нет, {player.playerClass} умер(";
                    //показать кнопку "Ок" - конец игры
                    MenuButton.Visible = true;
                    //отключить кнопку действия
                    DoButton.Enabled = false;
                    //
                    startMenuForm.EventCl += Cl;
                    startMenuForm.EventVis += Vis;
                }
                //если рыцарь не умер
                else
                    //включить кнопку действия
                    DoButton.Enabled = true;
            }
        }

        //cкрытие окна игры, открытие окна заставки
        private void MenuButton_Click(object sender, EventArgs e)
        {
            startMenuForm = new StartMenu();
            startMenuForm.Show();
            Hide();
        }

    }
}
