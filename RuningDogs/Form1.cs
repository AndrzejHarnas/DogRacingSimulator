using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuningDogs
{
    public partial class Main : Form
    {


        public Guy [] faceci = new Guy[3];
        public Greyhound[] grayhounds = new Greyhound[4];


        public Main()
        {
            InitializeComponent();

            minimumBetLabel.Text = "Minimalny zakład wynosi: "+cashBet.Minimum.ToString()+" zł";

            faceci[0] = new Guy();
            faceci[1] = new Guy();
            faceci[2] = new Guy();

            faceci[0].Name = "Janek";
            faceci[0].Cash = 50;
            faceci[0].MyRadioButton = joeRadioButton;
            faceci[0].MyLabel = joeBetLabel;

            faceci[0].clearBet(); 
            faceci[0].updateLabels();
         
            faceci[1].Name = "Bartek";
            faceci[1].Cash = 75;
            faceci[1].MyRadioButton = bobRadioButton;
            faceci[1].MyLabel = bobBetLabel;

            faceci[1].clearBet();
            faceci[1].updateLabels();
         
            faceci[2].Name = "Arek";
            faceci[2].Cash = 45;
            faceci[2].MyRadioButton = alRadioButton;
            faceci[2].MyLabel = alBetLabel;
            faceci[2].clearBet();
            faceci[2].updateLabels();
    
            grayhounds[0] = new Greyhound() {
                StartingPosition = pictureBox1.Left,
                RaceTrackLength = raceTrack.Width,
                MyPictureBox = pictureBox1
            };

            grayhounds[1] = new Greyhound()
            {
                StartingPosition = pictureBox2.Left,
                RaceTrackLength = raceTrack.Width,
                MyPictureBox = pictureBox2
            };

            grayhounds[2] = new Greyhound()
            {
                StartingPosition = pictureBox3.Left,
                RaceTrackLength = raceTrack.Width,
                MyPictureBox = pictureBox3
            };

            grayhounds[3] = new Greyhound()
            {
                StartingPosition = pictureBox4.Left,
                RaceTrackLength = raceTrack.Width,
                MyPictureBox = pictureBox4
            };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (joeRadioButton.Checked)
            {
                if (faceci[0].placeBet((int)cashBet.Value, (int)DogToWinN.Value))
                {
                    
                    faceci[0].updateLabels();
                } else
                {
                    MessageBox.Show(faceci[0].Name +" ma zbyt mało pieniędzy żeby zawrzeć zakład");
                    faceci[0].Mybet.Amount = 0;
                    faceci[0].updateLabels();
                } 

            }

            if (bobRadioButton.Checked)
            {
                if (faceci[1].placeBet((int)cashBet.Value, (int)DogToWinN.Value))
                {

                    faceci[1].updateLabels();
                }
                else
                {
                    MessageBox.Show(faceci[1].Name + " ma zbyt mało pieniędzy żeby zawrzeć zakład");
                    faceci[1].Mybet.Amount = 0;
                    faceci[1].updateLabels();
                }
            }

            if (alRadioButton.Checked)
            {
                if (faceci[2].placeBet((int)cashBet.Value, (int)DogToWinN.Value))
                {

                    faceci[2].updateLabels();
                }
                else
                {
                    MessageBox.Show(faceci[2].Name + " ma zbyt mało pieniędzy żeby zawrzeć zakład");
                    faceci[2].Mybet.Amount = 0;
                    faceci[2].updateLabels();
                }
            }
        }

        private void joeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            faceci[0].updateLabels();
            Name.Text = faceci[0].Name;
        }

        private void bobRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            faceci[1].updateLabels();
            Name.Text = faceci[1].Name;
        }

        private void alRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            faceci[2].updateLabels();
            Name.Text = faceci[2].Name;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            grayhounds[0].MyRandom = new Random();
            Thread.Sleep(20);
            grayhounds[1].MyRandom = new Random();
            Thread.Sleep(20);
            grayhounds[2].MyRandom = new Random();
            Thread.Sleep(20);
            grayhounds[3].MyRandom = new Random();

            button1.Enabled = false;
            button2.Enabled = false;
            joeBetLabel.Enabled = false;
            bobBetLabel.Enabled = false;
            alBetLabel.Enabled = false;
            joeRadioButton.Enabled = false;
            bobRadioButton.Enabled = false;
            alRadioButton.Enabled = false;
            cashBet.Enabled = false;
            DogToWinN.Enabled = false;


            for (int i = 0; i <= 3; i++)
            {
                if (grayhounds[i].run() == true)
                {

                    timer1.Stop();
                    int Winner = i + 1;
                    MessageBox.Show("Pies nr: "+ Winner + " wygrał wyścig");
                
                    for(int k=0; k <= 2; k++) {

                        
                        faceci[k].collect(Winner - 1);
                        faceci[k].clearBet();
                        faceci[k].updateLabels();
                    }

                    button1.Enabled = true;
                    button2.Enabled = true;
                    joeBetLabel.Enabled = true;
                    bobBetLabel.Enabled = true;
                    alBetLabel.Enabled = true;
                    joeRadioButton.Enabled = true;
                    bobRadioButton.Enabled = true;
                    alRadioButton.Enabled = true;
                    cashBet.Enabled = true;
                    DogToWinN.Enabled = true;

                    for (int m = 0; m <= 3; m++)
                    {
                        grayhounds[m].TakeStartingPosition();
                    }

                };

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 3; i++)
            {

                grayhounds[i].TakeStartingPosition();


            }

                       timer1.Start();


        }
    }


    public class Guy
    {
        public String Name;
        public int Cash;
        public RadioButton MyRadioButton;
        public TextBox MyLabel;
        public Bet Mybet = new Bet();



        public void updateLabels()
        {
            MyRadioButton.Text = Name+" ma "+ Cash +" zł";
            MyLabel.Text = Mybet.getDescription();
      
        }

        public void clearBet()
        {
            Mybet.Amount = 0;

        }

        public bool placeBet(int Amount,int DogToWin)
        {
            Mybet.Amount = Amount;
            Mybet.Dog = DogToWin;

            if (Amount <= Cash)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public void collect(int Winner)
        {
           
           
            Cash=Cash + Mybet.PayOut(Winner);

        }

    }


    public class Bet
    {

        public int Amount;
        public int Dog;
        public Guy Bettor;

        public String getDescription()
        {

            if (Amount > 0)
            {
                return Bettor + " postawił " + Amount + " zł na psa numer: " + Dog;
            } else
            {
                return Bettor+ " nie zawarł zakładu";
            }

        }


        public int PayOut(int Winner)
        {
            if(Dog == Winner + 1)
            {
               return Amount;
            } else
            {
                return -Amount;
            }

        }



    }

    public class Greyhound {
        public int StartingPosition;
        public int RaceTrackLength;
        public PictureBox MyPictureBox;
        public int Location =0;
        public Random MyRandom;
   



        public bool run()
        {
            Location = Location + MyRandom.Next(1,10);
            MyPictureBox.Left = Location;

            if (Location >= RaceTrackLength-MyPictureBox.Width-20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TakeStartingPosition()
        {
            Location = StartingPosition;
            MyPictureBox.Left = Location;
        }


    }
}
