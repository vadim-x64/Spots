using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Spots {

    public partial class Form1 : Form {
        public Form2 form2;
        public Form3 form3;
        public Form4 form4;
        private Timer gameTimer;
        private int timeLeft;
        private int elapsedTimeInSeconds;
        private bool gameFinished = false;
        private List<int> numbers;
        private int tileSize;
        private bool isDragging = false;
        private int draggingIndex = -1;
        private Point dragPoint;
        private List<Bitmap> tileImages;
        private List<Bitmap> backgrounds;
        private int currentBackground = 0;
        private Timer restartTimer;
        private Timer restartTimer1;
        private AudioFileReader audioFileReader1;
        private IWavePlayer iWavePlayer1;
        private AudioFileReader audioFileReader2;
        private IWavePlayer iWavePlayer2;
        private AudioFileReader audioFileReader3;
        private IWavePlayer iWavePlayer3;
        private AudioFileReader audioFileReader4;
        private IWavePlayer iWavePlayer4;
        private AudioFileReader audioFileReader5;
        private IWavePlayer iWavePlayer5;
        private AudioFileReader audioFileReader6;
        private IWavePlayer iWavePlayer6;
        private AudioFileReader audioFileReader7;
        private IWavePlayer iWavePlayer7;
        private AudioFileReader audioFileReader8;
        private IWavePlayer iWavePlayer8;
        private AudioFileReader audioFileReader9;
        private IWavePlayer iWavePlayer9;
        private AudioFileReader audioFileReader10;
        private IWavePlayer iWavePlayer10;
        private bool isMuted = false;
        private bool isPaused = false;
        private bool isGameOver = false;
        public bool isFormVisible = false;

        public Form1() {
            InitializeComponent();
            form2 = new Form2();
            form3 = new Form3();
            form4 = new Form4();
            form4.FormClosed += new FormClosedEventHandler(Form4_FormClosed);
            panel1.Visible = false;
            label1.Visible = false;
            button2.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox7.Visible = false;
            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += new EventHandler(GameTimer_Tick);
            iWavePlayer1 = new WaveOut();
            audioFileReader1 = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\playing.mp3");
            iWavePlayer1.Init(audioFileReader1);
            iWavePlayer1.PlaybackStopped += OnPlaybackStopped1;
            restartTimer1 = new Timer();
            restartTimer1.Interval = 3000;
            restartTimer1.Tick += RestartTimer_Tick1;
            iWavePlayer2 = new WaveOut();
            audioFileReader2 = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\click.mp3");
            iWavePlayer2.Init(audioFileReader2);
            iWavePlayer3 = new WaveOut();
            audioFileReader3 = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\touch.mp3");
            iWavePlayer3.Init(audioFileReader3);
            iWavePlayer4 = new WaveOut();
            audioFileReader4 = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\clock.mp3");
            iWavePlayer4.Init(audioFileReader4);
            iWavePlayer5 = new WaveOut();
            audioFileReader5 = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\error.mp3");
            iWavePlayer5.Init(audioFileReader5);
            iWavePlayer6 = new WaveOut();
            audioFileReader6 = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\fail.mp3");
            iWavePlayer6.Init(audioFileReader6);
            iWavePlayer7 = new WaveOut();
            audioFileReader7 = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\winner.mp3");
            iWavePlayer7.Init(audioFileReader7);
            iWavePlayer8 = new WaveOut();
            audioFileReader8 = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\start.mp3");
            iWavePlayer8.Init(audioFileReader8);
            audioFileReader8.Position = 0;
            iWavePlayer8.Play();
            iWavePlayer8.PlaybackStopped += OnPlaybackStopped;
            restartTimer = new Timer();
            restartTimer.Interval = 3000;
            restartTimer.Tick += RestartTimer_Tick;
            iWavePlayer9 = new WaveOut();
            audioFileReader9 = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\pause.mp3");
            iWavePlayer9.Init(audioFileReader9);
            iWavePlayer10 = new WaveOut();
            audioFileReader10 = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\unpause.mp3");
            iWavePlayer10.Init(audioFileReader10);
            InitializeNumbers();
            UpdateTileSize();
            panel1.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(panel1, true, null);
            LoadTileImages();
            LoadBackgrounds();
            UpdateTileSizeAndPosition();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e) {
            if (!isGameOver && !isMuted) {
                restartTimer.Start();
            }
        }

        private void OnPlaybackStopped1(object sender, StoppedEventArgs e) {
            if (!isGameOver && !isMuted) {
                restartTimer1.Start();
            }
        }

        private void RestartTimer_Tick(object sender, EventArgs e) {
            restartTimer.Stop();
            audioFileReader8.Position = 0;
            iWavePlayer8.Play();
        }

        private void RestartTimer_Tick1(object sender, EventArgs e) {
            restartTimer1.Stop();
            audioFileReader1.Position = 0;
            iWavePlayer1.Play();
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            audioFileReader3.Position = 0;
            iWavePlayer3.Play();
            audioFileReader8.Position = 0;
            iWavePlayer8.Play();

            if (!isMuted) {
                iWavePlayer1.Pause();
            }

            if (!isPaused) {
                pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\stop.png");   
            } else {
                pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\continue.png");  
            }

            if (timeLeft < 60) {
                audioFileReader4.Position = 0;
                iWavePlayer4.Stop();
            }

            gameTimer.Stop();
            panel1.Visible = false;
            panel1.Enabled = true;
            label1.ForeColor = Color.FromArgb(0, 192, 0);
            label1.Text = label1.ForeColor.ToArgb().ToString();
            label1.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox4.Enabled = true;
            pictureBox3.Enabled = true;
            pictureBox5.Visible = false;
            pictureBox6.Visible = true;
            pictureBox7.Visible = false;
            button1.Visible = true;
            button2.Visible = false;
            isGameOver = false;
            isPaused = false;
            pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\stop.png");
        }

        private void pictureBox2_Click(object sender, EventArgs e) {
            if (!isMuted) {
                audioFileReader1.Position = 0;
                iWavePlayer1.Play();
            }
          
            audioFileReader3.Position = 0;
            iWavePlayer3.Play();
            
            if (!isPaused) {
                pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\stop.png");
            } else {
                pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\continue.png");
            }
            
            elapsedTimeInSeconds = 0;
            gameFinished = false;
            gameTimer.Start();
            label1.ForeColor = Color.FromArgb(0, 192, 0);
            label1.Text = label1.ForeColor.ToArgb().ToString();
            button2.Visible = false;
            pictureBox5.Visible = true;
            pictureBox4.Enabled = true;
            StartGame();
            InitializeNumbers();
            panel1.Enabled = true;
            panel1.Invalidate();
            isGameOver = false;
            audioFileReader4.Position = 0;
            iWavePlayer4.Stop();
            isPaused = false;
            pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\stop.png");
        }

        private void pictureBox3_Click(object sender, EventArgs e) {
            isFormVisible = !isFormVisible;
            audioFileReader3.Position = 0;
            iWavePlayer3.Play();

            if (isFormVisible) {
                iWavePlayer1.Pause();
                gameTimer.Stop();
                pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\continue.png");
                form4.ShowDialog();
            } 
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e) {
            if (!isPaused) {
                isPaused = false;
                gameTimer.Start();

                if (!isMuted) {
                    iWavePlayer1.Play();
                }
                pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\stop.png");
            }
            isFormVisible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e) {
            audioFileReader3.Position = 0;
            iWavePlayer3.Play();
            currentBackground = (currentBackground + 1) % backgrounds.Count;
            panel1.BackgroundImage = backgrounds[currentBackground];
        }

        public void pictureBox5_Click(object sender, EventArgs e) {
            if (!isPaused) {
                audioFileReader9.Position = 0;
                iWavePlayer9.Play();
            } else {
                audioFileReader10.Position = 0;
                iWavePlayer10.Play();
            }

            isPaused = !isPaused;

            if (isPaused) {
                iWavePlayer1.Pause();
                pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\continue.png");
                panel1.Enabled = false;
                gameTimer.Stop();
                iWavePlayer4.Stop();
            } else {
                if (!isMuted) {
                    iWavePlayer1.Play();            
                }
                
                pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\stop.png");
                panel1.Enabled = true;
                gameTimer.Start();

                if (timeLeft < 60) {
                    iWavePlayer4.Play();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            iWavePlayer8.Pause();
            audioFileReader8.Position = 0;

            if (!isMuted) {
                audioFileReader1.Position = 0;
                iWavePlayer1.Play();
            }

            audioFileReader2.Position = 0;
            iWavePlayer2.Play();
            panel1.Visible = true;
            label1.Visible = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox6.Visible = false;
            pictureBox7.Visible = true;
            button1.Visible = false;
            StartGame();
            InitializeNumbers();
            panel1.Invalidate();
        }

        private void pictureBox6_Click(object sender, EventArgs e) {
            audioFileReader3.Position = 0;
            iWavePlayer3.Play();
            form3.ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e) {
            isMuted = !isMuted;

            if (isMuted) {
                pictureBox7.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\mute.png");
                iWavePlayer1.Pause();
            } else {
                pictureBox7.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\unmute.png");
                
                if (!isPaused && !isGameOver) {
                    iWavePlayer1.Play();
                }
            }
        }

        private void LoadBackgrounds() {
            backgrounds = new List<Bitmap>();

            for (int i = 1; i <= 5; i++) {
                string imagesPath = $"C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\field{i}.jpg";
                Bitmap background = new Bitmap(imagesPath);
                backgrounds.Add(background);
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            UpdatePanelSizeAndPosition();
        }

        private void UpdatePanelSizeAndPosition() {
            UpdateTileSize();
            CenterPanel();
        }

        private void UpdateTileSize() {
            tileSize = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 5;
            UpdatePanelSize();
        }

        private void CenterPanel() {
            panel1.Location = new Point((this.ClientSize.Width - panel1.Width) / 2, (this.ClientSize.Height - panel1.Height) / 2);
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            Font font = new Font("Arial", 24);

            for (int i = 0; i < numbers.Count; i++) {
                int number = numbers[i];
                
                if (number != 0 && !(isDragging && i == draggingIndex)) {
                    DrawTile(g, font, i, number);
                }
            }

            if (isDragging) {
                int hoverIndex = GetTileIndexAtPoint(dragPoint);
                
                if (hoverIndex != -1) {
                    bool isHoverOverSelf = (hoverIndex == draggingIndex);
                    bool isHoverOverEmpty = (numbers[hoverIndex] == 0);
                    
                    if (isHoverOverSelf || isHoverOverEmpty) {
                        DrawHoverTile(g, hoverIndex, Color.BurlyWood, false);
                    } else {
                        DrawHoverTile(g, hoverIndex, Color.Red, true);
                    }
                }
                DrawTile(g, font, draggingIndex, numbers[draggingIndex], true);
            }
        }

        private void DrawHoverTile(Graphics g, int index, Color hoverColor, bool showCross) {
            float x = (index % 4) * tileSize;
            float y = (index / 4) * tileSize;
            float padding = tileSize * 0.11f;
            float circleSize = tileSize - (2 * padding);
            
            RectangleF tileCircle = new RectangleF(x + padding, y + padding, circleSize, circleSize);
            
            if (showCross) {
                using (Pen crossPen = new Pen(Color.Red, 20)) {
                    g.DrawLine(crossPen, x + padding, y + padding, x + circleSize + padding, y + circleSize + padding);
                    g.DrawLine(crossPen, x + circleSize + padding, y + padding, x + padding, y + circleSize + padding);
                }
            } else {
                using (Brush hoverBrush = new SolidBrush(hoverColor)) {
                    g.FillEllipse(hoverBrush, tileCircle);
                }
            }
        }

        private void DrawTile(Graphics g, Font font, int index, int number, bool isDragging = false) {
            float x = (index % 4) * tileSize;
            float y = (index / 4) * tileSize;
            
            if (isDragging) {
                x = dragPoint.X - (tileSize / 2);
                y = dragPoint.Y - (tileSize / 2);
            }
            
            RectangleF tileRect = new RectangleF(x, y, tileSize, tileSize);
            
            if (number > 0 && number <= tileImages.Count) {
                float padding = tileSize * 0.11f;
                
                RectangleF imageRect = new RectangleF(x + padding, y + padding, tileSize - 2 * padding, tileSize - 2 * padding);
                
                g.DrawImage(tileImages[number - 1], imageRect);
            }
        }

        private void panel1_Resize(object sender, EventArgs e) {
            UpdateTileSize();
            panel1.Invalidate();
        }

        private void UpdateTileSizeAndPosition() {
            tileSize = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 5;
            UpdatePanelSize();
            CenterPanel();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            int index = GetTileIndexAtPoint(e.Location);
            
            if (index != -1 && IsMovable(index)) {
                isDragging = true;
                draggingIndex = index;
                dragPoint = e.Location;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (isDragging) {
                dragPoint = new Point(Math.Max(0, Math.Min(e.Location.X, panel1.Width)), Math.Max(0, Math.Min(e.Location.Y, panel1.Height)));
                panel1.Invalidate();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            if (isDragging) {
                isDragging = false;

                if (!panel1.ClientRectangle.Contains(e.Location)) {
                    isFormVisible = !isFormVisible;

                    if (!isFormVisible) { 
                        audioFileReader5.Position = 0;
                        iWavePlayer5.Play();
                        gameTimer.Stop();
                        iWavePlayer1.Pause();
                        pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\continue.png");
                        form2.ShowDialog();
                    } 

                    if (!isMuted) {
                        iWavePlayer1.Play();
                    }
                    
                    pictureBox5.Image = Image.FromFile("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\stop.png");

                    if (!gameFinished) {
                        gameTimer.Start();
                    }
                    
                    draggingIndex = -1;
                    panel1.Invalidate();
                    return;
                }

                int targetIndex = GetTileIndexAtPoint(e.Location);
                
                if (CanMoveTo(draggingIndex, targetIndex)) {
                    SwapTiles(draggingIndex, targetIndex);
                    
                    if (CheckPuzzleSolved()) {
                        ShowResultMessage(true);
                    }
                }
                panel1.Invalidate();
            }
        }

        private bool CheckPuzzleSolved() {
            for (int i = 0; i < numbers.Count - 1; i++) {
                if (numbers[i] != i + 1) {
                    return false;
                }
            }
            return true;
        }

        private bool CanMoveTo(int fromIndex, int toIndex) {
            return numbers[toIndex] == 0;
        }

        private bool IsMovable(int index) {
            int emptyIndex = numbers.IndexOf(0);
            
            if ((index == emptyIndex - 1 && emptyIndex % 4 != 0) || (index == emptyIndex + 1 && index % 4 != 0)) {
                return true;
            }
            return index == emptyIndex - 4 || index == emptyIndex + 4;
        }

        private void SwapTiles(int index1, int index2) {
            if (index1 == -1 || index2 == -1 || index1 == index2) 
                return;
            
            if (numbers[index1] == 0 || numbers[index2] == 0) {
                int temp = numbers[index1];
                numbers[index1] = numbers[index2];
                numbers[index2] = temp;
            }
        }

        private int GetTileIndexAtPoint(Point point) {
            int x = point.X / tileSize;
            int y = point.Y / tileSize;
            int index = x + y * 4;
            return (index >= 0 && index < 16) ? index : -1;
        }

        private void InitializeNumbers() {
            numbers = Enumerable.Range(1, 15).OrderBy(x => Guid.NewGuid()).ToList();
            numbers.Add(0);
        }

        private void Form1_Resize(object sender, EventArgs e) {
            UpdatePanelSizeAndPosition();
            panel1.Invalidate();
        }

        private void StartGame() {
            timeLeft = 5 * 60;
            UpdateTimeLabel();
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e) {
            if (timeLeft > 0) {
                timeLeft--;
                UpdateTimeLabel();

                if (timeLeft < 60) {
                    label1.ForeColor = Color.Red;
                    iWavePlayer4.Play();
                }

                if (IsGameCompleted()) {
                    gameTimer.Stop(); 
                }
            } else {
                gameTimer.Stop();
                ShowResultMessage(false);
            }
        }

        private void UpdateTimeLabel() {
            label1.Text = $"{timeLeft / 60}:{timeLeft % 60:D2}";
        }

        private bool IsGameCompleted() {
            for (int i = 0; i < numbers.Count - 1; i++) {
                if (numbers[i] != i + 1) 
                    return false;
            }
            return true;
        }

        private void ShowResultMessage(bool isSuccess) {
            isGameOver = true;
           
            if (isSuccess) {
                iWavePlayer1.Stop();
                audioFileReader7.Position = 0;
                iWavePlayer7.Play();
                iWavePlayer4.Stop();
                button2.Text = "ВИ ВИГРАЛИ!";
                button2.ForeColor = Color.Green;
                button2.BackColor = Color.White;
                button2.FlatAppearance.BorderColor = Color.Green;
                button2.FlatAppearance.MouseDownBackColor = Color.White;
                button2.FlatAppearance.MouseOverBackColor = Color.White;
                button2.Visible = true;
                panel1.Enabled = false;
                pictureBox5.Visible = false;
                pictureBox4.Enabled = false;
                pictureBox3.Enabled = false;
            } else {
                iWavePlayer1.Stop();
                audioFileReader6.Position = 0;
                iWavePlayer6.Play();
                iWavePlayer4.Stop();
                button2.Text = "ВИ ПРОГРАЛИ!";
                button2.ForeColor = Color.Red;
                button2.BackColor = Color.Black;
                button2.FlatAppearance.BorderColor = Color.Red;
                button2.FlatAppearance.MouseDownBackColor = Color.Black;
                button2.FlatAppearance.MouseOverBackColor = Color.Black;
                button2.Visible = true;
                panel1.Enabled = false;
                pictureBox5.Visible = false;
                pictureBox4.Enabled = false;
                pictureBox3.Enabled = false;
            }
            restartTimer.Stop();
            restartTimer1.Stop();
        }

        private void UpdatePanelSize() {
            int panelDimension = tileSize * 4;
            panel1.Size = new Size(panelDimension, panelDimension);
        }

        private void LoadTileImages() {
            tileImages = new List<Bitmap>();
            
            for (int i = 1; i <= 15; i++) {
                string imagePath = $"C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\{i}.png";
                Bitmap image = new Bitmap(imagePath);
                tileImages.Add(image);
            }
        }
    }
}