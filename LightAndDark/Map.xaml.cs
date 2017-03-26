using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
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
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Reflection;
using System.IO;

namespace LightAndDark
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    /// 
    public partial class Map : Window
    {
        ObservableCollection<Statistics> itemsFromDbMap;
        public int playerAPstatus;
        public int enemyAPstatus;
        public int pHPstatus;
        public int eHPstatus;

        public Map(int check)
        {          
            InitializeComponent();

            // getting root path
            string rootLocation = System.AppDomain.CurrentDomain.BaseDirectory;

            SoundPlayer player = new SoundPlayer(System.IO.Path.GetFullPath(rootLocation + "/music/Track04.wav"));            
            player.Load();
            player.Play();

            //Adding enemies to database
            itemsFromDbMap = new ObservableCollection<Statistics>(Database.GetItemsNotDoneAsync().Result);
            if (itemsFromDbMap.Count < 12)
            {
                Statistics enemy01 = new Statistics();
                enemy01.Name = "Night Lynx";
                enemy01.Type = "Shadow of Tenebris";
                enemy01.HP = 720;
                enemy01.AP = 55;
                enemy01.checkID = 4;
                Database.SaveItemAsync(enemy01);

                Statistics enemy02 = new Statistics();
                enemy02.Name = "Blightseeker";
                enemy02.Type = "The Ancient Face";
                enemy02.HP = 1280;
                enemy02.AP = 70;
                enemy02.checkID = 5;
                Database.SaveItemAsync(enemy02);

                Statistics enemy03 = new Statistics();
                enemy03.Name = "Vexspawn";
                enemy03.Type = "The Anguished";
                enemy03.HP = 880;
                enemy03.AP = 60;
                enemy03.checkID = 6;
                Database.SaveItemAsync(enemy03);

                Statistics enemy04 = new Statistics();
                enemy04.Name = "Auramirage";
                enemy04.Type = "The Nasty Anomaly";
                enemy04.HP = 1030;
                enemy04.AP = 65;
                enemy04.checkID = 7;
                Database.SaveItemAsync(enemy04);

                Statistics enemy05 = new Statistics();
                enemy05.Name = "Spiritfoot";
                enemy05.Type = "The Dirty Charmer";
                enemy05.HP = 1500;
                enemy05.AP = 80;
                enemy05.checkID = 8;
                Database.SaveItemAsync(enemy05);

                Statistics enemy06 = new Statistics();
                enemy06.Name = "Grimesword";
                enemy06.Type = "The Obsidian Killer";
                enemy06.HP = 1310;
                enemy06.AP = 75;
                enemy06.checkID = 9;
                Database.SaveItemAsync(enemy06);

                Statistics enemy07 = new Statistics();
                enemy07.Name = "Abyssteeth";
                enemy07.Type = "The Dreadbrood";
                enemy07.HP = 1300;
                enemy07.AP = 85;
                enemy07.checkID = 10;
                Database.SaveItemAsync(enemy07);

                Statistics enemy08 = new Statistics();
                enemy08.Name = "Metalghoul";
                enemy08.Type = "The Deadly Horror";
                enemy08.HP = 1420;
                enemy08.AP = 80;
                enemy08.checkID = 11;
                Database.SaveItemAsync(enemy08);

                Statistics boss = new Statistics();
                boss.Name = "Flamelich";
                boss.Type = "The Parallel Deformity";
                boss.HP = 2100;
                boss.AP = 110;
                boss.checkID = 12;
                Database.SaveItemAsync(boss);
            }

            /*
             * Info about database content
            var itemsFromDb = Database.GetItemsNotDoneAsync().Result;
            ItemsCount.Content = "Items in Database " + itemsFromDb.Count;
            ToDoItemsListView.ItemsSource = itemsFromDb;
            */

            //For showing information about player in the game
            var itemsFromDb1 = Database.GetItemsNotDoneAsyncCharCheck(check).Result;
            NameTextBlock01.DataContext = itemsFromDb1;
            ToolTipChar01.DataContext = itemsFromDb1;
            Player01HP.DataContext = itemsFromDb1;
            Player01AP.DataContext = itemsFromDb1;
            MaxStatusPlayer01HP.DataContext = itemsFromDb1;
            ActualStatusPlayer01HP.DataContext = itemsFromDb1;
            ProgressBarPlayerHP.DataContext = itemsFromDb1;

            //MaxStatusPlayer01HP.Content = ProgressBarPlayerHP.Maximum;

            //Changing player background on selected character
            if (NameTextBlock01.Text == "Carol")
            {
                ImageChar01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/light_char_02.jpg"));
            }
            if (NameTextBlock01.Text == "Sheen")
            {
                ImageChar01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/light_char_03.jpg"));
            }


            //Calling method Creation() for the first enemy
            Creation();
            
            //MessageBox for variable check
            /*System.Windows.MessageBox.Show(check.ToString());*/


        }

        //Method Creation() with first enemy and how to distinguish it in first battle
        private void Creation()
        {
            if (Enemy01CheckLabel.Content.ToString() == "Night Lynx")
            {
                int enemycheck = 4;
                var itemsFromDb1 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb1;
                EnemyToolTip01.DataContext = itemsFromDb1;
                Enemy01HP.DataContext = itemsFromDb1;
                Enemy01AP.DataContext = itemsFromDb1;
                MaxStatusEnemy01HP.DataContext = itemsFromDb1;
                ActualStatusEnemy01HP.DataContext = itemsFromDb1;
                ProgressBarEnemyHP.DataContext = itemsFromDb1;
            }
        }

        //Database connection
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

        //Setting count to default value zero
        int count = 0;

        //Next button used in storyline, it displays main line of the game, where the player progresses
        //Concept is to add count in every new click - storytext X battle
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            //Count add +1
            count++;
            //Story textblock animation
            ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
            //Storytext
            StoryTextBlock.Text =
                "Prologue: You're coming closer to the Alman lake" +
                " when suddenly a Tenebris engage you from the shadows." +
                " Take this chance to try how powerful you are and kill one of them.";
            StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];     
                                                             
            if (count == 2)
            {
                count++;
                //In this count, there will be a battle with this enemy, it's used for the previous check
                Enemy01CheckLabel.Content = "Night Lynx";
                Creation();
                //Disable storytext, enable button for the battle start
                StoryLabel.Visibility = Visibility.Hidden;
                Fight01Button.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;               
            }
            if (count == 3)
            {
                StoryTextBlock.Text =
                    "It looks like my first enemy was defeated, I don't want to see them anymore!" +
                    " It was an easy fight,.. but truly, I know that this was just" +
                    " one of the weak Tenebri. So in the end I'll have to fight with" +
                    " much stronger enemies. For now on I have to move on";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 4)
            {
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "So, I’m going to progress on my journey to the mighty source of light." +
                    " As I see on the Alman lake shore, there is a Tenebris guardian " +
                    " keeping eye on everything that happens here. If I want to take" +
                    " another step, I must fight him and defeat him!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

            }
            if (count == 5)
            {
                count++;
                Enemy01CheckLabel.Content = "Blightseeker";
                StoryLabel.Visibility = Visibility.Hidden;
                Fight02Button.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
                //Changing enemy pic for next battle
                Enemy01.Source = new BitmapImage(new Uri(@"pack://application:,,,/pics/enemy_02.jpg"));
                //Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/enemy_02.jpg"));
            }
            if (count == 6)
            {
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "This Tenebris guardian was harder than that shadow, he had a lot of HP…" +
                    " it was an exhausting fight. I need to take a rest before I can go any" +
                    " further. And while I‘m at it, I can think of a plan, how to overcome next area – the Forrest.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 7)
            {
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "There will be a lot of Tenebris hiding in shadows, it won’t be easy to stay unnoticed." +
                    " I can’t come up with any plan that would solve this, I just know, that it will be best" +
                    " to stay near the Alman lake, there are some fireflies which can help me there, surpass the endless darkness.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"]; 
            }
            if (count == 8)
            {
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "I’m fine now, but I don't know how long the Tenebris will have the light on their side. So it's" +
                    " not bad idea to quickly move on. So, let’s continue walking towards the Forrest.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"]; 
            }
            if (count == 9)
            {
                //Playing music
                SoundPlayer player = new SoundPlayer("C:\\Users\\Luky\\Sounds\\Track05.wav");
                player.Load();
                player.Play();

                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "And here I am – at the entrence to the Forrest, I thought that there will be way to go near" +
                    " the lake from the very beginning, but there isn’t any way. I have to go into the deep" +
                    " Forrest first and then as quickly as possible go near the shore, where I’ll be safer.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
                
                //Changing background area
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_01.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 10)
            {
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "So far so good,.. at least I‘ve thought that, before that Tenebri just saw my light." +
                    " It looks like a very small and weak enemy, but I shouldn’t understimate it, let’s defeat it!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

            }
            if (count == 11)
            {
                count++;
                Enemy01CheckLabel.Content = "Vexspawn";               
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
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "Wait a minute, I’ve already been here?! It looks like some kind of illusion, I must find a source" +
                    " of it, so I can go any further. I‘m looking all around me, but still haven’t seen anything that" +
                    " could cause this. It looks like I have to use my light, to see more in this dark forrest and find" +
                    " the source of the illusion.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 13)
            {
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
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
                Enemy01CheckLabel.Content = "Auramirage";
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
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
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
                SoundPlayer player = new SoundPlayer("C:\\Users\\Luky\\Sounds\\08_amb.wav");
                player.Load();
                player.Play();
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
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
                Enemy01CheckLabel.Content = "Spiritfoot";
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
                SoundPlayer player = new SoundPlayer("C:\\Users\\Luky\\Sounds\\Soundtrack.wav");
                player.Load();
                player.Play();
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "Get rekt Forrest Tenebris, I’ve made it! But I really need to recover myself, I have to" +
                    " meditate for a bit near the water, for us it’s a way how we lights can recover our" +
                    " stamina, but it takes a while.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/area_03.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 21)
            {
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "Alright, I’m better now, so what do we have here – an entrance to the" +
                    " Tower. I will carefully try to proceed. Or maybe not? This could be a little problem" +
                    " a Tenebri with a katana is just looking right at me. I doubt that I could escape him." +
                    " Fight with all I’ve got to reach the light!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 22)
            { 
                count++;
                Enemy01CheckLabel.Content = "Grimesword";
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
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "Let’s go inside and check the light on the top… what, the light is gone? That’s really" +
                    " wierd, I’m sure that I saw it before the first Tenebri was defeated near the Alman lake.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 25)
            {
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
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
                SoundPlayer player = new SoundPlayer("C:\\Users\\Luky\\Sounds\\01_paper_self.wav");
                player.Load();
                player.Play();
                //This is the place where storyline divide into two ways, these two conditions displays each of the ways
                //First choice
                if (FirstChoiceAreaButtonWasClicked)
                {
                    ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
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

                //Second choice
                if (SecondChoiceAreaButtonWasClicked)
                {
                    ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                    StoryTextBlock.Text =
                        "The climate is drasticaly changing on my way to the second tower, it’s much colder here." +
                        " Also I haven’t seen any Temebri so far, I don’t know if I should be happy about that. I" +
                        " just have to be caraful, who knows what kind of darkness can hide in these conditions.";
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
                    ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
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
                    ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
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
                    Enemy01CheckLabel.Content = "Abyssteeth";
                    StoryLabel.Visibility = Visibility.Hidden;
                    Fight07Button.Visibility = Visibility.Visible;
                    NextButton.Visibility = Visibility.Hidden;
                    Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/enemy_09.jpg"));
                }

                if (SecondChoiceAreaButtonWasClicked)
                {
                    ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
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
                    Enemy01CheckLabel.Content = "Metalghoul";
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
                    ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
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
                SoundPlayer player = new SoundPlayer("C:\\Users\\Luky\\Sounds\\01_amb_darkness.wav");
                player.Load();
                player.Play();
                //Storyline is connected into one
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "I’m back here, really.. as I should be surprised by anything in this world. Now I see it clearly, both" +
                    " towers are without lights. And.. was that here before? Lot of lights in the center of Alman lake – Altar" +
                    " or something like that. I’m sure that wasn’t there before.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/map_01.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 33)
            {
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "First of all, I should recover a bit from the last battle, it looks like I’ll meet my" +
                    " final enemy, so I should be prepared for that.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 34)
            {
                ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                StoryTextBlock.Text =
                    "Alright, I’m better and here’s the entrace. Now, let’s face my biggest enemy, the" +
                    " strongest enemy guarding our light my friends. I’ll give it everything I’ve got. MITTE LUCEM! Give me" +
                    " your highest power, LIGHT!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/boss_entrance_area.jpg", UriKind.Absolute));
                this.Background = myBrush;
            }
            if (count == 35)
            {
                count++;
                SoundPlayer player = new SoundPlayer("C:\\Users\\Luky\\Sounds\\Track03.wav");
                player.Load();
                player.Play();
                Enemy01CheckLabel.Content = "Flamelich";
                StoryLabel.Visibility = Visibility.Hidden;
                Fight09Button.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
                Enemy01.Source = new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/boss.jpg"));
            }
            if (count == 36)
            {              
                StoryTextBlock.Text =
                    "WE’VE DONE IT LIGHTS! We’ve won over darkness once again! Now with the power of this light I can do something" +
                    " about this situation. But it’s not enough light to take everything back to normal. I can just choose" +
                    " between those two options.";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
            if (count == 38)
            {
                //Two choices for player in the ending
                if (FirstChoiceHopeButtonWasClicked || SecondChoiceHopeButtonWasClicked)
                {
                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource =
                        new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/hope_area.jpg", UriKind.Absolute));
                    this.Background = myBrush;

                    ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                    StoryTextBlock.Text =
                        "Epilogue: That was the story of  a brave light, which was fighting for the side of hope and won every fight till" +
                        " the end, despite of all the loneliness and desperation. Purpose of this game was to show you, that if there is" +
                        " even small trace of hope, you should chase it and never let it go.";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
                }
            }
            if (count == 39)
            {
                if (FirstChoiceHopeButtonWasClicked || SecondChoiceHopeButtonWasClicked)
                {
                    ((Storyboard)FindResource("animate")).Begin(StoryTextBlock);
                    StoryTextBlock.Text =
                        "Epilogue: Thank you for playing this game. Game creator: Lukas Drechsel ; . . . . . . . . . . Music (5th): Tomas Biza";
                    StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];

                    NextButton.Content = "The END";
                }
            }
            if (count == 40)
            {
                //Game is completed, going back to menu
                MainWindow selectionWindow = new MainWindow();
                selectionWindow.Show();
                this.Close();
            }

        }

        //Defining bools to check if player clicked on button
        private bool FirstChoiceAreaButtonWasClicked = false;
        private bool SecondChoiceAreaButtonWasClicked = false;
        private bool FirstChoiceHopeButtonWasClicked = false;
        private bool SecondChoiceHopeButtonWasClicked = false;
        private bool AttackButtonWasClicked = false;

        //Button for first choice in the storyline ways
        private void FirstChoiceAreaButton_Click(object sender, RoutedEventArgs e)
        {
            //Button was clicked, setting boolean to true
            FirstChoiceAreaButtonWasClicked = true;
            //Also using count, to continue in the storyline
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

        //Button for second choice in the storyline ways
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

        //Button for first choice in the storyline ending
        private void FirstChoiceHopeButton_Click(object sender, RoutedEventArgs e)
        {
            FirstChoiceHopeButtonWasClicked = true;
            count++;
            if (count == 37)
            {
                StoryLabel.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Visible;
                FirstChoiceHopeButton.Visibility = Visibility.Hidden;
                SecondChoiceHopeButton.Visibility = Visibility.Hidden;
                StoryTextBlock.Text =
                    "What world would it be without me living in it? I’m sure my friends will be proud for me that I’ve defeated" +
                    " the Tenebri and once again saved the world from darkness. Now let’s use it for it’s purpose. LUCIFER LUX!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
        }

        //Button for second choice in the storyline ending
        private void SecondChoiceHopeButton_Click(object sender, RoutedEventArgs e)
        {
            SecondChoiceHopeButtonWasClicked = true;
            count++;
            if (count == 37)
            {
                StoryLabel.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Visible;
                FirstChoiceHopeButton.Visibility = Visibility.Hidden;
                SecondChoiceHopeButton.Visibility = Visibility.Hidden;
                StoryTextBlock.Text =
                    "What friend would I be if I can’t even show my friends this beautiful world again? The answer is clear, I’ve" +
                    " done my job, now it’s my turn to go to sleep. I’m giving this world to all of you.. REINCARNATION, ET LUMEN!";
                StoryTextBlock.Style = (Style)Application.Current.Resources["ListViewItemTextBlockStyle"];
            }
        }

        //Method ShowFight(), it's used when the fight started, to change storytext for all battle mechanics
        private void ShowFight()
        {           
            ImageChar01.Visibility = Visibility.Visible;
            Enemy01.Visibility = Visibility.Visible;
            AttackButton.Visibility = Visibility.Visible;
            ChargeButton.Visibility = Visibility.Visible;            
            NameTextBlock01.Visibility = Visibility.Visible;
            EnemyTextBlock01.Visibility = Visibility.Visible;
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

        private void NextStage()
        {
            ImageChar01.Visibility = Visibility.Hidden;
            Enemy01.Visibility = Visibility.Hidden;
            AttackButton.Visibility = Visibility.Hidden;
            ChargeButton.Visibility = Visibility.Hidden;
            NextStageButton.Visibility = Visibility.Hidden;
            VictoryTextBlock.Visibility = Visibility.Hidden;
            LoseTextBlock.Visibility = Visibility.Hidden;
            NameTextBlock01.Visibility = Visibility.Hidden;
            EnemyTextBlock01.Visibility = Visibility.Hidden;
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

        //Button for entering next stage after fight to change battle mechanics for storytext
        private void NextStageButton_Click(object sender, RoutedEventArgs e)
        {
            NextStage();
            if (count == 36)
            {
                SoundPlayer player = new SoundPlayer("C:\\Users\\Luky\\Sounds\\09_amb_safe.wav");
                player.Load();
                player.Play();
                FirstChoiceHopeButton.Visibility = Visibility.Visible;
                SecondChoiceHopeButton.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
            }
        }

        //Button for charging an attack in the battle
        private async void ChargeButton_Click(object sender, RoutedEventArgs e)
        {
            AttackButtonWasClicked = false;

            //Setting variable progress for running progress bar with task run
            var progress = new Progress<int>(value => ProgressBarLoop.Value = value);
            await Task.Run(() =>
            {
                //Calling method Health() for variables
                HealthConditions();

                //Magic line of code for updating UI elements
                this.Dispatcher.Invoke(() =>
                {
                    //Content with player and enemy AP, adding it into public variables
                    playerAPstatus = Int32.Parse(Player01AP.Content.ToString());
                    enemyAPstatus = Int32.Parse(Enemy01AP.Content.ToString());
                });

                //For cycle for infinite running progress bar, with max value as player AP
                for (int progressBarValue = 0; progressBarValue < playerAPstatus + 1; progressBarValue++)
                {
                    //Condition if the progress bar reaches end, start again from zero
                    if (progressBarValue >= playerAPstatus)
                    {
                        progressBarValue = 0;
                    }

                    //If player clicked on attack button
                    if (AttackButtonWasClicked)
                    {
                        //If enemy HP is higher than zero
                        if (eHPstatus > 0)
                        {

                            this.Dispatcher.Invoke(() =>
                            {
                                //Add to variable enemy HP minus progress bar value, which equals to what player charged on his AP value
                                int updateEnemyHP = (eHPstatus - progressBarValue);
                                //Updating enemy content and saving changes to database
                                ActualStatusEnemy01HP.Content = updateEnemyHP;                               
                                string enemyName = EnemyTextBlock01.Text;
                                Database.UpdateItems(updateEnemyHP, enemyName);
                            });                        

                            //If player HP is higher than zero
                            if (pHPstatus > 0)
                            {
                                //Little delay for enemy attack
                                Thread.Sleep(500);
                                this.Dispatcher.Invoke(() =>
                                {
                                    int[] numbers = new int[11] { -50, -45, -40, -35, -30, -25, -20, -15, -10, -5, 0 };
                                    Random rd = new Random();
                                    int randomIndex = rd.Next(0, numbers.Length);
                                    int randomNumber = numbers[randomIndex];

                                    //Add to variable player HP minus enemy AP value + some randomize elements
                                    int updatePlayerHP = (pHPstatus - enemyAPstatus - randomNumber);
                                    //Updating player content and saving changes to database
                                    ActualStatusPlayer01HP.Content = updatePlayerHP;
                                    string playerName = NameTextBlock01.Text;
                                    Database.UpdateItems(updatePlayerHP, playerName);
                                });                                      
                            }
                            break;
                        }
                    }                   

                    //Actual running progress bar with its speed
                    ((IProgress<int>)progress).Report(progressBarValue);
                    Thread.Sleep(5);
                }              
            });
            //Calling method Health() for battle ending conditions
            HealthConditions();
        }

        //All battle entering buttons, their separated because each is used in different place with each meaning (they're also placed in game different)
        private void Fight01Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFight();
            Fight01Button.Visibility = Visibility.Hidden;
            HoldHelp.Visibility = Visibility.Visible;
        }
        private void Fight02Button_Click(object sender, RoutedEventArgs e)
        {
            HealthEnemies();
            ShowFight();
            Fight02Button.Visibility = Visibility.Hidden;       
        }
        private void Fight03Button_Click(object sender, RoutedEventArgs e)
        {
            HealthEnemies();
            ShowFight();
            Fight03Button.Visibility = Visibility.Hidden;
        }
        private void Fight04Button_Click(object sender, RoutedEventArgs e)
        {
            HealthEnemies();
            ShowFight();
            Fight04Button.Visibility = Visibility.Hidden;
        }
        private void Fight05Button_Click(object sender, RoutedEventArgs e)
        {
            HealthEnemies();
            ShowFight();
            Fight05Button.Visibility = Visibility.Hidden;
        }
        private void Fight06Button_Click(object sender, RoutedEventArgs e)
        {
            HealthEnemies();
            ShowFight();
            Fight06Button.Visibility = Visibility.Hidden;
        }
        private void Fight07Button_Click(object sender, RoutedEventArgs e)
        {
            HealthEnemies();
            ShowFight();
            Fight07Button.Visibility = Visibility.Hidden;
        }
        private void Fight08Button_Click(object sender, RoutedEventArgs e)
        {
            HealthEnemies();
            ShowFight();
            Fight08Button.Visibility = Visibility.Hidden;
        }
        private void Fight09Button_Click(object sender, RoutedEventArgs e)
        {
            HealthEnemies();
            ShowFight();
            Fight09Button.Visibility = Visibility.Hidden;

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                new BitmapImage(new Uri("https://student.sps-prosek.cz/~drechlu14/pics/boss_area.jpg", UriKind.Absolute));
            this.Background = myBrush;
        }
        /*private void Heal01Button_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                int playerHP = Int32.Parse(ActualStatusPlayer01HP.Content.ToString());
                int playerHeal = (playerHP + 1000);
                string playerName = NameTextBlock01.Text;
                Database.UpdateItems(playerHeal, playerName);
                NextStage();
                Heal01Button.Visibility = Visibility.Hidden;
            });
        }*/

        //Method Health() with HP, AP definition and battle end conditions
        private void HealthConditions()
        {

            //pHPstatus = Int32.Parse(Player01HP.Content.ToString());
            this.Dispatcher.Invoke(() =>
            {
                var playerHPstatus = Convert.ToString(ActualStatusPlayer01HP.Content);           
                pHPstatus = Int32.Parse(playerHPstatus);         
                //ProgressBarPlayerHP.Maximum = pHPstatus;


                var enemyHPstatus = Convert.ToString(ActualStatusEnemy01HP.Content);
                eHPstatus = Int32.Parse(enemyHPstatus);
                //ProgressBarEnemyHP.Maximum = eHPstatus;
                //ProgressBarEnemyHP.Maximum = Convert.ToString(MaxStatusEnemy01HP.Content);
                //ProgressBarEnemyHP.Value = eHPstatus;

                //If enemy HP is less then zero show next stage button, player is winner
                if (eHPstatus <= 0)
                {
                    NextStageButton.Visibility = Visibility.Visible;
                    VictoryTextBlock.Visibility = Visibility.Visible;
                    AttackButton.Visibility = Visibility.Hidden;
                    ChargeButton.Visibility = Visibility.Hidden;
                    HoldHelp.Visibility = Visibility.Hidden;
                }
                //If player HP is less then zero show lose button, player is loser
                if (pHPstatus <= 0)
                {
                    LoseButton.Visibility = Visibility.Visible;
                    LoseTextBlock.Visibility = Visibility.Visible;
                    AttackButton.Visibility = Visibility.Hidden;
                    ChargeButton.Visibility = Visibility.Hidden;
                    HoldHelp.Visibility = Visibility.Hidden;
                }
            });
        }

        //Method HealthEnemies() for checking conditions and how to distinguish them in each battle by their names
        private void HealthEnemies()
        {           
            if (Enemy01CheckLabel.Content.ToString() == "Blightseeker")
            {
                int enemycheck = 5;
                var itemsFromDb1 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb1;
                EnemyToolTip01.DataContext = itemsFromDb1;
                Enemy01HP.DataContext = itemsFromDb1;
                Enemy01AP.DataContext = itemsFromDb1;
                MaxStatusEnemy01HP.DataContext = itemsFromDb1;
                ActualStatusEnemy01HPCheck.DataContext = itemsFromDb1;
                ProgressBarEnemyHP.DataContext = itemsFromDb1;

                ActualStatusEnemy01HP.Content = ActualStatusEnemy01HPCheck.Content;
                Health();
            }
            if (Enemy01CheckLabel.Content.ToString() == "Vexspawn")
            {
                int enemycheck = 6;
                var itemsFromDb1 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb1;
                EnemyToolTip01.DataContext = itemsFromDb1;
                Enemy01HP.DataContext = itemsFromDb1;
                Enemy01AP.DataContext = itemsFromDb1;
                MaxStatusEnemy01HP.DataContext = itemsFromDb1;
                ActualStatusEnemy01HPCheck.DataContext = itemsFromDb1;
                ProgressBarEnemyHP.DataContext = itemsFromDb1;

                ActualStatusEnemy01HP.Content = ActualStatusEnemy01HPCheck.Content;
                Health();
            }
            if (Enemy01CheckLabel.Content.ToString() == "Auramirage")
            {
                int enemycheck = 7;
                var itemsFromDb1 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb1;
                EnemyToolTip01.DataContext = itemsFromDb1;
                Enemy01HP.DataContext = itemsFromDb1;
                Enemy01AP.DataContext = itemsFromDb1;
                MaxStatusEnemy01HP.DataContext = itemsFromDb1;
                ActualStatusEnemy01HPCheck.DataContext = itemsFromDb1;
                ProgressBarEnemyHP.DataContext = itemsFromDb1;

                ActualStatusEnemy01HP.Content = ActualStatusEnemy01HPCheck.Content;
                Health();
            }
            if (Enemy01CheckLabel.Content.ToString() == "Spiritfoot")
            {
                int enemycheck = 8;
                var itemsFromDb1 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb1;
                EnemyToolTip01.DataContext = itemsFromDb1;
                Enemy01HP.DataContext = itemsFromDb1;
                Enemy01AP.DataContext = itemsFromDb1;
                MaxStatusEnemy01HP.DataContext = itemsFromDb1;
                ActualStatusEnemy01HPCheck.DataContext = itemsFromDb1;
                ProgressBarEnemyHP.DataContext = itemsFromDb1;

                ActualStatusEnemy01HP.Content = ActualStatusEnemy01HPCheck.Content;
                Health();
            }
            if (Enemy01CheckLabel.Content.ToString() == "Grimesword")
            {
                int enemycheck = 9;
                var itemsFromDb1 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb1;
                EnemyToolTip01.DataContext = itemsFromDb1;
                Enemy01HP.DataContext = itemsFromDb1;
                Enemy01AP.DataContext = itemsFromDb1;
                MaxStatusEnemy01HP.DataContext = itemsFromDb1;
                ActualStatusEnemy01HPCheck.DataContext = itemsFromDb1;
                ProgressBarEnemyHP.DataContext = itemsFromDb1;

                ActualStatusEnemy01HP.Content = ActualStatusEnemy01HPCheck.Content;
                Health();
            }
            if (Enemy01CheckLabel.Content.ToString() == "Abyssteeth")
            {
                int enemycheck = 11;
                var itemsFromDb1 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb1;
                EnemyToolTip01.DataContext = itemsFromDb1;
                Enemy01HP.DataContext = itemsFromDb1;
                Enemy01AP.DataContext = itemsFromDb1;
                MaxStatusEnemy01HP.DataContext = itemsFromDb1;
                ActualStatusEnemy01HPCheck.DataContext = itemsFromDb1;
                ProgressBarEnemyHP.DataContext = itemsFromDb1;

                ActualStatusEnemy01HP.Content = ActualStatusEnemy01HPCheck.Content;
                Health();
            }
            if (Enemy01CheckLabel.Content.ToString() == "Metalghoul")
            {
                int enemycheck = 10;
                var itemsFromDb1 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb1;
                EnemyToolTip01.DataContext = itemsFromDb1;
                Enemy01HP.DataContext = itemsFromDb1;
                Enemy01AP.DataContext = itemsFromDb1;
                MaxStatusEnemy01HP.DataContext = itemsFromDb1;
                ActualStatusEnemy01HPCheck.DataContext = itemsFromDb1;
                ProgressBarEnemyHP.DataContext = itemsFromDb1;

                ActualStatusEnemy01HP.Content = ActualStatusEnemy01HPCheck.Content;
                Health();
            }
            if (Enemy01CheckLabel.Content.ToString() == "Flamelich")
            {
                int enemycheck = 12;
                var itemsFromDb1 = Database.GetItemsNotDoneAsyncEnemyCheck(enemycheck).Result;
                EnemyTextBlock01.DataContext = itemsFromDb1;
                EnemyToolTip01.DataContext = itemsFromDb1;
                Enemy01HP.DataContext = itemsFromDb1;
                Enemy01AP.DataContext = itemsFromDb1;
                MaxStatusEnemy01HP.DataContext = itemsFromDb1;
                ActualStatusEnemy01HPCheck.DataContext = itemsFromDb1;
                ProgressBarEnemyHP.DataContext = itemsFromDb1;

                ActualStatusEnemy01HP.Content = ActualStatusEnemy01HPCheck.Content;
                Health();
            }
        }

        //Method Health() with player and enemy HP
        private void Health()
        {
            this.Dispatcher.Invoke(() =>
            {
                var playerHPstatus = Convert.ToString(ActualStatusPlayer01HP.Content);
                pHPstatus = Int32.Parse(playerHPstatus);
                ProgressBarPlayerHP.Maximum = pHPstatus;

                var enemyHPstatus = Convert.ToString(ActualStatusEnemy01HP.Content);
                eHPstatus = Int32.Parse(enemyHPstatus);
                ProgressBarEnemyHP.Maximum = eHPstatus;
            });
        }

        //Button used for attacking, basicaly used for purpose of charge button
        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            AttackButtonWasClicked = true;

            HealthConditions();
        }

        //Lose button sends player back to menu
        private void LoseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow selectionWindow = new MainWindow();
            selectionWindow.Show();
            this.Close();
        }

    }
}