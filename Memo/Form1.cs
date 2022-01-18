using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memo
{
    public partial class Form1 : Form
    {
        Label firstClicked = null; //Indica la prima label cliccata
        Label secondClicked = null; //Indica la seconda label cliccata

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        //Sceglie un icona random per i riquadri
        Random random = new Random();

        //Lista delle icone
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        //Assegna le icone dalla lista a un riquadro random
        private void AssignIconsToSquares()
        {
            //Per ogni label
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label; //Converte la variabile ocntrol in un etichetta iconLabel
                //Se la conversione funziona
                if(iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count); //Genera numero random
                    iconLabel.Text = icons[randomNumber]; //Assegna una delle icone alla label
                    iconLabel.ForeColor = iconLabel.BackColor; //Nasconde le icone
                    icons.RemoveAt(randomNumber); //Rimuove l'icona pescata dalla lista
                }
            }
        }

        //Quando il giocatore clicca le icone
        private void label_Click(object sender, EventArgs e)
        {
            //Se il timer sta andando ignora i clic
            if(timer1.Enabled == true)
            {
                return;
            }

            Label clickedLabel = sender as Label;

            if(clickedLabel != null)
            {
                //Se il colore e' nero la label e' gia' stata cliccata, ignora il clic
                if (clickedLabel.ForeColor == Color.Black)
                    return;
                //Se firstClicked e' null firstClicked e' uguale alla
                //label cliccata e cambia il colore a nero
                if(firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                //Se il player arriva qui ha gia' cliccato il primo
                //e non e' null, quindi deve essere il secondo clic
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                //Controlla se il player ha vinto
                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;

                    return;
                }

                //Se il player arriva qui ha cliccato due icone diverse,
                //fa partire il timer che le nasconde
                timer1.Start();
            }
        }

        /* Il timer parte quando il giocatore clicca due icone
        diverse, conta 750 millisecondi e nasconde le icone */
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Ferma il timer
            timer1.Stop();

            //Nasconde le icone
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            //Resetta fistClicked e secondClicked
            firstClicked = null;
            secondClicked = null;
        }

        //Controlla se tutte le icone sono state accoppiate, se si' il giocatore vince
        private void CheckForWinner()
        {
            //Controlla tutte le label
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if(iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                    {
                        return;
                    }
                }
            }

            //Se il loop non ha trovato icone con colore testo
            //uguale al colore dello sfondo il giocatore vince
            MessageBox.Show("Hai vinto!!");
            Close();
        }
    }
}
