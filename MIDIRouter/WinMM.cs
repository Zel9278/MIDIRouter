using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MIDIRouter
{
    internal class WinMM
    {
        public struct MidiOutCaps
        {
            public ushort wMid;
            public ushort wPid;
            public uint vDriverVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public uint dwSupport;
        }
        [DllImport("winmm.dll")]
        public static extern uint midiOutGetNumDevs();
        [DllImport("winmm.dll")]
        public static extern uint midiOutGetDevCaps(uint uDevID, out MidiOutCaps pmic, int cbmic);
        [DllImport("winmm.dll")]
        public static extern uint midiOutOpen(out IntPtr lphMidiOut, int uDeviceID, IntPtr dwCallback, IntPtr dwInstance, uint dwFlags);
        [DllImport("winmm.dll")]
        public static extern uint midiOutShortMsg(IntPtr hMidiOut, uint dwMsg);
        [DllImport("winmm.dll")]
        public static extern uint midiOutClose(IntPtr hMidiOut);

        public struct MidiInCaps
        {
            public ushort wMid;
            public ushort wPid;
            public uint vDriverVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public uint dwSupport;
        }

        [DllImport("winmm.dll")]
        public static extern uint midiInGetNumDevs();
        [DllImport("winmm.dll")]
        public static extern uint midiInGetDevCaps(uint uDevID, out MidiInCaps pmic, int cbmic);
        [DllImport("winmm.dll")]
        public static extern MMRESULT midiInOpen(out IntPtr lphMidiIn, int uDeviceId, MidiInProcDelegate dwCallback, IntPtr dwInstance, MidiOpenFlags dwFlags);
        public delegate void MidiInProcDelegate(IntPtr hMidiIn, MidiInMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2);
        [DllImport("winmm.dll")]
        public static extern uint midiInStart(IntPtr hMidiIn);
        [DllImport("winmm.dll")]
        public static extern MMRESULT midiInClose(IntPtr hMidiIn);
        [DllImport("winmm.dll")]
        public static extern uint midiInStop(IntPtr hMidiIn);

        public enum MMRESULT : uint
        {
            MMSYSERR_BASE = 0,
            MMSYSERR_NOERROR = MMSYSERR_BASE + 0
        }

        public enum MidiOpenFlags : uint
        {
            CALLBACK_FUNCTION = 0x30000,
            CALLBACK_TYPEMASK = 0x70000,
            CALLBACK_NULL = 0x00000
        }

        public enum MidiInMessage : uint
        {
            MIM_OPEN = 0x3C1,
            MIM_CLOSE = 0x3C2,
            MIM_NOTE = 0x3C3
        }

        public enum MidiInNoteEvent : int
        {
            MINE_ON = 0x9,
            MINE_OFF = 0x8,
            MINE_MODE = 0xB
        }

        public struct output
        {
            public int index { get; set; }
            public string szPname { get; set; }
        }

        public struct input
        {
            public int index { get; set; }
            public string szPname { get; set; }
        }

        public IntPtr hMidiOut;
        public IntPtr hMidiIn;

        private List<output> midiOutputList = new List<output>();
        private int defaultOutputIndex = -1;
        private List<input> midiInputList = new List<input>();
        private int defaultInputIndex = -1;

        public bool isOutputActive = false;
        public bool isInputActive = false;

        private MidiInProcDelegate midiInProc;

        public WinMM(bool useDefaultOut = true, bool useDefaultIn = true)
        {
            midiInProc = new MidiInProcDelegate(MIDIInEvent);

            uint midiOutNumDevs = midiOutGetNumDevs();
            for (uint i = 0; i < midiOutNumDevs; i++)
            {
                MidiOutCaps midiOutCaps = new MidiOutCaps();
                midiOutGetDevCaps(i, out midiOutCaps, Marshal.SizeOf(typeof(MidiOutCaps)));

                midiOutputList.Add(new output { index = Convert.ToInt32(i), szPname = midiOutCaps.szPname });
            }

            uint midiInNumDevs = midiInGetNumDevs();
            for (uint i = 0; i < midiInNumDevs; i++)
            {
                MidiInCaps midiInCaps = new MidiInCaps();
                midiInGetDevCaps(i, out midiInCaps, Marshal.SizeOf(typeof(MidiInCaps)));

                midiInputList.Add(new input { index = Convert.ToInt32(i), szPname = midiInCaps.szPname });
            }

            if (useDefaultOut)
            {
                midiOutOpen(out hMidiOut, defaultOutputIndex, IntPtr.Zero, IntPtr.Zero, uint.MinValue);
                midiOutShortMsg(hMidiOut, 0x0000C0);
                isOutputActive = true;
            }

            if (useDefaultIn)
            {
                midiInOpen(out hMidiIn, defaultInputIndex, midiInProc, IntPtr.Zero, MidiOpenFlags.CALLBACK_FUNCTION);
                midiInStart(hMidiIn);
                isInputActive = true;
            }
        }

        public void ChangeOutput(int outputIndex)
        {
            isOutputActive = false;
            if (!Equals(hMidiOut, IntPtr.Zero))
            {
                midiOutClose(hMidiOut);
            }

            midiOutOpen(out hMidiOut, outputIndex, IntPtr.Zero, IntPtr.Zero, uint.MinValue);
            midiOutShortMsg(hMidiOut, 0x0000C0);
            isOutputActive = true;
        }

        public void ChangeInput(int inputIndex)
        {
            isInputActive = false;
            if (!Equals(hMidiIn, IntPtr.Zero))
            {
                midiInStop(hMidiIn);
                midiInClose(hMidiIn);
            }

            midiInOpen(out hMidiIn, inputIndex, midiInProc, IntPtr.Zero, MidiOpenFlags.CALLBACK_FUNCTION);
            midiInStart(hMidiIn);
            isInputActive = true;
        }

        public List<output> GetOutputList()
        {
            return midiOutputList;
        }

        public List<input> GetInputList()
        {
            return midiInputList;
        }

        public void MIDIInEvent(IntPtr lphMidiIn, MidiInMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2)
        {
            if (!isOutputActive || !isOutputActive) return;
            if (hMidiOut == null) return;
            if (wMsg == MidiInMessage.MIM_NOTE)
            {
                byte[] p1 = BitConverter.GetBytes(dwParam1.ToInt32());
                byte cmd = Convert.ToByte(p1[0] >> 4);
                byte note = p1[1];
                byte vel = p1[2];

                byte[] vals = new byte[4] { p1[0], note, vel, 0x00 };

                midiOutShortMsg(hMidiOut, BitConverter.ToUInt32(vals, 0));
            }
        }
    }
}
