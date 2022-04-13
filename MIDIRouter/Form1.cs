using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIDIRouter
{
    public partial class Form1 : Form
    {
        WinMM winMM;

        public Form1()
        {
            winMM = new WinMM(false, false);
            InitializeComponent();
            InitializeLists();
        }

        private void InitializeLists()
        {
            MIDIOutBox.DataSource = winMM.GetOutputList();
            MIDIOutBox.DisplayMember = "szPname";
            MIDIOutBox.ValueMember = "index";
            MIDIOutBox.SelectedItem = MIDIOutBox.Items.Count == 0 ? null : MIDIOutBox.Items[0];

            MIDIInBox.DataSource = winMM.GetInputList();
            MIDIInBox.DisplayMember = "szPname";
            MIDIInBox.ValueMember = "index";
            MIDIInBox.SelectedItem = MIDIInBox.Items.Count == 0 ? null : MIDIInBox.Items[0];
        }

        private void MIDIOutBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MIDIOutBox.SelectedItem == null) return;
            WinMM.output midiOutCaps = (WinMM.output)MIDIOutBox.SelectedItem;
            winMM.ChangeOutput(midiOutCaps.index);
        }

        private void MIDIInBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MIDIOutBox.SelectedItem == null) return;
            WinMM.input midiInCaps = (WinMM.input)MIDIInBox.SelectedItem;
            winMM.ChangeInput(midiInCaps.index);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            winMM = new WinMM(false, false);
            InitializeLists();
        }
    }
}
