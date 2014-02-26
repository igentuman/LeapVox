// SineWaveOscillator.cs by Charles Petzold, November 2009
using System;
using NAudio.Wave;

namespace LeapVox.Audio
{
    class SineWaveOscillator : WaveProvider16
    {
        double phaseAngle;

        public SineWaveOscillator(int sampleRate): 
            base(sampleRate, 1)
        {
        }

        public double Frequency { set; get; }
        public short Amplitude { set; get; }

        public override int Read(short[] buffer, int offset, int sampleCount)
        {
            for (int index = 0; index < sampleCount; index++)
            {
                buffer[offset + index] = (short)(Amplitude * Math.Sin(phaseAngle));
                phaseAngle += 2 * Math.PI * Frequency / WaveFormat.SampleRate;

                if (phaseAngle > 2 * Math.PI)
                    phaseAngle -= 2 * Math.PI;
            }
            return sampleCount;
        }
    }
}
