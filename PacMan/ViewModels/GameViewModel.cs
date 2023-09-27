using PacMan.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PacMan.Commands;  // Importing namespace for RelayCommand
using PacMan.Views;
using PacMan.ViewModels.Ghosts;
using PacMan.ViewModels;
using System.Windows.Input;
using PacMan.Models;
using PacMan.Enums;
using System.Windows.Threading;
using System.Threading;
//using System.Drawing;
using PacMan.Views.Entities;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Automation;
using System.Media;

namespace PacMan.ViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        public int GameViewWidth { get; set; } = 848;
        public int GameViewHeight { get; set; } = 700-40;
        public MainCharacter MainCharacter { get; set; } = new MainCharacter();
        public GhostViewModel Ghosts { get; set; } = new GhostViewModel();
        public GhostBlue GhostBlueView { get; set; } = new GhostBlue();
        public BlueGhostViewModel BlueGhostVM { get; set; } = new BlueGhostViewModel();
        public GhostGreen GhostGreenView { get; set; } = new GhostGreen();
        public GreenGhostViewModel GreenGhostVM { get; set; } = new GreenGhostViewModel();
        public PlayerViewModel PlayerVM { get; set; } = new PlayerViewModel();
        public UserControl EndScreen { get; set; } = new UserControl();
        public LoseScreen LoseScreen { get; set; } = new LoseScreen();
        SoundPlayer ScoreSoundEffect { get; set; } = new SoundPlayer(Properties.Resources.ScoreSound);

        public int GhostSize { get; set; }

        public int McSize { get; set; }
        private bool isImage1 = true;


        public Visibility EndScreenVisibility { get; set; } = Visibility.Collapsed;
        public int PlayerEarnedScore { get; set; }
        public int CurrentPLayerLives { get; set; }

        public static Movement MovementDirection { get; set; }
        
        public double MainCharacterX { get; set; }
        public double MainCharacterY { get; set; }
        public double BlueGhostX { get; set; } = -100;
        public double BlueGhostY { get; set; }
        public double GreenGhostX { get; set; } = 100;
        public double GreenGhostY { get; set; }

        public ICommand BlueGhostAiCommand { get;}
        public BaseUserControl CurrentUserControl { get; set; }

        public ObservableCollection<Obstacles> Obstacles { get; } = new ObservableCollection<Obstacles>();
        public ObservableCollection<GoldCoinViewModel> GoldCoins { get; } = new ObservableCollection<GoldCoinViewModel>();
        public ObservableCollection<PlayerLifeModel> PlayerLives { get; } = new ObservableCollection<PlayerLifeModel>();


        public bool blueGhostCollision = false;
        double movementSpeed = 2;
        private readonly DispatcherTimer timer = new DispatcherTimer();
        int timerSpeed = 10;
        int timerImageInterval = 0; // Counter that set how often image part of code in MCmovement method is run. 




        public GameViewModel()
        {

            BlueGhostVM = new BlueGhostViewModel();
            GhostSize = Ghosts.GhostSize;

            McSize = MainCharacter.Size;
            CurrentPLayerLives = PlayerVM.PlayerLives;
            
            MovementDirection = Movement.Down;

            CreateObstaclesList();
            CreateCoinsList();
            CreatePLayerLivesList();
            PlaceOutCharacters();

            timer.Interval = TimeSpan.FromMilliseconds(timerSpeed);
            timer.Tick += GhostMovementTimer;
            timer.Tick += MainCharacterMovementTimer;
            timer.Start();
            

            BlueGhostAiCommand = new RelayCommand(execute: x => BlueGhostVM.Ai((AiDirectionPackage)x));
        }

        private void CreatePLayerLivesList()
        {
            PlayerLives.Clear();
            for (int i = 0; i < CurrentPLayerLives; i++)
            {
                PlayerLives.Add(new PlayerLifeModel());
            }
        }

        private void CreateObstaclesList()
       {
            Obstacles.Add(new Obstacles { Height = 20, Width = 578, XPosition = 142, YPosition = 70 });
            Obstacles.Add(new Obstacles { Height = 20, Width = 402, XPosition = 231, YPosition = 159 });
            Obstacles.Add(new Obstacles { Height = 20, Width = 124, XPosition = 142, YPosition = 339 });
            Obstacles.Add(new Obstacles { Height = 20, Width = 124, XPosition = 142, YPosition = 467 });
            Obstacles.Add(new Obstacles { Height = 20, Width = 124, XPosition = 596, YPosition = 467 });
            Obstacles.Add(new Obstacles { Height = 20, Width = 124, XPosition = 596, YPosition = 339 });
            Obstacles.Add(new Obstacles { Height = 187, Width = 20, XPosition = 142, YPosition = 155 });
            Obstacles.Add(new Obstacles { Height = 187, Width = 20, XPosition = 700, YPosition = 155 });
            Obstacles.Add(new Obstacles { Height = 109, Width = 20, XPosition = 613, YPosition = 178 });
            Obstacles.Add(new Obstacles { Height = 109, Width = 20, XPosition = 231, YPosition = 178 });
            Obstacles.Add(new Obstacles { Height = 82, Width = 20, XPosition = 142, YPosition = 485 });
            Obstacles.Add(new Obstacles { Height = 82, Width = 20, XPosition = 700, YPosition = 485 });
            Obstacles.Add(new Obstacles { Height = 82, Width = 76, XPosition = 395, YPosition = 487 });
       }

        private void CreateCoinsList()
        {
 
            int ypos = 25;
            for (int i = 0; i < 7; i++)
            {
                int xpos = 10;
                
                GoldCoins.Add(new GoldCoinViewModel { XPosition = xpos, YPosition = ypos });
                xpos = 100;

                for (int k = 0; k < 10; k++)
                {
                    if(!(xpos == 430 && ypos == 525))
                    {
                        GoldCoins.Add(new GoldCoinViewModel { XPosition = xpos, YPosition = ypos });

                    }
                    xpos += 110;

                }

                ypos += 100;


            }
        }

        private void PlaceOutCharacters()
        {
            MainCharacter.XPosition = MainCharacter.xStartPosition;
            MainCharacter.YPosition = MainCharacter.yStartPosition;
            GhostBlueView.YPosition = GhostBlueView.yStartPosition;
            GhostBlueView.XPosition = GhostBlueView.xStartPosition;
            GhostGreenView.YPosition = GhostGreenView.yStartPosition;
            GhostGreenView.XPosition = GhostGreenView.xStartPosition;
        }

        public void MainCharacterMovementTimer(object? sender, EventArgs e)
        {
            CurrentUserControl = MainCharacter;
            MainCharacterX = MainCharacter.XPosition;
            MainCharacterY = MainCharacter.YPosition;
            MovementDirection = MainCharacter.movement;
            if (timerImageInterval == 15) // Swaps between two images that is set by the MC direction.   
            {
                if (isImage1)
                {
                    MainCharacter.MainCharacterImage.Source = new BitmapImage(new Uri($"{MainCharacter.firstCurrentImage}"));
                }
                else
                {
                    MainCharacter.MainCharacterImage.Source = new BitmapImage(new Uri($"{MainCharacter.secondCurrentImage}"));
                }
                isImage1 = !isImage1;
                timerImageInterval = 0;
            }
            timerImageInterval++;
            MoveContentControl(MovementDirection);

            foreach(var goldcoin in GoldCoins)
            {
                if(IsCollision(MainCharacter, goldcoin))
                {
                    goldcoin.GoldCoinVisibility = Visibility.Collapsed;
                    GoldCoins.Remove(goldcoin);
                    PlayerEarnedScore++;
                    ScoreSoundEffect.Play();
                    //movementSpeed += 0.1; // Ta bort kommentar för att öka svårighetsgraden

                    break;
                }
            }
            if (PlayerEarnedScore == 55)
            {
                EndScreen = new WinScreen();
                EndScreenVisibility = Visibility.Visible;
                timer.Stop();
            }
        }

        private bool IsCollision(MainCharacter mainCharacter, GoldCoinViewModel goldCoin)
        {
            
            double mainCharacterLeft = mainCharacter.XPosition;
            double mainCharacterTop = mainCharacter.YPosition;
            double mainCharacterRight = mainCharacterLeft + mainCharacter.ActualWidth;
            double mainCharacterBottom = mainCharacterTop + mainCharacter.ActualHeight;

            double goldCoinLeft = goldCoin.XPosition;
            double goldCoinTop = goldCoin.YPosition;
            double goldCoinRight = goldCoinLeft + goldCoin.Width;
            double goldCoinBottom = goldCoinTop + goldCoin.Height;

            // Look if there is a collision between mainCharacter and goldCoin.
            bool collisionDetected = !(mainCharacterRight < goldCoinLeft || mainCharacterLeft > goldCoinRight ||
                                       mainCharacterBottom < goldCoinTop || mainCharacterTop > goldCoinBottom);

            return collisionDetected;
        }

        public Movement GetBlueGhostMovementDirection()
        {
            return BlueGhostVM.MovementDirection;
        }
        int GreenGhostCounter = 0;
        private void GhostMovementTimer(object sender, EventArgs e)
        {
            BlueGhostX = GhostBlueView.XPosition;
            BlueGhostY = GhostBlueView.YPosition;
            GreenGhostX = GhostGreenView.XPosition;
            GreenGhostY = GhostGreenView.YPosition;

            CurrentUserControl = GhostBlueView;
            AiDirectionPackage AiPackage = new AiDirectionPackage(new Point(BlueGhostX, BlueGhostY), new Point(MainCharacterX, MainCharacterY), CurrentUserControl.CollisionCheck);
            BlueGhostVM.Ai(AiPackage);
            MoveContentControl(BlueGhostVM.MovementDirection);

            if (GreenGhostCounter == 3) // Budget solution to make the green ghost move 3 times as slow as the other ghosts
            { 
                AiPackage = new AiDirectionPackage(new Point(GreenGhostX, GreenGhostY), new Point(MainCharacterX, MainCharacterY), CurrentUserControl.CollisionCheck);
                CurrentUserControl = GhostGreenView;
                GreenGhostVM.Ai(AiPackage);
                MoveContentControl(GreenGhostVM.MovementDirection);
                GreenGhostCounter = 0;
            }
            else
            {
                GreenGhostCounter++;
            }
        }

        private void MoveContentControl(Movement movementDirection)
        {
            
            double currentPositionX = CurrentUserControl.XPosition;
            double currentPositionY = CurrentUserControl.YPosition;
            if (CurrentUserControl.Occupation == Occupation.Ghost){ CurrentUserControl.CollisionCheck = true; }
            
            switch (movementDirection)
            {
                case Movement.Up:
                    if (BorderColisionUp(currentPositionY)) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.YPosition = 0;
                    }
                    else if (WallCollision(  movementDirection)) { }
                    else { CurrentUserControl.YPosition -= movementSpeed; if (CurrentUserControl.Occupation == Occupation.Ghost) { CurrentUserControl.CollisionCheck = false; } }
                    break;
                case Movement.Down:
                    if (CollisionDown(currentPositionY + CurrentUserControl.ActualHeight )) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.YPosition = GameViewHeight - CurrentUserControl.ActualHeight;
                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.YPosition += movementSpeed; if (CurrentUserControl.Occupation == Occupation.Ghost) { CurrentUserControl.CollisionCheck = false; } }
                    break;
                case Movement.Left:
                    if (CollisionLeft(currentPositionX)) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.XPosition = 0;

                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.XPosition -= movementSpeed; if (CurrentUserControl.Occupation == Occupation.Ghost) { CurrentUserControl.CollisionCheck = false; } }
                    break;

                case Movement.Right:
                    if (CollisionRight(currentPositionX + CurrentUserControl.ActualWidth)) // If no collision is detected with the border of the GameView
                    {
                        CurrentUserControl.XPosition = GameViewWidth - CurrentUserControl.ActualWidth;

                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.XPosition += movementSpeed; if (CurrentUserControl.Occupation == Occupation.Ghost) { CurrentUserControl.CollisionCheck = false; } }
                    break;


            }
            GhostAndMcCollision();
        }

        private void GhostAndMcCollision()
        {
            if (CurrentUserControl.XPosition < MainCharacterX + MainCharacter.ActualWidth &&
                    CurrentUserControl.XPosition + CurrentUserControl.ActualWidth > MainCharacterX &&
                    CurrentUserControl.YPosition < MainCharacterY + MainCharacter.ActualHeight &&
                    CurrentUserControl.YPosition + CurrentUserControl.ActualHeight > MainCharacterY
                    && CurrentUserControl.Occupation == Occupation.Ghost)
            {
                timer.Stop();
                LoseALife();
                CreatePLayerLivesList();
            }
        }

        private void LoseALife()
        {
            if (CurrentPLayerLives != 0)
            {
                CurrentPLayerLives--;
                PlaceOutCharacters();
                timer.Start();
            }
            else
            {
                EndScreen = LoseScreen;
                LoseScreen.StartAnimation();
                EndScreenVisibility = Visibility.Visible;
            }
        }

        #region collision controls
        private bool WallCollision( Movement movementDirection)
        {
            if (CurrentUserControl.ShouldCollideWithWalls == false) { return false; }
            double nextX = CurrentUserControl.XPosition;
            double nextY = CurrentUserControl.YPosition;
            switch (movementDirection)  // Predicts the next step of the selected UserControl
            {
                case Movement.Left:
                    nextX = CurrentUserControl.XPosition - movementSpeed;
                    break;
                case Movement.Right:
                    nextX = CurrentUserControl.XPosition + movementSpeed;
                    break;
                case Movement.Up:
                    nextY = CurrentUserControl.YPosition - movementSpeed;
                    break;
                case Movement.Down:
                    nextY = CurrentUserControl.YPosition + movementSpeed;
                    break;
            }
            
            foreach (var obstacle in Obstacles) //Checks if the UserControl colides with any of the obstacles and moves it accordingly 
            {

                if (nextX < obstacle.XPosition + obstacle.Width &&
                    nextX + CurrentUserControl.ActualWidth > obstacle.XPosition &&
                    nextY < obstacle.YPosition + obstacle.Height &&
                    nextY + CurrentUserControl.ActualHeight > obstacle.YPosition)
                {
                    switch (movementDirection)
                    {
                        case Movement.Left:
                            CurrentUserControl.XPosition = obstacle.XPosition + obstacle.Width;
                            break;
                        case Movement.Right:
                            CurrentUserControl.XPosition = obstacle.XPosition - CurrentUserControl.ActualWidth; 
                            break;
                        case Movement.Up:
                            CurrentUserControl.YPosition = obstacle.YPosition + obstacle.Height; 
                            break;
                        case Movement.Down:
                            CurrentUserControl.YPosition = obstacle.YPosition - CurrentUserControl.ActualHeight;
                            break;
                    }
                    return true; // Collision detected
                }
            }
            return false;
        }

        public bool BorderColisionUp(double currentPositionY)
        {
            double newPosition = currentPositionY - movementSpeed;
            if (newPosition < 0) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        public bool CollisionDown(double currentPositionY)
        {
            double newPosition = currentPositionY + movementSpeed;
            if (newPosition > GameViewHeight) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        public bool CollisionLeft(double currentPositionX)
        {
            double newPosition = currentPositionX - movementSpeed;
            if (newPosition < 0) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        public bool CollisionRight(double currentPositionX)
        {
            double newPosition = currentPositionX + movementSpeed;
            if (newPosition > GameViewWidth) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        #endregion

    }
}
