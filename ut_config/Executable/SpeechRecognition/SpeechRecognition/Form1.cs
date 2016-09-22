using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Diagnostics;

namespace SpeechRecognition
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices cList = new Choices();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            cList.Add(new string[] { "hello", "how are you", "what is the current time", "thank you", "open firefox", "close" });
            Grammar gr = new Grammar(new GrammarBuilder(cList));

            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "hello":
                    ss.SpeakAsync("hello akshay");
                    break;
                case "how are you":
                    ss.SpeakAsync("I am good. How are you");
                    break;
                case "what is the current time":
                    ss.SpeakAsync("current time is " + DateTime.Now.ToString());
                    break;
                case "open firefox":
                    Process.Start("firefox", "http://www.google.com");
                        break;
                case "close":
                    Application.Exit();
                    break;
            }
            richTextBox1.Text += e.Result.Text.ToString() + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            button1.Enabled = true;
            button2.Enabled = false; 
        }
    }
}
