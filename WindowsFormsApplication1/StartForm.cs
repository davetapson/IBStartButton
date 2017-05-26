using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();

            //#/bin/sh
            //# Run the IB Gateway by passing along all of the necessary parameters  
            //# Syntax is:
            //# java -cp [java options] [IBJts directory] [username=XXX] [password=ZZZ] 
            //# Get the username from command-line parameter 1 (or hard code this USER=xxxxx)
            //            USER =$1
            //# Get password from command-line parameter 2 (or hard code this PASS=xxxxx)
            //PASS =$2
            //# Get the current date and time (to the minute)
            //DATE =`date +% Y % m % d % H % M`
            //# Java Executable - be specific about this so you know which JRE you are using
            //JAVAEXE =/ usr / bin / java
            //# Java Runtime Options - these specify which jts.jar files to work with and how to
            //# configure Java's memory model
            //JAVAOPTIONS = "-cp jts.jar:total.2013.jar -Dsun.java2d.noddraw=true -Xmx512M ibgateway.GWClient"
            //# Log file - make sure whatever you name this file has enough space in the folder
            //LOGFILE =/ tmp / ibg_running_${ DATE}.log
            //# IBJts working directory - change this to where you have the IB Gateway installed
            //            IBJTSDIR =/ home / rholowczak / ib / IBJts
            //# Assemble the command line and run it! 
            //${ JAVAEXE} ${ JAVAOPTIONS} ${ IBJTSDIR}
            //            username =${ USER}
            //            password =${ PASS} > ${ LOGFILE}
            //            2 > &1
            //# Note the  2>&1  at the end of the command line re-directs standard error into the same 
            //# log file as standard outpu
        }

        private static void Start()
        {
            string userName = "d4v3t4ps1";
            string password = "Makatini1";
            DateTime date = DateTime.Now;
            string javaPath = @"C:\Program Files (x86)\Java\jdk1.8.0_92\jre\bin\java";
            string javaOptions = @" -cp jts.jar:total.2013.jar - Dsun.java2d.noddraw = true - Xmx512M ibgateway.GWClient ";
            string logFile = @" C:\temp\ib_gatewayibg_running_" + date + ".log";
            string IBJTSDIR = @" C:\Jts\ibgateway\952\jars";

            string script = javaPath + javaOptions + IBJTSDIR + " username = " + userName + " password = " + password + " > " + logFile + " 2>&1";

            try
            {
                var proc6 = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"C:\Jts\ibgateway\952\ibgateway.exe",
                        Arguments = " username=" + userName + " password=" + password,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = false,
                        WorkingDirectory = @"C:\Jts\ibgateway\952\"
                    }
                };

                proc6.Start();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DoTick();
        }

        private void DoTick()
        {
            Process[] pname = Process.GetProcessesByName("ibgateway");
            if (pname.Length == 0)
            {
                txtStatus.Text = "Not running :(";
                txtStatus.BackColor = Color.Gray;
                btnStart.Enabled = true;
                btnStart.BackColor = Color.Green;

                if (chkKeepRunning.Checked) Start();
            }
            else
            {
                txtStatus.Text = "Running :)";
                txtStatus.BackColor = Color.Green;
                btnStart.BackColor = Color.Gray;
                btnStart.Enabled = false;
            }
        }
    }
}
