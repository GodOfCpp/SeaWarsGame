using System.Text.RegularExpressions;

namespace SeaWarsGame
{
    public partial class MenuGame : Form
    {
        private const int mapSize = 10;
        private const int cellSize = 30;
        private const string alphabet = "¿¡¬√ƒ≈∆«» ";

        private int[,] myMap = new int[mapSize, mapSize];
        private int[,] enemyMap = new int[mapSize, mapSize];

        private bool isPlaying;

        private Chat chat;


        public void CreateMaps()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = 0;
                    enemyMap[i, j] = 0;

                    Button myButton = new Button();
                    myButton.Click += ConfigureShips;
                    myButton.BackColor = Color.White;
                    Button enemyButton = new Button();

                    if (j == 0 || i == 0)
                    {
                        myButton.BackColor = Color.Gray;
                        enemyButton.BackColor = Color.Gray;

                        if (i == 0 && j > 0)
                        {
                            myButton.Text = alphabet[j-1].ToString();
                            enemyButton.Text = alphabet[j-1].ToString();
                        }

                        if (j == 0 && i > 0)
                        {
                            myButton.Text = i.ToString();
                            enemyButton.Text = i.ToString();
                        }
                    }
                    myButton.Location = new Point(j * cellSize, i * cellSize);
                    myButton.Size = new Size(cellSize, cellSize);
                    Invoke(() => this.Controls.Add(myButton));

                    enemyButton.Location = new Point(320 + j * cellSize, i * cellSize);
                    enemyButton.Size = new Size(cellSize, cellSize);
                    Invoke(() => this.Controls.Add(enemyButton));
                }
            }

            Button startButton = new Button();
            startButton.Text = "Õ‡˜‡Ú¸";
            startButton.Location = new Point(ClientSize.Width/2 - startButton.Width/2, mapSize * cellSize + 20);
            startButton.Click += new EventHandler(Start);
            Invoke(() => this.Controls.Add(startButton));
        }

        private bool checkShips()
        {
            var isBattleShip = false;
            var isCruiser = false;
            var isDestroyer = false;
            var isBoat = false;

            return true;

        }

        public void Start(object sender, EventArgs e)
        {
            if (checkShips())
            {
                isPlaying = true;
                chat.SendMsg("ships ready");
            }
            else
            {
                MessageBox.Show("Wrong ships location");
            }
        }

        public void ConfigureShips(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

            if (!isPlaying)
            {
                if (myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == 0)
                {
                    pressedButton.BackColor = Color.Red;
                    myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 1;
                }
                else
                {
                    pressedButton.BackColor = Color.White;
                    myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 0;
                }
            }
        }

        private void startGame_Click(object? sender, System.EventArgs? e)
        {
            this.Controls.Clear();
            Label requestIp = new Label();
            requestIp.Text = "Enter the IP of the person you want to play with";
            requestIp.AutoSize = true;
            requestIp.Location = new Point(180, 0);
            this.Controls.Add(requestIp);

            TextBox IpAddressBox = new TextBox();
            IpAddressBox.Location = new Point(this.ClientSize.Width / 2 - IpAddressBox.Width / 2, this.ClientSize.Height / 2 - IpAddressBox.Height / 2 - 50);
            IpAddressBox.AcceptsReturn = true;
            IpAddressBox.PlaceholderText = "Enter IP here";
            IpAddressBox.KeyPress += IpAddressBox_KeyPress;
            this.Controls.Add(IpAddressBox);
        }


        private async void StartChatting(string usrname, string ip, string localPort, string remotePort)
        {
            Label connectionWaiting = new Label();
            connectionWaiting.Text = "Waiting for connection...";
            connectionWaiting.Size = new Size(200, 100);
            connectionWaiting.Location = new Point(this.ClientSize.Width / 2 - connectionWaiting.Width / 2 + 40, this.ClientSize.Height / 2 - connectionWaiting.Height / 2);
            if (InvokeRequired) Invoke(() => this.Controls.Add(connectionWaiting));

            chat = new Chat(usrname, ip, localPort, remotePort);
            Task.Run(chat.ReceiveMsgAsync);
            chat.SendMsgAsync();
            if (chat.username == "user") chat.SendMsg("ready");
            while (chat.lastMsg != "ready") { }
            if (chat.username == "host") chat.SendMsg("ready");
            Invoke(() => this.Controls.Clear());
            await Task.Run(() => CreateMaps());
            while (chat.lastMsg != "ships ready") { }
        }

        private async void StartConnection(String ip)
        {
            await Task.Run(() => StartChatting("host", "127.0.0.1", "8000", "8001"));
        }

        private bool checkIp(String ip)
        {
            Regex r = new Regex(@"((1\d\d|2([0-4]\d|5[0-5])|\D\d\d?)\.?){4}$");
            return r.IsMatch(ip);
        }
        private async void IpAddressBox_KeyPress(object? sender, KeyPressEventArgs? e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                var ip = tb.Text.ToString();
                if (!checkIp(ip))
                    MessageBox.Show("Wront IP!");
                else 
                {
                    this.Controls.Clear();

                    await Task.Run(() => StartConnection(ip));
                }
            }
        }
        private async void connectGame_Click(object? sender, System.EventArgs? e)
        {
           // await Task.Run(() => ConnectionWait());
            this.Controls.Clear();
            await Task.Run(() => StartChatting("user", "127.0.0.1", "8001", "8000"));
        }



        private void StartButton()
        {
            Button startGame = new Button();
            startGame.BackColor = Color.Gray;
            startGame.Text = "Start";
            startGame.Location = new Point(this.ClientSize.Width/2 - startGame.Width/2, this.ClientSize.Height/2 - startGame.Height/2 - 50);
            startGame.Click += startGame_Click;

            this.Controls.Add(startGame);
        }


        private void ConnectButton()
        {
            Button connectButton = new Button();
            connectButton.BackColor = Color.Gray;
            connectButton.Text = "Connect";
            connectButton.Location = new Point(this.ClientSize.Width / 2 - connectButton.Width / 2, this.ClientSize.Height / 2 - connectButton.Height / 2 + 50);
            connectButton.Click += connectGame_Click;
            this.Controls.Add(connectButton);


        }
        public void Menu()
        {
            StartButton();
            ConnectButton();
        }

        public void InitGame()
        {
            CreateMaps();
        }

        public void InitMenu()
        {
            isPlaying = false;
            Menu();
        }


        public MenuGame()
        {
            InitializeComponent();
            this.Size = new Size(640, 480);
            this.Text = "Sea Wars by thienlao";
            InitMenu();
    
        }
    }
}