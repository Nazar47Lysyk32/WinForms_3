using System;
using System.Windows.Forms;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

public partial class Form1 : Form
{
    private List<System.Threading.Timer> timers = new List<System.Threading.Timer>();

    public Form1()
    {
        InitializeComponent();
        btnAddTask.Click += new EventHandler(btnAddTask_Click);
        btnRemoveTask.Click += new EventHandler(btnRemoveTask_Click);
    }

    private void btnAddTask_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(textBox1.Text))
        {
            DateTime reminderTime = dateTimePicker1.Value;
            DateTime now = DateTime.Now;

            if (reminderTime <= now)
            {
                MessageBox.Show("Please set the reminder time in the future.");
                return;
            }

            string taskDescription = textBox1.Text;
            listBox1.Items.Add($"{taskDescription} (Reminder set for {reminderTime})");
            textBox1.Clear();

            TimeSpan delay = reminderTime - now;
            System.Threading.Timer timer = new System.Threading.Timer(ShowReminder, taskDescription, delay, TimeSpan.FromMilliseconds(-1));
            timers.Add(timer);
        }
        else
        {
            MessageBox.Show("Please enter some text for the task.");
        }
    }

    private void ShowReminder(object state)
    {
        string taskDescription = (string)state;
        MessageBox.Show("Reminder: " + taskDescription, "Task Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnRemoveTask_Click(object sender, EventArgs e)
    {
        if (listBox1.SelectedIndex != -1)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }
        else
        {
            MessageBox.Show("Please select a task to remove.");
        }
    }
}

