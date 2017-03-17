using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LightAndDark
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    /// 
    public partial class Map : Window
    {
        ObservableCollection<Statistics> itemsFromDbMap;
        public int progressBarValue;
        public int pHPstatus;
        public int eHPstatus;
        //private Statistics _player;
        //private Statistics _enemy;

        public Map(int check)
        {          
            InitializeComponent();
    

            itemsFromDbMap = new ObservableCollection<Statistics>(Database.GetItemsNotDoneAsync().Result);
            for (int i = 0; i <= 1; i++)
            {
                if (itemsFromDbMap.Count < 4)
                {
                    Statistics enemy01 = new Statistics();
                    enemy01.enemycheck = 1;
                    enemy01.Name = "Night Lynx";
                    enemy01.Type = "Shadow of Tenebris";
                    enemy01.HP = 720;
                    enemy01.AP = 80;
                    Database.SaveItemAsync(enemy01);

                    Statistics enemy02 = new Statistics();
                    enemy02.enemycheck = 2;
                    enemy02.Name = "Blightseeker";
                    enemy02.Type = "The Ancient Face";
                    enemy02.HP = 1380;
                    enemy02.AP = 70;
                    Database.SaveItemAsync(enemy02);

                    Statistics enemy03 = new Statistics();
                    enemy03.enemycheck = 3;
                    enemy03.Name = "Vexspawn";
                    enemy03.Type = "The Anguished Abnormality";
                    enemy03.HP = 880;
                    enemy03.AP = 60;
                    Database.SaveItemAsync(enemy03);
                }
            }

            /*info about database content
            var itemsFromDb = Database.GetItemsNotDoneAsync().Result;
            ItemsCount.Content = "Items in Database " + itemsFromDb.Count;
            ToDoItemsListView.ItemsSource = itemsFromDb;*/


            var itemsFromDb1 = Database.GetItemsNotDoneAsyncCharCheck(check).Result;
            NameTextBlock01.DataContext = itemsFromDb1;
            ToolTipChar01.DataContext = itemsFromDb1;
            Player01HP.DataContext = itemsFromDb1;
            Player01AP.DataContext = itemsFromDb1;
            MaxStatusPlayer01HP.DataContext = itemsFromDb1;
            ActualStatusPlayer01HP.DataContext = itemsFromDb1;

            Creation();
            
            /*System.Windows.MessageBox.Show(check.ToString());*/


        }

        private void Creation()
        {
            if (Enemy01CheckLabel.Content.ToString() == "Night Lynx")
            {
                int enemycheck = 4;
                var itemsFromDb2 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb2;
                EnemyToolTip01.DataContext = itemsFromDb2;
                Enemy01HP.DataContext = itemsFromDb2;
                Enemy01AP.DataContext = itemsFromDb2;
                MaxStatusEnemy01HP.DataContext = itemsFromDb2;
                ActualStatusEnemy01HP.DataContext = itemsFromDb2;
            }
            if (Enemy01CheckLabel.Content.ToString() == "Blightseeker")
            {
                int enemycheck = 5;
                var itemsFromDb3 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb3;
                EnemyToolTip01.DataContext = itemsFromDb3;
                Enemy01HP.DataContext = itemsFromDb3;
                Enemy01AP.DataContext = itemsFromDb3;
                MaxStatusEnemy01HP.DataContext = itemsFromDb3;
                ActualStatusEnemy01HP.DataContext = itemsFromDb3;
            }
                        
            //Health();
        }

        private static StatisticsDatabase _database;
        public static StatisticsDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var fileHelper = new FileHelper();
                    _database = new StatisticsDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _database;
            }
        }


        int count = 0;

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            count++;
            StoryTextBlock.Text =
                "Prologue: You're coming closer to the Alman lake" +
                " when suddenly a Tenebris engage you from the shadows." +
                " Take this chance to try how powerful you are and kill one of them.";
            StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            
            if (count == 2)
            {
                count++;
                StoryLabel.Visibility = Visibility.Hidden;
                Fight01Button.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
            }
            if (count == 3)
            {
                StoryTextBlock.Text =
                    "It looks like my first enemy is defeated, I don't wanna see them anymore!" +
                    " It was an easy fight,.. but truly, I know that this was just" +
                    " one of the weak Tenebri. So in the end I'll have to fight with" +
                    " much stronger enemies. For now on I have to move on";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
                //System.Windows.MessageBox.Show(count.ToString());
            }
            if (count == 4)
            {
                StoryTextBlock.Text =
                    "So, I’m going to progress on my journey to the mighty source of light." +
                    " As I see on the Alman lake shore, there is a Tenebris guardian " +
                    " keeping eye on everything that happens here. If I want to take" +
                    " another step, I must fight him and defeat him!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
                Enemy01CheckLabel.Content = "Blightseeker";
                Creation();
            }
            if (count == 5)
            {
                count++;
                StoryLabel.Visibility = Visibility.Hidden;
                Fight02Button.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
                Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/enemy_02.jpg"));
            }
            if (count == 6)
            {
                StoryTextBlock.Text =
                    "This Tenebris guardian was harder than that shadow, he had a lot of HP…" +
                    " it was an exhausting fight. I need to take a rest before I can go any" +
                    " further. And while I‘m at it, I can think of a plan, how to overcome next area – the Forrest.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 7)
            {
                StoryTextBlock.Text =
                    "There will be a lot of Tenebris hiding in shadows, it won’t be easy to stay unnoticed." +
                    " I can’t come up with any plan that would solve this, I just know, that it will be best" +
                    " to stay near the Alman lake, there are some fireflies which can help me there, surpass the endless darkness.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"]; 
            }
            if (count == 8)
            {
                StoryTextBlock.Text =
                    "I’m fine now, I‘ve already rest enough, let’s continue walking towards the Forrest.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"]; 
            }
            if (count == 9)
            {
                StoryTextBlock.Text =
                    "And here I am – at the entrence to the Forrest, I thought that there will be way to go near" +
                    " the lake from the very beginning, but there isn’t any way. I have to go into the deep" +
                    " Forrest first and then as quickly as possible go near the shore, where I’ll be safer.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_01.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 10)
            {
                StoryTextBlock.Text =
                    "So far so good,.. at least I‘ve thought that, before that Tenebri just saw my light." +
                    " It looks like a very small and weak enemy, but I shouldn’t understimate it, let’s defeat it!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 11)
            {
                count++;
                StoryLabel.Visibility = Visibility.Hidden;
                Fight03Button.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
                Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/enemy_04.jpg"));
            }
            if (count == 12)
            {
                StoryTextBlock.Text =
                    "That was quite a balanced Tenebri, wasn’t expecting that. At least it looks like I can go further." +
                    " I‘m still going, but haven’t seen any Tenebri or shore in the distance. Maybe it’s just my" +
                    " imagination, but it feels like I should arrive at the shore at anytime, but I can’t even see it.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 13)
            {
                StoryTextBlock.Text =
                    "Wait a minute, I’ve already been here?! It looks like some kind of illusion, I must find a source" +
                    " of it, so I can go any further. I‘m looking all around me, but still haven’t seen anything that" +
                    " could cause this. It looks like I have to use my light, to see more in this dark forrest and find" +
                    " the source of the illusion.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 13)
            {
                StoryTextBlock.Text =
                    "„Extende ad lucem!“. My view distence is larger now and what just caught my eye" +
                    " is something suspicious, it looks like some kind of dark vampire, maybe it’s" +
                    " cause of this illusion, I must defeat it!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_01_light.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 14)
            {
                count++;
                StoryLabel.Visibility = Visibility.Hidden;
                Fight04Button.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
                Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/enemy_05.jpg"));
            }
            if (count == 15)
            {
                StoryTextBlock.Text =
                    "That Tenebri didn’t just died in a normal way… it looked like that it just" +
                    " disapeared in a wierd way, what was that? I’m confused, but I have to" +
                    " continue and see if I escaped from that illusion.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_01.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 16)
            {
                StoryTextBlock.Text =
                    "Awesome, I’m out of it, finally something else. And what’s even better, I can" +
                    " see the exit, it’s not the shore tho, but I can tell where I’m now, near the tower" +
                    " that I need to examine for the source of light.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_02.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 17)
            {
                StoryTextBlock.Text =
                    "So let’s take a few more steps.. whoooaaaa! It looks like we have a company here," +
                    " this Tenebri is certainly not in a good mood for what are we doing here. It " +
                    " looks like he’s the leader of the weaker Forrest Tenebri. I can’t think any longer," +
                    " he’s coming this way and really fast!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 18)
            {
                count++;
                StoryLabel.Visibility = Visibility.Hidden;
                Fight05Button.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
                Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/enemy_06.jpg"));
            }
            if (count == 19)
            {
                StoryTextBlock.Text =
                    "Now that was an enemy, as I call the miniboss. I’m quite exhausted, but I must exit the Forrest" +
                    " first. It wouldn’t be nice if some miniboss friends were here waiting for me.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 20)
            {
                StoryTextBlock.Text =
                    "Get rekt Forrest Tenebris, I’ve made it! But I really need to recover myself, I have to" +
                    " meditate for a bit near the water, for us it’s a way how we can heal ourselfes" +
                    " or other comrades, but it takes a while.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                //Add regenerate button near the water +100-200HP then continue

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_03.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 21)
            {
                StoryTextBlock.Text =
                    "(Later..) Alright, I’m better now, so what do we have here – an entrance to the" +
                    " Tower. I will carefully try to proceed. Or maybe not? This could be a little problem" +
                    " a Tenebri with a katana is just looking right at me. I doubt that i could escape him." +
                    " Fight with all I’ve got to reach the light!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 22)
            { 
                count++;
                StoryLabel.Visibility = Visibility.Hidden;
                Fight06Button.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
                Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/enemy_07.jpg"));
            }
            if (count == 23)
            {
                StoryTextBlock.Text =
                    "Alright, that was an intense battle, I’ve barely dodged his fast paced attacks." +
                    " How could he be so fast with his katana? Nevermind, let’s progress, I’m really" +
                    " interested what’s at the top of the tower and I hope he was the only guardian";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 24)
            {
                StoryTextBlock.Text =
                    "Let’s go inside and check the light on the top… what, the light is gone? That’s really" +
                    " wierd, I’m sure that I saw it before the first Tenebri was defeated near the Alman lake.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 25)
            {
                FirstChoiceAreaButton.Visibility = Visibility.Visible;
                SecondChoiceAreaButton.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
                StoryTextBlock.Text =
                    "That means I have to choose where I’ll go next, first choice is to come behind the lake and see if anything" +
                    " happened on the other side and second choice is to try the second tower, where was also the light. Sooo.. which one?";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 27)
            {
                if (FirstChoiceAreaButtonWasClicked)
                {
                    StoryTextBlock.Text =
                        "Those mountains are quite amazing! Open areas are better choice for me, that’s for sure." +
                        " Still I’m surprised that there weren’t any Tenebri on the way here, maybe I’m just" +
                        " lucky, but that won’t last for long I’m on my way to the Alman lake.";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource =
                        new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_06.jpg", UriKind.Absolute));
                    this.Background = myBrush;
                }

                if (SecondChoiceAreaButtonWasClicked)
                {
                    StoryTextBlock.Text =
                        "The climate is drasticaly changing on my way to the second tower, it’s much colder here." +
                        " Also the landscape, from forrest plains to mountains. I haven’t seen any Temebri so far, I" +
                        " don’t know if I should be happy about that. I just have to be caraful, who knows what kind" +
                        " of darkness can hide in these conditions.";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource =
                        new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_04.jpg", UriKind.Absolute));
                    this.Background = myBrush;
                }
            }
            if (count == 28)
            {
                if (FirstChoiceAreaButtonWasClicked)
                {
                    StoryTextBlock.Text =
                        "A big tree, interesting. WHAT!? A Tenebri with light behind" +
                        " him, I must defeat him, but it won’t be that easy, this guy looks tough.";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource =
                        new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_07.jpg", UriKind.Absolute));
                    this.Background = myBrush;
                }

                if (SecondChoiceAreaButtonWasClicked)
                {
                    StoryTextBlock.Text =
                        "Here we are, at the entrace of the second tower, without any guard. This seems suspicious, but who cares." +
                        " I need to climb at the top to see the light.. or even darkness.";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
                }
            }
            if (count == 29)
            {
                if (FirstChoiceAreaButtonWasClicked)
                {
                    count++;
                    StoryLabel.Visibility = Visibility.Hidden;
                    Fight07Button.Visibility = Visibility.Visible;
                    NextButton.Visibility = Visibility.Hidden;
                    Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/enemy_09.jpg"));
                }

                if (SecondChoiceAreaButtonWasClicked)
                {
                    StoryTextBlock.Text =
                        "WHAT?! How did I get there? I was just right in the mountains and now I’m in the air on some" +
                        " wierd flying tower. And like if that wasn’t enough. Avarage size Tenebri is in front of me, it" +
                        " looks like he’s the master of this tower. I must defeat him!";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource =
                        new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_05.jpg", UriKind.Absolute));
                    this.Background = myBrush;
                }
            }
            if (count == 30)
            {
                if (FirstChoiceAreaButtonWasClicked)
                {
                    StoryTextBlock.Text =
                        "As I expected, really tough – and the light just disappeared! Maybe it’s something inside the" +
                        " tree, let’s explore it.";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
                }

                if (SecondChoiceAreaButtonWasClicked)
                {
                    count++;
                    StoryLabel.Visibility = Visibility.Hidden;
                    Fight08Button.Visibility = Visibility.Visible;
                    NextButton.Visibility = Visibility.Hidden;
                    Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/enemy_08.jpg"));
                }
            }
            if (count == 31)
            {
                if (FirstChoiceAreaButtonWasClicked)
                {
                    StoryTextBlock.Text =
                        "Daaaaarknesss everywhere. Wait! A portal.. something is happening.";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
                }

                if (SecondChoiceAreaButtonWasClicked)
                {
                    StoryTextBlock.Text =
                        "He was sure an incredibly skilled Tenebri, but now it’s no time to lose – the tower is falling down!" +
                        " I must get to the place where I was „teleported“ or I’m really done here.";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
                }
            }
            if (count == 32)
            {
                StoryTextBlock.Text =
                    "I’m back here, really.. as I should be surprised by anything in this world. Now I see it clearly, both" +
                    " towers are without lights. And.. was that here before? Lot of light in the center of Alman lake – Altar" +
                    " or something like that. I’m sure that wasn’t there before.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/map_01.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 33)
            {
                StoryTextBlock.Text =
                    "First of all, I should heal my wounds from last battle, it looks like I’ll meet my" +
                    " final enemy, so I should be prepared for that.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 34)
            {
                StoryTextBlock.Text =
                    "(Later..) Alright, I’m better and here’s the entrace. Now, let’s face my biggest enemy, the" +
                    " strongest enemy guarding our light my friends. I’ll give it everything I’ve got. MITTE LUCEM! Give me" +
                    " your highest power, LIGHT! (+50AP)";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/boss_entrance_area.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 35)
            {
                count++;
                StoryLabel.Visibility = Visibility.Hidden;
                Fight09Button.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
                Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/boss.jpg"));

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/boss_area.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 36)
            {
                FirstChoiceHopeButton.Visibility = Visibility.Visible;
                SecondChoiceHopeButton.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;

                StoryTextBlock.Text =
                    "WE’VE DONE IT LIGHTS! We’ve won over darkness for once! Now with the power of this light I can do something" +
                    " about this situation. But it’s not enough light to take everything back to normal. I can just choose" +
                    " between those two options.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/boss_entrance_area.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 38)
            {
                if (FirstChoiceHopeButtonWasClicked || SecondChoiceHopeButtonWasClicked)
                {
                    StoryTextBlock.Text =
                        "Epilogue: That was the story of  a brave light, which was fighting for the side of hope and won every fight till" +
                        " the end, despite of all the loneliness and desperation. Purpose of this game was to show you, that if there is" +
                        " even small trace of hope, you should chase it and never let it go.";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource =
                        new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/hope_area.jpg", UriKind.Absolute));
                    this.Background = myBrush;
                }
            }
            if (count == 39)
            {
                if (FirstChoiceHopeButtonWasClicked || SecondChoiceHopeButtonWasClicked)
                {
                    StoryTextBlock.Text =
                        "Epilogue: Thank you for playing my game. Author: Lukas Drechsel";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                    NextButton.Content = "The END";
                }
            }
            if (count == 40)
            {
                MainWindow selectionWindow = new MainWindow();
                selectionWindow.Show();
                this.Close();
            }

        }

        private bool FirstChoiceAreaButtonWasClicked = false;
        private bool SecondChoiceAreaButtonWasClicked = false;
        private bool FirstChoiceHopeButtonWasClicked = false;
        private bool SecondChoiceHopeButtonWasClicked = false;

        private void FirstChoiceAreaButton_Click(object sender, RoutedEventArgs e)
        {
            FirstChoiceAreaButtonWasClicked = true;
            count++;
            if (count == 26)
            {
                StoryLabel.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Visible;
                FirstChoiceAreaButton.Visibility = Visibility.Hidden;
                SecondChoiceAreaButton.Visibility = Visibility.Hidden;
                StoryTextBlock.Text =
                    "Alman lake sound like a right choice, there must be something if the light isn’t here. I’m going to find out what happened";
                StoryTextBlock.Style = (Style) Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
        }

        private void SecondChoiceAreaButton_Click(object sender, RoutedEventArgs e)
        {
            SecondChoiceAreaButtonWasClicked = true;
            count++;
            if (count == 26)
            {
                StoryLabel.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Visible;
                FirstChoiceAreaButton.Visibility = Visibility.Hidden;
                SecondChoiceAreaButton.Visibility = Visibility.Hidden;
                StoryTextBlock.Text =
                    "Investigate the second tower should be the right choice. I saw a light there and I expect that I'll find out more then here.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
        }

        private void FirstChoiceHopeButton_Click(object sender, RoutedEventArgs e)
        {
            FirstChoiceHopeButtonWasClicked = true;
            count++;
            if (count == 37)
            {
                StoryLabel.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Visible;
                FirstChoiceAreaButton.Visibility = Visibility.Hidden;
                SecondChoiceAreaButton.Visibility = Visibility.Hidden;
                StoryTextBlock.Text =
                    "What world would it be without me living in it? I’m sure my friends will be proud for me that I’ve defeated" +
                    " the Tenebri and once again saved the world from darkness. Now let’s use it for it’s purpose. LUCIFER LUX!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
        }

        private void SecondChoiceHopeButton_Click(object sender, RoutedEventArgs e)
        {
            SecondChoiceHopeButtonWasClicked = true;
            count++;
            if (count == 37)
            {
                StoryLabel.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Visible;
                FirstChoiceAreaButton.Visibility = Visibility.Hidden;
                SecondChoiceAreaButton.Visibility = Visibility.Hidden;
                StoryTextBlock.Text =
                    "What friend would I be if I can’t even show my friends this beautiful world again? The answer is clear, I’ve" +
                    " done my job, now it’s my turn to go to sleep. I’m giving this world to all of you.. REINCARNATION, ET LUMEN!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
        }

        private void ShowFight()
        {
            ImageChar01.Visibility = Visibility.Visible;
            Enemy01.Visibility = Visibility.Visible;
            AttackButton.Visibility = Visibility.Visible;
            ChargeButton.Visibility = Visibility.Visible;
            NextStageButton.Visibility = Visibility.Visible;
            NameTextBlock01.Visibility = Visibility.Visible;
            ActualStatusPlayer01HP.Visibility = Visibility.Visible;
            DivideStatusPlayer01HP.Visibility = Visibility.Visible;
            MaxStatusPlayer01HP.Visibility = Visibility.Visible;
            ActualStatusEnemy01HP.Visibility = Visibility.Visible;
            DivideStatusEnemy01HP.Visibility = Visibility.Visible;
            MaxStatusEnemy01HP.Visibility = Visibility.Visible;
            ProgressBarLoop.Visibility = Visibility.Visible;
            ProgressBarPlayerHP.Visibility = Visibility.Visible;
            ProgressBarEnemyHP.Visibility = Visibility.Visible;
        }

        private async void Fight01Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            Fight01Button.Visibility = Visibility.Hidden;

            AttackButtonWasClicked = false;

            var progress = new Progress<int>(value => ProgressBarLoop.Value = value);
            await Task.Run(() =>
            {

                for (int i = 0; i < 100; i++)
                {
                    if (i >= 99)
                    {
                        i = 0;
                    }

                    if (AttackButtonWasClicked)
                    {

                        break;
                        progressBarValue = Int32.Parse(ProgressBarLoop.Value.ToString());
                    }

                    ((IProgress<int>)progress).Report(i);
                    Thread.Sleep(10);
                }

                // Thread.Sleep(2000);
                // AutoClickFight_Click();   
            });
        }

        private async void ChargeButton_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            AttackButtonWasClicked = false;

            var progress = new Progress<int>(value => ProgressBarLoop.Value = value);
            await Task.Run(() =>
            {

                for (int i = 0; i < 100; i++)
                {
                    if (i >= 99)
                    {
                        i = 0;
                    }

                    if (AttackButtonWasClicked)
                    {

                        break;
                        progressBarValue = Int32.Parse(ProgressBarLoop.Value.ToString());
                    }

                    ((IProgress<int>)progress).Report(i);
                    Thread.Sleep(10);
                }

                   // Thread.Sleep(2000);
                   // AutoClickFight_Click();   
           });
        }



        private void Fight02Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            Fight02Button.Visibility = Visibility.Hidden;       
        }
        private void Fight03Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            Fight03Button.Visibility = Visibility.Hidden;
        }
        private void Fight04Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            Fight04Button.Visibility = Visibility.Hidden;
        }
        private void Fight05Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            Fight05Button.Visibility = Visibility.Hidden;
        }
        private void Fight06Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            Fight06Button.Visibility = Visibility.Hidden;
        }
        private void Fight07Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            Fight07Button.Visibility = Visibility.Hidden;
        }
        private void Fight08Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            Fight08Button.Visibility = Visibility.Hidden;
        }
        private void Fight09Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            Fight09Button.Visibility = Visibility.Hidden;
        }


        private bool AttackButtonWasClicked = false;

        private void Health()
        {
            //System.Windows.MessageBox.Show(pHPstatus.ToString());
            //pHPstatus = Int32.Parse(Player01HP.Content.ToString());
            //var playerHPstatus = Convert.ToString(Player01HP.Content);
            //pHPstatus = Int32.Parse(playerHPstatus);
            var playerAPstatus = Convert.ToString(Player01AP.Content);
            //var pAPstatus = ;


            var enemyHPstatus = Convert.ToString(Enemy01HP.Content);
            eHPstatus = Int32.Parse(enemyHPstatus);
            var enemyAPstatus = Convert.ToString(Enemy01AP.Content);
            //var eAPstatus = Int32.Parse(enemyAPstatus);
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            AttackButtonWasClicked = true;

            Health();

            int[] numbers = new int[16] { -50, -40, -30, -20, -10, 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            Random rd = new Random();
            int randomIndex = rd.Next(0, numbers.Length);
            int randomNumber = numbers[randomIndex];

            if (eHPstatus > 0)
            {
                int updateEnemyHP = Math.Abs(eHPstatus - progressBarValue - randomNumber);
                ActualStatusEnemy01HP.Content = updateEnemyHP;
                Database.UpdateItems(updateEnemyHP);
                
                
                /*Statistics item = new Statistics();
                item.ID = 4;
                item.enemycheck = 1;
                item.HP = updateEnemyHP;
                Database.SaveItemAsync(item);*/
            }
            else
            {
                //Show next stage button after win
            }

            if (pHPstatus > 0)
            {
                int updatePlayerHP = Math.Abs(pHPstatus - progressBarValue - randomNumber);
                ActualStatusPlayer01HP.Content = updatePlayerHP;
                Database.UpdateItems(updatePlayerHP);

                //int updatePlayerHP = Math.Abs(pHPstatus - eAPstatus - randomNumber);
                //ActualStatusPlayer01HP.Content = updatePlayerHP;

                /*Statistics item = new Statistics();
                item.ID = 1;
                item.HP = updatePlayerHP;
                Database.SaveItemAsync(item);*/
            }
            else
            {
                //Show losser button after lose
            }

                //PlayerUpdate01HP.Content = updateEnemyHP;
                //System.Windows.MessageBox.Show(updateEnemyHP.ToString());


        }
        private void NextStageButton_Click(object sender, RoutedEventArgs e)
        {
            ImageChar01.Visibility = Visibility.Hidden;
            Enemy01.Visibility = Visibility.Hidden;
            AttackButton.Visibility = Visibility.Hidden;
            ChargeButton.Visibility = Visibility.Hidden;
            NextStageButton.Visibility = Visibility.Hidden;
            NameTextBlock01.Visibility = Visibility.Hidden;
            ActualStatusPlayer01HP.Visibility = Visibility.Hidden;
            DivideStatusPlayer01HP.Visibility = Visibility.Hidden;
            MaxStatusPlayer01HP.Visibility = Visibility.Hidden;
            ActualStatusEnemy01HP.Visibility = Visibility.Hidden;
            DivideStatusEnemy01HP.Visibility = Visibility.Hidden;
            MaxStatusEnemy01HP.Visibility = Visibility.Hidden;
            StoryLabel.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Visible;
            ProgressBarLoop.Visibility = Visibility.Hidden;
            ProgressBarPlayerHP.Visibility = Visibility.Hidden;
            ProgressBarEnemyHP.Visibility = Visibility.Hidden;
        }



    }
}
