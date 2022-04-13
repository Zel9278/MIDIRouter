namespace MIDIRouter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MIDIIn = new System.Windows.Forms.Label();
            this.MIDIInBox = new System.Windows.Forms.ComboBox();
            this.MIDIOut = new System.Windows.Forms.Label();
            this.MIDIOutBox = new System.Windows.Forms.ComboBox();
            this.Refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MIDIIn
            // 
            this.MIDIIn.AutoSize = true;
            this.MIDIIn.Location = new System.Drawing.Point(12, 42);
            this.MIDIIn.Name = "MIDIIn";
            this.MIDIIn.Size = new System.Drawing.Size(54, 13);
            this.MIDIIn.TabIndex = 0;
            this.MIDIIn.Text = "MIDIInput";
            // 
            // MIDIInBox
            // 
            this.MIDIInBox.FormattingEnabled = true;
            this.MIDIInBox.Location = new System.Drawing.Point(80, 39);
            this.MIDIInBox.Name = "MIDIInBox";
            this.MIDIInBox.Size = new System.Drawing.Size(121, 21);
            this.MIDIInBox.TabIndex = 1;
            this.MIDIInBox.SelectedIndexChanged += new System.EventHandler(this.MIDIInBox_SelectedIndexChanged);
            // 
            // MIDIOut
            // 
            this.MIDIOut.AutoSize = true;
            this.MIDIOut.Location = new System.Drawing.Point(12, 15);
            this.MIDIOut.Name = "MIDIOut";
            this.MIDIOut.Size = new System.Drawing.Size(62, 13);
            this.MIDIOut.TabIndex = 0;
            this.MIDIOut.Text = "MIDIOutput";
            this.MIDIOut.UseMnemonic = false;
            // 
            // MIDIOutBox
            // 
            this.MIDIOutBox.FormattingEnabled = true;
            this.MIDIOutBox.Location = new System.Drawing.Point(80, 12);
            this.MIDIOutBox.Name = "MIDIOutBox";
            this.MIDIOutBox.Size = new System.Drawing.Size(121, 21);
            this.MIDIOutBox.TabIndex = 1;
            this.MIDIOutBox.SelectedIndexChanged += new System.EventHandler(this.MIDIOutBox_SelectedIndexChanged);
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(12, 126);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(189, 23);
            this.Refresh.TabIndex = 2;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.MIDIOutBox);
            this.Controls.Add(this.MIDIInBox);
            this.Controls.Add(this.MIDIOut);
            this.Controls.Add(this.MIDIIn);
            this.MaximumSize = new System.Drawing.Size(400, 200);
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MIDIIn;
        private System.Windows.Forms.ComboBox MIDIInBox;
        private System.Windows.Forms.Label MIDIOut;
        private System.Windows.Forms.ComboBox MIDIOutBox;
        private System.Windows.Forms.Button Refresh;
    }
}

